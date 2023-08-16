using KnightsAndDragons.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace KnightsAndDragons.Core.DTOs
{
    public class ExportUserDto
    {
        public int Id { get; set; }

        public string Username { get; set; } = null!;

        public string? Token { get; set; } = null!;

        public ICollection<Dragon> Dragons { get; set; } = null!;

        public ICollection<Knight> Knights { get; set; } = null!;
    }
}

