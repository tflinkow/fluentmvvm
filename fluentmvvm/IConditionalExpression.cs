using System;
using System.Diagnostics.Contracts;

namespace FluentMvvm
{
    /// <summary>
    ///     Provides methods that determine whether or not the following fluent calls should execute.
    /// </summary>
    public interface IConditionalExpression
    {
        /// <summary>
        ///     If <paramref name="condition" /> is <c>false</c>, no following
        ///     <c>Set</c>, <see cref="IDependencyExpression.Affects(string)" /> or
        ///     <see cref="IDependencyExpression.Affects(System.Windows.Input.ICommand)" /> will be executed.
        /// </summary>
        /// <param name="condition">only if set to <c>true</c> will the following fluent calls be executed.</param>
        [Pure]
        IPropertySetExpression When(bool condition);

        /// <summary>
        ///     If <paramref name="condition" /> evaluates to <c>false</c>, no following
        ///     <c>Set</c>, <see cref="IDependencyExpression.Affects(string)" /> or
        ///     <see cref="IDependencyExpression.Affects(System.Windows.Input.ICommand)" /> will be executed.
        /// </summary>
        /// <param name="condition">only if evaluates to <c>true</c> will the following fluent calls be executed.</param>
        /// <exception cref="ArgumentNullException"><paramref name="condition" /> is <c>null</c>.</exception>
        [Pure]
        IPropertySetExpression When(Func<bool> condition);
    }
}