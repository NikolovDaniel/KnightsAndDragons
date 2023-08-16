using System;
using KnightsAndDragons.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace KnightsAndDragons.Infrastructure.DTOs.Export
{
    public class ExportKnightDto
    {
        [JsonProperty("UserId")]
        public int UserId { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; } = null!;

        [JsonProperty("AttackPower")]
        public int AttackPower { get; set; }

        [JsonProperty("Health")]
        public int Health { get; set; }

        [JsonProperty("Mana")]
        public int Mana { get; set; }

        [JsonProperty("Level")]
        public int Level { get; set; }

        [JsonProperty("Experience")]
        public int Experience { get; set; }

        [JsonProperty("Gold")]
        public int Gold { get; set; }
    }
}

