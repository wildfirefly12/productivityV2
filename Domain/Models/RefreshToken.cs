using Domain.Models;

namespace Domain.Models
{
    public class RefreshToken
    {
        public RefreshToken(string token)
        {
            Token = token;
        }

        public long RefreshTokenId { get; set; }
        public User User { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; } = DateTime.UtcNow.AddDays(7);
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime? Revoked { get; set; }
        public bool IsActive => Revoked == null && !IsExpired;
        
    }
}