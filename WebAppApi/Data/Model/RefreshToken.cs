using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    [Table("RefreshToken")]
   public class RefreshToken
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserID { get; set; }
        [ForeignKey(nameof(UserID))]
        public User User { get; set; }

        public string Token { get; set; }
        public string JwtId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime ExpireTime { get; set; }

    }
}
