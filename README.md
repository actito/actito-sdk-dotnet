[<img src="https://raw.githubusercontent.com/actito/actito-sdk-ios/main/.assets/logo.png"/>](https://actito.com)

# Actito .NET MAUI SDK

[![GitHub release](https://img.shields.io/github/v/release/actito/actito-sdk-dotnet)](https://github.com/actito/actito-sdk-dotnet/releases)
[![License](https://img.shields.io/github/license/actito/actito-sdk-dotnet)](https://github.com/actito/actito-sdk-dotnet/blob/main/LICENSE)

The Actito .NET MAUI SDK makes it quick and easy to communicate efficiently with many of the Actito API services and enables you to seamlessly integrate our various features, from Push Notifications to Contextualised Storage.

Get started with our [📚 integration guides](https://developers.actito.com/docs/push-implementation/net-maui/setup) and [example projects](#examples).


Table of contents
=================

* [Features](#features)
* [Installation](#installation)
    * [Requirements](#requirements)
    * [Configuration](#configuration)
* [Getting Started](#getting-started)
* [Examples](#examples)


## Features

**Push notifications**: Receive push notifications and automatically track its engagement.

**Push notifications UI**: Use native screens and elements to display your push notifications and handle its actions with zero effort.

**In-app messaging**: Automatically show relevant in-app content to your users with zero effort.

**Inbox**: Apps with a built-in message inbox enjoy higher conversions due to its nature of keeping messages around that can be opened as many times as users want. The SDK gives you all the tools necessary to build your inbox UI.

**Geo**: Transform your user's location into relevant information, automate how you segment your users based on location behaviour and create truly contextual notifications.

**Loyalty**: Harness the power of digital cards that live beyond your app and are always in your customer’s pocket.

**Assets**: Add powerful contextual marketing features to your apps. Show the right content to the right users at the right time or location. Maximise the content you're already creating without increasing development costs.


## Installation

### Requirements

* Android 6 (API level 23) and above
* iOS 13 and above

### Configuration

Add the .NET packages to your `*.csproj` and follow the Getting Started guide.

```bash
# Required
dotnet add package Actito

# Optional modules
dotnet add package Actito.Assets
dotnet add package Actito.Geo
dotnet add package Actito.InAppMessaging
dotnet add package Actito.Inbox
dotnet add package Actito.Loyalty
dotnet add package Actito.Push
dotnet add package Actito.Push.UI
dotnet add package Actito.UserInbox
```

## Getting Started

### Integration
Get started with our [📚 integration guides](https://developers.actito.com/docs/push-implementation/net-maui/setup) and [example projects](#examples).


### Examples
- The [example project](https://github.com/actito/actito-sdk-dotnet/tree/main/Sample) demonstrates other integrations in a simplified fashion, to quickly understand how a given feature should be implemented.
