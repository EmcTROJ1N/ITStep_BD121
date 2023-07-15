namespace FileExplorer.Models;

public class FileEditViewModel
{
    public FileModel File { get; set; }
    public string Text { get; set; }

    public FileEditViewModel(FileModel file)
    {
        this.File = file;
        this.Text = System.IO.File.ReadAllText(file.FullName);
    }
}