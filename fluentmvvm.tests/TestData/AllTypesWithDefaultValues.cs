using System;
using System.ComponentModel;

namespace FluentMvvm.Tests.TestData
{
    // NOTE: When changing the default values also change the test!
    public class AllTypesWithDefaultValues
    {
        [DefaultValue(true)]
        public bool Bool { get; set; }

        [DefaultValue(120)]
        public byte Byte { get; set; }

        [DefaultValue(-123)]
        public sbyte SByte { get; set; }

        [DefaultValue('?')]
        public char Char { get; set; }

        public decimal Decimal { get; set; }

        [DefaultValue(123.45)]
        public double Double { get; set; }

        [DefaultValue(321.54)]
        public float Float { get; set; }

        [DefaultValue(-9000)]
        public short Short { get; set; }

        [DefaultValue(10000)]
        public ushort UShort { get; set; }

        [DefaultValue(-123456789)]
        public int Int { get; set; }

        [DefaultValue(987654321)]
        public uint UInt { get; set; }

        [DefaultValue(-1000000000)]
        public long Long { get; set; }

        [DefaultValue(2000000000)]
        public ulong ULong { get; set; }

        [DefaultValue("Hello World")]
        public string String { get; set; }

        public DateTime DateTime { get; set; }

        public TestClass TestClass { get; set; }
        public TestStruct TestStruct { get; set; }

        [DefaultValue(TestData.TestEnum.Red)]
        public TestEnum TestEnum { get; set; }
    }
}