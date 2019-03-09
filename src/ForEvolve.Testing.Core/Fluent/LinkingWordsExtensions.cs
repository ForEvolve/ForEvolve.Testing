using ForEvolve.Testing.Fluent;

namespace System
{
    public static class LinkingWordsExtensions
    {
        public static TAssertion That<TAssertion>(this TAssertion previous)
            where TAssertion : BaseAssertion => previous;

        public static TAssertion And<TAssertion>(this TAssertion previous) 
            where TAssertion : BaseAssertion => previous;

        public static TAssertion Is<TAssertion>(this TAssertion previous)
            where TAssertion : BaseAssertion => previous;

        public static TAssertion Are<TAssertion>(this TAssertion previous)
            where TAssertion : BaseAssertion => previous;

        public static TAssertion A<TAssertion>(this TAssertion previous)
            where TAssertion : BaseAssertion => previous;

        public static TAssertion An<TAssertion>(this TAssertion previous)
            where TAssertion : BaseAssertion => previous;

        public static TAssertion Be<TAssertion>(this TAssertion previous)
            where TAssertion : BaseAssertion => previous;
    }
}
