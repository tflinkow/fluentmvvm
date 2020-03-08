using System;
using System.Windows.Input;

namespace FluentMvvm.Fluent
{
    /// <summary>
    ///     Provides methods that determine whether or not the following fluent calls should execute.
    /// </summary>
    public interface IConditionalExpression
    {
        /// <summary>
        ///     If <paramref name="condition" /> is <c>false</c>, no following
        ///     <see cref="IPropertySetExpression.Set{T}" />, <see cref="IDependencyExpression.Affects(string)" /> or
        ///     <see cref="IDependencyExpression.Affects(ICommand)" /> will be executed.
        /// </summary>
        /// <param name="condition">only if set to <c>true</c> will the following fluent calls be executed.</param>
        /// <returns></returns>
        IPropertySetExpression When(bool condition);

        /// <summary>
        ///     If <paramref name="condition" /> evaluates to <c>false</c>, no following
        ///     <see cref="IPropertySetExpression.Set{T}" />, <see cref="IDependencyExpression.Affects(string)" /> or
        ///     <see cref="IDependencyExpression.Affects(ICommand)" /> will be executed.
        /// </summary>
        /// <param name="condition">only if evaluates to <c>true</c> will the following fluent calls be executed.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"><paramref name="condition" /> is <c>null</c>.</exception>
        IPropertySetExpression When(Func<bool> condition);
    }
}