using System.Net;

namespace Gradebook.Domain.Exceptions
{
    public abstract class GradebookException : Exception
    {
        public abstract HttpStatusCode StatusCode { get; }
        public GradebookException(string message) : base(message)
        { }
    }
}
