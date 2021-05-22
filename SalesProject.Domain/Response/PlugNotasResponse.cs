using System.Collections.Generic;

namespace SalesProject.Domain.Response
{
    public class PlugNotasResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public string Id { get; set; }
    }
}