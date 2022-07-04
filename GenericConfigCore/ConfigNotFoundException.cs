using System;
using System.Runtime.Serialization;

namespace GenericConfigCore
{
    [Serializable()]
    public class ConfigNotFoundException : Exception
    {

        public ConfigNotFoundException()
           : base()
        { }

        public ConfigNotFoundException(SerializationInfo info,
                                    StreamingContext context)
           : base(info, context)
        { }

    }
}
