namespace EcCoach.Web.ViewModels
{
    public class CurrentUser
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }

        public int OwnerId { get; set; }
    }
}
