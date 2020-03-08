using System;
using System.Collections.Generic;
using FluentMvvm.Tests.TestData;

namespace FluentMvvm.Tests.TestDataSources
{
    public class TestDataSource
    {
        // TODO: decimal does not seem to work correctly, see https://github.com/xunit/xunit/issues/1771
        // downgrading xunit to 2.3.1 seems to work

        public static IEnumerable<object[]> DefaultValues
        {
            get
            {
                yield return new object[] { default(decimal) };
                yield return new object[] { default(DateTime) };
                yield return new object[] { default(TestStruct) };
            }
        }

        public static IEnumerable<object[]> DefaultValuesWithNames
        {
            get
            {
                yield return new object[] { default(decimal), nameof(AllTypes.Decimal) };
                yield return new object[] { default(DateTime), nameof(AllTypes.DateTime) };
                yield return new object[] { default(TestStruct), nameof(AllTypes.TestStruct) };
            }
        }

        public static IEnumerable<object[]> DifferentValuesWithNames
        {
            get
            {
                yield return new object[] { TestValues.Decimal, nameof(AllTypes.Decimal) };
                yield return new object[] { TestValues.DateTime, nameof(AllTypes.DateTime) };
                yield return new object[] { TestValues.TestClass, nameof(AllTypes.TestClass) };
                yield return new object[] { TestValues.TestStruct, nameof(AllTypes.TestStruct) };
            }
        }

        public static IEnumerable<object[]> DefaultValuesWithDifferentValuesWithNames
        {
            get
            {
                yield return new object[] { default(decimal), TestValues.Decimal, nameof(AllTypes.Decimal) };
                yield return new object[] { default(DateTime), TestValues.DateTime, nameof(AllTypes.DateTime) };
                yield return new object[] { default(TestClass), TestValues.TestClass, nameof(AllTypes.TestClass) };
                yield return new object[] { default(TestStruct), TestValues.TestStruct, nameof(AllTypes.TestStruct) };
            }
        }

        public static IEnumerable<object[]> CommandsWithRaiseCanExecuteChanged
        {
            get
            {
                yield return new object[] { new TestICommand() };
                yield return new object[] { new TestIWpfCommand() };
            }
        }
    }
}