namespace ODEV;

public partial class KrediHesaplama : ContentPage
{
    public KrediHesaplama()
    {
        InitializeComponent();
    }

    private void Hesaplabutton(object sender, EventArgs e)
    {
        try
        {
            double tutar = Convert.ToDouble(TutarEntry.Text);
            double aylikFaizOrani = Convert.ToDouble(FaizEntry.Text) / 100;
            int aySayisi = Convert.ToInt32(AyEntry.Text);
            double kkdfOrani = 0.0;
            double bsmvOrani = 0.0;


            switch (Kredituru.SelectedItem?.ToString())
            {
                case "ihtiyac Kredisi":
                    kkdfOrani = 0.15; // %15
                    bsmvOrani = 0.10; // %10
                    break;
                case "Konut Kredisi":
                    kkdfOrani = 0.0; // %0
                    bsmvOrani = 0.0; // %0
                    break;
                case "Taşit Kredisi":
                    kkdfOrani = 0.15; // %15
                    bsmvOrani = 0.05; // %5
                    break;
                case "Ticari Kredisi":
                    kkdfOrani = 0.0; // %0
                    bsmvOrani = 0.05; // %5
                    break;
                default:
                    DisplayAlert("Hata", "Lütfen bir kredi türü seçin.", "Tamam");
                    return;
            }

            // Vergiler dahil toplam aylik faiz orani
            double toplamFaizOrani = aylikFaizOrani + (aylikFaizOrani * kkdfOrani) + (aylikFaizOrani * bsmvOrani);

            // Aylik ödeme hesaplama (annuite formülü)
            double aylikOdeme = (tutar * toplamFaizOrani * Math.Pow(1 + toplamFaizOrani, aySayisi)) /
                                (Math.Pow(1 + toplamFaizOrani, aySayisi) - 1);

            // Toplam ödeme ve toplam faiz hesaplamalari
            double toplamOdeme = aylikOdeme * aySayisi;
            double toplamFaiz = toplamOdeme - tutar;

            // Sonuçlari kullaniciya gösterme
            AylikOdemeLabel.Text = $"Aylik Ödeme:      {aylikOdeme:F2} TL";
            ToplamOdemeLabel.Text = $"Toplam Ödeme:      {toplamOdeme:F2} TL";
            ToplamFaizLabel.Text = $"Toplam Faiz:      {toplamFaiz:F2} TL";
        }
        catch (Exception)
        {
            DisplayAlert("Hata", "Lütfen geçerli sayisal degerler girin.", "Tamam");

        }


    }
}