using Microsoft.AspNetCore.Identity;  
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebKakeibo.Models
{
    public class MonthlyBudget
    {
        [Key]
        public int MonthlyBudgetId { get; set; }  //Navigationプロパティ構築のためには、クラス名＋Id
        [Required]
        [Display(Name = "年号")]
        public int YearNum { get; set; } = 0;
        [Required]
        [Display(Name = "月名")]
        public int MonthNum { get; set; } = 0;
        [Required]
        [Display(Name = "予算金額")]
        public int BudgetAmount { get; set; }
        public String? UserId { get; set; }        // IdentityUser クラス（テーブル）とリレーションシップを構築
        [ForeignKey("UserId")]                      // UserId プロパティを外部キーとして指定
        public IdentityUser? User { get; set; } = null;     // Navigationプロパティ

    }
}
