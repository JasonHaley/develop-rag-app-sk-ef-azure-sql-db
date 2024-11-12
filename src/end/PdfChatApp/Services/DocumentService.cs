using Microsoft.EntityFrameworkCore;
using PdfChatApp.Models;

namespace PdfChatApp.Services;
public class DocumentService(DocDbContext dbContext)
{
    public async Task<List<Document>?> GetDocumentsAsync()
    {
        return await dbContext.Documents.ToListAsync();
    }

    public async Task<Document?> GetDocumentAsync(int id)
    {
        return await dbContext.Documents.FindAsync(id);
    }

    public async Task<bool> DocumentExistsAsync(string name)
    {
        return await dbContext.Documents.AnyAsync(d => d.Name == name);
    }

    public async Task<Document?> AddDocumentAsync(Document document)
    {
        dbContext.Documents.Add(document);
        await dbContext.SaveChangesAsync();
        return document;
    }

    public async Task RemoveDocumentAsync(string name)
    {
        var document = await dbContext.Documents.FirstOrDefaultAsync(d => d.Name == name);
        if (document != null)
        {
            dbContext.Documents.Remove(document);
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task RemoveAllDocumentsAsync()
    {
        dbContext.Documents.RemoveRange(dbContext.Documents);
        await dbContext.SaveChangesAsync();
    }
}
