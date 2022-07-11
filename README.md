# OneLine
<p>
    	<a href="https://www.nuget.org/packages/OneLine" target="_blank">
         <img src="https://buildstats.info/nuget/OneLine?v=3.0.108" />
     </a>
</p>

OneLine is a multiplatform, standardized, redefined framework.

## <a href="https://www.youtube.com/watch?v=7CZL-Jo6TAE" target="_blank">See OneLine in Action!</a>

## General technical features 

- [x] Service Arquitecture (Dependency Injection & Inversion of Control)
  - [x] HttpClient's
  - [x] DbContext
  - [x] Smtp
  - [x] BlobStorage
  - [x] Repositories
  - [x] Authorization
  - [x] Authentication
  - [x] SignalR client and server hub context
  - [x] Resource manager localizer client and server
  - [x] Localized label, text and messages
  - [x] AuditTrails
  - [x] System exception logs   
- [x] Componentization
- [x] Single Page Application Arquitecture
- [x] RESTful API 
- [x] Standardized
- [x] Eventable
- [x] Listenable

## OneLine multi platform solution scaffolding tool features  (Commercially available)
- [x] RAD (Rapid Application Development) and Prototyping
- [x] WORE (Write once, run everywhere)
- [x] Multi platform deployment support (Thanks to [MobileBlazorBindings](https://github.com/dotnet/MobileBlazorBindings))
  - [x] Web Api 
  - [x] Blazor WAsm 
  - [x] Blazor Server
  - [x] Android 4.4 onwards
  - [x] iOS 12.0 onwards
  - [ ] Windows Desktop and WPF (Under Development)
  - [ ] MacOS Desktop (Under Development)
  - [ ] Linux (Planning TBA)
- [x] Database Providers Supported Over Entity Framework Core
  - [x] SQL Server 2012 onwards
  - [x] SQLite 3.7 onwards
  - [x] PostgreSQL
  - [x] MySQL
  - [x] MariaDB
  - [x] Oracle DB 11.2 onwards

Any other database engines can be supported as requested per need.

### Managements and Pre-implementations
- [x] User account management
- [x] Localized Emails Management
- [x] Authorization and authentication management using JWT
- [x] Default security pre implemented (Admin only)
- [x] Fully claims based security pre implemented
- [x] Interfaces, models, view models and search criteria model implementations
- [x] Localized validations implementation
- [x] Resource files with translations
  - [x] Resource.resx (Default English)
  - [x] Resource.en-US.resx
  - [x] Resource.es-PR.resx
- [x] Translations and localization management server and client side withing the app and the user account
- [x] Http Services with user token management and api integration
- [x] Message and Notifications management using SignalR (In-App only at the moment) 
- [x] User and Application State Management
- [x] User Info Storage Management (Encrypted by default)
  - [x] Session
  - [x] Persistent
- [ ] Session Token Lifetime Management
  - [x] Auto Renew Session Token when Session is Expired in a User Action after Server Response
  - [ ] Ask for More Session Time
  - [x] Redirect on Session Expired Client time out 
- [x] Configuration file management
- [x] Configuration file chooser per enviroment management (Debug (Development), Staging (Test) or Release (Production))
- [x] Pre-implemented localized core base classes for forms and data views per every table
  - [x] *CardViewComponent.razor
  - [x] *DetailsComponent.razor
  - [x] *FormViewComponent.razor
  - [x] *IndexViewComponent.razor
  - [x] *ListViewComponent.razor
  - [x] *ModalOptionsComponent.razor
  - [x] *TableViewComponent.razor
  - [x] *TypeaheadComponent.razor
- [x] Every UI component withing the same table context shares the same base UI class
- [x] Every UI component is editable and extendable as needed
- [x] Device Oriented UI/UX
  - [x] Desktop
  - [x] Tablet
  - [x] Mobile
- [x] Bootstrap 4 Template
- [x] Pre-Implemented Controllers with authorization and authentication
- [x] Pre-Implemented Repositories as services and pre registered service container
  - [x] Repository and Service patterns have been redefined and reimplemented as Repository Service using Base Api Context Service
- [x] RESTful API Arquitecture (JSON, XML and CSV)
- [x] .csv and .xlsx document templates for posting to the API per controller
- [x] Services are pre-registered per enviroment or platform
- [x] Response has a base api response format
- [x] Search results are always paged by default
- [x] CRUD methods are pre implemented supports single and multiple, also with performance multiple method.
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
- [x] Blobs doesn't never need a child table to support multiple file upload references
  - [x] Define a binary column field on your table and the tool will manage it for you saving the file reference in the binary column and the file in the physical storage.
- [x] Server exceptions are pre-handled and recorded
- [x] Every operation is auto audited with option of rolling back any operation at any time
- [x] SDK export available to share from base project for .net

### Default framework views implementation
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

### Multi Platform Services
- [x] Application configuration
- [x] Application configuration source
- [x] Application State
- [x] Device
- [x] Device Storage
- [x] Notification
- [x] Resource manager localizer
- [x] Save file
- [x] Supported cultures

### Blazor Core Features

- [x] Pre implemented base class for forms and data views
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
  
### Blazor Extended Core Features

- [x] Chained components behavior (behavior like stepper) with a few source modifications
  - [x] Forms (Back or Save/Next)
  - [x] CRUD (Create, Read, Update, Delete operations) (back and/or next) 
  - [x] Data Views (Select single or multiple, back and/or next)

## Let's talk about the benefits of the OneLine multi platform solution scaffolding tool
- Reduces and cuts development time and deliverables.
- Reduces and cuts the numbers of developers or team.
- Reduces and cuts development bugs or errors margin to or almost 0% (I have done myself a few dozen of projects without any bugs or errors of any class).
- Increase developers productivity starting from 500% to 1200% (This is my estimates based on how slow or fast I have delivered a project). 
- The development from scratch application real estimated time will be cut from 62% to 84% (You will only need to develop for 38% to 26% of the project).
- Extreme fast, optimized and high level development.
- Less coding to resolve general or specific software patterns.
- Less code means easier to maintain.
- What are the things I need to do or should take care starting from this advantage point?
  - Adjust labels text and translations on forms and data views (This will be improved in the near future but what you will get out of the box is still great).
  - Apply business and security rules to the web api and the web user interface.
  - Third party api/service integrations.
  - Dashboards and/or reports.

### DON'T REPEAT YOURSELF.

### DON'T WRITE CODE WITH STRESS OR WORKLOAD ANYMORE.

### WRITE CODE WITH PEACE OF MIND, SPEED AND CONFIDENCE.

### WORK SMART NOT HARD.

For questions, quote or <a href="https://www.youtube.com/watch?v=oqxUwLM5_hc" target="_blank">demo</a> of the service you can contact me via email at anthony.revocodez@gmail.com

Quotes are based on a tool which reverse engineering your database and estimates based on the tables, fields and table relationship design.

### Pre Requisites of OneLine Tool
- LibreTranslate web server on local/remote network (docker is what I use and should be prefered)
- EF Core Tools (dotnet tool install --global dotnet-ef --version 5.0.15)

## Roadmap

- Push notifications and notifications history Management (Cross Platform)
  - In-App notification and messages is available for now
- In-App Blob Storage Manager (Like any web drive service like dropbox, google drive, one drive, etc)
  - Read only view is available for now
- Cross Platform Device API's Implementations for Browser and Xamarin. 
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

## Developer Farewell Note
	
It has been a lifetime for me to work as a developer, as an employee as well as a professional service provider, but it is very sad to have been working for the last 12 years on more than 40+ projects in the banking industry, payment processing, government applications, web servers, databases, reports, web and mobile applications, github contributions in different projects including my personal ones and never see economic growth.

I have been exploited, I have even done developments that gave me half or less or they never paid me even under contracts.

Once, some time ago, I developed an application to serve and help citizens and the same government ended up giving the idea to a third service provider who developed it and were the ones who sold it to them and to other entities.

That filled me with a lot of anger and frustration because I wasted 1 year of my life for nothing, just like the 12 that I currently have.

I have always had good will together with many ideas of how to change the way we develop, but nobody has given me the opportunity to be heard and I have never had a problem that I could not solve, because I solve problems by the nature of my profession.

My idea of ​​programming templates and functionalities has been an idea that took me about 8 years to perfect and that would save any entity millions in development costs but nobody seems to see the fruit of the effort I have put into it.

I have submitted my development tool and no one seems to be interested, I have contacted microsoft several times, I have contacted PRITS several times and have never been answered.

I tried to sell my product on various sites and well I have reached a point in my life where I am very frustrated, unfocused and no longer feel love or passion for what I do.

I have completely lost interest in everything in life and honestly I have a family to support and I have lost what little I had when I have never had anything in my life.

I come from a poor and dysfunctional family who have never supported me.

That is not why you have to follow the same negative pattern.

You and all of us can make a difference, but when you are poor the things around you are almost like a curse.

It takes much more than good ideas to be successful, you have to have connections, you have to have a good presentation, you have to be tactful when speaking and know how to sell, you have to know how to implement things correctly by phase, you have to have a reputation for everything.

Surround yourself with positive people who are willing to help you or they are not affected by seeing you grow but that is where I have unintentionally failed.

Family circles and friends who have never given me any help or support and there is nothing worse than looking back and seeing how long I have walked alone, then I look at the present and I am still just as alone and there is no way to progress in that way.

You have to open your eyes, take a deep breath, see things as they are and know when to retire with dignity.

I don't know if I'll be back tomorrow, I just don't know.

I only leave this note here for interested developers to contribute.

I will leave everything there public and transparent as I have always been.

Life is hard, and when you cling to something that is hurting you, just let it go and seek to change your path.

I wish that many people benefit from my contributions and ideas, that at least my lost time will save time for other developers.

Of what one day was a dream for me, has been thrown into darkness.
