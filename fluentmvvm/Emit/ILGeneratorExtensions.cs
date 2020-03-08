using System;
using System.ComponentModel;
using System.Reflection.Emit;

namespace FluentMvvm.Emit
{
    /// <summary>
    ///     Provides extension methods for <see cref="ILGenerator" />.
    /// </summary>
    internal static class ILGeneratorExtensions
    {
        /// <summary>
        ///     Puts the correct instruction to load the specified <paramref name="value" /> on the instruction stream.
        /// </summary>
        /// <param name="ilGenerator">The <see cref="ILGenerator" />.</param>
        /// <param name="value">The value to load.</param>
        /// <exception cref="ArgumentNullException"><paramref name="value" /> is null.</exception>
        /// <exception cref="InvalidOperationException">
        ///     <paramref name="value" /> is <see cref="decimal" />, a class other than
        ///     string or a struct other than inbuilt primary types.
        /// </exception>
        public static void EmitLoadValue(this ILGenerator ilGenerator, object value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value is Enum @enum)
            {
                Type underlyingType = Enum.GetUnderlyingType(@enum.GetType());
                object converted = Convert.ChangeType(@enum, underlyingType);
                ilGenerator.EmitLoadValue(converted);
                return;
            }

            switch (value)
            {
                case bool @bool:
                    ilGenerator.Emit(@bool ? OpCodes.Ldc_I4_1 : OpCodes.Ldc_I4_0);
                    return;
                case byte @byte:
                    ilGenerator.Emit(OpCodes.Ldc_I4_S, @byte);
                    return;
                case sbyte @sbyte:
                    ilGenerator.Emit(OpCodes.Ldc_I4_S, @sbyte);
                    return;
                case char @char:
                    ilGenerator.Emit(OpCodes.Ldc_I4, @char);
                    return;
                case short @short:
                    ilGenerator.Emit(OpCodes.Ldc_I4, @short);
                    return;
                case ushort @ushort:
                    ilGenerator.Emit(OpCodes.Ldc_I4, @ushort);
                    return;
                case int @int:
                    ilGenerator.Emit(OpCodes.Ldc_I4, @int);
                    return;
                case uint @uint:
                    ilGenerator.Emit(OpCodes.Ldc_I4, @uint);
                    return;
                case long @long:
                    ilGenerator.Emit(OpCodes.Ldc_I8, @long);
                    return;
                case ulong @ulong:
                    ilGenerator.Emit(OpCodes.Ldc_I8, @ulong);
                    return;
                case float @float:
                    ilGenerator.Emit(OpCodes.Ldc_R4, @float);
                    return;
                case double @double:
                    ilGenerator.Emit(OpCodes.Ldc_R8, @double);
                    return;
                case string @string:
                    ilGenerator.Emit(OpCodes.Ldstr, @string);
                    return;

                default: // TODO: ILGeneratorExtensions should not know about DefaultValueAttribute
                    throw new InvalidOperationException($"The '{nameof(DefaultValueAttribute)}' cannot be with '{value.GetType()}'. Use the constructor to set a default value instead.");
            }
        }
    }
}