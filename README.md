![Github Actions](https://github.com/erikshafer/event-sourcing-ecommerce/actions/workflows/dotnet.yml/badge.svg?branch=main)

[<img src="https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white" height="20px" />](https://www.linkedin.com/in/erikshafer/) [![blog](https://img.shields.io/badge/blog-event--sourcing.dev-blue)](https://www.event-sourcing.dev/) [![Twitter Follow](https://img.shields.io/twitter/url?label=reach%20me%20%40Faelor&style=social&url=https%3A%2F%2Ftwitter.com%2Ffaelor)](https://twitter.com/faelor)

# Event Sourcing Ecommerce 🛒

<samp>TL;DR: A collection of event sourcing use cases that leverage EventStoreDB</samp>


## Table of Contents
- [1.0 What is this repository?](#what-is-this-repository)
- [2.0 Technologies, frameworks, and libraries, oh my!](#technologies-frameworks-and-libraries-oh-my)
  - [2.1 Polyglot](#polyglot) 
  - [2.2 Suggestions](#suggestions)
  - [2.3 Runtimes](#runtimes)
  - [2.4 Databases](#databases)
    - [2.4.1 Event Store](#event-store) 
    - [2.4.2 EventStoreDB Libraries](#eventstoredb-libraries)
    - [2.4.3 Other Databases (for queries, read models, analysis, etc)](#other-databases-for-queries-read-models-analysis-etc)
  - [2.5 Testing](#testing)
  - [2.6 Auxiliary and implicit dependencies](#auxiliary-and-implicit-dependencies)
  - [2.7 Serialization of data inside an (EventStoreDB) event body](#serialization-of-data-inside-an-eventstoredb-event-body)
- [3.0 Documentation](#documentation)
- [4.0 Roadmap](#roadmap)
- [5.0 Compatibility](#compatibility)
- [6.0 Installation Requirements](#installation-requirements)
- [7.0 How To Run](#how-to-run)
  - [7.1 Clone the repo](#clone-the-repo)
  - [7.2 Build via terminal](#build-via-terminal)
  - [7.3 Start services in Docker via terminal](#start-services-in-docker-via-terminal)
  - [7.4 End services in Docker via terminal](#end-services-in-docker-via-terminal)
  - [7.5 Running the API projects](#running-the-api-projects)
- [8.0 Notes to Self](#notes-to-self)
- [9.0 Resources](#resources)
  - [9.1 Thanks](#thanks)
  - [9.2 Tools Used](#tools-used)
- [10.0 Maintainer](#maintainer)
- [11.0 License](#license)

## What is this repository?

This repository's objective to demonstrate how an ecommerce backend can be built using the data storage technique known as event sourcing, along with related concepts frequently employed such as [event-driven architecture (EDA)](https://en.wikipedia.org/wiki/Event-driven_architecture), [Command and Query Responsibility Segregation (CQRS)](https://martinfowler.com/bliki/CQRS.html), and more.

The aim is to provide an assortment of use cases of varying complexity across different technologies. That is to say, examples that are beyond the `Hello World` level that showcase different methodologies and technologies.


## Technologies, frameworks, and libraries, oh my!

As mentioned, moderns tools are leverage to to demonstrate different ways to interact with [EventStoreDB](https://www.eventstore.com/eventstoredb), the event-native database. While it was written from the ground up for [Event Sourcing](https://www.eventstore.com/event-sourcing), there are other interesting uses the database can be used for that this repository may explore in the future.

### Polyglot

An exciting yet perhaps lofty idea is to have this single code repository be the home for different runtimes and programming languages that work in tandem. Where one module (service) is written in C# running in .NET, while another service it communicates with is written in TypeScript running Node.js.

If this proves to be too ambitious or if the community finds it confusing, changes can be made. Such as making different versions of this repository with each featuring a different language and runtime. 

### Suggestions

Is there a library, framework, or other piece of tech you would like to see here? Simply open an issue, pull request, or contact me directly (see above).  I would love to hear more about what you think should be highlighted here.

### Runtimes

- [.NET](https://dotnet.microsoft.com/)
  - AKA [dotnet](https://dotnet.microsoft.com/), using [C#](https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/)
- [Node.js](https://nodejs.org/en)
  - using [TypeScript](https://www.typescriptlang.org/)
  - TODO

<img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="dotnet" /> <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="csharp" /> <img src="https://img.shields.io/badge/Node%20js-339933?style=for-the-badge&logo=nodedotjs&logoColor=white" alt="nodejs" /> <img src="https://img.shields.io/badge/TypeScript-007ACC?style=for-the-badge&logo=typescript&logoColor=white" alt="typescript" />

### Databases

#### Event Store

- [EventStoreDB](https://eventstore.com/eventstoredb)

#### EventStoreDB Libraries
- [Eventuous](https://eventuous.dev/)
- [MicroPlumberd](https://github.com/modelingevolution/micro-plumberd)
- [Emmet](https://event-driven-io.github.io/emmett/)

#### Other Databases (for queries, read models, analysis, etc)

- [SQL Server](https://www.microsoft.com/en-us/sql-server/)
- [PostgreSQL](https://www.postgresql.org/)
- [Elasticsearch](https://www.elastic.co/)

### Testing
- [xUnit](https://github.com/xunit/xunit)
- [FluentAssertions](https://github.com/fluentassertions/fluentassertions)
- [Shouldly](https://github.com/shouldly/shouldly)

### Auxiliary and implicit dependencies
- [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json)
- [FluentValidation](https://github.com/FluentValidation/FluentValidation)

### Serialization of data inside an (EventStoreDB) event body
- [JSON](https://www.json.org/json-en.html)
  - While other options are available, JSON is the default for ESDB.
  - Why? The Projections subsystem inside ESDB works with JSON.
  - This is very handy when solving a *temporal correlation query*. [More information here](https://developers.eventstore.com/server/v5/projections.html#introduction-to-projections).

<img src="https://img.shields.io/badge/json-5E5C5C?style=for-the-badge&logo=json&logoColor=white" alt="json" />

## Documentation

Coming soon.


## Roadmap

More details coming soon.

In the meantime, check out how the modules of code are broken up:

### Value Streams 

Value Streams are a core concept in [Team Topologies](https://teamtopologies.com/). To Grossly simplify, think departments, divisions, or teams within a company.  That is, *organizing business and technology teams for fast flow.*

- **Retail** 🛒
  - storefront 
  - cart
  - checkout
- **Catalog** 📝
  - listings 
  - pricing
  - products
- **Ordering** 📦
  - orders
  - payments
  - customers
- **Supply Chain** 🚚 (formerly Inventory)
  - inventories (warehouse)
  - procurement (inbound)
  - fulfillment (outbound)
- **Data Analysis** 🔬
  - data science
  - data reporting

## Breakdown of modules

This early on in development, this is effectively a loose roadmap of what technologies will be used where. Better fits may be found or new technologies may want to be explored. All subject to change.

| Value Stream     | Module         | Runtime                                                                                                                     | Language                                                                                                                          | ESDB Library                                                         | Read DB(s), Analytics                                                                                                                                                                                                                                                                                                         |
|------------------|----------------|-----------------------------------------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------|----------------------------------------------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| 🏪 Retail        | Storefront     | <img src="https://img.shields.io/badge/Node%20js-339933?style=for-the-badge&logo=nodedotjs&logoColor=white" alt="nodejs" /> | <img src="https://img.shields.io/badge/TypeScript-007ACC?style=for-the-badge&logo=typescript&logoColor=white" alt="typescript" /> | [Emmet](https://event-driven-io.github.io/emmett/)                   | [<img src="https://img.shields.io/badge/Elastic_Search-005571?style=for-the-badge&logo=elasticsearch&logoColor=white" alt="elasticsearch" />](https://www.elastic.co/)                                                                                                                                                        |
| 🏪 Retail        | Cart           | <img src="https://img.shields.io/badge/Node%20js-339933?style=for-the-badge&logo=nodedotjs&logoColor=white" alt="nodejs" /> | <img src="https://img.shields.io/badge/TypeScript-007ACC?style=for-the-badge&logo=typescript&logoColor=white" alt="typescript" /> | [Emmet](https://event-driven-io.github.io/emmett/)                   | [<img src="https://img.shields.io/badge/Elastic_Search-005571?style=for-the-badge&logo=elasticsearch&logoColor=white" alt="elasticsearch" />](https://www.elastic.co/)                                                                                                                                                        |
| 🏪 Retail        | Checkout       | <img src="https://img.shields.io/badge/Node%20js-339933?style=for-the-badge&logo=nodedotjs&logoColor=white" alt="nodejs" /> | <img src="https://img.shields.io/badge/TypeScript-007ACC?style=for-the-badge&logo=typescript&logoColor=white" alt="typescript" /> | [Emmet](https://event-driven-io.github.io/emmett/)                   | N/A                                                                                                                                                                                                                                                                                                                           |
| 📝 Catalog       | Listings       | <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="dotnet" />         | <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="csharp" />               | [Eventuous](https://eventuous.dev/)                                  | [<img src="https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white" alt="postgresql" />](https://www.postgresql.org/)                                                                                                                                                              |
| 📝 Catalog       | Pricing        | <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="dotnet" />         | <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="csharp" />               | [MicroPlumberd](https://github.com/modelingevolution/micro-plumberd) | [<img src="https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white" alt="postgresql" />](https://www.postgresql.org/)                                                                                                                                                              |
| 📝 Catalog       | Products       | <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="dotnet" />         | <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="csharp" />               | [Eventuous](https://eventuous.dev/)                                  | [<img src="https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white" alt="postgresql" />](https://www.postgresql.org/)                                                                                                                                                              |
| 📦 Supply Chain  | Inventories    | <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="dotnet" />         | <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="csharp" />               | [Eventuous](https://eventuous.dev/)                                  | [<img src="https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white" alt="postgresql" />](https://www.postgresql.org/)                                                                                                                                                              |
| 📦 Supply Chain  | Procurement    | <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="dotnet" />         | <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="csharp" />               | [Eventuous](https://eventuous.dev/)                                  | [<img src="https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white" alt="postgresql" />](https://www.postgresql.org/)                                                                                                                                                              |
| 📦 Supply Chain  | Fulfillment    | <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="dotnet" />         | <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="csharp" />               | [Eventuous](https://eventuous.dev/)                                  | [<img src="https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white" alt="postgresql" />](https://www.postgresql.org/)                                                                                                                                                              |
| 🔬 Data Analysis | Data Science   | <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="dotnet" />         | <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="csharp" />               | N/A                                                                  | TBD                                                                                                                                                                                                                                                                                                                           |
| 🔬 Data Analysis | Data Reporting | <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="dotnet" />         | <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="csharp" />               | N/A                                                                  | [<img src="https://img.shields.io/badge/Microsoft_SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white" alt="sql server" />](https://www.microsoft.com/en-us/sql-server/) <img src="https://img.shields.io/badge/Tableau-E97627?style=for-the-badge&logo=Tableau&logoColor=white" alt="Tableau" /> |
| 🏛️ Legacy       | Legacy         | <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="dotnet" />         | <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="csharp" />               | N/A                                                                  | [<img src="https://img.shields.io/badge/Microsoft_SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white" alt="sql server" />](https://www.microsoft.com/en-us/sql-server/)                                                                                                                          |


### Disclaimer about similarities

Names, structure, and hierarchy are based on personal experiences and opinions derived from time spent in the ecommerce industry. They do not reflect the inner workings of any specific singular system, team, or organization.


## Compatibility

At this time it is preferred you build the projects on your machine directly, the traditional way.

In the future there will be an easier way to choose between running modules traditionally or with Docker.

As this time the background services, such as the databases, are ran inside of Docker containers.

[<img src="https://img.shields.io/badge/Docker-2CA5E0?style=for-the-badge&logo=docker&logoColor=white">](https://www.docker.com/)

## Installation Requirements

1. Install the [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
2. Install [Docker](https://www.docker.com/products/docker-desktop/) 

TODO: optional to have projects built within Docker and not require the .NET SDK to be installed.

## How To Run

### Clone the repo

Clone the repository via your preferred method. For example, using the command line:

```bash
git clone https://github.com/erikshafer/event-sourcing-ecommerce
```

### Build via terminal

Open a terminal and navigate to the root of the repository.

Build the dotnet solution (AKA the container of projects) with the following command at the root of the repository:

```bash
dotnet build
```

If you see no errors and a `Build succeeded.` message output, that means all the dotnet requirements were met. Hooray!

### Start services in Docker via terminal

Besure that after Docker is installed it is also actively running, otherwise the commands below will result in a `Docker daemon is not running` error.  

Run the following command to have Docker begin running (detached, so you can still use your terminal) all background services, such as databases, sinks, etc:

```bash
docker-compose up -d
```

This may take a moment to complete if this is your first time running the code. Specifically if you need to pull down the Docker images and then run the containers.

If there are no visible errors and you see a `Running 6/6` message output, all the Docker components, AKA the background services, are running successfully. Hooray!

### End services in Docker via terminal

As we ran `docker-compose` as a detached process, so we can continue to use the same terminal, we will need to manually stop the services as well with docker's compose feature. That can be done with the following command:

```bash
docker-compose down
```

If you didn't run Docker detached earlier (with no `-d` param), you can just escape out of the process with CTRL+C or whatever the binding is on your machine.

Check the [Docker Compose documentation](https://docs.docker.com/compose/intro/features-uses/) for more details.

### Running the API projects

To be finished (TBF). 👷🚧

As more vertical slices and implemented and projects are more fleshed out as a whole.

**TL;DR:** execute `dotnet run` where applicable. If you're a dotnet developer you likely know what to do!


## Notes to Self 

This is not an [Architectural Decision Record (ADR)](https://adr.github.io/).
🎵 [This is a tribute](https://www.youtube.com/watch?v=_lK4cX5xGiQ). 🎶

- **2024-May-01:** Updated dependencies. Resumed exploring MicroPlumberd and Emmet libraries and how they work with EventStoreDB.
  - Oh man. What if there was also a JVM module with Kotlin? And another with Elixir on the Erlang VM? Oh boy. Getting ahead of myself.
    - `dreaming != doing` 
- **2024-April-30:** Spent time on [Excalidraw](https://excalidraw.com/) to map out what modules can highlight different aspects, methods, languages, run-times, and libraries.
- **2024-April-24:** Added MicroPlumberd to the Pricing module and will explore it more soon.
- **2024-April-24:** While you can run the `docker-compose.yml` with success, nothing is working-working.


## Resources

Work in progress. 👷🚧


### Thanks

- **Oskar Dudycz** and his [EventSourcing.NetCore](https://github.com/oskardudycz/EventSourcing.NetCore) code repository and [event-driven.io](https://event-driven.io/) blog.
  - Oskar's style of developing applications using concepts like CQRS and Event Sourcing has certainly rubbed off on me. That will be evident when looking at some of this very code! Aggregate design especially.
- **Derek Comartin** writes articles on [CodeOpinion.com](https://codeopinion.com/derek-comartin/) and produces accompanying videos on his [YouTube channel](https://www.youtube.com/@CodeOpinion) of the same name.
- **Greg Young** and his involvement in getting so much of this rolling. His contributions helped this community and interest in this way of storing data get its start. And no, I didn't mean CARS. I mean CQRS!
- **Event Store**, at time of this writing, is my employer! The individuals at Event Store are phenomenal in too many ways to write here. Thank you all for your professional and personal support.
- **JetBrains**, I love your IDEs and Kotlin. That is all.
- More to come!

### Tools Used

Big fan of the JetBrains suite of IDEs, such as Rider and IntelliJ IDEA. As well as the Kotlin (JVM) programming language. Big, big fan!

<img src="https://img.shields.io/badge/Rider-000000?style=for-the-badge&logo=Rider&logoColor=white" alt="jetbrains rider">


## Maintainer

Erik "Faelor" Shafer

blog: www.event-sourcing.dev


## License

[MIT license](./LICENSE).
