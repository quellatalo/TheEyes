using System;

namespace Qellatalo.Nin.TheEyes
{
    /// <summary>
    /// The exception that is thrown when an ineffective Pattern is processed on an image scan.
    /// </summary>
    public class InvalidPatternException : ArgumentException
    {
        /// <summary>
        /// Initalizes a default InvalidPatternException.
        /// </summary>
        public InvalidPatternException() : base() { }

        /// <summary>
        /// Initalizes an InvalidPatternException with a custom message.
        /// </summary>
        /// <param name="message">The error message embedded in the exception.</param>
        public InvalidPatternException(string message) : base(message) { }

        /// <summary>
        /// Initalizes an InvalidPatternException with a custom message, and an inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message embedded in the exception.</param>
        /// <param name="innerException">The inner exception that is the cause of this exception.</param>
        public InvalidPatternException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Initalizes an InvalidPatternException with serialized data.
        /// </summary>
        /// <param name="info">An object that holds the serialized data.</param>
        /// <param name="context">An object that describes the source or destination of the serialized data.</param>
        public InvalidPatternException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
