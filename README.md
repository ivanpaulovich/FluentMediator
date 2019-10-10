## Install
[![All Contributors](https://img.shields.io/badge/all_contributors-2-orange.svg?style=flat-square)](#contributors)

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

<!-- ALL-CONTRIBUTORS-LIST:START - Do not remove or modify this section -->
<!-- prettier-ignore -->
<table>
  <tr>
    <td align="center"><a href="https://paulovich.net"><img src="https://avatars3.githubusercontent.com/u/7133698?v=4" width="100px;" alt="Ivan Paulovich"/><br /><sub><b>Ivan Paulovich</b></sub></a><br /><a href="https://github.com/ivanpaulovich/FluentMediator/commits?author=ivanpaulovich" title="Code">💻</a> <a href="#design-ivanpaulovich" title="Design">🎨</a> <a href="https://github.com/ivanpaulovich/FluentMediator/commits?author=ivanpaulovich" title="Tests">⚠️</a></td>
    <td align="center"><a href="http://www.carselind.se"><img src="https://avatars1.githubusercontent.com/u/439028?v=4" width="100px;" alt="Joakim Carselind"/><br /><sub><b>Joakim Carselind</b></sub></a><br /><a href="#review-joacar" title="Reviewed Pull Requests">👀</a> <a href="#ideas-joacar" title="Ideas, Planning, & Feedback">🤔</a></td>
  </tr>
</table>

<!-- ALL-CONTRIBUTORS-LIST:END -->