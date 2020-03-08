using System;
using System.Runtime.CompilerServices;
using FluentMvvm.Fluent;

namespace FluentMvvm
{
    public abstract partial class FluentViewModelBase
    {
        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IDependencyExpression Set<T>(T value, [CallerMemberName] string propertyName = "")
        {
            return this.DetermineReturnValue(this.backingFields.Set(value, propertyName), propertyName);
        }

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IDependencyExpression Set(bool value, [CallerMemberName] string propertyName = "")
        {
            return this.DetermineReturnValue(this.backingFields.Set(value, propertyName), propertyName);
        }

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IDependencyExpression Set(byte value, [CallerMemberName] string propertyName = "")
        {
            return this.DetermineReturnValue(this.backingFields.Set(value, propertyName), propertyName);
        }

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IDependencyExpression Set(sbyte value, [CallerMemberName] string propertyName = "")
        {
            return this.DetermineReturnValue(this.backingFields.Set(value, propertyName), propertyName);
        }

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IDependencyExpression Set(char value, [CallerMemberName] string propertyName = "")
        {
            return this.DetermineReturnValue(this.backingFields.Set(value, propertyName), propertyName);
        }

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IDependencyExpression Set(decimal value, [CallerMemberName] string propertyName = "")
        {
            return this.DetermineReturnValue(this.backingFields.Set(value, propertyName), propertyName);
        }

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IDependencyExpression Set(double value, [CallerMemberName] string propertyName = "")
        {
            return this.DetermineReturnValue(this.backingFields.Set(value, propertyName), propertyName);
        }

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IDependencyExpression Set(float value, [CallerMemberName] string propertyName = "")
        {
            return this.DetermineReturnValue(this.backingFields.Set(value, propertyName), propertyName);
        }

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IDependencyExpression Set(short value, [CallerMemberName] string propertyName = "")
        {
            return this.DetermineReturnValue(this.backingFields.Set(value, propertyName), propertyName);
        }

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IDependencyExpression Set(ushort value, [CallerMemberName] string propertyName = "")
        {
            return this.DetermineReturnValue(this.backingFields.Set(value, propertyName), propertyName);
        }

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IDependencyExpression Set(int value, [CallerMemberName] string propertyName = "")
        {
            return this.DetermineReturnValue(this.backingFields.Set(value, propertyName), propertyName);
        }

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IDependencyExpression Set(uint value, [CallerMemberName] string propertyName = "")
        {
            return this.DetermineReturnValue(this.backingFields.Set(value, propertyName), propertyName);
        }

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IDependencyExpression Set(long value, [CallerMemberName] string propertyName = "")
        {
            return this.DetermineReturnValue(this.backingFields.Set(value, propertyName), propertyName);
        }

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IDependencyExpression Set(ulong value, [CallerMemberName] string propertyName = "")
        {
            return this.DetermineReturnValue(this.backingFields.Set(value, propertyName), propertyName);
        }

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IDependencyExpression Set(string value, [CallerMemberName] string propertyName = "")
        {
            return this.DetermineReturnValue(this.backingFields.Set(value, propertyName), propertyName);
        }

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IDependencyExpression Set(DateTime value, [CallerMemberName] string propertyName = "")
        {
            return this.DetermineReturnValue(this.backingFields.Set(value, propertyName), propertyName);
        }

        /// <summary>
        ///     Returns the correct <see cref="IDependencyExpression" /> implementation; either this instance, if
        ///     <paramref name="returnValue" /> is <c>true</c>, or a <see cref="EmptyFluentAction" /> otherwise.
        /// </summary>
        /// <param name="returnValue">Indicates whether the call to <c>Set</c> or its overloads returned <c>true</c>.</param>
        /// <param name="propertyName">The name of the property that was set.</param>
        /// <returns>A <see cref="IDependencyExpression" /> to continue the fluent call chain with.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IDependencyExpression DetermineReturnValue(bool returnValue, string propertyName)
        {
            if (returnValue)
            {
                this.RaisePropertyChanged(propertyName);
                this.AfterSet();
                return this;
            }

            return EmptyFluentAction.Default;
        }
    }
}