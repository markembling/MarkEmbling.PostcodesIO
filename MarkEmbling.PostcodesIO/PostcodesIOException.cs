using System;

namespace MarkEmbling.PostcodesIO {
    public class PostcodesIOException : Exception {
        public PostcodesIOException(Exception innerException)
            : base("Error retrieving response. Please check inner exception for details.", innerException) { }
    }
}