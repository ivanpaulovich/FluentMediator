## Install

```
dotnet add FluentMediator
```

or 

```
Install-Package FluentMediator
```

### Setup

```c#
services.AddFluentMediator(m => {
    m.On<PingRequest>().Pipeline()
        .Call<IPingHandler>((handler, req) => handler.MyMethod(req))
        .Call<IPingHandler>((handler, req) => handler.MyLongMethod(req));
});
```

### Publish

```c#
mediator.Publish<PingRequest>(ping);
```

### Send

```c#
PingResponse response = mediator.Send<PingResponse>(new PingRequest("Ping"));
Console.WriteLine(response.Message); // Prints "Pong"
```

## Why

## Contributors
