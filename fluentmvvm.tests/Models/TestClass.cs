// ReSharper disable MemberCanBeInternal
namespace FluentMvvm.Tests.Models
{
    public sealed class TestClass
    {
        public int A { get; set; }
        public int? B { get; set; }
        public TestClass C { get; set; }
        public TestStruct D { get; set; }
        public string E { get; set; }

        internal int Internal { get; set; }
        public int GetOnly { get; }
    }
}