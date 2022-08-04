## Api Web Music Player

Minimal API for music site

## Requirements

* ASP.NET Core
* Git
* Visual Studio 2022

## Common setup

Clone the repo

```bash
git clone https://github.com/kharkovdenys/api_webmusicplayer.git
cd api_webmusicplayer
```

Create and connect to ***Azure SQL Database*** using local secrets ("ConnectionStrings:connect")

Creating tables using a [query](https://github.com/kharkovdenys/api_webmusicplayer/blob/main/Api/query.sql)

## Steps for read-only access

Select Start Debugging on the Debug start view or press F5

Open [https://localhost:7030](https://localhost:7030) and take a look around

## Unit Tests

Start project and run this command

```bash
dotnet test
```