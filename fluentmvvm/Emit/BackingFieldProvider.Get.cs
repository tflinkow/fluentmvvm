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
        ///     Builds the <see cref="IPropertyGetExpression.Get{T}" /> method and its overloads.
        /// </summary>
        /// <param name="targetType">The type of the view model to generate the backing fields for.</param>
        /// <param name="typeBuilder">The <see cref="TypeBuilder" />.</param>
        /// <param name="fields">The generated backing fields.</param>
        private static void BuildGetMethods(Type targetType, TypeBuilder typeBuilder, IReadOnlyList<FieldInfo> fields)
        {
            BackingFieldProvider.BuildGetOverload(typeBuilder, MethodInfoCache.GetBool, targetType, fields);
            BackingFieldProvider.BuildGetOverload(typeBuilder, MethodInfoCache.GetByte, targetType, fields);
            BackingFieldProvider.BuildGetOverload(typeBuilder, MethodInfoCache.GetSByte, targetType, fields);
            BackingFieldProvider.BuildGetOverload(typeBuilder, MethodInfoCache.GetChar, targetType, fields);
            BackingFieldProvider.BuildGetOverload(typeBuilder, MethodInfoCache.GetDouble, targetType, fields);
            BackingFieldProvider.BuildGetOverload(typeBuilder, MethodInfoCache.GetFloat, targetType, fields);
            BackingFieldProvider.BuildGetOverload(typeBuilder, MethodInfoCache.GetInt16, targetType, fields);
            BackingFieldProvider.BuildGetOverload(typeBuilder, MethodInfoCache.GetUInt16, targetType, fields);
            BackingFieldProvider.BuildGetOverload(typeBuilder, MethodInfoCache.GetInt32, targetType, fields);
            BackingFieldProvider.BuildGetOverload(typeBuilder, MethodInfoCache.GetUInt32, targetType, fields);
            BackingFieldProvider.BuildGetOverload(typeBuilder, MethodInfoCache.GetInt64, targetType, fields);
            BackingFieldProvider.BuildGetOverload(typeBuilder, MethodInfoCache.GetUInt64, targetType, fields);

            BackingFieldProvider.BuildGetOverload(typeBuilder, MethodInfoCache.GetString, targetType, fields);
            BackingFieldProvider.BuildGetOverload(typeBuilder, MethodInfoCache.GetDecimal, targetType, fields);
            BackingFieldProvider.BuildGetOverload(typeBuilder, MethodInfoCache.GetDateTime, targetType, fields);

            BackingFieldProvider.BuildGenericGet(typeBuilder, MethodInfoCache.GetGeneric, targetType, fields);
        }

        /// <summary>
        ///     Builds a non-generic <c>Get</c> method that only takes those fields from <paramref name="allFields" /> into account
        ///     that are of type <paramref name="targetType" />.
        /// </summary>
        /// <param name="typeBuilder">The <see cref="TypeBuilder" />.</param>
        /// <param name="methodInfo">The <see cref="MethodInfo" /> specifying which overload to build.</param>
        /// <param name="targetType">The type of the view model to generate the backing fields for.</param>
        /// <param name="allFields">All generated backing fields.</param>
        private static void BuildGetOverload(TypeBuilder typeBuilder, MethodInfo methodInfo, Type targetType, IEnumerable<FieldInfo> allFields)
        {
            MethodBuilder methodBuilder = typeBuilder.DefineMethod(methodInfo.Name, BackingFieldProvider.GeneratedMethodAttributes, methodInfo.ReturnType, new[] { typeof(string) });
            IReadOnlyList<FieldInfo> relevantFields = allFields.Where(x => x.FieldType == methodInfo.ReturnType).ToArray();

            BackingFieldProvider.EmitGetMethodBody(methodInfo, targetType, methodBuilder, relevantFields);

            typeBuilder.DefineMethodOverride(methodBuilder, methodInfo);
        }

        /// <summary>
        ///     Builds the generic <see cref="IPropertyGetExpression.Get{T}" /> method that takes all backing fields into account.
        /// </summary>
        /// <param name="typeBuilder">The <see cref="TypeBuilder" />.</param>
        /// <param name="methodInfo">The <see cref="MethodInfo" /> specifying which overload to build.</param>
        /// <param name="targetType">The type of the view model to generate the backing fields for.</param>
        /// <param name="allFields">All generated backing fields.</param>
        private static void BuildGenericGet(TypeBuilder typeBuilder, MethodInfo methodInfo, Type targetType, IReadOnlyList<FieldInfo> allFields)
        {
            MethodBuilder methodBuilder = typeBuilder.DefineMethod(methodInfo.Name, BackingFieldProvider.GeneratedMethodAttributes);
            GenericTypeParameterBuilder genericType = methodBuilder.DefineGenericParameters("T")[0];
            methodBuilder.SetReturnType(genericType);
            methodBuilder.SetParameters(typeof(string));

            BackingFieldProvider.EmitGetMethodBody(methodInfo, targetType, methodBuilder, allFields, genericType);

            typeBuilder.DefineMethodOverride(methodBuilder, methodInfo);
        }

        /// <summary>
        ///     Emit the method body of the <see cref="IPropertyGetExpression.Get{T}" /> method or its overloads. The method body
        ///     consists of one big if-clause with branches for every
        ///     field in <paramref name="fields" />. In the if-branches the field value is returned.
        /// </summary>
        /// <param name="methodInfo">The <see cref="MethodInfo" /> specifying which overload to build.</param>
        /// <param name="targetType">The type of the view model to generate the backing fields for.</param>
        /// <param name="methodBuilder">The <see cref="MethodBuilder" />.</param>
        /// <param name="fields">All generated backing fields of the specific type to build the Set overload for.</param>
        /// <param name="genericType">
        ///     The generic type definition in case the <see cref="IPropertyGetExpression.Get{T}" /> is
        ///     generated, or <c>null</c> for the non-generic overloads.
        /// </param>
        private static void EmitGetMethodBody(MethodInfo methodInfo, Type targetType, MethodBuilder methodBuilder, IReadOnlyList<FieldInfo> fields, Type? genericType = null)
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
                ilGenerator.MarkLabel(fieldLabels[i]);

                ilGenerator.Emit(OpCodes.Ldarg_1);
                ilGenerator.Emit(OpCodes.Ldstr, fields[i].Name);
                ilGenerator.Emit(OpCodes.Call, MethodInfoCache.StringOpEquality);

                ilGenerator.Emit(OpCodes.Brfalse, i == fieldLabels.Length - 1 ? propertyNotFoundLabel : fieldLabels[i + 1]);

                ilGenerator.Emit(OpCodes.Ldarg_0);
                ilGenerator.Emit(OpCodes.Ldfld, fields[i]);

                if (genericType != null)
                {
                    // TODO: null ref exception for strings (and possibly all other nullable types?)
                    //if (fields[i].FieldType == typeof(string))
                    //{
                    //    // If the generic type is not string, we need to explicitly throw an exception.
                    //    // Performance is not a concern here, because for performance reasons there is an
                    //    // explicit GetString(string name) overload.
                    //    ilGenerator.Emit(OpCodes.Isinst, genericType);
                    //    var genericTypeIsStringLabel = ilGenerator.DefineLabel();

                    //    ilGenerator.Emit(OpCodes.Brtrue, genericTypeIsStringLabel);

                    //    ilGenerator.Emit(OpCodes.Ldstr, "Cannot cast string value to '{0}'.");
                    //    ilGenerator.Emit(OpCodes.Ldtoken, genericType);
                    //    ilGenerator.Emit(OpCodes.Call, MethodInfoCache.GetTypeFromHandle);
                    //    ilGenerator.Emit(OpCodes.Call, MethodInfoCache.StringFormat2);
                    //    ilGenerator.Emit(OpCodes.Newobj, ConstructorInfoCache.InvalidCastException);
                    //    ilGenerator.Emit(OpCodes.Throw);

                    //    ilGenerator.MarkLabel(genericTypeIsStringLabel);
                    //    ilGenerator.Emit(OpCodes.Ldarg_0);
                    //    ilGenerator.Emit(OpCodes.Ldfld, fields[i]);
                    //}

                    if (fields[i].FieldType.IsValueType)
                    {
                        ilGenerator.Emit(OpCodes.Box, fields[i].FieldType);
                    }

                    ilGenerator.Emit(OpCodes.Unbox_Any, genericType);
                }

                ilGenerator.Emit(OpCodes.Ret);
            }

            ilGenerator.MarkLabel(propertyNotFoundLabel);
            BackingFieldProvider.EmitThrowPropertyNotFound(ilGenerator, methodInfo.ReturnType, targetType, true);
        }
    }
}