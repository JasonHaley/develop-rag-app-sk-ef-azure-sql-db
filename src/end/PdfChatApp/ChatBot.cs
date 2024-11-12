
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel;
using System.Text;
using PdfChatApp.Plugins;

namespace PdfChatApp;
public class ChatBot(Kernel kernel, IChatCompletionService chatCompletionService)
{
    public async Task StartAsync()
    {
        kernel.ImportPluginFromPromptDirectory("Prompts");
        kernel.ImportPluginFromType<DbRetrieverPlugin>();

        OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new()
        {
            ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions,
            Temperature = 0.2f,
            MaxTokens = 1000
        };

        var responseTokens = new StringBuilder();
        ChatHistory chatHistory = new ChatHistory("You are a chatbot that can answer questions about the internal documentation."); // Could add "Be brief with your responses."
        while (true)
        {
            Console.Write("\nUser: ");

            var question = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(question))
            {
                break;
            }
            chatHistory.AddUserMessage(question);
            responseTokens.Clear();
            await foreach (var token in chatCompletionService.GetStreamingChatMessageContentsAsync(chatHistory, openAIPromptExecutionSettings, kernel))
            {
                Console.Write(token);
                responseTokens.Append(token);
            }

            chatHistory.AddAssistantMessage(responseTokens.ToString());
            Console.WriteLine();
        }
    }
}
