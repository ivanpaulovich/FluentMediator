using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace FluentMediator
{
    public class PipelineCollection<P> where P : class
    {
        private IDictionary<Type, P> _pipelines;

        public PipelineCollection()
        {
            _pipelines = new Dictionary<Type, P>();
        }

        public void Add<Request>(P pipeline)
        {
            _pipelines.Add(typeof(Request), pipeline);
        }

        public bool Contains<Request>([NotNullWhen(true)] out P? pipeline)
        {
            if (!_pipelines.ContainsKey(typeof(Request)))
            {
                pipeline = default(P);
                return false;
            }

            pipeline = _pipelines[typeof(Request)];
            return true;
        }
    }
}