using System.ComponentModel.DataAnnotations;

namespace WebKakeibo.Models
{
    public class PaymentType
    {
        public int PaymentTypeId { get; set; } //Navigationプロパティ構築のためには、クラス名＋Id
        [Required]
        [Display(Name = "支払方法名")]  
        [StringLength(50)]
        public string TypeName { get; set; } = string.Empty;  // 例：現金、クレジットカード、電子マネーなど


    }
}
