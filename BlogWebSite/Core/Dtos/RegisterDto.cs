namespace Core.Dtos
{
    public class RegisterDto
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string password { get; set; }
        public Guid? RoleId { get; set; }
    }
}
