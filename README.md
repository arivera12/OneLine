# OneLine (Development, beta coming soon)
<p>
    	<a href="https://www.nuget.org/packages/OneLine">
         <img src="https://buildstats.info/nuget/OneLine?v=2.0.89" />
     </a>
     <a href="https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=RSE2NMEG3F7QU&source=url">
         <img src="https://img.shields.io/badge/Donate-PayPal-green.svg" />
     </a>
</p>

OneLine is a multiplatform, standardized, redefined framework.

## General Technical Features 

- [x] Standardized
- [x] Eventable
- [x] Listenable
- [x] Chainable
- [x] Overridable
- [x] Recyclable

## Multi platform solution Scaffolding Tool  (Commercially available)
- [x] RAD (Rapid Application Development) and Prototyping
- [x] WORE (Write once, run everywhere)
- [x] Multi platform deployment support (Thanks to [MobileBlazorBindings](https://github.com/dotnet/MobileBlazorBindings))
  - [x] Web Api 
  - [x] Blazor WAsm 
  - [x] Blazor Server
  - [x] Android 4.4 onwards
  - [x] iOS 12.0 onwards
  - [x] UWP Build 16299 onwards (Universal Windows Platform)
  - [ ] Windows (Coming Soon)
  - [ ] MacOS (Coming Soon)
  - [ ] Linux (Coming Soon)
- [ ] Multiple Database Providers Support
  - [x] SQL Server 2012 onwards
  - [ ] SQLite 3.7 onwards
  - [ ] Azure Cosmos DB SQL API (Still planning for support)
  - [ ] PostgreSQL
  - [ ] MySQL
  - [ ] MariaDB
  - [ ] Oracle DB 11.2 onwards

### Managements and Pre-implementations
- [x] User account management
- [x] Localized Emails Management
- [x] Authorization and authentication management
- [x] Default security pre implemented (Admin only)
- [x] Claims based security pre implemented
- [x] Interfaces, models and view models implementations
- [x] Localized validations implementation
- [x] Resource files with translations
  - [x] Resource.resx (Default English)
  - [x] Resource.en-US.resx
  - [x] Resource.es-PR.resx
- [x] Translations and localization management server and client side withing the app and the user account
- [x] Http Services with user token management and integration
- [x] Message and Notifications management using SignalR (In-App only at the moment) 
- [x] User and Application State Management
- [x] User Info Storage Management (Encrypted by default)
  - [x] Session
  - [x] Persistent
- [ ] Session Token Lifetime Management
  - [ ] Ask for More Session Time
  - [ ] Auto Renew Session Token on User Confirm
  - [ ] Redirect on Session Expired Client time out
  - [x] Redirect when Session is Expired in a User Action after Server Response 
- [x] Configuration file management
- [x] Configuration file chooser per enviroment management (Debug (Development) or Release (Production))
- [x] Pre-implemented localized core base classes for forms and data views per every table
  - [x] *CardViewComponent.razor
  - [x] *DetailsComponent.razor
  - [x] *FormViewComponent.razor
  - [x] *IndexViewComponent.razor
  - [x] *ListViewComponent.razor
  - [x] *ModalOptionsComponent.razor
  - [x] *TableViewComponent.razor
  - [x] *TypeaheadComponent.razor
- [x] Every UI component is editable as needed
- [x] Device Oriented UI/UX
  - [x] Desktop
  - [x] Tablet
  - [x] Mobile
- [x] Bootstrap 4 Template
- [x] Pre-Implemented Service Enviroment (Controllers and Repositories)
- [x] Rest Arquitecture (JSON)
- [x] Services are pre-registered per enviroment or platform
- [x] Request data is validated always using it's default validator
- [x] Response has a base api response format
- [x] Search results are always paged by default
- [x] Repository and Service patterns have been redefined and reimplemented as Repository Service
- [x] CRUD methods are pre implemented supports single and multiple, also with performance multiple method.
- [ ] Import data uploading *.csv file for create or update operations
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
- [x] SDK export available to share from base project for .net

## Default framework views implementation
- [x] Login
- [x] Register
- [x] Forgot password
- [x] Reset password
- [x] Translate Component
- [x] Confirm Email/Account
- [x] Users account management
- [x] Audit trails
- [x] Exception logs
- [x] Supported cultures
- [x] Notification messages
- [x] User blobs
- [x] Smtp pre implementation and integration with localized basic email templates
- [x] File component
- [x] Navigation sidebar menu component with icons
- [x] Empty layout
- [x] Log out

## Multi Platform Services
- [x] Application configuration
- [x] Application configuration source
- [x] Application State
- [x] Device
- [x] Device Storage
- [x] Notification
- [x] Resource manager localizer
- [x] Save file
- [x] Supported cultures

## Blazor Core Features

- [x] Pre implmented base class for forms and fata views
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
  
## Blazor Extended Core Features

- [x] Chained components behavior (behavior like stepper)
  - [x] Forms (Back or Save/Next)
  - [x] CRUD (Create, Read, Update, Delete operations) (back and/or next) 
  - [x] Data Views (Select single or multiple, back and/or next)

## Roadmap

- Push notifications and notifications history Management (Cross Platform)
  - In-App notification and messages si available for now
- In-App Blob Storage Manager (Like any web drive service like dropbox, google drive, one drive, etc)
  - Read only view is available for now
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
- In-Component user defined (persistent or by expiration date) notification alerts
- Create a translator service inside the tool to translate resource files from one language to another
- Chat (Cross Platform)
  - From clients to app/service representative
  - Between users
  - Groups
- App/service representative chat with connected clients
- User invites other users to manage profile including access permissions management 
- Confirm action by password

  
### Known Random Issues on Visual Studio

#### When web client and web api applications starts the first time crashes and closes unexpectedly without throwing any error.

This error sometimes happens randomly the first time only, just run it again and it should start after that.

#### Swagger Api documentation not working and throws an error on the web page.

This error sometimes happens randomly, to fix it delete the `.vs` folder on the solution path and run again it should work after that.
