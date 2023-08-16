using System;
using KnightsAndDragons.Infrastructure.DTOs.Export;
using KnightsAndDragons.Infrastructure.Data;
using Newtonsoft.Json;
using KnightsAndDragons.Core.Contracts;
using KnightsAndDragons.Core.Models;

namespace KnightsAndDragons.Infrastructure.Serializers
{
    public class JsonSerializer : IJsonSerializer
    {
        private KnightsAndDragonsDbContext _context;

        public JsonSerializer(KnightsAndDragonsDbContext context)
        {
            this._context = context;
        }

        public void Serialize()
        {
            //Get all Users and export them to JSON.
            ExportUsers();

            //Get all Knights and export them to JSON.
            ExportKnights();

            //Get all Dragons and export them to JSON.
            ExportDragons();
        }

        private void ExportDragons()
        {
            var dragons = _context.Dragons
                              .Select(k => new ExportDragonDto()
                              {
                                  UserId = k.UserId,
                                  Name = k.Name,
                                  AttackPower = k.AttackPower,
                                  Health = k.Health,
                                  Level = k.Level,
                                  Experience = k.Experience,
                                  Gold = k.Gold
                              })
                              .ToList();

            string dragonsJson = JsonConvert.SerializeObject(dragons, Formatting.Indented);

            StreamWriter writer = new StreamWriter(@"/Users/danielnikolov/Desktop/KnightsAndDragons/Dragons.json");

            writer.WriteLine(dragonsJson);

            writer.Close();
        }

        private void ExportKnights()
        {
            var knights = _context.Knights
                              .Select(k => new ExportKnightDto()
                              {
                                  UserId = k.UserId,
                                  Name = k.Name,
                                  AttackPower = k.AttackPower,
                                  Health = k.Health,
                                  Mana = k.Mana,
                                  Level = k.Level,
                                  Experience = k.Experience,
                                  Gold = k.Gold
                              })
            .ToList();

            string knightsJson = JsonConvert.SerializeObject(knights, Formatting.Indented);

            StreamWriter writer = new StreamWriter(@"/Users/danielnikolov/Desktop/KnightsAndDragons/Knights.json");

            writer.WriteLine(knightsJson);

            writer.Close();
        }

        private void ExportUsers()
        {
            var users = _context.Users
                             .Select(u => new ExportUserDto()
                             {
                                 Username = u.Username,
                                 Password = u.Password
                             })
                             .ToList();

            string usersJson = JsonConvert.SerializeObject(users, Formatting.Indented);

            StreamWriter writer = new StreamWriter("/Users/danielnikolov/Desktop/KnightsAndDragons/Users.json");

            writer.WriteLine(usersJson);

            writer.Close();
        }
    }
}

