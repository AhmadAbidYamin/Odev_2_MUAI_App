
namespace ODEV;

public partial class VucutKitleIndexi : ContentPage
{
	public VucutKitleIndexi()
	{
		InitializeComponent();
	
	}
	
	private void Hesaplabutton(object sender, EventArgs e)
	{
		if (double.TryParse(WeightEntery.Text, out double weight) &&
			double.TryParse(HeightEntery.Text, out double height))
		{
			double heightinmeters = height / 100;
			double bmi = weight / (heightinmeters * heightinmeters);

			string category;
			if (bmi < 16)
			{
				category = "Çok Zayif";
			}
			else if (bmi < 16.99)
			{
				category = "Orta Düzeyde Zayif";
			}
			else if (bmi < 18.5)
			{
				category = "Hafif Düzeyde Zayif";
			}
			else if(bmi <24.99)
			{
                category = "Normal";
            }
            else if (bmi < 29.99)
            {
                category = "Kilolu";
            }
            else if (bmi < 34.99)
            {
                category = "1. Derecede Obez";
            }
            else if (bmi < 39.90)
            {
                category = "2. Derecede Obez";
            }
            else
			{
				category = "Morbid Obez";
			}
			Resultlabel.Text = $"Vucut Kitle indexi : {bmi:F1}";
            Resultlabel2.Text = $"Kategori : {category}";
        }
		else
		{
			Resultlabel.Text = "Lütfen düzgün verileri giriniz!";
		}
	}
		
}