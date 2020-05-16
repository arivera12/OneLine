# OneLine

OneLine is an abstraction standardized redefined framework.

## OneLine is still under heavy development and testing. 

It will be completed with documentation to use very soon.

## What is OneLine goal?

OneLine starts development from a pre-implemented and pre-applied environment.

## How OneLine framework differs from the others frameworks?

OneLine tries to abstract development efforts breaking common development patterns in a redefined way. 

### But how? 

Just by starting off from general and specific applications behaviors.

### How this can be achieve? 

1. Defining generic interfaces/headers.
2. Applying the appropriate implementations.
3. Use actions and/or events callbacks between a task.
4. Use actions before and/or after any specific task execution inside a task.

### What are this actions and/or events?

This actions and/or events are just callbacks that can be listened to perform derived actions or Tasks.
 
This is were we decide what actions and/or Tasks to perform Before, On, or After any Task.

All actions and/or events should be classified as a general or particular case use.

Take note that OneLine may use open source libraries to achieve resolving tasks more easier and faster.

### What is the key?

The key are callbacks and overridable methods/functions.

## Known Random Issues

### When web client and web api applications starts the first time crashes and closes unexpectedly without throwing any error.

This error sometimes happens randomly the first time only, just run it again and it should start after that.

### Swagger Api documentation not working and throws an error on the web page.

This error sometimes happens randomly, to fix it delete the `.vs` folder on the solution path and run again it should work after that.
