# Event Sourcing Ecommerce 🛒

*build statuses go here.*

[![Twitter Follow](https://img.shields.io/twitter/url?label=reach%20me%20%40Faelor&style=social&url=https%3A%2F%2Ftwitter.com%2Ffaelor)](https://twitter.com/faelor)

## What is this repository?

**TL;DR:** A collection of event sourcing knowledge.

The code in this repository is meant to demonstrate how an ecommerce backend can be built using event sourcing (a data storage technique, or paradigm if you will) along with related concepts frequently employed.

The eventual goal is to have something a bit more "realistic" than what is typically found on public code repositories. That is to say the code will be a bit more hardened and include edge cases most developers typically see in production code and not in examples.

## Technologies, frameworks, and libraries, oh my!

A goal of this project is to use modern (actively maintained) tools to demonstrate different ways to interact with EventStoreDB.

If you would like to suggest such a tool, please open an issue, a pull request, or contact me (see above). I would love to hear more!

### Primary

- [.NET](https://dotnet.microsoft.com/), AKA [dotnet](https://dotnet.microsoft.com/) (using [C#](https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/))
- [EventStoreDB](https://eventstore.com/eventstoredb)

#### EventStoreDB Libraries
- [Eventuous](https://eventuous.dev/) (modules: catalog, inventory)
- [MicroPlumberd](https://github.com/modelingevolution/micro-plumberd) (modules: pricing)

### Testing
- [xUnit](https://github.com/xunit/xunit)
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

Names and hierarchy are based on personal opinions and experiences in ecommerce but do not necessarily reflect the inner workings of any specific singular system, team, or organization.

- **Catalog**
  - products
  - pricing
  - listings
- **Ordering**
  - orders
  - payments
  - customers
- **Inventory**
  - inventories (warehouse)
  - procurement (inbound)
  - fulfillment (outbound)
- **Enterprise**
  - data science
  - data reporting


## Change log

Coming soon.


## Resources

While there is no intent to write a master-list of resources, I'll be providing links to some resources that have influenced this project.


### Thanks

- **Oskar Dudycz** and his [EventSourcing.NetCore](https://github.com/oskardudycz/EventSourcing.NetCore) code repository and [event-driven.io](https://event-driven.io/) blog.
  - Oskar's style of developing applications using concepts like CQRS and Event Sourcing has certainly rubbed off on me. That will be evident when looking at some of this very code! Aggregate design especially.
- **Derek Comartin** writes articles on [CodeOpinion.com](https://codeopinion.com/derek-comartin/) and produces accompanying videos on his [YouTube channel](https://www.youtube.com/@CodeOpinion) of the same name.
- **Greg Young** and his involvement in getting so much of this rolling. His contributions helped this community and interest in this way of storing data get its start. And no, I didn't mean CARS. I mean CQRS!
- **Event Store**, at time of this writing, is my employer! The individuals at Event Store are phenomenal in too many ways to write here. Thank you all for your professional and personal support.
- More to come!


## Maintainer

Erik "Faelor" Shafer

blog: www.event-sourcing.dev


## License

[MIT license](./LICENSE).
