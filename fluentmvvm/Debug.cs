using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace FluentMvvm
{
    [ExcludeFromCodeCoverage]
    // The System.Diagnostics.Debug class is not correctly annotated in netstandard  (and apparently will never be, see https://github.com/dotnet/roslyn/issues/37979),
    // and causes wrong nullability analysis.
    internal sealed class Debug
    {
        /// <summary>Checks for a condition; if the condition is <c>false</c>, displays a message box that shows the call stack.</summary>
        /// <param name="condition">
        ///     The conditional expression to evaluate. If the condition is <c>true</c>, a failure message is
        ///     not sent and the message box is not displayed.
        /// </param>
        [Conditional("DEBUG")]
        public static void Assert([DoesNotReturnIf(false)] bool condition)
        {
            System.Diagnostics.Debug.Assert(condition);
        }

        /// <summary>
        ///     Checks for a condition; if the condition is <c>false</c>, outputs a specified message and displays a message
        ///     box that shows the call stack.
        /// </summary>
        /// <param name="condition">
        ///     The conditional expression to evaluate. If the condition is <c>true</c>, the specified message
        ///     is not sent and the message box is not displayed.
        /// </param>
        /// <param name="message">The message to send to the <see cref="System.Diagnostics.Trace.Listeners"></see> collection.</param>
        [Conditional("DEBUG")]
        public static void Assert([DoesNotReturnIf(false)] bool condition, string message)
        {
            System.Diagnostics.Debug.Assert(condition, message);
        }
    }
}