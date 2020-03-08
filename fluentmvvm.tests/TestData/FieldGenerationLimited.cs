namespace FluentMvvm.Tests.TestData
{
    public sealed class FieldGenerationLimited
    {
        private int PrivateWritableInstance { get; set; }
        public int PublicWritableInstance { get; set; }
        public static int PublicWritableStatic { get; set; }
        public int PublicInstance { get; }

        [SuppressFieldGeneration]
        public int PublicWritableInstanceSuppressed { get; set; }
    }
}