# Event Sourcing Ecommerce 🛒

*build statuses go here.*

[![Twitter Follow](https://img.shields.io/twitter/url?label=reach%20me%20%40Faelor&style=social&url=https%3A%2F%2Ftwitter.com%2Ffaelor)](https://twitter.com/faelor)

## What is this repository?

**TL;DR:** A collection of event sourcing knowledge.

The code in this repository is meant to show how an ecommerce backend can be built using event sourcing and other related techniques, patterns, and technologies.

## Technologies, frameworks, libraries, and tools utilized

Dependencies brought in are to reduce boilerplate, avoid reinventing-the-wheel, and help improve the developer experience (DX). That is, they should help with maintainability and readability.

### Primary

- [.NET](https://dotnet.microsoft.com/), AKA [dotnet](https://dotnet.microsoft.com/) (using [C#](https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/))
- [EventStoreDB](https://eventstore.com/eventstoredb)
- [Eventuous](https://eventuous.dev/)

### Testing
-  [xUnit](https://github.com/xunit/xunit)
- [FluentAssertions](https://github.com/fluentassertions/fluentassertions)
- [Shouldly](https://github.com/shouldly/shouldly)

### Auxiliary and implicit dependencies
- [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json)
- [FluentValidation](https://github.com/FluentValidation/FluentValidation)


## Documentation

Coming soon.


## Roadmap

More details coming soon.

### Value Streams / Divisions / Virtual Teams / Departments / etc

Names are structure are based on personal experiences in ecommerce but do not necessarily reflect the inner workings of specific systems used in the past.

- **Catalog**
  - products
  - pricing
- **Marketplaces**
  - listings
  - ordering
  - marketplaces
- **Supply Chain**
  - inventory
  - warehouses
  - fulfillment
- **Enterprise**
  - financing
  - data management
    - reporting


## Change log

Coming soon.


## Resources

While there is no intent to write a master-list of resources, I'll be providing links to some resources that have influenced this project.

### Thanks

- **Oskar Dudycz** and his [EventSourcing.NetCore](https://github.com/oskardudycz/EventSourcing.NetCore) code repository and [event-driven.io](https://event-driven.io/) blog.
  - Oskar's style of developing applications using concepts like CQRS and Event Sourcing has certainly rubbed off on me. That will be evident when looking at some of this very code! Aggregate design especially.
- **Derek Comartin** writes articles on [CodeOpinion.com](https://codeopinion.com/derek-comartin/) and produces accompanying videos on his [YouTube channel](https://www.youtube.com/@CodeOpinion) of the same name.
- More to come!


## Maintainer

Erik "Faelor" Shafer

blog: www.event-sourcing.dev


## License

[MIT license](./LICENSE).
