using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using FluentMvvm.Fluent;

namespace FluentMvvm.Emit
{
    /// <summary>
    ///     Builds a class containing the backing fields for a view model's public writable instance properties.
    /// </summary>
    internal static partial class BackingFieldProvider
    {
        /// <summary>
        ///     The namespace of the generated types.
        /// </summary>
        private const string AssemblyNamespace = "FluentMvvm.Dynamic";

        /// <summary>
        ///     The <see cref="TypeAttributes" /> if the generated types.
        /// </summary>
        private const TypeAttributes GeneratedTypeAttributes = TypeAttributes.BeforeFieldInit | TypeAttributes.Class | TypeAttributes.Sealed;

        /// <summary>
        ///     The <see cref="MethodAttributes" /> used when emitting methods implementing
        ///     <see cref="IPropertyGetExpression" /> and <see cref="IPropertySetExpression" />.
        /// </summary>
        private const MethodAttributes GeneratedMethodAttributes = MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.Final | MethodAttributes.HideBySig | MethodAttributes.NewSlot;

        /// <summary>
        ///     The name of the generated assembly.
        /// </summary>
        private static readonly AssemblyName assemblyName = new AssemblyName("fluentmvvm.dynamic");

        /// <summary>
        ///     The lazily instantiated <see cref="System.Reflection.Emit.AssemblyBuilder" /> used to build dynamic assemblies.
        /// </summary>
        private static readonly Lazy<AssemblyBuilder> assemblyBuilderLazy = new Lazy<AssemblyBuilder>(() => AssemblyBuilder.DefineDynamicAssembly(BackingFieldProvider.assemblyName, AssemblyBuilderAccess.Run));

        /// <summary>
        ///     The lazily instantiated <see cref="System.Reflection.Emit.ModuleBuilder" /> used to build modules in dynamic
        ///     assemblies.
        /// </summary>
        private static readonly Lazy<ModuleBuilder> moduleBuilderLazy = new Lazy<ModuleBuilder>(() => BackingFieldProvider.AssemblyBuilder.DefineDynamicModule(BackingFieldProvider.assemblyName.Name));

        /// <summary>
        ///     Indicates whether backing field generation is suppressed for the type itself.
        /// </summary>
        private static bool fieldGenerationSuppressed;

        /// <summary>
        ///     A cache for generated types to avoid defining new types for the same target type.
        /// </summary>
        private static readonly IDictionary<Type, Type> cache = new ConcurrentDictionary<Type, Type>();

        /// <summary>
        ///     The dynamic assembly builder.
        /// </summary>
        /// <value>
        ///     The assembly builder.
        /// </value>
        private static AssemblyBuilder AssemblyBuilder => BackingFieldProvider.assemblyBuilderLazy.Value;

        /// <summary>
        ///     The module builder of the dynamic assembly.
        /// </summary>
        /// <value>
        ///     The module builder.
        /// </value>
        private static ModuleBuilder ModuleBuilder => BackingFieldProvider.moduleBuilderLazy.Value;

        /// <summary>
        ///     Gets the class containing the backing fields of <paramref name="targetType" />'s public writable instance
        ///     properties.
        /// </summary>
        /// <param name="targetType">The type of the view model to generate the backing fields for.</param>
        /// <returns>
        ///     A class containing backing fields for all public writable instance properties of
        ///     <paramref name="targetType" />.
        /// </returns>
        /// <remarks>
        ///     The backing fields are named exactly as their properties.
        /// </remarks>
        public static IBackingFieldProvider Get(Type targetType)
        {
            if (!BackingFieldProvider.cache.ContainsKey(targetType))
            {
                BackingFieldProvider.cache[targetType] = BackingFieldProvider.BuildForType(targetType);
            }

            Type generatedType = BackingFieldProvider.cache[targetType];

            return (IBackingFieldProvider) Activator.CreateInstance(generatedType);
        }

        /// <summary>
        ///     Gets the name of the type to generate.
        /// </summary>
        /// <param name="forTargetType">The type of the view model.</param>
        /// <returns>The name of the type to generate.</returns>
        private static string GetTypeName(Type forTargetType)
        {
            return $"{BackingFieldProvider.AssemblyNamespace}.<{forTargetType.Name}>BackingFields";
        }

        /// <summary>
        ///     Builds the class containing the backing fields of <paramref name="targetType" />'s public writable instance
        ///     properties.
        /// </summary>
        /// <param name="targetType">The type of the view model to generate the backing fields for.</param>
        /// <returns>
        ///     The type of the class containing the backing fields
        /// </returns>
        /// <exception cref="InternalException">The generated type for is null.</exception>
        private static Type BuildForType(Type targetType)
        {
            BackingFieldProvider.fieldGenerationSuppressed = targetType.GetCustomAttribute<SuppressFieldGenerationAttribute>() != null;

            IEnumerable<BackingFieldInfo> properties;

            if (BackingFieldProvider.fieldGenerationSuppressed)
            {
                properties = Array.Empty<BackingFieldInfo>();
            }
            else
            {
                properties = BackingFieldProvider.GetRelevantProperties(targetType);
            }

            TypeBuilder typeBuilder = BackingFieldProvider.ModuleBuilder.DefineType(BackingFieldProvider.GetTypeName(targetType), BackingFieldProvider.GeneratedTypeAttributes);
            typeBuilder.AddInterfaceImplementation(typeof(IBackingFieldProvider));

            IReadOnlyList<BackingFieldInfo> fields = BackingFieldProvider.BuildBackingFields(properties, typeBuilder).ToArray();
            IReadOnlyList<FieldInfo> onlyFields = fields.Select(x => x.Field).ToArray();

            if (fields.Count(x => x.HasOverriddenDefaultValue) != 0)
            {
                ConstructorBuilder constructorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, Type.EmptyTypes);
                BackingFieldProvider.BuildConstructor(fields, constructorBuilder);
            }

            BackingFieldProvider.BuildGetMethods(targetType, typeBuilder, onlyFields);
            BackingFieldProvider.BuildSetMethods(targetType, typeBuilder, onlyFields);

            TypeInfo? createdType = typeBuilder.CreateTypeInfo();

            if (createdType is null)
            {
                throw new InternalException($"The generated type for '{targetType.Name}' is null.");
            }

            return createdType.AsType();
        }

        /// <summary>
        ///     Builds a constructor initializing all fields that have an overridden default value.
        /// </summary>
        /// <param name="fields">The generated fields.</param>
        /// <param name="constructorBuilder">The constructor builder.</param>
        private static void BuildConstructor(IEnumerable<BackingFieldInfo> fields, ConstructorBuilder constructorBuilder)
        {
            ILGenerator ilGenerator = constructorBuilder.GetILGenerator();

            foreach (BackingFieldInfo field in fields)
            {
                if (!field.HasOverriddenDefaultValue)
                {
                    continue;
                }

                ilGenerator.Emit(OpCodes.Ldarg_0);
                ilGenerator.EmitLoadValue(field.DefaultValue!); // TODO: what if string null value??
                ilGenerator.Emit(OpCodes.Stfld, field.Field!);
            }

            ilGenerator.Emit(OpCodes.Ret);
        }

        /// <summary>
        ///     Gets the properties to build backing fields for. Only considers public writable instance properties that do
        ///     not have a <see cref="SuppressFieldGenerationAttribute" /> applied to them.
        /// </summary>
        /// <param name="type">The type to build backing fields for.</param>
        /// <returns>
        ///     A collection of all properties to build backing fields for.
        /// </returns>
        private static IEnumerable<BackingFieldInfo> GetRelevantProperties(IReflect type)
        {
            foreach (PropertyInfo property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (property.CanWrite && property.GetCustomAttribute<SuppressFieldGenerationAttribute>() is null)
                {
                    yield return new BackingFieldInfo(property);
                }
            }
        }

        /// <summary>
        ///     Builds the backing fields for the public writable instance properties <paramref name="properties" />.
        /// </summary>
        /// <param name="properties">The properties to build backing fields for.</param>
        /// <param name="typeBuilder">The type builder.</param>
        /// <returns>
        ///     The generated backing fields.
        /// </returns>
        private static IEnumerable<BackingFieldInfo> BuildBackingFields(IEnumerable<BackingFieldInfo> properties, TypeBuilder typeBuilder)
        {
            foreach (BackingFieldInfo field in properties)
            {
                PropertyInfo property = field.Property;
                field.Field = typeBuilder.DefineField(property.Name, property.PropertyType, FieldAttributes.Private);

                yield return field;
            }
        }

        /// <summary>
        ///     Emits exception throwing code for when <paramref name="targetType" /> does not have any public writable
        ///     instance properties of type <paramref name="propertyType" />.
        /// </summary>
        /// <param name="ilGenerator">The <see cref="ILGenerator" />.</param>
        /// <param name="propertyType">The type of the property.</param>
        /// <param name="targetType">The type to build backing fields for.</param>
        private static void EmitNoPropertyOfType(ILGenerator ilGenerator, Type propertyType, Type targetType)
        {
            if (propertyType.IsGenericParameter)
            {
                ilGenerator.Emit(OpCodes.Ldstr, $"Type '{targetType}' has no public writable instance property of type " + "'{0}'.");
                ilGenerator.Emit(OpCodes.Ldtoken, propertyType);
                ilGenerator.Emit(OpCodes.Call, MethodInfoCache.GetTypeFromHandle);
                ilGenerator.Emit(OpCodes.Call, MethodInfoCache.StringFormat2);
            }
            else
            {
                ilGenerator.Emit(OpCodes.Ldstr, $"Type '{targetType}' has no public writable instance property of type '{propertyType}'.");
            }

            ilGenerator.Emit(OpCodes.Newobj, ConstructorInfoCache.ArgumentException);
            ilGenerator.Emit(OpCodes.Throw);
        }

        /// <summary>
        ///     Emits exception throwing code for when <paramref name="targetType" /> is marked with
        ///     <see cref="SuppressFieldGenerationAttribute" />.
        /// </summary>
        /// <param name="ilGenerator">The <see cref="ILGenerator" />.</param>
        /// <param name="targetType">The type to build backing fields for.</param>
        private static void EmitThrowFieldGenerationSuppressed(ILGenerator ilGenerator, Type targetType)
        {
            ilGenerator.Emit(OpCodes.Ldstr, $"Backing field generation is suppressed for type '{targetType}'.");
            ilGenerator.Emit(OpCodes.Newobj, ConstructorInfoCache.InvalidOperationException);
            ilGenerator.Emit(OpCodes.Throw);
        }

        /// <summary>
        ///     Emits exception throwing code for when no property of the specified name and type exists.
        /// </summary>
        /// <param name="ilGenerator">The <see cref="ILGenerator" />.</param>
        /// <param name="propertyType">The type of the property.</param>
        /// <param name="targetType">The type to build backing fields for.</param>
        /// <param name="forGetMethod">
        ///     Indicates whether the exception throwing code is contained in a
        ///     <see cref="IPropertyGetExpression" /> method or in a <see cref="IPropertySetExpression" /> method.
        /// </param>
        private static void EmitThrowPropertyNotFound(ILGenerator ilGenerator, Type propertyType, Type targetType, bool forGetMethod)
        {
            ilGenerator.Emit(forGetMethod ? OpCodes.Ldarg_1 : OpCodes.Ldarg_2);
            ilGenerator.Emit(OpCodes.Call, MethodInfoCache.StringIsNullOrWhiteSpace);

            Label validFieldName = ilGenerator.DefineLabel();

            ilGenerator.Emit(OpCodes.Brfalse, validFieldName);

            ilGenerator.Emit(OpCodes.Ldstr, "The specified property name must not be empty or consist only of white-space characters.");
            ilGenerator.Emit(OpCodes.Newobj, ConstructorInfoCache.ArgumentException);
            ilGenerator.Emit(OpCodes.Throw);

            ilGenerator.MarkLabel(validFieldName);

            ilGenerator.Emit(OpCodes.Ldstr, $"Cannot find a public writable instance property of type '{propertyType}' named '");
            ilGenerator.Emit(forGetMethod ? OpCodes.Ldarg_1 : OpCodes.Ldarg_2);
            ilGenerator.Emit(OpCodes.Ldstr, $"' on type '{targetType.Name}'.");
            ilGenerator.Emit(OpCodes.Call, MethodInfoCache.StringConcat3);
            ilGenerator.Emit(OpCodes.Newobj, ConstructorInfoCache.ArgumentException);
            ilGenerator.Emit(OpCodes.Throw);
        }
    }
}