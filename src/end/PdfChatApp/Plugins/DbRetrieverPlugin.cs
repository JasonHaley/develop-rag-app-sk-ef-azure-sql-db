using Microsoft.SemanticKernel;
using PdfChatApp.Retrievers;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace PdfChatApp.Plugins;

public class DbRetrieverPlugin(DbRetriever retriever)
{
    [KernelFunction, Description("Searches the internal documentation.")]
    public async Task<string> RetrieveAsync([Description("User's message"), Required] string question, Kernel kernel)
    {
        var searchResults = await retriever.RetrieveLocalAsync(question, 5);

        // TODO: Move to Trace Logging
        var resultsAsJson = JsonSerializer.Serialize(searchResults, new JsonSerializerOptions { WriteIndented = true });
        Console.WriteLine("\n\n---------------------------------------");
        Console.WriteLine("Search string: " + question);
        Console.WriteLine(resultsAsJson);
        Console.WriteLine("---------------------------------------\n\n");

        var rag = kernel.Plugins["Prompts"];

        var llmResult = await kernel.InvokeAsync(rag["BasicRAG"],
            new() {
                { "question", question },
                { "context", JsonSerializer.Serialize(searchResults) }
            }
        );

        return llmResult.ToString();
    }
}
