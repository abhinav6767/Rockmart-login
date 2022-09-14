using System.ComponentModel.DataAnnotations;

namespace Rockmart_login.Security_Model
{
    public class UserRefreshTokens
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string RefreshToken { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
