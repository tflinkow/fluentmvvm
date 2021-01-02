using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace FluentMvvm.Internals
{
    [ExcludeFromCodeCoverage]
    internal static class RuntimeHelper
    {
        /// <summary>
        /// Indicates whether literal strings are interned.
        /// </summary>
        public static readonly bool AreLiteralsInterned = String.IsInterned("literal") != null;

        [Conditional("DEBUG")]
        public static void AssertInterned(string str)
        {
            if (RuntimeHelper.AreLiteralsInterned)
            {
                Debug.Assert(String.IsInterned(str) != null);
            }
        }
    }
}