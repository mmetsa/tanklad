namespace DTO.App
{
    public class JwtResponse
    {
        public string Token { get; set; } = default!;
        public string Firstname { get; set; } = default!;
        public string Lastname { get; set; } = default!;
    }
}