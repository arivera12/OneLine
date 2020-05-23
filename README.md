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
- [x] Cross Platform Support for Blazor (client/server), Xamarin, UWP, and Electron.Net.
- [ ] Multiple Database Providers Support
  - [x] SQL Server 2012 onwards
  - [ ] SQLite 3.7 onwards
  - [ ] Azure Cosmos DB SQL API
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
  - [x] Single and Multiple State Management
  
## Client Side Blazor Features

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

## Known Random Issues

### When web client and web api applications starts the first time crashes and closes unexpectedly without throwing any error.

This error sometimes happens randomly the first time only, just run it again and it should start after that.

### Swagger Api documentation not working and throws an error on the web page.

This error sometimes happens randomly, to fix it delete the `.vs` folder on the solution path and run again it should work after that.
