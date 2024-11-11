# Funda.RealEstateAnalyzer
Real estate analyzer service. The service requests data from Funda API to analyze the current estate market by city. A RESTful API provides a broker's list by listed properties number. Built using .NET 8.0. Includes rate limiting handling.

Swager starts on: /swagger/index.html

# Used technologies

.NET 8.0

Polly (https://github.com/App-vNext/Polly) - For HTTP policies and rate-limiting handling

AutoMapper (https://automapper.org/) - object-object mapper

Serilog (https://serilog.net/) - logger

MediatR (https://github.com/jbogard/MediatR) - mediator implementation in .NET

# How to run
1) Run WebApi project. Swagger with API description is available on /swagger/index.html.
