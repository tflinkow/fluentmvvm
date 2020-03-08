using System;
using System.Runtime.CompilerServices;

namespace FluentMvvm
{
    public abstract partial class FluentViewModelBase
    {
        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T Get<T>([CallerMemberName] string propertyName = "")
        {
            return this.backingFields.Get<T>(propertyName);
        }

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool GetBool([CallerMemberName] string propertyName = "")
        {
            return this.backingFields.GetBool(propertyName);
        }

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public byte GetByte([CallerMemberName] string propertyName = "")
        {
            return this.backingFields.GetByte(propertyName);
        }

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public sbyte GetSByte([CallerMemberName] string propertyName = "")
        {
            return this.backingFields.GetSByte(propertyName);
        }

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public char GetChar([CallerMemberName] string propertyName = "")
        {
            return this.backingFields.GetChar(propertyName);
        }

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public decimal GetDecimal([CallerMemberName] string propertyName = "")
        {
            return this.backingFields.GetDecimal(propertyName);
        }

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double GetDouble([CallerMemberName] string propertyName = "")
        {
            return this.backingFields.GetDouble(propertyName);
        }

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetFloat([CallerMemberName] string propertyName = "")
        {
            return this.backingFields.GetFloat(propertyName);
        }

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public short GetInt16([CallerMemberName] string propertyName = "")
        {
            return this.backingFields.GetInt16(propertyName);
        }

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ushort GetUInt16([CallerMemberName] string propertyName = "")
        {
            return this.backingFields.GetUInt16(propertyName);
        }

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetInt32([CallerMemberName] string propertyName = "")
        {
            return this.backingFields.GetInt32(propertyName);
        }

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetUInt32([CallerMemberName] string propertyName = "")
        {
            return this.backingFields.GetUInt32(propertyName);
        }

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public long GetInt64([CallerMemberName] string propertyName = "")
        {
            return this.backingFields.GetInt64(propertyName);
        }

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetUInt64([CallerMemberName] string propertyName = "")
        {
            return this.backingFields.GetUInt64(propertyName);
        }

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string GetString([CallerMemberName] string propertyName = "")
        {
            return this.backingFields.GetString(propertyName);
        }

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DateTime GetDateTime([CallerMemberName] string propertyName = "")
        {
            return this.backingFields.GetDateTime(propertyName);
        }
    }
}