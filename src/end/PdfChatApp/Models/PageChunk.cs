namespace PdfChatApp.Models;

public class PageChunk
{
    public int Id { get; set; }
    public int PageId { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public int Number { get; set; }
    public required string Text { get; set; }
    public float[]? LocalEmbedding { get; set; }
    public float[]? Ada2Embedding { get; set; }

    public virtual Page? Page { get; set; }
}
