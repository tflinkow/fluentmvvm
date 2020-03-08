using System;
using System.Linq;
using System.Reflection;
using FluentMvvm.Fluent;

namespace FluentMvvm.Emit
{
    /// <summary>
    ///     Caches frequently used <see cref="MethodInfo" />s so that they are not retrieved every time.
    /// </summary>
    internal static class MethodInfoCache
    {
        /// <summary>
        ///     The generic <see cref="IPropertyGetExpression.Get{T}" /> method.
        /// </summary>
        public static readonly MethodInfo GetGeneric = typeof(IPropertyGetExpression).GetMethod(nameof(IPropertyGetExpression.Get));

        /// <summary>
        ///     The non-generic <see cref="IPropertyGetExpression.GetBool" /> method.
        /// </summary>
        public static readonly MethodInfo GetBool = typeof(IPropertyGetExpression).GetMethod(nameof(IPropertyGetExpression.GetBool));

        /// <summary>
        ///     The non-generic <see cref="IPropertyGetExpression.GetByte" /> method.
        /// </summary>
        public static readonly MethodInfo GetByte = typeof(IPropertyGetExpression).GetMethod(nameof(IPropertyGetExpression.GetByte));

        /// <summary>
        ///     The non-generic <see cref="IPropertyGetExpression.GetSByte" /> method.
        /// </summary>
        public static readonly MethodInfo GetSByte = typeof(IPropertyGetExpression).GetMethod(nameof(IPropertyGetExpression.GetSByte));

        /// <summary>
        ///     The non-generic <see cref="IPropertyGetExpression.GetChar" /> method.
        /// </summary>
        public static readonly MethodInfo GetChar = typeof(IPropertyGetExpression).GetMethod(nameof(IPropertyGetExpression.GetChar));

        /// <summary>
        ///     The non-generic <see cref="IPropertyGetExpression.GetDecimal" /> method.
        /// </summary>
        public static readonly MethodInfo GetDecimal = typeof(IPropertyGetExpression).GetMethod(nameof(IPropertyGetExpression.GetDecimal));

        /// <summary>
        ///     The non-generic <see cref="IPropertyGetExpression.GetDouble" /> method.
        /// </summary>
        public static readonly MethodInfo GetDouble = typeof(IPropertyGetExpression).GetMethod(nameof(IPropertyGetExpression.GetDouble));

        /// <summary>
        ///     The non-generic <see cref="IPropertyGetExpression.GetFloat" /> method.
        /// </summary>
        public static readonly MethodInfo GetFloat = typeof(IPropertyGetExpression).GetMethod(nameof(IPropertyGetExpression.GetFloat));

        /// <summary>
        ///     The non-generic <see cref="IPropertyGetExpression.GetInt16" /> method.
        /// </summary>
        public static readonly MethodInfo GetInt16 = typeof(IPropertyGetExpression).GetMethod(nameof(IPropertyGetExpression.GetInt16));

        /// <summary>
        ///     The non-generic <see cref="IPropertyGetExpression.GetUInt16" /> method.
        /// </summary>
        public static readonly MethodInfo GetUInt16 = typeof(IPropertyGetExpression).GetMethod(nameof(IPropertyGetExpression.GetUInt16));

        /// <summary>
        ///     The non-generic <see cref="IPropertyGetExpression.GetInt32" /> method.
        /// </summary>
        public static readonly MethodInfo GetInt32 = typeof(IPropertyGetExpression).GetMethod(nameof(IPropertyGetExpression.GetInt32));

        /// <summary>
        ///     The non-generic <see cref="IPropertyGetExpression.GetUInt32" /> method.
        /// </summary>
        public static readonly MethodInfo GetUInt32 = typeof(IPropertyGetExpression).GetMethod(nameof(IPropertyGetExpression.GetUInt32));

        /// <summary>
        ///     The non-generic <see cref="IPropertyGetExpression.GetInt64" /> method.
        /// </summary>
        public static readonly MethodInfo GetInt64 = typeof(IPropertyGetExpression).GetMethod(nameof(IPropertyGetExpression.GetInt64));

        /// <summary>
        ///     The non-generic <see cref="IPropertyGetExpression.GetUInt64" /> method.
        /// </summary>
        public static readonly MethodInfo GetUInt64 = typeof(IPropertyGetExpression).GetMethod(nameof(IPropertyGetExpression.GetUInt64));

        /// <summary>
        ///     The non-generic <see cref="IPropertyGetExpression.GetString" /> method.
        /// </summary>
        public static readonly MethodInfo GetString = typeof(IPropertyGetExpression).GetMethod(nameof(IPropertyGetExpression.GetString));

        /// <summary>
        ///     The non-generic <see cref="IPropertyGetExpression.GetDateTime" /> method.
        /// </summary>
        public static readonly MethodInfo GetDateTime = typeof(IPropertyGetExpression).GetMethod(nameof(IPropertyGetExpression.GetDateTime));

        /// <summary>
        ///     The generic <see cref="IPropertySetExpression.Set{T}" /> method.
        /// </summary>
        public static readonly MethodInfo SetGeneric = typeof(IBackingFieldProvider).GetMethods().Single(x => x.Name == nameof(IBackingFieldProvider.Set) && x.IsGenericMethod);

        /// <summary>
        ///     The non-generic <see cref="IPropertySetExpression.Set(bool,string)" /> method.
        /// </summary>
        public static readonly MethodInfo SetBool = typeof(IBackingFieldProvider).GetMethod(nameof(IBackingFieldProvider.Set), new[] { typeof(bool), typeof(string) });

        /// <summary>
        ///     The non-generic <see cref="IPropertySetExpression.Set(byte,string)" /> method.
        /// </summary>
        public static readonly MethodInfo SetByte = typeof(IBackingFieldProvider).GetMethod(nameof(IBackingFieldProvider.Set), new[] { typeof(byte), typeof(string) });

        /// <summary>
        ///     The non-generic <see cref="IPropertySetExpression.Set(sbyte,string)" /> method.
        /// </summary>
        public static readonly MethodInfo SetSByte = typeof(IBackingFieldProvider).GetMethod(nameof(IBackingFieldProvider.Set), new[] { typeof(sbyte), typeof(string) });

        /// <summary>
        ///     The non-generic <see cref="IPropertySetExpression.Set(char,string)" /> method.
        /// </summary>
        public static readonly MethodInfo SetChar = typeof(IBackingFieldProvider).GetMethod(nameof(IBackingFieldProvider.Set), new[] { typeof(char), typeof(string) });

        /// <summary>
        ///     The non-generic <see cref="IPropertySetExpression.Set(decimal,string)" /> method.
        /// </summary>
        public static readonly MethodInfo SetDecimal = typeof(IBackingFieldProvider).GetMethod(nameof(IBackingFieldProvider.Set), new[] { typeof(decimal), typeof(string) });

        /// <summary>
        ///     The non-generic <see cref="IPropertySetExpression.Set(double,string)" /> method.
        /// </summary>
        public static readonly MethodInfo SetDouble = typeof(IBackingFieldProvider).GetMethod(nameof(IBackingFieldProvider.Set), new[] { typeof(double), typeof(string) });

        /// <summary>
        ///     The non-generic <see cref="IPropertySetExpression.Set(float,string)" /> method.
        /// </summary>
        public static readonly MethodInfo SetFloat = typeof(IBackingFieldProvider).GetMethod(nameof(IBackingFieldProvider.Set), new[] { typeof(float), typeof(string) });

        /// <summary>
        ///     The non-generic <see cref="IPropertySetExpression.Set(short,string)" /> method.
        /// </summary>
        public static readonly MethodInfo SetInt16 = typeof(IBackingFieldProvider).GetMethod(nameof(IBackingFieldProvider.Set), new[] { typeof(short), typeof(string) });

        /// <summary>
        ///     The non-generic <see cref="IPropertySetExpression.Set(ushort,string)" /> method.
        /// </summary>
        public static readonly MethodInfo SetUInt16 = typeof(IBackingFieldProvider).GetMethod(nameof(IBackingFieldProvider.Set), new[] { typeof(ushort), typeof(string) });

        /// <summary>
        ///     The non-generic <see cref="IPropertySetExpression.Set(int,string)" /> method.
        /// </summary>
        public static readonly MethodInfo SetInt32 = typeof(IBackingFieldProvider).GetMethod(nameof(IBackingFieldProvider.Set), new[] { typeof(int), typeof(string) });

        /// <summary>
        ///     The non-generic <see cref="IPropertySetExpression.Set(uint,string)" /> method.
        /// </summary>
        public static readonly MethodInfo SetUInt32 = typeof(IBackingFieldProvider).GetMethod(nameof(IBackingFieldProvider.Set), new[] { typeof(uint), typeof(string) });

        /// <summary>
        ///     The non-generic <see cref="IPropertySetExpression.Set(long,string)" /> method.
        /// </summary>
        public static readonly MethodInfo SetInt64 = typeof(IBackingFieldProvider).GetMethod(nameof(IBackingFieldProvider.Set), new[] { typeof(long), typeof(string) });

        /// <summary>
        ///     The non-generic <see cref="IPropertySetExpression.Set(ulong,string)" /> method.
        /// </summary>
        public static readonly MethodInfo SetUInt64 = typeof(IBackingFieldProvider).GetMethod(nameof(IBackingFieldProvider.Set), new[] { typeof(ulong), typeof(string) });

        /// <summary>
        ///     The non-generic <see cref="IPropertySetExpression.Set(string,string)" /> method.
        /// </summary>
        public static readonly MethodInfo SetString = typeof(IBackingFieldProvider).GetMethod(nameof(IBackingFieldProvider.Set), new[] { typeof(string), typeof(string) });

        /// <summary>
        ///     The non-generic <see cref="IPropertySetExpression.Set(DateTime,string)" /> method.
        /// </summary>
        public static readonly MethodInfo SetDateTime = typeof(IBackingFieldProvider).GetMethod(nameof(IBackingFieldProvider.Set), new[] { typeof(DateTime), typeof(string) });

        /// <summary>
        ///     The operator <see cref="String.op_Equality" />.
        /// </summary>
        public static readonly MethodInfo StringOpEquality = typeof(string).GetMethod("op_Equality");

        /// <summary>
        ///     The <see cref="String.IsNullOrWhiteSpace" /> method.
        /// </summary>
        public static readonly MethodInfo StringIsNullOrWhiteSpace = typeof(string).GetMethod(nameof(String.IsNullOrWhiteSpace));

        /// <summary>
        ///     The <see cref="String.Concat(string,string,string)" /> overload taking 3 string parameters.
        /// </summary>
        public static readonly MethodInfo StringConcat3 = typeof(string).GetMethod(nameof(String.Concat), new[] { typeof(string), typeof(string), typeof(string) });

        /// <summary>
        ///     The <see cref="String.Format(string,object)" /> overload taking 2 parameters in total.
        /// </summary>
        public static readonly MethodInfo StringFormat2 = typeof(string).GetMethod(nameof(String.Format), new[] { typeof(string), typeof(object) });

        /// <summary>
        ///     The <see cref="Object.Equals(object)" /> overload taking 1 parameter.
        /// </summary>
        public static readonly MethodInfo ObjectEquals1 = typeof(object).GetMethod(nameof(Object.Equals), new[] { typeof(object) });

        /// <summary>
        ///     The <see cref="Type.GetTypeFromHandle" /> method.
        /// </summary>
        public static readonly MethodInfo GetTypeFromHandle = typeof(Type).GetMethod(nameof(Type.GetTypeFromHandle));
    }
}