using System.Windows.Input;

using JetBrains.Annotations;

namespace FluentMvvm.Fluent
{
    /// <summary>An object implementing all fluent methods as methods doing nothing.</summary>
    /// <seealso cref="FluentMvvm.Fluent.IPropertySetExpression" />
    /// <seealso cref="FluentMvvm.Fluent.IDependencyExpression" />
    /// <seealso cref="IPropertySetExpression" />
    /// <seealso cref="IDependencyExpression" />
    internal sealed class EmptyFluentAction : IPropertySetExpression, IDependencyExpression
    {
        /// <summary>Gets a singleton <see cref="EmptyFluentAction" />.</summary>
        /// <value>A singleton instance of an <see cref="EmptyFluentAction" />.</value>
        [NotNull]
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
        [ContractAnnotation("=> false")]
        public bool WasUpdated()
        {
            return false;
        }

        /// <inheritdoc />
        public IDependencyExpression Set(object value, string propertyName = null)
        {
            return this;
        }
    }
}