using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;


namespace ODEV;

public class TaskItem
{
	public string Name { get; set; }
	public bool IsComplete { get; set; }
}
	public partial class ToDoList : ContentPage
	{
		private ObservableCollection<TaskItem> tasks;
        private const string fileName = "tasks.txt";
    public ToDoList()
		{
			InitializeComponent();
			tasks = new ObservableCollection<TaskItem>();
			NotListe.ItemsSource = tasks;
            LoadTasks();
		}
    private async void LoadTasks()
    {
        try
        {
            var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);
            if (File.Exists(filePath))
            {
                var lines = await File.ReadAllLinesAsync(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 2)
                    {
                        tasks.Add(new TaskItem
                        {
                            Name = parts[0],
                            IsComplete = bool.Parse(parts[1])
                        });
                    }
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("HATA", "Bir hata meydana geldi", "OK");
        }
    }
		private void Elkebutton(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(TaskEntry.Text))
			{
				tasks.Add(new TaskItem { Name = TaskEntry.Text, IsComplete = false });
				TaskEntry.Text = string.Empty;
			}
		}
    private async void Editbutton(object sender, EventArgs e)
    {
        if (sender is ImageButton button && button.CommandParameter is TaskItem taskToEdit)
        {
            string newTaskName = await DisplayPromptAsync("Güncelle", "Notu güncelleyin:", initialValue: taskToEdit.Name);
            if (!string.IsNullOrWhiteSpace(newTaskName))
            {
                taskToEdit.Name = newTaskName;
                NotListe.ItemsSource = null;
                NotListe.ItemsSource = tasks;
                
            }
        }
    }
    private async void Deletebutton(object sender, EventArgs e)
    {
        if (sender is ImageButton button && button.CommandParameter is TaskItem taskToDelete)
        {
            bool confirmDelete = await DisplayAlert("Notu Sil", "Notu silmek mi istiyorsunuz?", "YES", "NO");
            if (confirmDelete)
            {
                tasks.Remove(taskToDelete);
               
            }
        }
    }
    private async void Savebutton(object sender, EventArgs e)
    {
        try
        {
            var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);
            using (var writer = new StreamWriter(filePath))
            {
                foreach (var task in tasks)
                {
                    await writer.WriteLineAsync($"{task.Name},{task.IsComplete}");
                }
            }
            await DisplayAlert("Kaydedildi", "Notunuz Kaydedildi!", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", "Kaydedilmedi!", "OK");
        }
    }
    }
