using System;

namespace FluentMvvm
{
    /// <summary>
    ///     Represents an unexpected error in the library that is likely caused by an internal error.
    /// </summary>
    /// <remarks>
    ///     Internal by design as it indicates a flaw in the library that cannot be fixed by the user and should not be caught
    ///     either.
    /// </remarks>
    /// <seealso cref="System.Exception" />
    internal sealed class InternalException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="InternalException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InternalException(string message) : base($"{message}{Environment.NewLine}Please help resolving this error by creating an issue using this link: https://github.com/flinkow/fluentmvvm/issues/new?title=Internal%20error:.")
        {
        }
    }
}