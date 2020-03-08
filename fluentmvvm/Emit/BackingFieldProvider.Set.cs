using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using FluentMvvm.Fluent;

namespace FluentMvvm.Emit
{
    internal static partial class BackingFieldProvider
    {
        /// <summary>
        ///     Builds the <see cref="IPropertySetExpression.Set{T}" /> method and its overloads.
        /// </summary>
        /// <param name="targetType">The type of the view model to generate the backing fields for.</param>
        /// <param name="typeBuilder">The <see cref="TypeBuilder" />.</param>
        /// <param name="fields">The generated backing fields.</param>
        private static void BuildSetMethods(Type targetType, TypeBuilder typeBuilder, IReadOnlyList<FieldInfo> fields)
        {
            BackingFieldProvider.BuildSetOverload(typeBuilder, MethodInfoCache.SetBool, targetType, fields);
            BackingFieldProvider.BuildSetOverload(typeBuilder, MethodInfoCache.SetByte, targetType, fields);
            BackingFieldProvider.BuildSetOverload(typeBuilder, MethodInfoCache.SetSByte, targetType, fields);
            BackingFieldProvider.BuildSetOverload(typeBuilder, MethodInfoCache.SetChar, targetType, fields);
            BackingFieldProvider.BuildSetOverload(typeBuilder, MethodInfoCache.SetDouble, targetType, fields);
            BackingFieldProvider.BuildSetOverload(typeBuilder, MethodInfoCache.SetFloat, targetType, fields);
            BackingFieldProvider.BuildSetOverload(typeBuilder, MethodInfoCache.SetInt16, targetType, fields);
            BackingFieldProvider.BuildSetOverload(typeBuilder, MethodInfoCache.SetUInt16, targetType, fields);
            BackingFieldProvider.BuildSetOverload(typeBuilder, MethodInfoCache.SetInt32, targetType, fields);
            BackingFieldProvider.BuildSetOverload(typeBuilder, MethodInfoCache.SetUInt32, targetType, fields);
            BackingFieldProvider.BuildSetOverload(typeBuilder, MethodInfoCache.SetInt64, targetType, fields);
            BackingFieldProvider.BuildSetOverload(typeBuilder, MethodInfoCache.SetUInt64, targetType, fields);

            BackingFieldProvider.BuildSetOverload(typeBuilder, MethodInfoCache.SetString, targetType, fields);
            BackingFieldProvider.BuildSetOverload(typeBuilder, MethodInfoCache.SetDecimal, targetType, fields);
            BackingFieldProvider.BuildSetOverload(typeBuilder, MethodInfoCache.SetDateTime, targetType, fields);

            BackingFieldProvider.BuildGenericSet(typeBuilder, MethodInfoCache.SetGeneric, targetType, fields);
        }

        /// <summary>
        ///     Builds a non-generic <c>Set</c> method that only takes those fields from <paramref name="allFields" /> into account
        ///     that are of type <paramref name="targetType" />.
        /// </summary>
        /// <param name="typeBuilder">The <see cref="TypeBuilder" />.</param>
        /// <param name="methodInfo">The <see cref="MethodInfo" /> specifying which overload to build.</param>
        /// <param name="targetType">The type of the view model to generate the backing fields for.</param>
        /// <param name="allFields">All generated backing fields.</param>
        private static void BuildSetOverload(TypeBuilder typeBuilder, MethodInfo methodInfo, Type targetType, IEnumerable<FieldInfo> allFields)
        {
            Type parameterType = methodInfo.GetParameters()[0].ParameterType;

            MethodBuilder methodBuilder = typeBuilder.DefineMethod(methodInfo.Name, BackingFieldProvider.GeneratedMethodAttributes, methodInfo.ReturnType, new[] { parameterType, typeof(string) });

            IReadOnlyList<FieldInfo> relevantFields = allFields.Where(x => x.FieldType == parameterType).ToArray();

            BackingFieldProvider.EmitSetMethodBody(methodInfo, targetType, methodBuilder, relevantFields);

            typeBuilder.DefineMethodOverride(methodBuilder, methodInfo);
        }

        /// <summary>
        ///     Builds the generic <see cref="IPropertySetExpression.Set{T}" /> method that takes all backing fields into account.
        /// </summary>
        /// <param name="typeBuilder">The <see cref="TypeBuilder" />.</param>
        /// <param name="methodInfo">The <see cref="MethodInfo" /> specifying which overload to build.</param>
        /// <param name="targetType">The type of the view model to generate the backing fields for.</param>
        /// <param name="allFields">All generated backing fields.</param>
        private static void BuildGenericSet(TypeBuilder typeBuilder, MethodInfo methodInfo, Type targetType, IReadOnlyList<FieldInfo> allFields)
        {
            MethodBuilder methodBuilder = typeBuilder.DefineMethod(methodInfo.Name, BackingFieldProvider.GeneratedMethodAttributes);
            GenericTypeParameterBuilder genericParameter = methodBuilder.DefineGenericParameters("T")[0];
            methodBuilder.SetParameters(genericParameter, typeof(string));
            methodBuilder.SetReturnType(methodInfo.ReturnType);

            BackingFieldProvider.EmitSetMethodBody(methodInfo, targetType, methodBuilder, allFields, genericParameter);

            typeBuilder.DefineMethodOverride(methodBuilder, methodInfo);
        }

        /// <summary>
        ///     Emits the method body of the <see cref="IPropertySetExpression.Set{T}" /> method or its overloads. The method body
        ///     consists
        ///     of one big if-clause with branches for each field in <paramref name="fields" />. In the if-branches the old
        ///     value is compared to the new one, and if they differ, the new
        ///     value is stored in the field and <c>true</c> is returned; otherwise, <c>false</c>.
        /// </summary>
        /// <param name="methodInfo">The <see cref="MethodInfo" /> specifying which overload to build.</param>
        /// <param name="targetType">The type of the view model to generate the backing fields for.</param>
        /// <param name="methodBuilder">The <see cref="MethodBuilder" />.</param>
        /// <param name="fields">All generated backing fields of the specific type to build the Set overload for.</param>
        /// <param name="genericType">Type of the generic.</param>
        private static void EmitSetMethodBody(MethodInfo methodInfo, Type targetType, MethodBuilder methodBuilder, IReadOnlyList<FieldInfo> fields, Type? genericType = null)
        {
            ILGenerator ilGenerator = methodBuilder.GetILGenerator();

            if (BackingFieldProvider.fieldGenerationSuppressed)
            {
                BackingFieldProvider.EmitThrowFieldGenerationSuppressed(ilGenerator, targetType);
                return;
            }

            if (fields.Count is 0)
            {
                BackingFieldProvider.EmitNoPropertyOfType(ilGenerator, methodInfo.ReturnType, targetType);
                return;
            }

            Label propertyNotFoundLabel = ilGenerator.DefineLabel();
            Label[] fieldLabels = fields.Select(x => ilGenerator.DefineLabel()).ToArray();

            for (int i = 0; i < fieldLabels.Length; i++)
            {
                FieldInfo currentField = fields[i];

                ilGenerator.MarkLabel(fieldLabels[i]);

                ilGenerator.Emit(OpCodes.Ldarg_2);
                ilGenerator.Emit(OpCodes.Ldstr, currentField.Name);
                ilGenerator.Emit(OpCodes.Call, MethodInfoCache.StringOpEquality);

                ilGenerator.Emit(OpCodes.Brfalse, i == fieldLabels.Length - 1 ? propertyNotFoundLabel : fieldLabels[i + 1]);

                if (methodInfo == MethodInfoCache.SetGeneric)
                {
                    BackingFieldProvider.EmitTypeAwareSetWithBoxing(ilGenerator, currentField, genericType!);
                }
                else
                {
                    BackingFieldProvider.EmitTypeAwareSet(ilGenerator, currentField);
                }

                ilGenerator.Emit(OpCodes.Stfld, currentField);
                ilGenerator.Emit(OpCodes.Ldc_I4_1);
                ilGenerator.Emit(OpCodes.Ret);
            }

            ilGenerator.MarkLabel(propertyNotFoundLabel);
            BackingFieldProvider.EmitThrowPropertyNotFound(ilGenerator, methodInfo.GetParameters()[0].ParameterType, targetType, false);
        }

        /// <summary>
        ///     Determines whether the specified <paramref name="type" /> is a structure without an overloaded <c>operator==</c>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///     <c>true</c> if <paramref name="type" /> is a structure without an overloaded <c>operator==</c>; otherwise,
        ///     <c>false</c>.
        /// </returns>
        private static bool IsStructWithoutOpEquality(Type type)
        {
            if (type.IsEnum || type.IsClass)
            {
                return false;
            }

            return type.GetMethod("op_Equality") is null;
        }

        /// <summary>
        ///     Emits an if-branch with different code depending on whether the field type is a reference or value type etc.
        /// </summary>
        /// <param name="ilGenerator">The <see cref="ILGenerator" />.</param>
        /// <param name="field">The generated backing field to use in the if-branch.</param>
        private static void EmitTypeAwareSet(ILGenerator ilGenerator, FieldInfo field)
        {
            Type fieldType = field.FieldType;

            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldfld, field);
            ilGenerator.Emit(OpCodes.Ldarg_1);

            Label valuesNotEqual = ilGenerator.DefineLabel();

            if (fieldType.IsPrimitive)
            {
                // int, bool, float, ...
                ilGenerator.Emit(OpCodes.Bne_Un, valuesNotEqual);
            }
            else
            {
                // string, decimal and DateTime
                ilGenerator.Emit(OpCodes.Call, fieldType.GetMethod("op_Equality"));
                ilGenerator.Emit(OpCodes.Brfalse, valuesNotEqual);
            }

            ilGenerator.Emit(OpCodes.Ldc_I4_0);
            ilGenerator.Emit(OpCodes.Ret);

            ilGenerator.MarkLabel(valuesNotEqual);

            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldarg_1);
        }

        /// <summary>
        ///     Emits an if-branch with different code depending on whether the field type is a reference or value type etc. for
        ///     use in the generic <see cref="IPropertySetExpression.Set{T}" /> overload.
        /// </summary>
        /// <param name="ilGenerator">The <see cref="ILGenerator" />.</param>
        /// <param name="field">The generated backing field to use in the if-branch.</param>
        /// <param name="genericType">Type of the generic.</param>
        private static void EmitTypeAwareSetWithBoxing(ILGenerator ilGenerator, FieldInfo field, Type genericType)
        {
            Type fieldType = field.FieldType;
            LocalBuilder local = ilGenerator.DeclareLocal(fieldType);

            ilGenerator.Emit(OpCodes.Ldarg_1);

            ilGenerator.Emit(OpCodes.Box, genericType);
            ilGenerator.Emit(OpCodes.Unbox_Any, fieldType);

            ilGenerator.Emit(OpCodes.Stloc, local);
            ilGenerator.Emit(OpCodes.Ldarg_0);

            Label valuesNotEqual = ilGenerator.DefineLabel();

            if (fieldType.IsEnum || fieldType.IsPrimitive || fieldType.IsClass && fieldType != typeof(string))
            {
                // enumerations, primitive types (int, bool, float, ...) and classes apart from string
                ilGenerator.Emit(OpCodes.Ldfld, field);
                ilGenerator.Emit(OpCodes.Ldloc, local);
                ilGenerator.Emit(OpCodes.Bne_Un, valuesNotEqual);
            }
            else if (BackingFieldProvider.IsStructWithoutOpEquality(fieldType))
            {
                // structs without overloaded operator==
                ilGenerator.Emit(OpCodes.Ldflda, field);
                ilGenerator.Emit(OpCodes.Ldloc, local);
                ilGenerator.Emit(OpCodes.Box, fieldType);
                ilGenerator.Emit(OpCodes.Constrained, fieldType);
                ilGenerator.Emit(OpCodes.Callvirt, MethodInfoCache.ObjectEquals1);
                ilGenerator.Emit(OpCodes.Brfalse, valuesNotEqual);
            }
            else if (fieldType == typeof(decimal) || fieldType == typeof(DateTime) || fieldType == typeof(string) || fieldType.IsValueType)
            {
                //decimal, DateTime, string and structs with overloaded operator==
                ilGenerator.Emit(OpCodes.Ldfld, field);
                ilGenerator.Emit(OpCodes.Ldloc, local);
                ilGenerator.Emit(OpCodes.Call, fieldType.GetMethod("op_Equality"));
                ilGenerator.Emit(OpCodes.Brfalse, valuesNotEqual);
            }
            else
            {
                throw new InternalException($"Cannot emit type-aware set code for type '{fieldType}'.");
            }

            ilGenerator.Emit(OpCodes.Ldc_I4_0);
            ilGenerator.Emit(OpCodes.Ret);

            ilGenerator.MarkLabel(valuesNotEqual);

            ilGenerator.Emit(OpCodes.Ldarg_0);
            ilGenerator.Emit(OpCodes.Ldloc, local);
        }
    }
}