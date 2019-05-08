using System;

namespace SpyStoreService.Tests.Base
{
    public abstract class BaseTestClass : IDisposable
    {
        protected string ServiceAddress = "http://localhost:60910/api/";
        protected string RootAddress = String.Empty;
        public virtual void Dispose()
        {
        }
    }
}
