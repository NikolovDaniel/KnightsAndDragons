using System;
using KnightsAndDragons.Core.Contracts;

namespace KnightsAndDragons.Core.Services
{
    public class JsonDeserializerServices : IJsonDeserializerServices
    {
        private IJsonDeserializer _deserializer;

        public JsonDeserializerServices(IJsonDeserializer deserializer)
        {
            this._deserializer = deserializer;
        }

        public void Deserialize()
        {
            this._deserializer.Deserialize();
        }
    }
}

