namespace GiveCRM.Infrastructure
{
    using System;

    public interface IHttpRequest
    {
        Uri Url { get; } 
    }
}