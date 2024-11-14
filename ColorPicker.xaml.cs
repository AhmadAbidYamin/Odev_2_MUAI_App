

namespace ODEV;

public partial class ColorPicker : ContentPage
{
	public ColorPicker()
	{
		InitializeComponent();
	}
	private async void Copybuttonclicked(object sender, EventArgs e)
	{
		if(sender is Button button)
		{
			await Clipboard.SetTextAsync(button.Text);
			await DisplayAlert("KOPYALANDI", $"'{button.Text}' RENK KODU KOPYALANDI.", "OK");
		}
	}
	private void Randombuttonclicked(object sender, EventArgs e)
	{
		Random random = new Random();
		RedSlider.Value = random.Next(0, 256);
		GreenSlider.Value = random.Next(0, 256);
		BlueSlider.Value = random.Next(0, 256);

	}
	private void SetColor(Object sender, ValueChangedEventArgs args)
	{
		 Slider target = sender as Slider;
		int v = (int)target.Value;
		if (target.MaximumTrackColor == Colors.Red)
		{
			Redvalue.Text = v.ToString();
		}
		else if (target.MaximumTrackColor == Colors.Green)
		{
			Greenvalue.Text = v.ToString();
		}
		else 
		{
			Bluevalue.Text = v.ToString();
		}
		UpdateColor();
	}
	private void UpdateColor()
	{
		Int32 r = Int32.Parse(Redvalue.Text);
        Int32 g = Int32.Parse(Greenvalue.Text);
        Int32 b = Int32.Parse(Bluevalue.Text);

		RenkSecici.BackgroundColor = Color.FromRgb(r, g, b);
        RenkSecici.Stroke = Color.FromRgb(r, g, b);
        ColorCode.Text = "#" + r.ToString("X2") + g.ToString("X2") + b.ToString("X2");
    }
}