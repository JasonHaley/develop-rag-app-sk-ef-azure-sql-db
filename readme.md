# Build a RAG App using Semantic Kernel, Entity Framework and Azure SQL DB

This hands on lab explores how to use the new public preview of [Azure SQL's Native Vector Support](https://devblogs.microsoft.com/azure-sql/exciting-announcement-public-preview-of-native-vector-support-in-azure-sql-database/) with Semantic Kernel and Entity Framework Core to build a retrieval augmented generation (RAG) app. 

## Introduction

Many experienced .NET developers are using Azure SQL for their day-to-day work on business applications. As these developers begin to retool and pick up new Generative AI skills, up until recently they have been disappointed with having to adopt and learn a new database just to store vector embeddings. As of November 6, 2024 [Azure SQL announced the public preview of native vector support](https://devblogs.microsoft.com/azure-sql/exciting-announcement-public-preview-of-native-vector-support-in-azure-sql-database/). This means .NET developers can now focus on getting hands on learning, prototyping and building GenAI apps using Azure SQL as the database to put their embeddings.

> NOTE: as of the writing of this lab, the Native Vector Support is in Public Preview.

Developing applications that have Generative AI functionality tend follow a 33/33/33 rule:

* 1/3 of the develop time is spent focusing on data preparation 
* 1/3 of the time is spent on the actual Generative AI logic
* 1/3 of the time is regular .NET application development

This lab starts with creating and configuring an Azure SQL database. You then get started with the .NET code. We provide a minimal console application for a starting point. The first phase is to create the database tables, configure the model to use the new VECTOR type, then create the code to ingest the PDF we will use for the test data. The second phase you will build out the application logic using Semantic Kernel and Entity Framework Core to retrieve similar data (performing a similarity search in Azure SQL) to use in a RAG chat bot loop. By the end you'll have a chat bot which you can use to ask the test data questions in natural language.

## Prerequisites

* Experience working with the Azure Portal
* Basic knowledge of creating resources in Azure
* Experience working with [Entity Framework Core migrations](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli)

## What you will need

* Azure Subscription - [you can create a Free account](https://azure.microsoft.com/en-us/pricing/purchase-options/azure-account?icid=azurefreeaccount&azure-portal=true) if you don't have one.
    * Ability to create a new Azure SQL Server and DB
    * Ability to create Azure Open AI resource and deployments
* VS Code (Visual Studio will work but right now the steps are written for VS Code)
* [Entity Framework Core .NET Core CLI tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet#installing-the-tools)
* SQL Server Management Studio

## Learning Objectives

1. Create an Azure SQL DB and configure the application to use Vector search using Entity Framework
2. Implement code to ingest PDF's into the Azure SQL DB along with their vector embeddings
3. Configure and create Semantic Kernel plugins to search the database
4. Use all the above to build a chat bot that will answer questions from PDF contents

## [Next: Get started with the database and test data >](part1-1.md)