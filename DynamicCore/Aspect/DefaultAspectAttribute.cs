using System;
using System.Collections.Generic;
using System.Text;

namespace Umi.Dynamic.Core.Aspect
{
    /// <summary>
    /// 默认的
    /// </summary>
    public sealed class DefaultAspectAttribute : AspectAttributeBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="metadata"></param>
        public override object Interceptor(AspectMetadata metadata)
        {
            metadata.Processed();
            return metadata.Return;
        }
    }
}
