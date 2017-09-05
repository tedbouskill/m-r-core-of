# .NET Core - Onion Framework - Event Sourcing - CQRS

This repository was created to modernize Gregory Young's C# example of [Event Sourcing](https://martinfowler.com/eaaDev/EventSourcing.html) and [CQRS](https://martinfowler.com/bliki/CQRS.html) on GitHub
[SimpleCQRS](https://github.com/gregoryyoung/m-r/tree/master/SimpleCQRS)

## Background

The original sample combined Domain Driven Design, Event Sourcing, and CQRS patterns, however, as a basic sample it did not include patterns such as:

* Onion Architecture/Framework
* Dependency Injection
* Microservices

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
