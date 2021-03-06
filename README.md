﻿[logo]: https://raw.githubusercontent.com/Geeksltd/Zebble.Torch/master/icon.png "Zebble.Torch"


## Zebble.Torch

![logo]

A plugin for using device torch in Zebble applications.


[![NuGet](https://img.shields.io/nuget/v/Zebble.Torch.svg?label=NuGet)](https://www.nuget.org/packages/Zebble.Torch/)

> This plugin make you able to turn on and off the device torch in your Zebble application on Android, IOS, and UWP platforms.

<br>


### Setup
* Available on NuGet: [https://www.nuget.org/packages/Zebble.Torch/](https://www.nuget.org/packages/Zebble.Torch/)
* Install in your platform client projects.
* Available for iOS, Android and UWP.
<br>


### Api Usage

Call `Zebble.Device.Torch` from any project to gain access to APIs.

##### Checking the device availablity:
```csharp
// Determines if a torch feature is available on the device.
if (await Zebble.Device.Torch.IsAvailable()) { ... }
```
<br>

##### Turning it on or off:
```csharp
// This will switch the torch on.
await Zebble.Device.Torch.TurnOn(); 

// This will switch the torch off.
await Zebble.Device.Torch.TurnOff();
```
<br>


### Methods
| Method       | Return Type  | Parameters                          | Android | iOS | Windows |
| :----------- | :----------- | :-----------                        | :------ | :-- | :------ |
| IsAvailable         | Task<bool&gt;| -| x       | x   | x       |
| TurnOn         | Task| errorAction-> OnError| x       | x   | x       |
| TurnOff         | Task| errorAction-> OnError| x       | x   | x       |
