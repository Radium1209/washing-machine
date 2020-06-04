# Washing-Machine

## Introduction

This is an emulation program of the washing machine. This washing machine can control temperature, change washing time, and choose whether sterilize or not. It uses multithreading to implement three independent part. These three parts can respectively show a menu, read input, and control the washing time.



## Environment

- git

- .Net Core 3.1



## Installation

### Clone

You need to clone the repository into your local machine. To do so, run the following command in any command prompt. 

```
git clone https://github.com/Radium1209/washing-machine.git
```

### Release

After cloning the repository to your local machine, you need to come into the project directory use cd command.

```
cd .\washing-machine\washing-machine\
```

Then release this project.

```
dotnet publish -c Release -r win10-x64
```

### Run

After that, you can find exe file in the directory which prints on the command prompt. you can run this exe file and start  to simulation.

```
.\bin\Release\netcoreapp3.1\win10-x64\publish\washing-machine.exe
```



## Usage

Press any key to enter the main menu. In the main menu, you can choose to start washing or change the washing mode.

- 0 - Start Washing
- 1 - Set temperature
- 2 - Select washing method
- 3 - Set Washing time

You can change the washing mode in three parameters, and each parameter has its own mode.

- Temperature(`C30-50`, `C50-70`, `C70-90`)
- Washing Method(`NORMAL`, `STERILIZE`)
- Washing Time(`T15`, `T30`, `T45`, `T60`)

More information about each parameter. `C30_50` represents the temperature will control between 30 degrees centigrade to 50 degrees centigrade. Washing method `STERILIZE` represents during the washing, the machine will also sterilize the clothes and make them cleaner. Washing Time `T15` represents 15 minutes.



## Source code layout

Inside the root are the following important directories:

- `washing-machine` which contains the main solution.

Below the `washing-machine` is the main c# files.

### Program.cs

This file is used to create a washing machine objects and run the machine.

### WashingMachine.cs

This file contains the most important class `WashingMachine`.

In this class, it has three threads, `readThread`, `writeThread` and `timeThread`.

- `readThread` take over function `GetOption()`
- `writeThread` take over function `PrintMenu()`
- `timeThread` take over function `Washing()`

`GetOption()` will get user's input and choose what option to do next. It will invoke `SetTemperature()`, `SelectWashingMethod()` or `SetWashingTime()` depends on variable `_menuType`.

`PrintMenu()` will display three kinds of menu by invoking `MainMenu()`, `TemperatureMenu()`, `WashingMethodMenu()` or `WashingMethodMenu`. It also depends on variable `_menuType`.

`Washing()` will simulate the washing action of the machine, and It will running 5 seconds to represents washing for one minute in the real life.

`Run()`, the whole machine is started by `Run()`, it will start `readThread` and `writeThred`, but `timeThread` will work only we choose to start in the main menu.





