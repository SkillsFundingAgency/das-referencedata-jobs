## ⛔Never push sensitive information such as client id's, secrets or keys into repositories including in the README file⛔

# Reference Data Jobs

<img src="https://avatars.githubusercontent.com/u/9841374?s=200&v=4" align="right" alt="UK Government logo">

_Update these badges with the correct information for this project. These give the status of the project at a glance and also sign-post developers to the appropriate resources they will need to get up and running_

[![Build Status](https://sfa-gov-uk.visualstudio.com/Digital%20Apprenticeship%20Service/_apis/build/status%2Fdas-referencedata-jobs?repoName=SkillsFundingAgency%2Fdas-referencedata-jobs&branchName=main)](https://sfa-gov-uk.visualstudio.com/Digital%20Apprenticeship%20Service/_build/latest?definitionId=3654&repoName=SkillsFundingAgency%2Fdas-referencedata-jobs&branchName=main)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=SkillsFundingAgency_das-referencedata-jobs&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=SkillsFundingAgency_das-refererncedata-jobs)
[![License](https://img.shields.io/badge/license-MIT-lightgrey.svg?longCache=true&style=flat-square)](https://en.wikipedia.org/wiki/MIT_License)

This is part of the Reference Data project, and is solely used to start the import process for each of the external data sources.

## How It Works

A NServiceBus timer periodically starts the import process, this import process calls the outer API which in turn fetches the external data and populates it in the appropriate databases. 

_For Example_
```
The latest data is currently being fetched for 

1) Education Organisations and stored in the education organisation database
2) Public Sector Organisations and stored in the public sector database

Note: Company data and Charity data is still fetched via the old reference data orojects
```

## 🚀 Installation

### Pre-Requisites

* A clone of this repository
* Storage emulator like Azurite for local config source

### Config

You can find the latest config file in [das-employer-config repository](https://github.com/SkillsFundingAgency/das-employer-config/blob/master/das-referencedata-jobs/SFA.DAS.ReferenceData.Jobs.json). 

In the `SFA.RefernceData.Jobs` project, if not exist already, add local.settings.json file with following content:
```
{
  "IsEncrypted": false,
  "Values": {
    "EnvironmentName": "LOCAL",
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "ConfigurationStorageConnectionString": "UseDevelopmentStorage=true",
    "ConfigNames": "SFA.DAS.ReferernceData.Jobs",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
    "ImportSchedule": "*/5 * * * *"
  },
}

```
When actively developing this function, you may which to change the Azure Table Storage config

Row Key: SFA.DAS.ReferenceData.Jobs_1.0

Partition Key: LOCAL

Data:

```json
{
  "ReferenceDataApim": {
    "ApiBaseUrl": "https://localhost:7112/",
    "SubscriptionKey": "key",
    "ApiVersion": "1"
  }
}

```

## 🔗 External Dependencies

The function uses the following inner apis 
  * [das-public-sector-organisations-api](https://github.com/SkillsFundingAgency/das-public-sector-organisations-api)
  * [das-educational-organisations-api](https://github.com/SkillsFundingAgency/das-educational-organisations-api)

## Technologies

_List the key technologies in-use in the project. This will give an indication as to the skill set required to understand and contribute to the project_

_For Example_
```
* .Net 6.0
* Azure Functions V4
* Azure Table Storage
* NServiceBus
* NUnit
* Moq
* FluentAssertions
```

## 🐛 Known Issues

Ensure 'local.settings.json' is not deployed as it will override the deployments environment values
