using System;
using System.Threading.Tasks;
using MiddlewareConvention1;
using MiddlewareConvention2;
using Owin;

namespace InteropTestApp
{
    public static class AppBuilderExtensions
    {
        public static TApp Build<TApp>(
            this IAppBuilder builder,
            Action<IAppBuilder> configuration)
        {
            return (TApp)builder.Build(
                typeof(TApp),
                nested => configuration((IAppBuilder)nested));
        }

        public static void Run(
            this IAppBuilder builder,
            object app)
        {
            builder.Use(new Func<object, object>(_ => app));
        }
    }

    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            builder.UseAlpha("one", "two");

            builder.Use(Beta.Middleware("three", "four"));

            builder.Use((AppDelegate)Main);

            builder.Properties["hello"] = "world";

            builder.AddSignatureConversion(new Func<CustomThing, AppDelegate>(Convert1));
            builder.AddSignatureConversion(new Func<AppDelegate, CustomThing>(Convert2));

            var thing = builder.Build<CustomThing>(
                x => x.UseAlpha("five", "six"));
        }

        public Task<ResultParameters> Main(CallParameters call)
        {
            throw new NotImplementedException();
        }

        public AppDelegate Convert1(CustomThing thing)
        {
            return call =>
            {
                thing.Invoke("called");
                return null;
            };
        }

        public CustomThing Convert2(AppDelegate app)
        {
            var thing = new CustomThing();
            return thing;
        }

        public class CustomThing
        {
            public string Invoke(string request)
            {
                return "response";
            }
        }
    }
}
