# Apprenticeship Programmes API - *prerelease*

|               |               |
| ------------- | ------------- |
|![crest] (https://assets.publishing.service.gov.uk/government/assets/crests/org_crest_27px-916806dcf065e7273830577de490d5c7c42f36ddec83e907efe62086785f24fb.png)|Apprenticeship Programmes API|
| Build | <img alt="Build Status" src="https://sfa-gov-uk.visualstudio.com/_apis/public/build/definitions/c39e0c0b-7aff-4606-b160-3566f3bbce23/166/badge" /> |
| .Net Client | [![](https://img.shields.io/nuget/v/SFA.DAS.Apprenticeships.Api.Client.svg)](https://www.nuget.org/packages/SFA.DAS.Apprenticeships.Api.Client/)
| Coverity | [![](https://scan.coverity.com/projects/10689/badge.svg)](https://scan.coverity.com/projects/skillsfundingagency-das-apprenticeship-programs-api) |
| Web | http://das-prd-apprenticeshipinfoservice.cloudapp.net/ | 
| Swagger | [![](http://online.swagger.io/validator?url=http://das-prd-apprenticeshipinfoservice.cloudapp.net:80/swagger/docs/v1)](http://das-prd-apprenticeshipinfoservice.cloudapp.net:80/swagger/docs/v1) |
| Gitter | [![](https://badges.gitter.im/gitterHQ/gitterHQ.github.io.svg)](https://gitter.im/sfa-das-apprenticeship-programmes-api/Lobby?utm_source=share-link&utm_medium=link&utm_campaign=share-link) |

A public API from the Skills Funding Agency to provide a list of 
- Standards
- Frameworks
- Assessment Organisations
- Providers

and their relationships

## Consumers
- [Find Apprenticeship Training](https://github.com/SkillsFundingAgency/das-search)
- [Employer Apprenticeships Service](https://github.com/SkillsFundingAgency/das-employerapprenticeshipsservice)

**if you're not on here let us know so we don't break your application**

## Architecture

### Other components
- [Apprenticeship Programmes Indexer](https://github.com/SkillsFundingAgency/das-apprenticeship-programs-indexer)

### Dependencies 
- Elasticsearch 2.3.5

## Usage

### Basic
```c#
using(var client = new StandardApiClient())
{
   var standard = client.Get(12);
}

using(var client = new FrameworkApiClient())
{
   var framework = client.Get("403-1-8"); // NOTE the ID format changing
}

using(var client = new ProviderApiClient())
{
   var provider = client.Get(1003141);
}

using(var client = new AssessmentOrgsApiClient())
{
	var org = client.Get("EPA0001");
}
```

### StructureMap
```c#
For<IStandardApiClient>().Use<StandardApiClient>();
For<IFrameworkApiClient>().Use<FrameworkApiClient>();
For<IProviderApiClient>().Use<ProviderApiClient>();
For<IAssessmentOrgsApiClient>().Use<ProviderApiClient>();
```

