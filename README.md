ScottishGlen Asset Tracking System

A lightweight Windows desktop application for the purpose of recording hardware assets in an Azure SQL database and linking each asset to a responsible employee. Built for the company ScottishGlen as part of its adoption of the CIS Critical Security Controls through an Asset Inventory.

*Current Status* This is a part 2 prototype for CMP307 Assessment portfolio. For this part the completed 'Add Asset' use case is fully implemented. View, edit and delete have been in consideration during construction of the program and are planned for later iterations, this is by no means the completed product ready for deployment.

Features (prototype)

•	Automatically collects system name, manufacturer, model, asset type and IP address from the machine it runs on
•	Select the responsible employee from a drop-down
•	Optional purchase date (from the asset's sticker) and free-text notes
•	Automatically generated unique asset ID (GUID)
•	Input validation with clear error messages
•	Friendly handling of database connection errors
•	Clear on-screen instructions at all times


Technology


| Language / Framework | C# on .NET 8, Windows Forms |

| Database | Azure SQL Database |

| Data access | Microsoft.Data.SqlClient (parameterised queries) |

| Hardware details | Windows Management Instrumentation (System.Management) |

| Testing | xUnit |

| Version control | Git |

| Task management |

Architecture:

                      +-------------------+
                      | AddAssetForm (UI) |
                      +---------+---------+
                                |
          +---------------------+---------------------+
          v                     v                     v
+-------------------+ +-------------------+ +-------------------+
| HardwareInfo      | | AssetService      | | SqlEmployee       |
| Collector         | | (validation)      | | Repository        |
| (reads PC: WMI)   | |                   | | (loads employees) |
+-------------------+ +---------+---------+ +---------+---------+
                                |                     |
                                v                     |
                      +-------------------+           |
                      | SqlAsset          |           |
                      | Repository        |           |
                      | (saves assets)    |           |
                      +---------+---------+           |
                                |                     |
                                v                     v
                      +-----------------------------------------+
                      |            Azure SQL Database           |
                      +-----------------------------------------+



Models hold the data: `Asset`, `Employee`, `Department`
Data contains the repository interface and the SQL implementations
Services contain validation `AssetService` and hardware collection `HardwareInfoCollector`
UI contains the Windows Forms screen

Design decisions: repository pattern behind `IAssetRepository`, constructor dependency injection, parameterised SQL to prevent SQL injection, and the connection string kept out of source control.

Project structure

AssetTracker/
  Models/       Asset, Employee, Department
  Data/         IAssetRepository, SqlAssetRepository, SqlEmployeeRepository
  Services/     AssetService, HardwareInfoCollector
  UI/           AddAssetForm
  Database/     schema.sql, seed.sql
  Docs/         UML diagrams and design documentation
AssetTracker.Tests/   Unit tests for AssetService


Minimum Current Requirements

Windows 10 or 11
Visual Studio 2022 with the **.NET desktop development** workload (to build from source)
.NET 8 SDK
An Azure SQL database (the free offer is enough)

Setup

Download ZIP and open: AssetTracker.exe (please allow for 20seconds-40 seconds and if fails please close and open again)

Security notes

All SQL uses parameters (no string concatenation)
Credentials are loaded from an untracked configuration file
Planned: Microsoft Entra ID authentication and least-privilege database roles

Roadmap

- View, edit and delete assets
- Employee management screens (add, edit, delete)
- Duplicate-asset detection
- Integration tests against a test database

Commit conventions

Commits use a prefix: 
•	feat: Introduces a new feature to the application or library
•	fix: Patches or resolves a bug in the application 
•	test: Adds missing tests or corrects/refactors existing tests without changing production code.
•	docs: Updates documentation only
•	chore : Covers routine maintenance, build processes, or auxiliary tool/dependency updates that do not modify production or test code
