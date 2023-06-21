namespace ConsoleClient
{
    public class LoginModel
    {
        public string Account { get; set; }

        public string Password { get; set; }
    }

    public class LoginResult
    {
        public bool Success { get; set; }

        public string Token { get; set; }
    }
}
