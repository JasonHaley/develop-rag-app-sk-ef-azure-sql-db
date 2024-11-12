using Microsoft.Extensions.Hosting;
using PdfChatApp.Configuration;
using System.CommandLine;

var builder = Host.CreateApplicationBuilder(args).AddAppSettings();

// TODO: Connect to a real database
// TODO: Add DocumentService
// TODO: Add PdfIngestor

// TODO: Add ChatCompletionService
// TODO: Add DbRetriever

var services = builder.Build().Services;

var rootCommand = CommandOptions.RootCommand;

rootCommand.SetHandler(
    async (context) =>
    {
        var options = CommandOptions.GetParsedAppOptions(context);

        if (!string.IsNullOrEmpty(options.Remove))
        {
            // TODO: Add logic to remove a document from the db
        }
        else if (!string.IsNullOrEmpty(options.Files))
        {
            // TODO: Add logic to ingest PDF files
        }
        else if (options.RemoveAll)
        {
            // TODO: Add logic to remove all documents from the db
        }
        else
        {
            // TODO: Add logic to start the chatbot
            Console.WriteLine("Chatbot mode not implemented yet.");
        }
    });

await rootCommand.InvokeAsync(args);