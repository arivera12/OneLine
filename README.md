# OneLine

OneLine is an abstracted standardized redefined framework.

## General Technical Features 

- [x] Highly Abstracted
- [x] Generic
- [x] Standardized
- [x] Eventable
- [x] Chainable
- [x] Recyclable
- [x] Overridable
- [x] Scaffolding Tooling
- [x] Pre-Implemented Enviroment
- [x] Cross Platform Support for Blazor (client/server), Xamarin, UWP, and Electron.Net (Thanks to [BlazorMobile](https://github.com/Daddoon/BlazorMobile)).
- [ ] Multiple Database Providers Support
  - [x] SQL Server 2012 onwards
  - [ ] SQLite 3.7 onwards
  - [ ] Azure Cosmos DB SQL API (Still planning for support)
  - [ ] PostgreSQL
  - [ ] MySQL
  - [ ] MariaDB
  - [ ] Oracle DB 11.2 onwards

## Client Side Core Features

- [x] Bindable
- [x] Eventable
- [x] Listenable
- [x] Chainable
- [x] Form Management 
  - [x] Load
  - [x] Validate
  - [x] Save (create & update)
  - [x] Reset
  - [x] Cancel
  - [x] Delete
  - [x] Form State Management
  - [x] Blob Management
  - [x] Request and Response Management
  - [x] Single and Multiple State Management  
- [x] Data View Management
  - [x] Load
  - [x] Search
  - [x] Select Record/s
  - [x] Records Selection Mode (single or multiple)
  - [x] Filtering
  - [x] Sorting
  - [x] Paged Data Management
    - [x] Previous & next page
    - [x] Page index and size
    - [x] Page sort by
  - [x] Collection Mode Management (append or replace)
  - [x] Request and Response Management
  
## Client Side Blazor Features

- [x] Pre-Implemented Users Account Basic Management
- [x] Application State Management
  - [x] User Info Storage Management (Encrypted by default)
    - [x] Session
    - [x] Persistent
  - [ ] Session Token Lifetime Management
- [x] Multi Language Support using Resource Files
- [x] Translator Component
- [x] Enviroment Chooser
- [x] In-Memory Configuration File
- [x] Anonymous and Authorized Base Classes for Forms and Data Views

## Server Side API Features
- [x] Pre-Implemented Users Account Basic Management
- [x] Services are pre-registered
- [x] Everything is secured and available for admins only by default
- [x] Request data is pre validate before arriving the controller method
- [x] Response has a base API Response Format
- [x] Search results are always paged by default
- [x] Repository and Service patterns have been redefined and reimplemented as Database Context Extended as Repository Service
- [x] CRUD methods are pre implemented supports single and multiple
- [x] Import data uploading *.csv file for create or update operations.
- [x] Exporting data in *.csv file.
- [x] Multiple Blob Storage Provider Support (Thanks to [Storage.Net](https://github.com/aloneguid/storage)).
  - [x] Microsoft Azure
    - [x] Blob Storage
    - [x] File Storage
    - [x] Data Lake Gen 1
    - [x] Data Lake Gen 2
    - [x] Storage Queue
    - [x] Event Hubs
    - [x] Service Bus
  - [x] Amazon Web Services
    - [x] Simple Storage Service (S3)
    - [x] Simple Queue Service (SQS)
  - [x] Google Cloud Platform
    - [x] Cloud Storage
  - [x] Misc
    - [x] Azure Databricks DBFS
    - [x] Service Fabric Reliable Collections
    - [x] Azure Key Vault
    - [x] Local Disk (Blobs, Messaging)
    - [x] Zip Archive (Blobs)
    - [x] In-Memory (Blobs, Messaging)
    - [x] FTP (Blobs)
- [x] Blobs Management is centralized
  - [x] Blobs doesn't never need a child tables to support multiple file upload references
  - [x] Define and apply rules to the expected blob
- [x] Server exceptions are pre-handled and recorded
- [x] Every operation is auto audited with option of rolling back any operation at any time

## Client And Server Shared Features
- [x] Models
- [x] Validations
- [x] Http Services
- [x] Language Translations (English and Spanish (Partially))

### Known Random Issues on Visual Studio

#### When web client and web api applications starts the first time crashes and closes unexpectedly without throwing any error.

This error sometimes happens randomly the first time only, just run it again and it should start after that.

#### Swagger Api documentation not working and throws an error on the web page.

This error sometimes happens randomly, to fix it delete the `.vs` folder on the solution path and run again it should work after that.
