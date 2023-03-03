# Event Sourcing Ecommerce 🛒

*build statuses go here.*

[![Twitter Follow](https://img.shields.io/twitter/url?label=reach%20me%20%40Faelor&style=social&url=https%3A%2F%2Ftwitter.com%2Ffaelor)](https://twitter.com/faelor)

## What is this project?

A pragmatic solution showing how to design and build an architecture in .NET using techniques like Event Sourcing, DDD, and CQRS. The goal is to include other powerful patterns found and desired in distributed systems such as sagas / process managers, durable boxes (inbox, outbox), dead letter queues (DLQ), and others.


## Technologies, frameworks, and libraries utilized

While this project is in a work-in-progress state, it's important to note there are a few pivotal frameworks and libraries being leveraged. They have been picked for their ability to improve the developer experience (DX). That meaning they should improve productivity, improve maintainability, reduce boilerplate and focus on domain problems, and in general be as painless to use as possible.


### The Critter Stack

This solution is being developed using the "Critter Stack", which includes [Marten](https://github.com/JasperFx/marten) + [Wolverine](https://github.com/JasperFx/wolverine). More information on their integration can be found [here](https://wolverine.netlify.app/guide/durability/marten.html).


### Primary

- [Dotnet](https://dotnet.microsoft.com/)
- [PostgreSQL](https://www.postgresql.org/)
- [Marten](https://github.com/JasperFx/marten)
- [Wolverine](https://github.com/JasperFx/wolverine)
- [FluentValidation](https://github.com/FluentValidation/FluentValidation)


### Testing
-  [xUnit](https://github.com/xunit/xunit)
- [FluentAssertions](https://github.com/fluentassertions/fluentassertions)
- [Shouldly](https://github.com/shouldly/shouldly)


### Auxiliary and implicit dependencies
- [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json)


## Documentation

Coming soon.


## Roadmap

More details coming soon.

### Value Streams / Divisions / Virtual Teams

🔳 Catalog (products, pricing)
🔳 Marketplaces (listings, ordering, marketplaces)
🔳 Supply Chain (inventory, warehouses, fulfillment)
🔳 Enterprise (financing, reporting)


## Change log

Coming soon.


## Maintainer

Erik "Faelor" Shafer

blog: www.event-sourcing.dev


## License

[MIT license](./LICENSE).
