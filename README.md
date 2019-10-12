# FluentMediator
[![Build Status](https://ivanpaulovich.visualstudio.com/FluentMediator/_apis/build/status/ivanpaulovich.FluentMediator?branchName=master)](https://ivanpaulovich.visualstudio.com/FluentMediator/_build/latest?definitionId=24&branchName=master) [![All Contributors](https://img.shields.io/badge/all_contributors-2-orange.svg?style=flat-square)](#contributors) ![GitHub issues](https://img.shields.io/github/issues/ivanpaulovich/FluentMediator) ![GitHub pull requests](https://img.shields.io/github/issues-pr/ivanpaulovich/FluentMediator)

:twisted_rightwards_arrows: We will not require you to add dependencies into your domain or to implement frameworks interfaces in your event handlers. Finally a really loosely coupled mediator library was born.

## Install

```
Install-Package FluentMediator
```

For seameless .NET Core integration:

```
Install-Package FluentMediator.Microsoft.Extensions.DependencyInjection
```

## How

Setup your events and pipelines using depedency injection. You can be very creative here! It supports async methods and cancellable tokens parameters, you could append multiple steps select one to return a message. An example:

```c#
services.AddFluentMediator(builder => {
    builder.On<PingRequest>().Pipeline()
        .Call<IPingHandler>((handler, request) => handler.MyMethod(request))
        .Call<IPingHandler>((handler, request) => handler.MyLongMethod(request))
        .Return<PingResponse, IPingHandler>(
            (handler, request) => handler.MyOtherMethod(request)
        );
});
```

Then you are able to use the injected `IMediator` interface.

### Publishing Events

```c#
// Puts the message in the pipeline, calls three handlers.
mediator.Publish<PingRequest>(ping);
```

### Sending Commands and Queries

```c#
// Calls the three handlers then get the response from `MyOtherMethod(PingRequest)`.
PingResponse response = mediator.Send<PingResponse>(new PingRequest("Ping"));
Console.WriteLine(response.Message); // Prints "Pong"
```

## Why

When designing *CQRS* or *Event-Driven* applications we need to publish events from the infrastructure layer into the *domain event handlers*. We do not want framework dependencies leaking out to the Model then **FluentMediator** was born. 

## Contributors

<!-- ALL-CONTRIBUTORS-LIST:START - Do not remove or modify this section -->
<!-- prettier-ignore -->
<table>
  <tr>
    <td align="center"><a href="https://paulovich.net"><img src="https://avatars3.githubusercontent.com/u/7133698?v=4" width="100px;" alt="Ivan Paulovich"/><br /><sub><b>Ivan Paulovich</b></sub></a><br /><a href="https://github.com/ivanpaulovich/FluentMediator/commits?author=ivanpaulovich" title="Code">💻</a> <a href="#design-ivanpaulovich" title="Design">🎨</a> <a href="https://github.com/ivanpaulovich/FluentMediator/commits?author=ivanpaulovich" title="Tests">⚠️</a></td>
    <td align="center"><a href="http://www.carselind.se"><img src="https://avatars1.githubusercontent.com/u/439028?v=4" width="100px;" alt="Joakim Carselind"/><br /><sub><b>Joakim Carselind</b></sub></a><br /><a href="#review-joacar" title="Reviewed Pull Requests">👀</a> <a href="#ideas-joacar" title="Ideas, Planning, & Feedback">🤔</a></td>
  </tr>
</table>

<!-- ALL-CONTRIBUTORS-LIST:END -->

Please contribute with [Issues](https://github.com/ivanpaulovich/FluentMediator/issues), [Wiki](https://github.com/ivanpaulovich/FluentMediator/wiki) and [Samples](https://github.com/ivanpaulovich/FluentMediator/tree/master/samples).
