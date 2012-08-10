using System;
using System.Collections.Generic;
using Owin;


namespace AlternateBuilder
{
    public class TestBuilder : IAppBuilder
    {
        readonly IDictionary<string, object> _properties = new Dictionary<string, object>();

        public IDictionary<string, object> Properties
        {
            get { return _properties; }
        }

        public IAppBuilder Use(object middleware, params object[] args)
        {
            return this;
        }

        public object Build(Type signature, Action<object> configuration)
        {
            configuration(this);
            return null;
        }

        public IAppBuilder AddSignatureConversion(Delegate conversion)
        {
            return this;
        }
    }
}

