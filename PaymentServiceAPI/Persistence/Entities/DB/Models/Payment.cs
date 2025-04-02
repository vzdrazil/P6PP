using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PaymentService.API.Persistence.Entities.DB.Models
{
    public class Payment : Entity
    {
        [Key]
        public ulong PaymentID { get; set; }

        [Required]
        public long UserId { get; set; }

        [Required]
        public long RoleId { get; set; }

        [Required]
        [MaxLength(20)]
        public string ReceiverBankNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string GiverBankNumber { get; set; } = string.Empty;

        [Required]
        public long Price { get; set; }

        [Required]
        public long CreditAmount { get; set; }

        [Required]
        public bool IsValid { get; set; }

        [Required]
        public string TransactionType { get; set; } = "credit"; // "credit" nebo "reservation"
        /*
        [ForeignKey("Id")]
        public UserMod? User { get; set; }
        */
    }
}
