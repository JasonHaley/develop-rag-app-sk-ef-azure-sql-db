using Microsoft.Extensions.Logging;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace PdfChatApp.Configuration;

public record class DefaultOptions(ILoggerFactory LoggerFactory);

internal static class CommandOptions
{
    internal static readonly Option<string> Files = new(name: "-f", description: "Files or directory to be processed");

    internal static readonly Option<string> Remove = new(name: "-r", description: "Remove a specified file's embeddings from the database.");

    internal static readonly Option<bool> RemoveAll = new(name: "-ra", description: "Removes all embeddings from the database.");

    internal static readonly RootCommand RootCommand = new(description: """
        Prepare documents by chunking and retrieving embeddings, then save to database.
        """)
        {
            Files,
            Remove,
            RemoveAll,
        };
    internal static AppOptions GetParsedAppOptions(InvocationContext context) => new(
            Files: context.ParseResult.GetValueForOption(Files),
            Remove: context.ParseResult.GetValueForOption(Remove),
            RemoveAll: context.ParseResult.GetValueForOption(RemoveAll),
            Console: context.Console);

    internal record class AppOptions(
        string? Files,
        string? Remove,
        bool RemoveAll,
        IConsole Console) : AppConsole(Console);

    internal record class AppConsole(IConsole Console);
}
