namespace PdfChatApp.Models;

public class Document
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Path { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    
    public virtual List<Page> Pages { get; set; } = [];
}
