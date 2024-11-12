using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using PdfChatApp;
using PdfChatApp.Configuration;
using PdfChatApp.Ingestors;
using PdfChatApp.Models;
using PdfChatApp.Retrievers;
using PdfChatApp.Services;
using System.CommandLine;

var builder = Host.CreateApplicationBuilder(args).AddAppSettings();

builder.Services.AddDbContext<DocDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlAzureDB"), o => o.UseVectorSearch()));

builder.Services.AddKernel().AddChatCompletionService(builder.Configuration.GetConnectionString("OpenAI"));

builder.Services.AddScoped<DocumentService>();
builder.Services.AddScoped<PdfIngestor>();
builder.Services.AddScoped<DbRetriever>();

var services = builder.Build().Services;

var rootCommand = CommandOptions.RootCommand;

rootCommand.SetHandler(
    async (context) =>
    {
        var options = CommandOptions.GetParsedAppOptions(context);

        if (!string.IsNullOrEmpty(options.Remove))
        {
            var documentService = services.GetRequiredService<DocumentService>();
            var docName = Path.GetFileNameWithoutExtension(options.Remove);
            await documentService.RemoveDocumentAsync(docName);
        }
        else if (!string.IsNullOrEmpty(options.Files))
        {
            var pdfIngestor = services.GetRequiredService<PdfIngestor>();
            await pdfIngestor.RunAsync(options.Files);
        }
        else if (options.RemoveAll)
        {
            var documentService = services.GetRequiredService<DocumentService>();
            await documentService.RemoveAllDocumentsAsync();
        }
        else
        {
            var chatCompletionService = services.GetRequiredService<IChatCompletionService>();
            var kernel = services.GetRequiredService<Kernel>();
            var chatBot = new ChatBot(kernel, chatCompletionService);
            await chatBot.StartAsync();
        }
    });

await rootCommand.InvokeAsync(args);