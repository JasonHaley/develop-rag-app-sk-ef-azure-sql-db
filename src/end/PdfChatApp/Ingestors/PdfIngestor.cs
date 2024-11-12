using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.FileSystemGlobbing.Abstractions;
using Microsoft.ML.Tokenizers;
using Microsoft.SemanticKernel.Text;
using PdfChatApp.Extensions;
using PdfChatApp.Services;
using SmartComponents.LocalEmbeddings.SemanticKernel;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.DocumentLayoutAnalysis.PageSegmenter;
using UglyToad.PdfPig.DocumentLayoutAnalysis.WordExtractor;
using Db = PdfChatApp.Models;

namespace PdfChatApp.Ingestors;

public class PdfIngestor(DocumentService documentService)
{
    public async Task RunAsync(string filesPath)
    {
        string[] files = GetFileNames(filesPath);

        Console.WriteLine($"Processing {files.Length} files...");

        var tasks = Enumerable.Range(0, files.Length)
           .Select(i =>
           {
               var fileName = files[i];
               return ProcessSingleFileAsync(fileName);
           });

        await Task.WhenAll(tasks);
    }

    private static string[] GetFileNames(string filesPath)
    {
        Matcher matcher = new();
        matcher.AddInclude(filesPath);

        var results = matcher.Execute(
            new DirectoryInfoWrapper(
                new DirectoryInfo(Directory.GetCurrentDirectory())));

        var files = results.HasMatches
            ? results.Files.Select(f => f.Path).ToArray()
            : [];
        return files;
    }

    public async Task ProcessSingleFileAsync(string file)
    {
        var docName = Path.GetFileNameWithoutExtension(file);

        if (await documentService.DocumentExistsAsync(docName))
        {
            Console.WriteLine($"Document {docName} already exists.");
            return;
        }
        
        Console.WriteLine($"Generating chunks for {file}...");
        var document = new Db.Document
        {
            Name = docName,
            Path = file
        };

        using var localEmbeddingGenerator = new LocalTextEmbeddingGenerationService();

        var chunkCount = 0;
        var tokenizer = TiktokenTokenizer.CreateForModel("gpt-4o");

        using var pdf = PdfDocument.Open(file);
        foreach (var page in pdf.GetPages())
        {
            var dbPage = new Db.Page
            {
                Number = page.Number,
            };

            var pageText = GetPageText(page);
            var paragraphs = TextChunker.SplitPlainTextParagraphs([pageText], 500, 100, null, text => tokenizer.CountTokens(text));

            for (int i = 0; i < paragraphs.Count; i++)
            {
                var paragraph = paragraphs[i];
                
                var pageChunk = new Db.PageChunk
                {
                    Text = paragraph,
                    Number = i,
                    Embedding = (await localEmbeddingGenerator.GenerateEmbeddingsAsync([paragraph])).ToFloatArray()
                };

                chunkCount++;

                dbPage.PageChunks.Add(pageChunk);
            }
            document.Pages.Add(dbPage);
        }

        await documentService.AddDocumentAsync(document);

        if (chunkCount > 0)
        {
            Console.WriteLine($"Generated {chunkCount} chunks.");
        }
    }
        
    private static string GetPageText(Page pdfPage)
    {
        var letters = pdfPage.Letters;
        var words = NearestNeighbourWordExtractor.Instance.GetWords(letters);
        var textBlocks = DocstrumBoundingBoxes.Instance.GetBlocks(words);
        return string.Join(Environment.NewLine + Environment.NewLine,
            textBlocks.Select(t => t.Text.ReplaceLineEndings(" ")));
    }
}
