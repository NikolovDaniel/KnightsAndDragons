using KnightsAndDragons.Core.Contracts;
using System.ComponentModel.DataAnnotations;

namespace KnightsAndDragons.Core.Models
{
    public class User : IUser
    {
        public User()
        {
            this.Dragons = new HashSet<Dragon>();
            this.Knights = new HashSet<Knight>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = null!;

        [Required]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$")]
        public string Password { get; set; } = null!;

        [MaxLength(200)]
        public string? Token { get; set; } = null!;

        public ICollection<Dragon> Dragons { get; set; } = null!;

        public ICollection<Knight> Knights { get; set; } = null!;
    }
}
