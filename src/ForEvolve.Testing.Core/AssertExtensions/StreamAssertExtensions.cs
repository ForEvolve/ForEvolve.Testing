using Xunit;

namespace System.IO
{
    public static class StreamAssertExtensions
    {
        /// <summary>
        /// Assert that the content of the stream is equal to the expectedResult.
        /// </summary>
        /// <param name="stream">The stream to be read.</param>
        /// <param name="expectedResult">The expected result to compare the content of the stream to.</param>
        public static void ShouldEqual(this Stream stream, string expectedResult)
        {
            stream.Seek(0, SeekOrigin.Begin);
            using (var reader = new StreamReader(stream))
            {
                var result = reader.ReadToEnd();
                Assert.Equal(expectedResult, result);
            }
        }
    }
}