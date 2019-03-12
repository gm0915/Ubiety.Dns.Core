# ![Logo](https://github.com/ubiety/Ubiety.Dns.Core/raw/develop/images/library64.png) Ubiety.Dns.Core
> A reusable DNS resolver for .NET Standard 2.0

[Professionally supported Ubiety.Dns.Core is coming soon](https://tidelift.com/subscription/pkg/nuget-ubiety-dns-core?utm_source=nuget-ubiety-dns-core&utm_medium=referral&utm_campaign=readme)

| Branch  | Quality                                                                                                                                                                                  | Travis CI | Appveyor                                                                                                                                                                           | Coverage                                                                                                                                                                     |
| ------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Master  | [![CodeFactor](https://www.codefactor.io/repository/github/ubiety/ubiety.dns.core/badge)](https://www.codefactor.io/repository/github/ubiety/ubiety.dns.core)                            |           | [![Build status](https://ci.appveyor.com/api/projects/status/d987cu46fasa23nx/branch/master?svg=true)](https://ci.appveyor.com/project/coder2000/ubiety-dns-core/branch/master)   | [![Coverage Status](https://coveralls.io/repos/github/ubiety/Ubiety.Dns.Core/badge.svg?branch=master)](https://coveralls.io/github/ubiety/Ubiety.Dns.Core?branch=master)   |
| Develop | [![CodeFactor](https://www.codefactor.io/repository/github/ubiety/ubiety.xmpp.core/badge/develop)](https://www.codefactor.io/repository/github/ubiety/ubiety.xmpp.core/overview/develop) |           | [![Build status](https://ci.appveyor.com/api/projects/status/d987cu46fasa23nx/branch/develop?svg=true)](https://ci.appveyor.com/project/coder2000/ubiety-dns-core/branch/develop) | [![Coverage Status](https://coveralls.io/repos/github/ubiety/Ubiety.Dns.Core/badge.svg?branch=develop)](https://coveralls.io/github/ubiety/Ubiety.Dns.Core?branch=develop) |


## Installing / Getting started

Ubiety DNS Core is available from NuGet

```shell
dotnet package install Ubiety.Dns.Core
```

You can also use your favorite NuGet client.

## Developing

Here's a brief intro about what a developer must do in order to start developing
the project further:

```shell
git clone https://github.com/ubiety/Ubiety.Dns.Core.git
cd Ubiety.Dns.Core
dotnet restore
```

Clone the repository and then restore the development requirements. You can use
any editor, Rider, VS Code or VS 2017. The library supports all .NET Core
platforms.

### Building

Building is simple

```shell
./build.ps1
```

### Deploying / Publishing

```shell
git pull
versionize
dotnet pack
dotnet nuget push
git push
```

## Contributing

When you publish something open source, one of the greatest motivations is that
anyone can just jump in and start contributing to your project.

These paragraphs are meant to welcome those kind souls to feel that they are
needed. You should state something like:

"If you'd like to contribute, please fork the repository and use a feature
branch. Pull requests are warmly welcome."

If there's anything else the developer needs to know (e.g. the code style
guide), you should link it here. If there's a lot of things to take into
consideration, it is common to separate this section to its own file called
`CONTRIBUTING.md` (or similar). If so, you should say that it exists here.

## Links

- Project homepage: <https://dns.ubiety.ca
- Repository: <https://github.com/ubiety/Ubiety.Dns.Core/>
- Issue tracker: <https://github.com/ubiety/Ubiety.Dns.Core/issues>
  - In case of sensitive bugs like security vulnerabilities, please contact
    issues@dieterlunn.ca directly instead of using issue tracker. We value your effort
    to improve the security and privacy of this project!
- Related projects:
  - Ubiety VersionIt: <https://github.com/ubiety/Ubiety.VersionIt/>
  - Ubiety Toolset: <https://github.com/ubiety/Ubiety.Toolset/>
  - Ubiety Xmpp: <https://github.com/ubiety/Ubiety.Xmpp.Core/>

## Sponsors

### Gold Sponsors

[![Gold Sponsors](https://opencollective.com/ubiety/tiers/gold-sponsor.svg?avatarHeight=36)](https://opencollective.com/ubiety/)

### Silver Sponsors

[![Silver Sponsors](https://opencollective.com/ubiety/tiers/silver-sponsor.svg?avatarHeight=36)](https://opencollective.com/ubiety/)

### Bronze Sponsors

[![Bronze Sponsors](https://opencollective.com/ubiety/tiers/bronze-sponsor.svg?avatarHeight=36)](https://opencollective.com/ubiety/)

## Licensing

The code in this project is licensed under the Apache License version 2.
