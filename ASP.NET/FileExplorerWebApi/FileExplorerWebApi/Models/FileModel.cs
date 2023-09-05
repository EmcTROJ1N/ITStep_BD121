using System.ComponentModel.DataAnnotations;

namespace FileExplorerWebApi.Models;

public class FileModel
{
    [Display(Name = "File name")]
    public string? FileName { get; set; }
    [Display(Name = "Full path")]
    public string? FullName { get; set; }
    [Display(Name = "File extension")]
    public string? Extension { get; set; }
    [Display(Name = "File creation date")]
    public DateTime? CreationDate { get; set; }
    [Display(Name = "File size")]
    public long? Size { get; set; }

    public FileModel(string fullname)
    {
        if (File.Exists(fullname))
        {
            FileInfo info = new FileInfo(fullname);
            this.FullName = info.FullName;
            this.FileName = info.Name;
            this.CreationDate = info.CreationTime;
            this.Extension = Path.GetExtension(fullname);
            this.Size = info.Length;
            
        }
        else if (Directory.Exists(fullname))
        {
            DirectoryInfo info = new DirectoryInfo(fullname);
            this.FullName = fullname;
            this.FileName = info.Name;
            this.CreationDate = info.CreationTime;
            this.Extension = Path.GetExtension(fullname);
            this.Size = 0;
        }
    }
}