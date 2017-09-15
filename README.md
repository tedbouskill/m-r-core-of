# .NET Core - Onion Framework - Event Sourcing - CQRS

This repository was created to modernize Gregory Young's C# example of [Event Sourcing](https://martinfowler.com/eaaDev/EventSourcing.html) and [CQRS](https://martinfowler.com/bliki/CQRS.html) on GitHub
[SimpleCQRS](https://github.com/gregoryyoung/m-r/tree/master/SimpleCQRS)

**NOTE: This project is using .NET Core 2.x on a Mac so be sure to have it installed and available.**

## Background

The original sample combined Domain Driven Design, Event Sourcing, and CQRS patterns, however, as a basic sample it did not include patterns such as:

* Onion Architecture/Framework
* Dependency Injection
* Microservices
* Actor Concurrency Model

After watching an organization build a live production site using the original sample, the need to build a modern version using additional patterns and best practices was a great opportunity to use .NET Core 2.0 on a solution that will be platform independent.

## Objectives

* Use .NET Core so the project is platform independent
* Use the following patterns
  * Domain Driven Design
  * Microservices
  * Onion Architecture/Framework
  * Dependency Injection
  * Event Sourcing
  * CQRS
* Implement a realistic Inventory Tracking service
* Create GitHub Tags to track different versions

## GitHub Version Tags

The following section lists the GitHub Tags and the state of the application in the versions

### Version1

First version of Inventory controller directly consuming a DbContext that is using a SQLite local DB for storage.  It only has two subprojects.  Infrastructure which contains the DbContext and DomainCore for domain models and interfaces.

It was intentionally kept very simple to mimic common practices in many companies.

### Version2

In this version, the goal was to demonstrate a solution that met the requirements for CQRS using a MVC controller as the command handler and implementing a InventoryRepository that had the capability of reading and writing from two different data stores. An Onion Framework project structure was added.  The WebApp is only connected with an Application and DomainCore project. The Infrastructure project is now only accessed via the Application project to implement access to the data stores.

This solution would work in production if there were two data stores configured in a master/slave relationship with the master used for writes and the slave for reads.  There is no Event Sourcing and because there is not an Actor Concurrency Model the last update event will win if a delete event hadn't happened first.

### Version3

This version includes an event table to track all events affecting inventory items.  It is not a full event sourcing implementation because it does not have the capability to rebuild an inventory items state using an aggregate service.

It could be used as a solution in production if a company only requires an audit trail for investigative or reporting purposes.

### Version4

This version combines Event Sourcing and CQRS.  The command handlers will publish to the Event Store, then the Write Store concurrently.  It will always update the Write Store with the latest aggregate from the Event Store. 

Although events are published to the command handler using the web controllers, it is possible for an event bus to publish events to the command handler so this version is production ready.
