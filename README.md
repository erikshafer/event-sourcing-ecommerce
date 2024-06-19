![Github Actions](https://github.com/erikshafer/event-sourcing-ecommerce/actions/workflows/dotnet.yml/badge.svg?branch=main)

[<img src="https://img.shields.io/badge/LinkedIn-0077B5?style=for-the-badge&logo=linkedin&logoColor=white" />](https://www.linkedin.com/in/erikshafer/) [<img src="https://img.shields.io/badge/YouTube-FF0000?style=for-the-badge&logo=youtube&logoColor=white" />](https://www.youtube.com/@event-sourcing)

[![blog](https://img.shields.io/badge/blog-event--sourcing.dev-blue)](https://www.event-sourcing.dev/) [![Twitter Follow](https://img.shields.io/twitter/url?label=reach%20me%20%40Faelor&style=social&url=https%3A%2F%2Ftwitter.com%2Ffaelor)](https://twitter.com/faelor)

# 🪄 Event Sourcing Ecommerce 🛒

<samp><strong>TL;DR:</strong> A collection of use cases utilizing event sourcing and event-driven architecture in the ecommerce domain that leverage the event-native database [EventStoreDB](https://www.eventstore.com/).</samp>

## 🗺️ Table of Contents
- [1.0 What is this repository?](#1.0)
- [2.0 Technologies, frameworks, and libraries, oh my!](#2.0)
  - [2.1 Polyglot](#2.1) 
  - [2.2 Suggestions](#2.2)
  - [2.3 Runtimes](#2.3)
  - [2.4 Databases](#2.4)
      - [2.4.1 Event Store](#2.4.1)
      - [2.4.2 EventStoreDB Libraries](#2.4.2)
      - [2.4.3 Other Databases (for queries, read models, analysis, etc)](#2.4.3)
  - [2.5 Other notable dependencies](#other-notable-dependencies)
  - [2.6 Testing](#testing)
- [3.0 Documentation](#documentation)
- [4.0 Roadmap](#4.0)
  - [4.1 Value Streams](#4.1)
  - [4.2 Proposed breakdown of modules](#4.2)
  - [4.3 Disclaimer](#4.3)
- [5.0 Compatibility](#5.0)
- [6.0 Installation Requirements](#6.0)
- [7.0 How To Run](#7.0)
  - [7.1 Clone the repo](#7.1)
  - [7.2 Build via terminal](#7.2)
  - [7.3 Start services in Docker via terminal](#7.3)
  - [7.4 End services in Docker via terminal](#7.4)
  - [7.5 Running the API projects](#7.5)
- [8.0 The Story](#8.0)
- [9.0 Resources](#9.0)
  - [9.1 Thanks](#9.1)
  - [9.2 Tools Used](#9.2)
- [10.0 Maintainer](#10.0)
- [11.0 License](#11.0)

## 🤔 What is this repository? <a id='1.0'></a>

This repository's objective to demonstrate how an ecommerce backend can be built using the data storage technique known as event sourcing, along with related concepts frequently employed such as [event-driven architecture (EDA)](https://en.wikipedia.org/wiki/Event-driven_architecture), [Command and Query Responsibility Segregation (CQRS)](https://martinfowler.com/bliki/CQRS.html), and more.

The aim is to provide an assortment of use cases of varying complexity across different technologies. That is to say, examples that are beyond the `Hello World` level that showcase different methodologies and technologies.


## 🧑‍💻 Technologies, frameworks, and libraries, oh my! <a id='2.0'></a>

Moderns tools are leveraged to demonstrate different ways to interact with [EventStoreDB](https://www.eventstore.com/eventstoredb), the event-native database. While it was written from the ground up for [Event Sourcing](https://www.eventstore.com/event-sourcing), there are other interesting uses the database can be used for that this repository may explore in the future.

### 🔤 Polyglot <a id='2.1'></a>

An exciting yet perhaps lofty idea is to have this single code repository be the home for different runtimes and programming languages that work in tandem. Where one module (service) is written in C# running in .NET, while another service it communicates with is written in TypeScript running Node.js.

If this proves to be too ambitious or if the community finds it confusing, changes can be made. Such as making different versions of this repository with each featuring a different language and runtime. 

### 📬 Suggestions <a id='2.2'></a>

Is there a library, framework, or other piece of tech you would like to see here? Simply open an issue, pull request, or contact me directly (see above).  I would love to hear more about what you think should be highlighted here.

### Runtimes <a id='2.3'></a>

- [.NET](https://dotnet.microsoft.com/)
  - AKA [dotnet](https://dotnet.microsoft.com/)
  - using [C#](https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/)
- [Node.js](https://nodejs.org/en)
  - using [TypeScript](https://www.typescriptlang.org/) (PAUSED)
- [JVM](https://www.java.com/en/)
  - using [Java](https://www.java.com/en/) and [Kotlin](https://kotlinlang.org/) (TODO)

### Databases <a id='2.4'></a>

#### Event Store

- [EventStoreDB](https://eventstore.com/eventstoredb)

#### EventStoreDB Libraries
- [Eventuous](https://eventuous.dev/)
- [MicroPlumberd](https://github.com/modelingevolution/micro-plumberd)
- [Emmet](https://event-driven-io.github.io/emmett/)

#### Other Databases (for reads, queries, analysis, etc.)

- [SQL Server](https://www.microsoft.com/en-us/sql-server/)
- [PostgreSQL](https://www.postgresql.org/)
- [Elasticsearch](https://www.elastic.co/)
- [MongoDB](https://www.mongodb.com/)

### Messaging
- TBD. Current candidates:
  - [Kafka](https://kafka.apache.org/)
    - Demonstrate how Kafka and ESDB can be great friends! 
  - [RabbitMQ](https://www.rabbitmq.com/)
    - Classic. Stable. Easiest option to get up and running with.

## 📝 Documentation

A companion guide is currently in development.


## 🛣️ Roadmap <a id='4.0'></a>

Details are being worked out and will be shared soon.

### Value Streams <a id='4.1'></a>

Value Streams are a core concept in [Team Topologies](https://teamtopologies.com/). To Grossly simplify, think departments, divisions, or teams within a company.  That is, *organizing business and technology teams for fast flow.*

## Proposed breakdown of modules <a id='4.2'></a>

This early on in development, this is effectively a loose roadmap of what technologies will be used where. Better fits may be found or other technologies may want to be explored. All subject to change.

| Value Stream         | Module         | Progress | Runtime                                                                                                                     | Language                                                                                                                          | ESDB Library                                                         | Additional Data Stores, etc.                                                                                                                                                                                                                                                                                                |
|----------------------|----------------|----------|-----------------------------------------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------|----------------------------------------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| 💼 Vendor Management | Portal         | 🔴       | <img src="https://img.shields.io/badge/Node%20js-339933?style=for-the-badge&logo=nodedotjs&logoColor=white" alt="nodejs" /> | <img src="https://img.shields.io/badge/TypeScript-007ACC?style=for-the-badge&logo=typescript&logoColor=white" alt="typescript" /> | [Emmet](https://event-driven-io.github.io/emmett/)                   | [<img src="https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white" alt="postgresql" />](https://www.postgresql.org/)                                                                                                                                                            |
| 🏪 Retail            | Storefront     | 🔴       | <img src="https://img.shields.io/badge/Node%20js-339933?style=for-the-badge&logo=nodedotjs&logoColor=white" alt="nodejs" /> | <img src="https://img.shields.io/badge/TypeScript-007ACC?style=for-the-badge&logo=typescript&logoColor=white" alt="typescript" /> | [Emmet](https://event-driven-io.github.io/emmett/)                   | [<img src="https://img.shields.io/badge/Elastic_Search-005571?style=for-the-badge&logo=elasticsearch&logoColor=white" alt="elasticsearch" />](https://www.elastic.co/)                                                                                                                                                      |
| 🏪 Retail            | Cart           | 🟢⚫⚫     | <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="dotnet" />         | <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="csharp" />               | [Eventuous](https://eventuous.dev/)                                  | [<img src="https://img.shields.io/badge/MongoDB-4EA94B?style=for-the-badge&logo=mongodb&logoColor=white" alt="mongodb" />](https://www.mongodb.com/)                                                                                                                                                                        |
| 📝 Catalog           | Listings       | 🔴       | <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="dotnet" />         | <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="csharp" />               | [Eventuous](https://eventuous.dev/)                                  | [<img src="https://img.shields.io/badge/MongoDB-4EA94B?style=for-the-badge&logo=mongodb&logoColor=white" alt="mongodb" />](https://www.mongodb.com/)                                                                                                                                                                        |
| 📝 Catalog           | Products       | 🟢⚫⚫     | <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="dotnet" />         | <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="csharp" />               | [Eventuous](https://eventuous.dev/)                                  | [<img src="https://img.shields.io/badge/MongoDB-4EA94B?style=for-the-badge&logo=mongodb&logoColor=white" alt="mongodb" />](https://www.mongodb.com/) [<img src="https://img.shields.io/badge/Elastic_Search-005571?style=for-the-badge&logo=elasticsearch&logoColor=white" alt="elasticsearch" />](https://www.elastic.co/) |
| 📝 Catalog           | Prices         | 🟢⚫⚫     | <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="dotnet" />         | <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="csharp" />               | [Eventuous](https://eventuous.dev/)                                  | [<img src="https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white" alt="postgresql" />](https://www.postgresql.org/)                                                                                                                                                            |
| 📦 Supply Chain      | Inventories    | 🔴       | <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="dotnet" />         | <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="csharp" />               | [Eventuous](https://eventuous.dev/)                                  | [<img src="https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white" alt="postgresql" />](https://www.postgresql.org/)                                                                                                                                                            |
| 📦 Supply Chain      | Procurement    | 🔴       | <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="dotnet" />         | <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="csharp" />               | [Eventuous](https://eventuous.dev/)                                  | [<img src="https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white" alt="postgresql" />](https://www.postgresql.org/)                                                                                                                                                            |
| 📦 Supply Chain      | Fulfillment    | 🔴       | <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="dotnet" />         | <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="csharp" />               | [Eventuous](https://eventuous.dev/)                                  | [<img src="https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white" alt="postgresql" />](https://www.postgresql.org/)                                                                                                                                                            |
| 🔬 Data Analysis     | Data Science   | 🔴       | <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="dotnet" />         | <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="csharp" />               | N/A                                                                  | TBD                                                                                                                                                                                                                                                                                                                         |
| 🔬 Data Analysis     | Data Reporting | 🔴       | <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="dotnet" />         | <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="csharp" />               | N/A                                                                  | TBD                                                                                                                                                                                                                                                                                                                         |
| 🏛️ Legacy           | Legacy         | 🔴       | <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="dotnet" />         | <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="csharp" />               | N/A                                                                  | [<img src="https://img.shields.io/badge/Microsoft_SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white" alt="sql server" />](https://www.microsoft.com/en-us/sql-server/)                                                                                                                        |
| TBD                  | TBD            | 🔴       | <img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="dotnet" />         | <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="csharp" />               | [MicroPlumberd](https://github.com/modelingevolution/micro-plumberd) | TBD                                                                                                                                                                                                                                                                                                                         |
| TBD                  | TBD            | 🔴       | <img src="https://img.shields.io/badge/Java-ED8B00?style=for-the-badge&logo=openjdk&logoColor=white" alt="java" />          | ![Java](https://a11ybadges.com/badge?logo=java)                                                                                   | TBD                                                                  | TBD                                                                                                                                                                                                                                                                                                                         |

### Disclaimer about similarities <a id='4.3'></a>

Names, structure, and hierarchy are based on personal experiences and opinions derived from time spent in the ecommerce industry. They do not reflect the inner workings of any specific singular system, team, or organization.


## 🔨 Compatibility <a id='5.0'></a>

At this time it is preferred you build the projects on your machine directly, the traditional way.

In the future there will be an easier way to choose between running modules traditionally or with Docker.

As this time the background services, such as the databases, are ran inside of Docker containers.

[<img src="https://img.shields.io/badge/Docker-2CA5E0?style=for-the-badge&logo=docker&logoColor=white">](https://www.docker.com/)

## 🛠️ Installation Requirements <a id='6.0'></a>

1. Install [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
2. Install [Docker](https://www.docker.com/products/docker-desktop/) 

<table>
  <tr>
    <td>TODO:</td>
    <td>enable projects to be built within Docker and not require the .NET SDK to be installed</td>
  </tr>
</table>

## 🚀 How To Run <a id='7.0'></a>

### Clone the repo <a id='7.1'></a>

Clone the repository via your preferred method. For example, using the command line:

```bash
git clone https://github.com/erikshafer/event-sourcing-ecommerce
```

### Build via terminal <a id='7.2'></a>

Open a terminal and navigate to the root of the repository.

Build the dotnet solution (AKA the container of projects) with the following command at the root of the repository:

```bash
dotnet build
```

If you see no errors and a `Build succeeded.` message output, that means all the dotnet requirements were met. Hooray!

### Start services in Docker via terminal <a id='7.3'></a>

Be sure that after Docker is installed it is also actively running, otherwise the commands below will result in a `Docker daemon is not running` error.  

Run the following command to have Docker begin running (detached, so you can still use your terminal) all background services, such as databases, sinks, etc.:

```bash
docker-compose up -d
```

This may take a moment to complete if this is your first time running the code. Specifically if you need to pull down the Docker images and then run the containers.

If there are no visible errors, and you see a `Running 6/6` message output, all the Docker components, AKA the background services, are running successfully. Hooray!

### End services in Docker via terminal <a id='7.4'></a>

As we ran `docker-compose` as a detached process, so we can continue to use the same terminal, we will need to manually stop the services as well with the docker-compose feature. That can be done with the following command:

```bash
docker-compose down
```

If you didn't run Docker detached earlier (with no `-d` param), you can just escape out of the process with CTRL+C or whatever the binding is on your machine.

Check the [Docker Compose documentation](https://docs.docker.com/compose/intro/features-uses/) for more details.

### Running the API projects <a id='7.5'></a>

Work In Progress 🚧 

As more vertical slices and implemented and projects are more fleshed out as a whole.

**TL;DR:** execute `dotnet run` where applicable. If you're a dotnet developer you likely know what to do!


## 📖 The Story <a id='8.0'></a>

An ecommerce company has grown out of its startup phase. It is needing to scale not just the amount of requests and responses it's capable of per second, but make itself capable to adapt to changing trends and shifts in the industry.

Enter event sourcing with EventStoreDB!

To be continued ⚠️  


## 🏫 Resources <a id='9.0'></a>

More to come 🚧

- Event Store blog and webinars
  - [A Beginner's Guide to Event Sourcing](https://www.eventstore.com/event-sourcing) 
  - [Introduction to Event Sourcing](https://learn.eventstore.com/webinar-recording-introduction-to-event-sourcing)
  - [Unravelling Event Sourcing: Key Definitions](https://www.eventstore.com/blog/event-sourcing-key-definitions) 
  - [EventStoreDB and PostgreSQL](https://www.eventstore.com/blog/comparing-eventstoredb-and-postgresql)
  - [10 problems that Event Sourcing can help solve for you](https://www.eventstore.com/blog/10-problems-that-event-sourcing-can-help-solve-for-you)
  - [Developers' tips for Event Sourcing & Event-Driven Architecture](https://www.eventstore.com/blog/developers-tips-for-event-sourcing-eda)

### Thanks <a id='9.1'></a>

- **Oskar Dudycz** and his [EventSourcing.NetCore](https://github.com/oskardudycz/EventSourcing.NetCore) code repository and [event-driven.io](https://event-driven.io/) blog.
  - Oskar's style of developing applications using concepts like CQRS and Event Sourcing has certainly rubbed off on me. That will be evident when looking at some of this very code! Aggregate design especially.
- **Derek Comartin** writes articles on [CodeOpinion.com](https://codeopinion.com/derek-comartin/) and produces accompanying videos on his [YouTube channel](https://www.youtube.com/@CodeOpinion) of the same name.
- **Greg Young** and his involvement in getting so much of this rolling. His contributions helped this community and interest in this way of storing data get its start. And no, I didn't mean CARS. I mean CQRS!
- **Event Store**, at time of this writing, is my employer! The individuals at Event Store are phenomenal in too many ways to write here. Thank you all for your professional and personal support.
- **JetBrains**, I love your IDEs and Kotlin. That is all.
- More to come!

### Tools Used <a id='9.2'></a>

I've been a large fan of [JetBrains](https://www.jetbrains.com/)' suite of Integrated Development Environments (IDEs) for the better part of a decade. That includes their dotnet IDE called [Rider](https://www.jetbrains.com/rider/) which is used to work on this effort.

<img src="https://img.shields.io/badge/Rider-000000?style=for-the-badge&logo=Rider&logoColor=white" alt="jetbrains rider">


## 👷‍♂️ Maintainer <a id='10.0'></a>

Erik "Faelor" Shafer

blog: www.event-sourcing.dev


## ⚖️ License <a id='11.0'></a>

[MIT license](./LICENSE).
