
using Microsoft.EntityFrameworkCore.Diagnostics;
namespace Core.Utilities.Interceptors
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptionBaseAttribute : Attribute, IInterceptor
    {
        public int Priority { get; set; }

        public virtual void Intercept(Castle.DynamicProxy.IInvocation invocation)
        {

        }
    }
}
