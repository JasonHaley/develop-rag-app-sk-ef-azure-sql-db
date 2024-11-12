## Get the code

In this section we will get the starter code from GitHub and use Entity Framework Core to build out our data models and context.

1. If you haven't already cloned the repository, go to [https://github.com/JasonHaley/develop-rag-app-sk-ef-azure-sql-db]9https://github.com/JasonHaley/develop-rag-app-sk-ef-azure-sql-db), click on the **Code dropdown** button and click **copy url to clipboard** button

![Code button](assets/part1-2-img1.jpg)

2. On your machine, you'll need a directory to put the code in. Open a console window and navigate to where you want to put the code. The run the following command:

```PowerShell
git clone https://github.com/JasonHaley/develop-rag-app-sk-ef-azure-sql-db.git
```

3. Once the code has downloaded run the following code in order open it in VS Code:

```PowerShell
cd develop-rag-app-sk-ef-azure-sql-db
code .
```

This should open VS Code with the directory loaded. Look for the **/src/start** folder.

![Start Folder](assets/part1-2-img2.jpg)

## A Quick Look At What Is Provided To Start With

In order to keep this lab focused on SQL Azure, Entity Framework and Semantic Kernel, the project already provides some none related but relevant things - let's look at those first. **Feel free to skip this!**

The **assets** folder - provides a couple of PDFs you can use for the sample data
The **Configuration** folder provides some helpers.
* `CommandOptions.cs` - provides basic command logic to use the **System.CommandLine** package for handling command line arguments.
* `ConfigurationExtentions.cs` - provides configuration logic for loading appsettings.json and a couple of helpers that simplify using a ConnectionString to load LLM settings for OpenAI or AzureOpenAI.
* `Extensions/ReadOnlyMemoryExtensions.cs` - has an extension method for converting a `ReadOnlyMemory<float>` to a `float[]` which will be used when saving embeddings.
* **Models** folder has three classes we will use for the object model representing a Document, Page and PageChunk. More on these soon.
* **ChatBot.cs** - is a starter class which will end up containing the messaging loop when we are done.
* **PdfChatApp.csproj** - This contains the package references as you would expect, however there are a few other things I want to highlight:

The **ProperyGroup** at the top:
```XML
<PropertyGroup>
	<OutputType>Exe</OutputType>
	<TargetFramework>net8.0</TargetFramework>
	<ImplicitUsings>enable</ImplicitUsings>
	<Nullable>enable</Nullable>
	<NoWarn>SKEXP0001;SKEXP0050;SKEXP0010;SKEXP0020</NoWarn>
	<!--<StartArguments>-f assets\ATaxonomyOfRAG.pdf</StartArguments>-->
	<!--<StartArguments>-f assets\*.pdf</StartArguments>-->
	<!--<StartArguments>-f assets\semantic-kernel.pdf</StartArguments>-->
	<!--<StartArguments>-r assets\semantic-kernel.pdf</StartArguments>-->
</PropertyGroup>
```

The `<NoWarn>SKEXP0001;SKEXP0050;SKEXP0010;SKEXP0020</NoWarn>` line is the easiest way to escape the experimental notifications you'll get with Semantic Kernel (due to us using prerelease functionality).

The lines like `<!--<StartArguments>-f assets\ATaxonomyOfRAG.pdf</StartArguments>-->` will be uncommented later when running the application as an option to passing the arguments on the command line.

The bottom **ItemGroup**:
```XML
<ItemGroup>
	<None Update="appsettings.Local.json">
	  <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
	</None>
	<None Update="appsettings.json">
		<CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
	</None>
	<None Update="appsettings.Local.json">
		<CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
	</None>
	<None Update="assets\ATaxonomyOfRAG.pdf">
		<CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
	</None>
	<None Update="assets\semantic-kernel.pdf">
		<CopyToOutputDirectory>Always</CopyToOutputDirectory>
	</None>
</ItemGroup>
```

You probably know what these are, but in case you don't - this is the information that lets dotnet know it needs to treat the copy sample pdf files and the appsettings files to the binary directory. Otherwise the files won't be found at runtime.

* `Program.cs` is the starting point with // TODO: items for placeholders.

The application is a console application that will run as a chat bot - if you don't pass any arguments on the command line. The argument options are:

| Argument       | Purpose        |
|----------------|----------------|
| -f | Takes the path to the PDF files (or directory) to injest into the database |
| -r | Takes a path to the PDF to remove from the database |
| -ra | Removes all files form the database |

> NOTE: if you just run PdfChatApp.exe with **no arguments** it will behave as a chat bot

