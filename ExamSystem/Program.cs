using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

// ----- 1. MƏLUMAT MODELLƏRİ (Entity Classes) -----
public class Ders
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string DersKodu { get; set; }
    public string DersAdi { get; set; }
    public int Sinifi { get; set; }
    public string MuellimAdi { get; set; }
    public string MuellimSoyadi { get; set; }
}

public class Sagird
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Nomresi { get; set; }
    public string Adi { get; set; }
    public string Soyadi { get; set; }
    public int Sinifi { get; set; }
}

public class Imtahan
{
    public int ImtahanID { get; set; }

    [ForeignKey("Ders")]
    public string DersKodu { get; set; }

    [ForeignKey("Sagird")]
    public int SagirdNomresi { get; set; }

    public DateTime ImtahanTarixi { get; set; }
    public int Qiymeti { get; set; }

    // Navigation Properties
    public virtual Ders Ders { get; set; }
    public virtual Sagird Sagird { get; set; }
}

public class SchoolDbContext : DbContext
{
    public DbSet<Ders> Dərslər { get; set; }
    public DbSet<Sagird> Şagirdlər { get; set; }
    public DbSet<Imtahan> İmtahanlar { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = "Server=DESKTOP-9038BT1\\SQLEXPRESS;Database=ExamDB_ConsoleApp;Trusted_Connection=True;TrustServerCertificate=True;";
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ders>().HasKey(d => d.DersKodu);
        modelBuilder.Entity<Sagird>().HasKey(s => s.Nomresi);
        modelBuilder.Entity<Imtahan>().HasKey(i => i.ImtahanID);
    }
}


public class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        using (var db = new SchoolDbContext())
        {
            db.Database.EnsureCreated();
        }

        bool davamEt = true;
        while (davamEt)
        {
            MenyunuGoster();
            Console.Write("Seçiminizi daxil edin: ");
            string secim = Console.ReadLine();

            switch (secim)
            {
                case "1":
                    DersElaveEt();
                    break;
                case "2":
                    SagirdElaveEt();
                    break;
                case "3":
                    ImtahanElaveEt();
                    break;
                case "4":
                    DerslereBax();
                    break;
                case "5":
                    SagirdlereBax();
                    break;
                case "6":
                    ImtahanlaraBax();
                    break;
                case "0":
                    davamEt = false;
                    Console.WriteLine("Proqramdan çıxılır...");
                    break;
                default:
                    Console.WriteLine("Yanlış seçim! Zəhmət olmasa, yenidən cəhd edin.");
                    break;
            }

            if (davamEt)
            {
                Console.WriteLine("\nMenyuya qayıtmaq üçün hər hansı bir düyməyə basın...");
                Console.ReadKey();
            }
        }
    }

    static void MenyunuGoster()
    {
        Console.Clear();
        Console.WriteLine("========== İMTANAN QEYDİYYAT SİSTEMİ (MS SQL Server) ==========");
        Console.WriteLine("1. Yeni Dərs Əlavə Et");
        Console.WriteLine("2. Yeni Şagird Əlavə Et");
        Console.WriteLine("3. İmtahan Nəticəsi Əlavə Et");
        Console.WriteLine("---------------------------------------------");
        Console.WriteLine("4. Bütün Dərslərə Bax");
        Console.WriteLine("5. Bütün Şagirdlərə Bax");
        Console.WriteLine("6. Bütün İmtahan Nəticələrinə Bax");
        Console.WriteLine("---------------------------------------------");
        Console.WriteLine("0. Çıxış");
        Console.WriteLine("==============================================================");
    }


    static void DersElaveEt()
    {
        try
        {
            Console.WriteLine("\n--- Yeni Dərs Əlavə Et ---");
            Console.Write("Dərsin kodu (3 simvol): ");
            string kod = Console.ReadLine();
            Console.Write("Dərsin adı: ");
            string ad = Console.ReadLine();
            Console.Write("Sinif: ");
            int sinif = int.Parse(Console.ReadLine());
            Console.Write("Müəllimin adı: ");
            string muellimAd = Console.ReadLine();
            Console.Write("Müəllimin soyadı: ");
            string muellimSoyad = Console.ReadLine();

            using (var db = new SchoolDbContext())
            {
                var yeniDers = new Ders { DersKodu = kod.ToUpper(), DersAdi = ad, Sinifi = sinif, MuellimAdi = muellimAd, MuellimSoyadi = muellimSoyad };
                db.Dərslər.Add(yeniDers);
                db.SaveChanges();
            }
            Console.WriteLine("Dərs uğurla bazaya əlavə edildi!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Xəta baş verdi: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"--- ƏSL SƏBƏB: {ex.InnerException.Message}");
            }
        }
    }

    static void SagirdElaveEt()
    {
        try
        {
            Console.WriteLine("\n--- Yeni Şagird Əlavə Et ---");
            Console.Write("Şagirdin nömrəsi: ");
            int nomre = int.Parse(Console.ReadLine());
            Console.Write("Adı: ");
            string ad = Console.ReadLine();
            Console.Write("Soyadı: ");
            string soyad = Console.ReadLine();
            Console.Write("Sinifi: ");
            int sinif = int.Parse(Console.ReadLine());

            using (var db = new SchoolDbContext())
            {
                var yeniSagird = new Sagird { Nomresi = nomre, Adi = ad, Soyadi = soyad, Sinifi = sinif };
                db.Şagirdlər.Add(yeniSagird);
                db.SaveChanges();
            }
            Console.WriteLine(" Şagird uğurla bazaya əlavə edildi!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($" Xəta baş verdi: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"--- ƏSL SƏBƏB: {ex.InnerException.Message}");
            }
        }
    }

    static void ImtahanElaveEt()
    {
        try
        {
            Console.WriteLine("\n--- İmtahan Nəticəsi Əlavə Et ---");
            using (var db = new SchoolDbContext())
            {
                if (!db.Dərslər.Any() || !db.Şagirdlər.Any())
                {
                    Console.WriteLine("İmtahan əlavə etmək üçün sistemdə ən azı bir dərs və bir şagird olmalıdır!");
                    return;
                }

                Console.WriteLine("Mövcud şagirdlər:");
                foreach (var s in db.Şagirdlər.ToList()) { Console.WriteLine($"- {s.Nomresi}: {s.Adi} {s.Soyadi}"); }
                Console.Write("Şagirdin nömrəsini seçin: ");
                int sagirdNomresi = int.Parse(Console.ReadLine());

                Console.WriteLine("\nMövcud dərslər:");
                foreach (var d in db.Dərslər.ToList()) { Console.WriteLine($"- {d.DersKodu}: {d.DersAdi}"); }
                Console.Write("Dərsin kodunu seçin: ");
                string dersKodu = Console.ReadLine().ToUpper();

                Console.Write("İmtahan tarixi (GG.AA.İİİİ): ");
                DateTime tarix = DateTime.Parse(Console.ReadLine());

                Console.Write("Qiymət (1-5 arası): ");
                int qiymet = int.Parse(Console.ReadLine());

                var yeniImtahan = new Imtahan { SagirdNomresi = sagirdNomresi, DersKodu = dersKodu, ImtahanTarixi = tarix, Qiymeti = qiymet };
                db.İmtahanlar.Add(yeniImtahan);
                db.SaveChanges();
            }
            Console.WriteLine(" İmtahan nəticəsi uğurla bazaya əlavə edildi!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Xəta baş verdi: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"--- ƏSL SƏBƏB: {ex.InnerException.Message}");
            }
        }
    }

    static void DerslereBax()
    {
        Console.WriteLine("\n--- Bütün Dərslər ---");
        using (var db = new SchoolDbContext())
        {
            var dersler = db.Dərslər.ToList();
            if (dersler.Count == 0)
            {
                Console.WriteLine("Sistemdə heç bir dərs yoxdur.");
                return;
            }
            Console.WriteLine("{0,-10} {1,-20} {2,-7} {3,-20}", "Kod", "Dərs Adı", "Sinif", "Müəllim");
            Console.WriteLine(new string('-', 60));
            foreach (var d in dersler)
            {
                Console.WriteLine("{0,-10} {1,-20} {2,-7} {3,-20}", d.DersKodu, d.DersAdi, d.Sinifi, $"{d.MuellimAdi} {d.MuellimSoyadi}");
            }
        }
    }

    static void SagirdlereBax()
    {
        Console.WriteLine("\n--- Bütün Şagirdlər ---");
        using (var db = new SchoolDbContext())
        {
            var sagirdler = db.Şagirdlər.ToList();
            if (sagirdler.Count == 0)
            {
                Console.WriteLine("Sistemdə heç bir şagird yoxdur.");
                return;
            }
            Console.WriteLine("{0,-10} {1,-25} {2,-7}", "Nömrə", "Ad Soyad", "Sinif");
            Console.WriteLine(new string('-', 50));
            foreach (var s in sagirdler)
            {
                Console.WriteLine("{0,-10} {1,-25} {2,-7}", s.Nomresi, $"{s.Adi} {s.Soyadi}", s.Sinifi);
            }
        }
    }

    static void ImtahanlaraBax()
    {
        Console.WriteLine("\n--- Bütün İmtahan Nəticələri ---");
        using (var db = new SchoolDbContext())
        {
            var imtahanlar = db.İmtahanlar.Include(i => i.Sagird).Include(i => i.Ders).ToList();
            if (imtahanlar.Count == 0)
            {
                Console.WriteLine("Sistemdə heç bir imtahan nəticəsi yoxdur.");
                return;
            }

            Console.WriteLine("{0,-25} {1,-20} {2,-15} {3,-7}", "Şagird", "Dərs", "Tarix", "Qiymət");
            Console.WriteLine(new string('-', 70));

            foreach (var i in imtahanlar)
            {
                string sagirdAd = i.Sagird != null ? $"{i.Sagird.Adi} {i.Sagird.Soyadi}" : "Bilinmir";
                string dersAd = i.Ders != null ? i.Ders.DersAdi : "Bilinmir";

                Console.WriteLine("{0,-25} {1,-20} {2,-15} {3,-7}", sagirdAd, dersAd, i.ImtahanTarixi.ToShortDateString(), i.Qiymeti);
            }
        }
    }
}
