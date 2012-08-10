using System;
using System.Threading.Tasks;
using MiddlewareConvention1;
using MiddlewareConvention2;
using Owin;

namespace StartupConvention1
{
    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            builder.UseAlpha("one", "two");

            builder.Use(Beta.Middleware("three", "four"));

            builder.Use((AppDelegate)Main);
        }

        public Task<ResultParameters> Main(CallParameters call)
        {
            throw new NotImplementedException();
        }
    }
}