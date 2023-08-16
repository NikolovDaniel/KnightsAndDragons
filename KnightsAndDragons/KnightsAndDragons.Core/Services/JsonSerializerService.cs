using System;
using KnightsAndDragons.Core.Contracts;

namespace KnightsAndDragons.Core.Services
{
    public class JsonSerializerService : IJsonSerializerServices
    {
        private IJsonSerializer _serializer;

        public JsonSerializerService(IJsonSerializer serializer)
        {
            this._serializer = serializer;
        }

        public void Serialize()
        {
            this._serializer.Serialize();
        }
    }
}

