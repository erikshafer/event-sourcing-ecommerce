# Event Sourcing Ecommerce 🛒

*build statuses go here.*

[![Twitter Follow](https://img.shields.io/twitter/url?label=reach%20me%20%40Faelor&style=social&url=https%3A%2F%2Ftwitter.com%2Ffaelor)](https://twitter.com/faelor)

## What is this project?

A pragmatic solution showing how to design and build an architecture in .NET using techniques like Event Sourcing, DDD, and CQRS. The goal is to include other powerful patterns found and desired in distributed systems such as sagas / process managers, durable boxes (inbox, outbox), dead letter queues (DLQ), and others.


## Technologies, frameworks, libraries, and tools utilized

While this project is in a work-in-progress state, it's important to note there are a few pivotal frameworks and libraries being leveraged. They have been picked for their ability to improve the developer experience (DX). That meaning they should improve productivity, improve maintainability, reduce boilerplate and focus on domain problems, and in general be as painless to use as possible.


### The 'Critter Stack'

This solution is being developed using the critter stack, which includes [Marten](https://github.com/JasperFx/marten) + [Wolverine](https://github.com/JasperFx/wolverine). More information on their integration can be found [here](https://wolverine.netlify.app/guide/durability/marten.html).


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

### Value Streams / Divisions / Virtual Teams / Departments / Better Term TBD

These names are based on my personal experiences in ecommerce and do not necessarily reflect specific systems, applications, teams, or names. There will likely be similarities, but as this project is meant to be a pragmatic example of certain software designs and architecture this is not a copy or anything of the sort of I have worked on previously. Aside from feeling awkward about doing so a questionable thing, it frankly would be too much work! :)

- Catalog
  - products
  - pricing
- Marketplaces
  - listings
  - ordering
  - marketplaces
- Supply Chain
  - inventory
  - warehouses
  - fulfillment
- Enterprise
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
