using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

[assembly: ImportedFromTypeLib("OWIN")]

namespace Owin
{
    [ComImport]
    [Guid("24d2798e-18c3-461a-97bf-83d5a0fd726e")]
    public interface IAppBuilder
    {
        IDictionary<string, object> Properties { get; }

        IAppBuilder Use(object middleware, params object[] args);
        object Build(Type signature, Action<object> configuration);

        IAppBuilder AddSignatureConversion(Delegate conversion);
    }

    /*
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
     */
}
