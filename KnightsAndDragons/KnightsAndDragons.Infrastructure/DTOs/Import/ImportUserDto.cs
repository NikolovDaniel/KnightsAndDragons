using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace KnightsAndDragons.Infrastructure.DTOs.Import
{
    public class ImportUserDto
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Username")]
        public string Username { get; set; } = null!;

        [JsonProperty("Password")]
        public string Password { get; set; } = null!;
    }
}

