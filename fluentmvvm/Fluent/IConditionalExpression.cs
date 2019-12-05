using System;
using System.Windows.Input;

using JetBrains.Annotations;

namespace FluentMvvm.Fluent
{
    /// <summary>Provides methods that determine whether or not the following fluent calls should execute.</summary>
    [PublicAPI]
    public interface IConditionalExpression
    {
        /// <summary>
        ///     If <paramref name="condition" /> is <c>false</c>, no following
        ///     <see cref="IPropertySetExpression.Set(object, string)" />, <see cref="IDependencyExpression.Affects(string)" /> or
        ///     <see cref="IDependencyExpression.Affects(ICommand)" /> will be executed.
        /// </summary>
        /// <param name="condition">only if set to <c>true</c> will the following fluent calls be executed.</param>
        [NotNull]
        IPropertySetExpression When(bool condition);

        /// <summary>
        ///     If <paramref name="condition" /> evaluates <c>false</c>, no following
        ///     <see cref="IPropertySetExpression.Set(object, string)" />, <see cref="IDependencyExpression.Affects(string)" /> or
        ///     <see cref="IDependencyExpression.Affects(ICommand)" /> will be executed.
        /// </summary>
        /// <param name="condition">only if evaluates to <c>true</c> will the following fluent calls be executed.</param>
        /// <exception cref="ArgumentNullException"><paramref name="condition" /> is <c>null</c>.</exception>
        [NotNull]
        [ContractAnnotation("null => halt")]
        IPropertySetExpression When([NotNull] Func<bool> condition);
    }
}