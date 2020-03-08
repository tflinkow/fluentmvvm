using System;

namespace FluentMvvm.Tests.TestData
{
    public class AllTypes
    {
        public bool Bool { get; set; }
        public byte Byte { get; set; }
        public sbyte SByte { get; set; }
        public char Char { get; set; }
        public decimal Decimal { get; set; }
        public double Double { get; set; }
        public float Float { get; set; }
        public short Short { get; set; }
        public ushort UShort { get; set; }
        public int Int { get; set; }
        public uint UInt { get; set; }
        public long Long { get; set; }
        public ulong ULong { get; set; }
        public string String { get; set; }
        public DateTime DateTime { get; set; }

        public TestClass TestClass { get; set; }
        public TestStruct TestStruct { get; set; }
        public TestEnum TestEnum { get; set; }
    }
}