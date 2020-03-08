using System;
using FluentMvvm.Emit;

namespace FluentMvvm.Tests.TestData
{
    internal sealed class TestViewModel : FluentViewModelBase
    {
        public TestViewModel(IBackingFieldProvider backingFieldProvider) : base(backingFieldProvider) { }

        public bool Bool
        {
            get => this.GetBool();
            set => this.Set(value);
        }

        public byte Byte
        {
            get => this.GetByte();
            set => this.Set(value);
        }

        public sbyte SByte
        {
            get => this.GetSByte();
            set => this.Set(value);
        }

        public char Char
        {
            get => this.GetChar();
            set => this.Set(value);
        }

        public decimal Decimal
        {
            get => this.GetDecimal();
            set => this.Set(value);
        }

        public double Double
        {
            get => this.GetDouble();
            set => this.Set(value);
        }

        public float Float
        {
            get => this.GetFloat();
            set => this.Set(value);
        }

        public short Short
        {
            get => this.GetInt16();
            set => this.Set(value);
        }

        public ushort UShort
        {
            get => this.GetUInt16();
            set => this.Set(value);
        }

        public int Int
        {
            get => this.GetInt32();
            set => this.Set(value);
        }

        public uint UInt
        {
            get => this.GetUInt32();
            set => this.Set(value);
        }

        public long Long
        {
            get => this.GetInt64();
            set => this.Set(value);
        }

        public ulong ULong
        {
            get => this.GetUInt64();
            set => this.Set(value);
        }

        public string String
        {
            get => this.GetString();
            set => this.Set(value);
        }

        public DateTime DateTime
        {
            get => this.GetDateTime();
            set => this.Set(value);
        }

        public TestClass TestClass
        {
            get => this.Get<TestClass>();
            set => this.Set(value);
        }

        public TestStruct TestStruct
        {
            get => this.Get<TestStruct>();
            set => this.Set(value);
        }

        public TestEnum TestEnum
        {
            get => this.Get<TestEnum>();
            set => this.Set(value);
        }
    }
}