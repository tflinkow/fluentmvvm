using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

using JetBrains.Annotations;

namespace FluentMvvm
{
    /// <summary>Builds a class containing the backing fields for a view model's public writable instance properties.</summary>
    [NoReorder]
    internal static class BackingFieldProvider
    {
        /// <summary>The name of the generated assembly.</summary>
        private const string AssemblyName = "fluentmvvm.dynamic";

        /// <summary>The namespace of the generated types.</summary>
        private const string AssemblyNamespace = "FluentMvvm.Dynamic";

        /// <summary>The <see cref="TypeAttributes" /> if the generated types.</summary>
        private const TypeAttributes GeneratedTypeAttributes = TypeAttributes.BeforeFieldInit | TypeAttributes.Class | TypeAttributes.Sealed;

        /// <summary>
        ///     The <see cref="MethodAttributes" /> for the implementations of
        ///     <see cref="IBackingFieldProvider.GetValueOf(string)" />,
        ///     <see cref="IBackingFieldProvider.SetValueOf(string, object)" /> methods.
        /// </summary>
        private const MethodAttributes GeneratedMethodAttributes = MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.NewSlot;

        /// <summary>The dynamic assembly builder.</summary>
        private static AssemblyBuilder assemblyBuilder;

        /// <summary>The module builder of the dynamic assembly.</summary>
        private static ModuleBuilder moduleBuilder;

        /// <summary>The <see cref="MethodInfo" /> for the <see cref="IBackingFieldProvider.GetValueOf(string)" /> method.</summary>
        private static readonly MethodInfo GetValueOfMethodInfo = typeof(IBackingFieldProvider).GetMethod(nameof(IBackingFieldProvider.GetValueOf));

        /// <summary>The <see cref="MethodInfo" /> for the <see cref="IBackingFieldProvider.SetValueOf(string, object)" /> method.</summary>
        private static readonly MethodInfo SetValueOfMethodInfo = typeof(IBackingFieldProvider).GetMethod(nameof(IBackingFieldProvider.SetValueOf));

        /// <summary>A cache for generated types to avoid defining new types for the same target type.</summary>
        private static readonly IDictionary<Type, Type> Cache = new ConcurrentDictionary<Type, Type>();

        /// <summary>
        ///     Gets the class containing the backing fields of <paramref name="targetType" />'s public writable instance
        ///     properties.
        /// </summary>
        /// <remarks>The backing fields are named exactly as their properties.</remarks>
        /// <param name="targetType">The type of the view model to generate the backing fields for.</param>
        /// <returns>
        ///     A class containing backing fields for all public writable instance properties of
        ///     <paramref name="targetType" />.
        /// </returns>
        [CanBeNull]
        public static IBackingFieldProvider Get([NotNull] Type targetType)
        {
            if (!BackingFieldProvider.Cache.ContainsKey(targetType))
            {
                BackingFieldProvider.Cache[targetType] = BackingFieldProvider.BuildForType(targetType);
            }

            Type generatedType = BackingFieldProvider.Cache[targetType];

            return generatedType is null ? null : (IBackingFieldProvider) Activator.CreateInstance(generatedType);
        }

        /// <summary>
        ///     Builds the class containing the backing fields of <paramref name="targetType" />'s public writable instance
        ///     properties.
        /// </summary>
        /// <param name="targetType">The type of the view model to generate the backing fields for.</param>
        /// <returns>The type of the class containing the backing fields</returns>
        [CanBeNull]
        private static Type BuildForType([NotNull] Type targetType)
        {
            if (targetType.GetCustomAttribute<SuppressFieldGenerationAttribute>() != null)
            {
                return default;
            }

            IReadOnlyList<PropertyInfo> properties = BackingFieldProvider.GetRelevantProperties(targetType);

            if (properties.Count is 0)
            {
                return default;
            }

            if (BackingFieldProvider.assemblyBuilder is null)
            {
                AssemblyName name = new AssemblyName(BackingFieldProvider.AssemblyName);
                BackingFieldProvider.assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(name, AssemblyBuilderAccess.Run);
                BackingFieldProvider.moduleBuilder = BackingFieldProvider.assemblyBuilder.DefineDynamicModule(name.Name);
            }

            TypeBuilder typeBuilder = BackingFieldProvider.moduleBuilder.DefineType($"{BackingFieldProvider.AssemblyNamespace}.BackingFieldProvider<{targetType.Name}>", BackingFieldProvider.GeneratedTypeAttributes);
            typeBuilder.AddInterfaceImplementation(typeof(IBackingFieldProvider));

            FieldInfo[] fields = BackingFieldProvider.BuildBackingFields(properties, typeBuilder);

            BackingFieldProvider.BuildConstructor(typeBuilder, properties, fields);
            BackingFieldProvider.BuildGetValueOf(targetType, typeBuilder, fields);
            BackingFieldProvider.BuildSetValueOf(targetType, typeBuilder, fields);

            return typeBuilder.CreateTypeInfo().AsType();
        }

        /// <summary>
        ///     Gets the properties to build backing fields for. Only considers public writable instance properties that do
        ///     not have a <see cref="SuppressFieldGenerationAttribute" /> applied to them.
        /// </summary>
        /// <param name="type">The type to build backing fields for.</param>
        /// <returns>A collection of all properties to build backing fields for.</returns>
        [NotNull]
        private static IReadOnlyList<PropertyInfo> GetRelevantProperties([NotNull] Type type)
        {
            return type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                       .Where(p => p.CanWrite && p.GetCustomAttribute<SuppressFieldGenerationAttribute>() is null)
                       .ToArray();
        }

        /// <summary>Builds the backing fields for the public writable instance properties <paramref name="properties" />.</summary>
        /// <param name="properties">The properties to build backing fields for.</param>
        /// <param name="typeBuilder">The type builder.</param>
        /// <returns>The generated backing fields.</returns>
        [NotNull]
        private static FieldInfo[] BuildBackingFields([NotNull] [ItemNotNull] IReadOnlyList<PropertyInfo> properties, [NotNull] TypeBuilder typeBuilder)
        {
            FieldInfo[] fields = new FieldInfo[properties.Count];

            for (int j = 0; j < properties.Count; j++)
            {
                fields[j] = typeBuilder.DefineField(properties[j].Name, typeof(object), FieldAttributes.Private);
            }

            return fields;
        }

        /// <summary>Builds the constructor of the class containing the backing fields.</summary>
        /// <param name="typeBuilder">The type builder.</param>
        /// <param name="properties">The public writable instance properties of the view model.</param>
        /// <param name="fields">The backing fields.</param>
        private static void BuildConstructor([NotNull] TypeBuilder typeBuilder, [NotNull] [ItemNotNull] IReadOnlyList<PropertyInfo> properties, [NotNull] [ItemNotNull] IReadOnlyList<FieldInfo> fields)
        {
            ILGenerator ilGenerator = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, Type.EmptyTypes)
                                                 .GetILGenerator();

            for (int i = 0; i < properties.Count; i++)
            {
                if (!properties[i].PropertyType.IsValueType)
                {
                    continue;
                }

                ilGenerator.Emit(OpCodes.Ldarg_0);
                ilGenerator.Emit(OpCodes.Ldtoken, properties[i].PropertyType);
                ilGenerator.Emit(OpCodes.Call, typeof(Type).GetMethod(nameof(Type.GetTypeFromHandle)));
                ilGenerator.Emit(OpCodes.Call, typeof(Activator).GetMethod(nameof(Activator.CreateInstance), new[] { typeof(Type) }));
                ilGenerator.Emit(OpCodes.Stfld, fields[i]);
            }

            ilGenerator.Emit(OpCodes.Ret);
        }

        /// <summary>Builds the <see cref="IBackingFieldProvider.GetValueOf" /> method.</summary>
        /// <param name="targetType">The type of the view model to generate the backing fields for.</param>
        /// <param name="typeBuilder">The type builder.</param>
        /// <param name="fields">The backing fields.</param>
        private static void BuildGetValueOf([NotNull] MemberInfo targetType, [NotNull] TypeBuilder typeBuilder, [NotNull] [ItemNotNull] IReadOnlyList<FieldInfo> fields)
        {
            MethodBuilder methodBuilder = typeBuilder.DefineMethod(BackingFieldProvider.GetValueOfMethodInfo.Name, BackingFieldProvider.GeneratedMethodAttributes, typeof(object), new[] { typeof(string) });
            ILGenerator ilGenerator = methodBuilder.GetILGenerator();
            BackingFieldProvider.WriteMethodBody(ilGenerator, fields, targetType.Name, true);
            typeBuilder.DefineMethodOverride(methodBuilder, BackingFieldProvider.GetValueOfMethodInfo);
        }

        /// <summary>Builds the <see cref="IBackingFieldProvider.SetValueOf" /> method.</summary>
        /// <param name="targetType">The type of the view model to generate the backing fields for.</param>
        /// <param name="typeBuilder">The type builder.</param>
        /// <param name="fields">The backing fields.</param>
        private static void BuildSetValueOf([NotNull] MemberInfo targetType, [NotNull] TypeBuilder typeBuilder, [ItemNotNull] [NotNull] IReadOnlyList<FieldInfo> fields)
        {
            MethodBuilder methodBuilder = typeBuilder.DefineMethod(BackingFieldProvider.SetValueOfMethodInfo.Name, BackingFieldProvider.GeneratedMethodAttributes, typeof(bool), new[] { typeof(string), typeof(object) });
            ILGenerator ilGenerator = methodBuilder.GetILGenerator();
            BackingFieldProvider.WriteMethodBody(ilGenerator, fields, targetType.Name, false);
            typeBuilder.DefineMethodOverride(methodBuilder, BackingFieldProvider.SetValueOfMethodInfo);
        }

        /// <summary>
        ///     Writes the method bodies for the <see cref="IBackingFieldProvider.GetValueOf" /> and
        ///     <see cref="IBackingFieldProvider.SetValueOf" /> methods.
        /// </summary>
        /// <param name="ilGenerator">The <see cref="ILGenerator" />.</param>
        /// <param name="fieldInfos">The backing fields.</param>
        /// <param name="targetTypeName">The name of the view model.</param>
        /// <param name="isGetValue">
        ///     if set to <c>true</c>, code specific for <see cref="IBackingFieldProvider.GetValueOf" /> is
        ///     generated; otherwise, code specific for <see cref="IBackingFieldProvider.SetValueOf" /> is generated.
        /// </param>
        private static void WriteMethodBody([NotNull] ILGenerator ilGenerator, [NotNull] IReadOnlyList<FieldInfo> fieldInfos, [NotNull] string targetTypeName, bool isGetValue)
        {
            Label fail = ilGenerator.DefineLabel();

            ilGenerator.Emit(OpCodes.Ldarg_1);
            ilGenerator.Emit(OpCodes.Brfalse, fail);

            Label[] labels = fieldInfos.Select(x => ilGenerator.DefineLabel()).ToArray();
            Label[] fieldFoundLabels = new Label[labels.Length];

            for (int i = 0; i < labels.Length; i++)
            {
                ilGenerator.MarkLabel(labels[i]);

                ilGenerator.Emit(OpCodes.Ldarg_1);
                ilGenerator.Emit(OpCodes.Ldstr, fieldInfos[i].Name);
                ilGenerator.Emit(OpCodes.Call, typeof(string).GetMethod("op_Equality"));

                fieldFoundLabels[i] = ilGenerator.DefineLabel();

                ilGenerator.Emit(OpCodes.Brtrue, fieldFoundLabels[i]);
            }

            ilGenerator.Emit(OpCodes.Br, fail);

            for (int i = 0; i < fieldFoundLabels.Length; i++)
            {
                ilGenerator.MarkLabel(fieldFoundLabels[i]);

                if (isGetValue)
                {
                    ilGenerator.Emit(OpCodes.Ldarg_0);
                    ilGenerator.Emit(OpCodes.Ldfld, fieldInfos[i]);
                    ilGenerator.Emit(OpCodes.Ret);
                }
                else
                {
                    ilGenerator.Emit(OpCodes.Ldarg_0);
                    ilGenerator.Emit(OpCodes.Ldfld, fieldInfos[i]);
                    ilGenerator.Emit(OpCodes.Ldarg_2);
                    ilGenerator.Emit(OpCodes.Call, typeof(object).GetMethod(nameof(Object.Equals), new[] { typeof(object), typeof(object) }));

                    Label valuesNotEqualLabel = ilGenerator.DefineLabel();

                    ilGenerator.Emit(OpCodes.Brfalse_S, valuesNotEqualLabel);
                    ilGenerator.Emit(OpCodes.Ldc_I4_0);
                    ilGenerator.Emit(OpCodes.Ret);

                    ilGenerator.MarkLabel(valuesNotEqualLabel);
                    ilGenerator.Emit(OpCodes.Ldarg_0);
                    ilGenerator.Emit(OpCodes.Ldarg_2);
                    ilGenerator.Emit(OpCodes.Stfld, fieldInfos[i]);
                    ilGenerator.Emit(OpCodes.Ldc_I4_1);
                    ilGenerator.Emit(OpCodes.Ret);
                }
            }

            ilGenerator.MarkLabel(fail);
            ilGenerator.Emit(OpCodes.Ldstr, "Cannot find a public writable instance property named '");
            ilGenerator.Emit(OpCodes.Ldarg_1);
            ilGenerator.Emit(OpCodes.Ldstr, $"' on type '{targetTypeName}'");
            ilGenerator.Emit(OpCodes.Call, typeof(string).GetMethod(nameof(String.Concat), new[] { typeof(string), typeof(string), typeof(string) }));
            ilGenerator.Emit(OpCodes.Newobj, typeof(ArgumentException).GetConstructor(new[] { typeof(string) }));
            ilGenerator.Emit(OpCodes.Throw);
        }
    }
}