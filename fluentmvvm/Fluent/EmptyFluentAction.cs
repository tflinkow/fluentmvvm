using System;
using System.Windows.Input;

namespace FluentMvvm.Fluent
{
    /// <summary>
    ///     An object implementing all fluent methods as methods doing nothing.
    /// </summary>
    /// <seealso cref="FluentMvvm.Fluent.IPropertySetExpression" />
    /// <seealso cref="FluentMvvm.Fluent.IDependencyExpression" />
    /// <seealso cref="IPropertySetExpression" />
    /// <seealso cref="IDependencyExpression" />
    /// <inheritdoc cref="IPropertySetExpression" />
    /// <inheritdoc cref="IPropertyGetExpression" />
    internal sealed class EmptyFluentAction : IPropertySetExpression, IDependencyExpression
    {
        /// <summary>
        ///     Gets a singleton <see cref="EmptyFluentAction" />.
        /// </summary>
        /// <value>
        ///     A singleton instance of an <see cref="EmptyFluentAction" />.
        /// </value>
        public static EmptyFluentAction Default { get; } = new EmptyFluentAction();

        /// <inheritdoc />
        public IDependencyExpression Affects(string propertyName)
        {
            return this;
        }

        /// <inheritdoc />
        public IDependencyExpression Affects(ICommand command)
        {
            return this;
        }

        /// <inheritdoc />
        public bool WasUpdated()
        {
            return false;
        }

        /// <inheritdoc />
        public IDependencyExpression Set<T>(T value, string propertyName = "")
        {
            return this;
        }

        /// <inheritdoc />
        public IDependencyExpression Set(bool value, string propertyName = "")
        {
            return this;
        }

        /// <inheritdoc />
        public IDependencyExpression Set(byte value, string propertyName = "")
        {
            return this;
        }

        /// <inheritdoc />
        public IDependencyExpression Set(sbyte value, string propertyName = "")
        {
            return this;
        }

        /// <inheritdoc />
        public IDependencyExpression Set(char value, string propertyName = "")
        {
            return this;
        }

        /// <inheritdoc />
        public IDependencyExpression Set(decimal value, string propertyName = "")
        {
            return this;
        }

        /// <inheritdoc />
        public IDependencyExpression Set(double value, string propertyName = "")
        {
            return this;
        }

        /// <inheritdoc />
        public IDependencyExpression Set(float value, string propertyName = "")
        {
            return this;
        }

        /// <inheritdoc />
        public IDependencyExpression Set(short value, string propertyName = "")
        {
            return this;
        }

        /// <inheritdoc />
        public IDependencyExpression Set(ushort value, string propertyName = "")
        {
            return this;
        }

        /// <inheritdoc />
        public IDependencyExpression Set(int value, string propertyName = "")
        {
            return this;
        }

        /// <inheritdoc />
        public IDependencyExpression Set(uint value, string propertyName = "")
        {
            return this;
        }

        /// <inheritdoc />
        public IDependencyExpression Set(long value, string propertyName = "")
        {
            return this;
        }

        /// <inheritdoc />
        public IDependencyExpression Set(ulong value, string propertyName = "")
        {
            return this;
        }

        /// <inheritdoc />
        public IDependencyExpression Set(string value, string propertyName = "")
        {
            return this;
        }

        /// <inheritdoc />
        public IDependencyExpression Set(DateTime value, string propertyName = "")
        {
            return this;
        }
    }
}