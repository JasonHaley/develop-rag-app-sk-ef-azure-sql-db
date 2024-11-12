using Microsoft.EntityFrameworkCore;
using PdfChatApp.Extensions;
using PdfChatApp.Models;
using SmartComponents.LocalEmbeddings.SemanticKernel;

namespace PdfChatApp.Retrievers;

public record DocumentInfo(int Id, int PageId, int ChunkId, string Name, string Path, string Text, double Similarity);

public class DbRetriever(DocDbContext dbContext)
{
    public async Task<List<DocumentInfo>> RetrieveLocalAsync(string text, int k)
    {
        using var localEmbeddingGenerator = new LocalTextEmbeddingGenerationService();

        var vector = (await localEmbeddingGenerator.GenerateEmbeddingsAsync([text])).ToFloatArray();

        return await dbContext.PageChunks
            .Include(pc => pc.Page).ThenInclude(p => p!.Document)
            .OrderBy(pc => EF.Functions.VectorDistance("cosine", pc.LocalEmbedding ?? new float[0], vector))
            .Take(k)
            .Select(pc => new DocumentInfo(pc!.Page!.Document!.Id, pc.PageId, pc.Id, pc.Page.Document.Name, pc.Page.Document.Path, pc.Text, EF.Functions.VectorDistance("cosine", pc.LocalEmbedding ?? new float[0], vector)))
            .ToListAsync();
    }
}