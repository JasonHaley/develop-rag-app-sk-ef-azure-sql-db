namespace PdfChatApp.Models;
public class Page
{
    public int Id { get; set; }
    public int DocumentId { get; set; }
    public int Number { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;

    public virtual Document? Document { get; set; }
    public virtual List<PageChunk> PageChunks { get; set; } = [];
}
