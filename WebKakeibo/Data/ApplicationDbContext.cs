using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebKakeibo.Models;
namespace WebKakeibo.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<WebKakeibo.Models.MonthlyBudget> MonthlyBudget { get; set; } = default!;
        public DbSet<WebKakeibo.Models.Payment> Payment { get; set; } = default!;
        public DbSet<WebKakeibo.Models.SubjectName> SubjectName { get; set; } = default!;
        public DbSet<WebKakeibo.Models.PaymentType> PaymentType { get; set; } = default!;
        //科目ImageDB登録用メソッド
        public void SeedImagesFromWwwroot()
        {
            try
            {
                // wwwroot/Images の絶対パスを取得
                var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
                if (!Directory.Exists(imagesPath))
                {
                    Console.WriteLine($"[ERROR] 画像フォルダが存在しません: {imagesPath}");
                    return;
                }
                // PNGファイル一覧を取得
                var pngFiles = Directory.GetFiles(imagesPath, "*.png", SearchOption.TopDirectoryOnly);
                foreach (var filePath in pngFiles)
                {
                    var fileName = Path.GetFileName(filePath);
                    // DBに保存する相対パス（例: "images/sample.png"）
                    var relativePath = Path.Combine("Images", fileName).Replace("\\", "/");
                    // 重複チェック
                    if (!SubjectName.Any(s => s.ImageUrl == relativePath))
                    {
                        var subject = new SubjectName
                        {
                            Name = Path.GetFileNameWithoutExtension(fileName), // ファイル名をName列に設定（拡張子なし）
                            ImageUrl = relativePath
                        };
                        SubjectName.Add(subject);
                    }
                }
                SaveChanges();
                Console.WriteLine("[INFO] 画像登録が完了しました。");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] 登録処理中にエラー: {ex.Message}");
            }
        }

    }
}
