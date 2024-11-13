## Test the ChatBot

If you made it this far - Congratulations!

Now is the fun part, we get to ask the PDF questions and see how it does at answering. 

If you are not already running the application, run the following:

```C#
dotnet run
```

You should see a prompt in the output like this:

```TEXT
User:
```

Type some question for the Semantic Kernel documentation, like any of these:

* What do I need to do to get started with Semantic Kernel?
* What is a native plugin?
* What languages does Semantic Kernel support?
* What prompt template formats does it support?
* Does it support Prompty?

```PowerShell
User: What do I need to do to get started with Semantic Kernel?


---------------------------------------
Search string: getting started with Semantic Kernel
[
  {
    "Id": 2,
    "PageId": 5,
    "ChunkId": 4,
    "Name": "semantic-kernel",
    "Path": "assets/semantic-kernel.pdf",
    "Text": "Now that you know what Semantic Kernel is, get started with the quick start guide. You\u2019ll build agents that automatically call functions to perform actions faster than any other SDK out there.\n\nGet started\n\nQuickly get started",
    "Similarity": 0.18434761346948558
  },
  {
    "Id": 2,
    "PageId": 6,
    "ChunkId": 5,
    "Name": "semantic-kernel",
    "Path": "assets/semantic-kernel.pdf",
    "Text": "Getting started with Semantic Kernel\n\nArticle \u2022 06/24/2024\n\nIn just a few steps, you can build your first AI agent with Semantic Kernel in either Python, .NET, or Java. This guide will show 
you how to...\n\nInstall the necessary packages Create a back-and-forth conversation with an AI Give an AI agent the ability to run your code Watch the AI create plans on the fly\n\nSemantic Kernel has several NuGet packages available. For most scenarios, however, you typically only need Microsoft.SemanticKernel.\n\nYou can install it using the following command:\n\nBash\n\nFor the full list of Nuget packages, please refer to the supported languages article.\n\nIf you\u0027re a Python or C# developer, you can quickly get started with our notebooks. These notebooks provide step-by-step guides on how to use Semantic Kernel to build AI agents.\n\nInstalling the SDK\n\ndotnet add package Microsoft.SemanticKernel\n\nQuickly get started with notebooks",
    "Similarity": 0.19021492906584636
  },
  {
    "Id": 2,
    "PageId": 3,
    "ChunkId": 2,
    "Name": "semantic-kernel",
    "Path": "assets/semantic-kernel.pdf",
    "Text": "Tell us about your PDF experience.\n\nIntroduction to Semantic Kernel Article \u2022 06/24/2024\n\nSemantic Kernel is a lightweight, open-source development kit that lets you easily build AI agents and integrate the latest AI models into your C#, Python, or Java codebase. It serves as an efficient middleware that enables rapid delivery of enterprise-grade solutions.\n\nMicrosoft and other Fortune 500 companies are already leveraging Semantic Kernel because it\u2019s flexible, modular, and observable. Backed with security enhancing capabilities like telemetry support, and hooks and filters so you\u2019ll feel confident you\u2019re delivering responsible AI solutions at scale.\n\nVersion 1.0\u002B support across C#, Python, and Java means it\u2019s reliable, committed to non breaking changes. Any existing chat-based APIs are 
easily expanded to support additional modalities like voice and video.\n\nSemantic Kernel was designed to be future proof, easily connecting your code to the latest AI models evolving with the technology as it advances. When new models are released, you\u2019ll simply swap them out without needing to rewrite your entire codebase.\n\nEnterprise ready",
    "Similarity": 0.19971081095821264
  },
  {
    "Id": 2,
    "PageId": 401,
    "ChunkId": 413,
    "Name": "semantic-kernel",
    "Path": "assets/semantic-kernel.pdf",
    "Text": "You should also familiarize yourself with the available documentation and tutorials. This will ensure that you are knowledgeable of core Semantic Kernel concepts and features so that you can help others during the hackathon. The following resources are highly recommended:\n\nWhat is Semantic Kernel? Semantic Kernel LinkedIn training video\n\nThe hackathon will consist of six main phases: welcome, overview, brainstorming, development, presentation, and feedback.\n\nHere is an approximate agenda and structure for each phase but feel free to modify this based on your team:\n\nLength (Minutes)\n\nPhase\n\nDescription\n\nDay 1\n\n15\n\nWelcome/Introductions\n\nThe hackathon facilitator will welcome the participants, introduce the goals and rules of the hackathon, and answer any questions.\n\n30\n\nOverview of Semantic Kernel\n\nThe facilitator will guide you through a live presentation that will give you an overview of AI and why it is important for solving problems in today\u0027s world. You will also see demos of how Semantic Kernel can be used for different scenarios.\n\n5\n\nChoose your Track\n\nReview slides in the deck for the specific track you\u2019ll pick for the hackathon.\n\n120\n\nBrainstorming\n\nThe facilitator will help you form 
teams based on your interests or skill levels. You will then brainstorm ideas for your own AI plugins or apps using design thinking techniques.\n\n20\n\nResponsible AI\n\nSpend some time reviewing Responsible AI 
principles and ensure your proposal follows these principles.\n\n60\n\nBreak/Lunch\n\nLunch or Break\n\n360\u002B\n\nDevelopment/Hack\n\nYou will use Semantic Kernel SDKs tools, and resources to develop, test, and deploy your projects. This could be\n\nRunning the hackathon\n\n\uFF89\n\nExpand table",
    "Similarity": 0.1999844929883695
  },
  {
    "Id": 2,
    "PageId": 22,
    "ChunkId": 21,
    "Name": "semantic-kernel",
    "Path": "assets/semantic-kernel.pdf",
    "Text": "Understanding the kernel Article \u2022 07/25/2024\n\nThe kernel is the central component of Semantic Kernel. At its simplest, the kernel is a Dependency Injection container that manages all of the services and plugins necessary to run your AI application. If you provide all of your services and plugins to the kernel, they will then be seamlessly used by the AI as needed.\n\nBecause the kernel has all of the services and plugins necessary to run both native code and AI services, it is used by nearly every component within the Semantic Kernel SDK to power your agents. This means that if you run any prompt or code in 
Semantic Kernel, the kernel will always be available to retrieve the necessary services and plugins.\n\nThis is extremely powerful, because it means you as a developer have a single place where you can configure, and most importantly monitor, your AI agents. Take for example, when you invoke a prompt from the kernel. When you do so, the kernel will...\n\n1. Select the best AI service to run the prompt. 2. Build the prompt using the provided prompt template. 3. Send the prompt to the AI service. 4. Receive and parse the response. 5. And finally return the response from the LLM to your application.\n\nThroughout this entire process, you can create events and middleware that are triggered at each of these steps. This means you can perform actions like logging, provide status updates to users, and most importantly responsible AI. All from a single place.\n\nThe kernel is at the center of your agents",
    "Similarity": 0.21656685082075444
  }
]
---------------------------------------


To get started with Semantic Kernel, you can build your first AI agent using Python, .NET, or Java. Here's a quick guide:

1. **Install Necessary Packages**: For most scenarios, you only need the `Microsoft.SemanticKernel` package. You can install it using the command:
      dotnet add package Microsoft.SemanticKernel
   
2. **Create a Back-and-Forth Conversation with AI**: Set up your environment to enable interaction with an AI agent.

3. **Enable AI to Run Your Code**: Allow the AI agent to execute your code as part of its operations.

4. **Watch AI Create Plans on the Fly**: Observe how the AI agent generates plans dynamically based on the interactions.

For Python or C# developers, there are notebooks available that provide step-by-step guides to help you get started quickly.
```

> NOTE: Don't be disappointed when you trip it up - this is just a beginning RAG application.

## [Next: Summary >](summary.md)



