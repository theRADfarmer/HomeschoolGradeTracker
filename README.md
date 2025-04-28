# Razor Pages Clean Architecture Template

## Project Architecture Guide
To build sites that are maintainable, scalable, and clean, we use a multi-layered Razor Pages architecture with Clean Architecture principles.
Folder Overview:
```
PlantYourSite/
├── src/
|   ├── PlantYourSite.Web/              # Razor Pages UI
│   ├── PlantYourSite.Application/      # Use cases & business logic
│   ├── PlantYourSite.Domain/           # Entities, interfaces, and rules
│   └── PlantYourSite.Infrastructure/   # Services, email, storage, etc.
├── tests/                              # Automated test projects
└── build/                              # CI/CD or deployment scripts
```

## Usage Tips:
Keep page logic thin in `PlantYourSite.Web`; defer to services in `Application`.


Define models and interfaces in `Domain`; implement them in `Infrastructure`.


Add features like `PricingEstimatorService`, `EmailSender`, or `WebAppBuilder` as services in `Application`, and inject them via `Startup.cs`.


Write unit tests for each layer using the tests/ folder.


This structure keeps your code clean, testable, and scalable as you grow into more advanced features
