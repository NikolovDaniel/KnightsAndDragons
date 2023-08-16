namespace KnightsAndDragons.Core.DTOs.ImportDtos
{
    public class ImportUserDto
    {
        public ImportUserDto(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public string Username { get; set; } = null!; 
        public string Password { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}

