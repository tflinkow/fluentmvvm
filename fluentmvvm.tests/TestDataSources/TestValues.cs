using System;
using FluentMvvm.Tests.TestData;

namespace FluentMvvm.Tests.TestDataSources
{
    public class TestValues
    {
        public const bool Bool = true;
        public const byte Byte = 41;
        public const sbyte SByte = 42;
        public const char Char = 'B';
        public const decimal Decimal = 45.5m;
        public const double Double = 46.6d;
        public const float Float = 47.7f;
        public const short Short = 48;
        public const ushort UShort = 49;
        public const int Int = 50;
        public const uint UInt = 51;
        public const long Long = 52;
        public const ulong ULong = 53;
        public const string String = "Foo";
        public static readonly DateTime DateTime = DateTime.ParseExact("24.12.2019", "dd.MM.yyyy", null);
        public static readonly TestClass TestClass = new TestClass { Id = 42 };
        public static readonly TestStruct TestStruct = new TestStruct { Id = 42 };
        public const TestEnum TestEnum = TestData.TestEnum.Green;
    }
}