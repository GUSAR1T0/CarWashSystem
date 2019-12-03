using System;

namespace VXDesign.Store.CarWashSystem.Server.Core.Common
{
    public class ErrorResult
    {
        public string? Source { get; }
        public string? Target { get; }
        public string Message { get; }
        public string? StackTrace { get; }

        public ErrorResult(Exception exception)
        {
            Source = exception.Source;
            Target = exception.TargetSite?.Name;
            Message = exception.Message;
            StackTrace = exception.StackTrace;
        }
    }
}