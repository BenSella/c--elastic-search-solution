# ElasticSearchSolution

## Overview

This project demonstrates how to interact with Elasticsearch using the `Elastic.Clients.Elasticsearch` package. 
It provides basic functionality to index, retrieve, and search documents in Elasticsearch, 
along with a health check to monitor the Elasticsearch service.

## Project Structure

ElasticSearchSolution/
├── Controller/
│   └── SimpleElasticTestController.cs        # Contains API endpoints for indexing, retrieving, and searching documents in Elasticsearch
├── HealthTest/
│   └── ElasticHealthCheck.cs                 # Health check for Elasticsearch connection
├── Utils/
│   ├── Interfaces/
│   │   └── IElasticSearchClient.cs           # Interface defining methods for Elasticsearch operations
│   └── ElasticSearchClient.cs                # Implementation of the Elasticsearch client, interacting with Elasticsearch APIs
├── Program.cs                                # Main entry point for the application, including service registration and health checks
├── ElasticSearchSolution.csproj              # Project file for building the solution
└── README.md