# OneLine (Development, beta coming soon)
<p>
    	<a href="https://www.nuget.org/packages/OneLine">
         <img src="https://buildstats.info/nuget/OneLine?v=2.0.89" />
     </a>
     <a href="https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=RSE2NMEG3F7QU&source=url">
         <img src="https://img.shields.io/badge/Donate-PayPal-green.svg" />
     </a>
</p>

OneLine is an abstracted standardized redefined framework.

## General Technical Features 

- [x] Highly Abstracted
- [x] Generic
- [x] Standardized
- [x] Eventable
- [x] Listenable
- [x] Chainable
- [x] Overridable
- [x] Recyclable
- [x] Scaffolding Tooling (Commercially available soon)
  - [x] RAD (Rapid Application Development) and Prototyping
  - [x] WORE (Write once, run everywhere)
  - [x] Multi Platform Deployment Support (Thanks to [BlazorMobile](https://github.com/Daddoon/BlazorMobile))
    - [x] Blazor WASM and Server
    - [x] Xamarin
      - [x] Android 4.4 onwards
      - [x] iOS 12.0 onwards
      - [x] UWP Build 16299 onwards (Universal Windows Platform)
    - [x] Windows 7 onwards using UWP or Electron.Net
    - [x] MacOS 10.10 (Yosemite) onwards using Electron.Net
    - [x] Linux using Electron.Net
      - [x] Ubuntu 12.04 onwards
      - [x] Fedora 21 onwards
      - [x] Debian 8 onwards
  - [ ] Multiple Database Providers Support
    - [x] SQL Server 2012 onwards
    - [ ] SQLite 3.7 onwards
    - [ ] Azure Cosmos DB SQL API (Still planning for support)
    - [ ] PostgreSQL
    - [ ] MySQL
      - [ ] MariaDB
      - [ ] Oracle DB 11.2 onwards
  - [x] Application State Management
  - [x] User Info Storage Management (Encrypted by default)
    - [x] Session
    - [x] Persistent
  - [ ] Session Token Lifetime Management
    - [ ] Ask for More Session Time
    - [ ] Auto Renew Session Token on User Confirm
    - [ ] Redirect on Session Expired Client time out
    - [x] Redirect when Session is Expired in a User Action after Server Response 
  - [x] Multi Language Support using Resource Files
  - [x] Translator Component
  - [x] Enviroment Chooser
  - [x] In-Memory Configuration File
  - [x] Pre-Implemented Core Base Classes for Forms and Data Views
    - [x] Anonymous
    - [x] Authorized
    - [x] Authorized by Roles
  - [x] Device Oriented UI/UX
    - [x] Desktop
    - [x] Tablet
    - [x] Mobile
  - [x] Skin Mode
    - [x] Light
    - [x] Dark
  - [x] Pre-Implemented Service Enviroment (Controllers and Repositories)
  - [x] Rest Arquitecture (JSON)
  - [x] Pre-Implemented Users Account Basic Management
  - [x] Services are pre-registered
  - [x] Everything is secured and available for admins only by default
  - [x] Request data is pre validate before arriving the controller method
  - [x] Response has a base API Response Format
  - [x] Search results are always paged by default
  - [x] Repository and Service patterns have been redefined and reimplemented as Database Context Extended as Repository Service
  - [x] CRUD methods are pre implemented supports single and multiple
  - [x] Import data uploading *.csv file for create or update operations
  - [x] Exporting data in *.csv file
  - [x] Multiple Blob Storage Provider Support (Thanks to [Storage.Net](https://github.com/aloneguid/storage))
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
  - [x] Blobs Management is centralized and auto managed
    - [x] Blobs doesn't never need a child tables to support multiple file upload references
  - [x] Server exceptions are pre-handled and recorded
  - [x] Every operation is auto audited with option of rolling back any operation at any time
  - [x] Data BaseContext
  - [x] Models
  - [x] Validations
  - [x] Http Services
  - [x] Form File Validations Rules
  - [x] Language Translations (English and Spanish (Extraction from DB))
  - [x] SDK for .net

## Core Features

- [x] Core Base Class for Forms and Data Views
  - [x] Load (with before and after actions)
  - [x] Validate (using fluentvalidation)
  - [x] Save (create & update) (with before and after actions)
  - [x] Reset (with before and after actions)
  - [x] Cancel (with before and after actions)
  - [x] Delete (with before and after actions)
  - [x] Form State Management (create, update, details, copy, delete & deleted)
  - [x] Blob Management
  - [x] Request and Response Management
  - [x] Single and Multiple State Management  
  - [x] Search (with before and after actions)
  - [x] Select Record/s (with before and after actions)
  - [x] Records Selection Mode (single or multiple)
  - [x] Minimum and Maximun Selection Range Management
  - [x] Minimun and Maximun Selection Reach Listenable
  - [x] Filtering (client and server)
  - [x] Sorting (client and server)
  - [x] Paged Data Management
  - [x] Go Previous Page (client and server)
  - [x] Go Next Page (client and server)
  - [x] Go to Page Index (client and server)
  - [x] Page Size (client and server)
  - [x] Page Sort By (client and server)
  - [x] Collection Mode Management (append or replace)
  - [x] Request and Response Management
- [x] Base DataBase Context
- [x] Base Repository Extension Methods
  
## Client Side Blazor Extending Core Features

- [x] Chained components behavior (behavior like stepper)
  - [x] Forms (Back or Save/Next)
  - [x] CRUD (Create, Read, Update, Delete operations) (back and/or next) 
  - [x] Data Views (Select single or multiple, back and/or next)

## Roadmap

- Push notifications and notifications history Management (Cross Platform)
  - App/service broadcasting
- In-App Blob Storage Manager (Like any web drive service like dropbox, google drive, one drive, etc)
- In-Component user defined (persistent or by expiration date) notification alerts
- Create a translator service inside the tool to translate resource files
- Chat (Cross Platform)
  - From clients to app/service representative
  - Between users
  - Groups
- App/service representative chat with connected clients
- Admin creates user/admin and manage access permissions
- User invites other users to manage profile including access permissions management 
- Confirm action by password
- Cross Platform Device API's Implementations for Browser, Electron.Net and Xamarin. 
  - Accelerometer
  - App Information
  - App Theme
  - Barometer
  - Battery
  - Clipboard
  - Color Converters
  - Compass
  - Connectivity
  - Detect Shake
  - Device Display Information
  - Device Information
  - Email
  - File System
  - Flashlight
  - Geocoding
  - Geolocation
  - Gyroscope
  - Launcher
  - Magnetometer
  - MainThread
  - Maps
  - Open Browser
  - Orientation Sensor
  - Permissions
  - Phone Dialer
  - Platform Extensions
  - Preferences
  - Secure Storage
  - Share
  - SMS
  - Text-to-Speech
  - Unit Converters
  - Version Tracking
  - Vibrate
  - Web Authenticator
  
### Known Random Issues on Visual Studio

#### When web client and web api applications starts the first time crashes and closes unexpectedly without throwing any error.

This error sometimes happens randomly the first time only, just run it again and it should start after that.

#### Swagger Api documentation not working and throws an error on the web page.

This error sometimes happens randomly, to fix it delete the `.vs` folder on the solution path and run again it should work after that.
