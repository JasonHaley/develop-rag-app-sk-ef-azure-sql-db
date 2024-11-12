# Part 2: Create the Retriever, Add Semantic Kernel and Plugins

In the second part we will create the RAG application. Fist let's work on the retrieval.

## Create the DbRetriever

This section we will implement code using Entity Framework Core to perform a similarity search using the embeddings we populated in the last section.

1. In the **PdfChatApp** project folder, create a new folder named **Retrievers** and add a file named **DbRetriever.cs** to it.

This will hold our logic to search the database.

2. Paste the following code into the **DbRetriever.cs** file you just created:

```C#
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
            .OrderBy(pc => EF.Functions.VectorDistance("cosine", pc.Embedding ?? new float[0], vector))
            .Take(k)
            .Select(pc => new DocumentInfo(pc!.Page!.Document!.Id, pc.PageId, pc.Id, pc.Page.Document.Name, pc.Page.Document.Path, pc.Text, EF.Functions.VectorDistance("cosine", pc.Embedding ?? new float[0], vector)))
            .ToListAsync();
    }
}
```

In the code, we first declare a `DocumentInfo` record to use in returning the information about the page chunk, document, text and similarity score.

The `DbRetriever` class has one method `RetrieveLocalAsync()` which uses the `LocalTextEmbeddingGenerationService` to get the embeddings for the passed in text, then uses the EF Core plugin mentioned earlier to use the `EF.Functions.VectorDistance()` to order the search results by their similarity score and returns the top k number of items.

Now we need to add Semantic Kernel.

## [Next: Add Semantic Kernel >](part2-2.md)