using System;
using KnightsAndDragons.Core.Contracts;

namespace KnightsAndDragons.UI.Controllers
{
    public class SerializerController
    {
        private IJsonSerializerServices _serializer;
        private IJsonDeserializerServices _deserializer;

        public SerializerController(IJsonSerializerServices serializer, IJsonDeserializerServices deserializer)
        {
            this._serializer = serializer;
            this._deserializer = deserializer;
        }

        public void Serialize()
        {
            this._serializer.Serialize();
        }

        public void Deserialize()
        {
            this._deserializer.Deserialize();
        }
    }
}

