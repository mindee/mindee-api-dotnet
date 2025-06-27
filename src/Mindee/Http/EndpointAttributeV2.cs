using System;

namespace Mindee.Http
{
    /// <summary>
    /// Is used to parameterize the associated endpoint on a model.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class EndpointAttributeV2 : Attribute
    {
        private readonly string _modelName;
        private readonly string _modelVersion;

        /// <summary>
        ///
        /// </summary>
        /// <param name="modelName">The name of the target model.</param>
        /// <param name="modelVersion">The version number of the model. Without the v (for example for the v1.2: 1.2).</param>
        public EndpointAttributeV2(
            string modelName
            , string modelVersion)
        {
            _modelName = modelName;
            _modelVersion = modelVersion;
        }

        /// <summary>
        /// The name of the product associated to the expected model.
        /// </summary>
        public virtual string ModelName
        {
            get { return _modelName; }
        }

        /// <summary>
        /// The version number of the API. Without the v (for example for the v1.2: 1.2).
        /// </summary>
        public virtual string ModelVersion
        {
            get { return _modelVersion; }
        }
    }
}
