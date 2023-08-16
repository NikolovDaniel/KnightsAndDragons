using System;
using KnightsAndDragons.Core.Contracts;
using KnightsAndDragons.Core.Models;
using KnightsAndDragons.Infrastructure.Data;
using Newtonsoft.Json;

namespace KnightsAndDragons.Infrastructure.Serializers
{
    public class JsonDeserializer : IJsonDeserializer
    {
        private KnightsAndDragonsDbContext _context;

        public JsonDeserializer(KnightsAndDragonsDbContext context)
        {
            this._context = context;
        }

        public void Deserialize()
        {
            //Read and import all the Users to the Database.
            ImportUsers();

            //Read and import all the Dragons to the Database.
            ImportDragons();

            //Read and import all the Knights to the Database.
            ImportKnights();
        }

        // All private methods reads a JSON file and import the data corresponding to its type.
        private void ImportKnights()
        {
            string knightReader = File.ReadAllText(@"/Users/danielnikolov/Desktop/KnightsAndDragons/Knights.json");

            var knights = JsonConvert.DeserializeObject<Knight[]>(knightReader);

            _context.Knights.AddRange(knights!);
            _context.SaveChanges();
        }

        private void ImportDragons()
        {
            string dragonsReader = File.ReadAllText(@"/Users/danielnikolov/Desktop/KnightsAndDragons/Dragons.json");

            var dragons = JsonConvert.DeserializeObject<Dragon[]>(dragonsReader);

            _context.Dragons.AddRangeAsync(dragons!);
            _context.SaveChanges();
        }

        private void ImportUsers()
        {
            string usersReader = File.ReadAllText(@"/Users/danielnikolov/Desktop/KnightsAndDragons/Users.json");

            var users = JsonConvert.DeserializeObject<User[]>(usersReader);

            _context.Users.AddRangeAsync(users!);
            _context.SaveChanges();
        }
    }
}

