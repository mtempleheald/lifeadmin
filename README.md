# LifeAdmin

Life admin for the agile.  
Agile people focus on value, not on plans.  
Calendars and traditional TODO apps focus on upfront planning.  
This is an attempt to solve the same issues in a different way.  

## Motivation

There is always a need to organise one's life, to do tasks that need doing, some people are more disciplined than others.  
If you lie in the less disciplined camp a TODO list can be quite demoralising...  

Yesterday-you said do task T on date D, date D comes around and Today-you doesn't want to do T.  
The end result of this is that the task doesn't get done, __and__ you feel bad about it.  
Essentially you'd be better off not having planned in the first place, there has to be a better way.  

## Vision

What if we instead captured only successful task completion?  
Every recorded event is positive, it feels good to achieve things, but there's no pressure to do so.  
Recurring events can then be analysed to prioritise activity by maximising value.

## Features

### Event capture

Once you complete a task, whether planned or otherwise, you record this as an _event_.  
Each event must relate to a _task_, for example 'cleaned kitchen'.  

### Reporting

Over time captured events can be analysed for useful patterns.  
For example, you can see that you have historically been completing 'cleaned kitchen' once every 7 days, but currently it has been 20 days, perhaps this should be high on the priority list.

### Persistent data storage

User-configured backend storage, such that the data can be accessed from multiple devices.  
Ideally supporting android's file system, Google Drive, Microsoft OneDrive, DropBox and others.  

### Smart analytics

Automate the pattern recognition part of reporting to aid prioritisation.  





# Development environment setup

This section is here just because I have a memory like a sieve and am likely to need to redo these steps.

For Windows simply follow [Microsoft installation guide](https://learn.microsoft.com/en-us/dotnet/maui/get-started/installation?view=net-maui-8.0&tabs=visual-studio-code#android)  

For Debian I mostly followed [Installing Maui on Linux](https://techcommunity.microsoft.com/t5/educator-developer-blog/net-maui-on-linux-with-visual-studio-code/ba-p/3982195), but I'd rather keep my own notes in case I skip steps or the blog disappears.  

Starting point - .NET8 already installed, along with VS Code, C# & C# DevKit extensions.  
Steps:

Install .NET maui workload:
`dotnet workload install maui-android`

Add .NET Maui VS Code extension - will complain about missing JDK & Android.
Can check status using `Ctrl + P` then `> .NET Maui configure android` & refresh android status.

Install OpenJDK following [Microsoft instructions](https://learn.microsoft.com/en-us/java/openjdk/install#debian-10---12):
```bash
sudo apt update
sudo apt install wget lsb-release -y
wget https://packages.microsoft.com/config/debian/$(lsb_release -rs)/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb

sudo apt update
sudo apt install msopenjdk-17
```

Using snap for installation of Android studio for simplicity
```bash
sudo apt install snapd
sudo snap install android-studio --classic
snap run android-studio
```

using either environment variables, VS Code settings or VS Code .NET Maui commands
Set Android SDK Path `export ANDROID_HOME=$HOME/Android/Sdk` to avoid build errors (aapt command).  
Set Java SDK Path `/usr/bin`

best added to profile to persist over restarts
`cat ~/.profile` & `nano ~/.profile`

recheck android status - `Ctrl + P > .NET Maui configure android` & refresh android status.  
Use Android Studio to add any missing components it complains about.  
I didn't bother with the optional dependencies.

We can now build `dotnet build`.  
But not run, `dotnet build -t:Run -f net8.0-android` fails to find an available device.

From a real android phone with developer settings enabled, enable wireless debugging and click in to see connection details.  
From the dev machine with `~/Android/Sdk/platform-tools/` added to $PATH run:
`adb pair`, using the port for pairing provided by the phone.  
`adb connect`, using the primary port provided by the phone.  

Re-run the application `dotnet build -t:Run -f net8.0-android` and test using the phone.

# Upgrading to .NET9

1. install .NET9
2. install workload `sudo dotnet workload install maui-android`, this will update `MauiVersion` referenced in the next step.
3. update csproj to set TargetFrameworks to `net9.0-x` instead of `net8.0-x`
   I took a shortcut here to address workload issues, enabling only android target framework (priority)
4. address breaking changes according to the [docs](https://learn.microsoft.com/en-us/dotnet/maui/whats-new/dotnet-9?view=net-maui-8.0#deprecated-apis)
5. build & fix remaining warnings

With `~/Android/Sdk/platform-tools/` added to $PATH to make `adb` available,
and the android phone already set to wireless debugging mode,
run the following commands from the solution root to push latest build to the phone:
```bash
adb pair {ip}:{port} {pairing code}
# successfully paired to {ip}:{port} [guid=adb-...]
adb connect {ip}:{port}
# connected to {ip}:{port}
dotnet build -t:Run -f net9.0-android
# Build succeeded in Xs
```

The app should open automatically on the phone, ready for manual testing.




# Debugging

While the app is running and connected, with Android studio running, checked via `adb devices`, examine logs issued by the app:

```bash
# see all warnings + by application id
adb logcat --pid=(adb shell pidof -s io.github.mtempleheald.lifeadmin) *:W -v time,brief,color
# filter warnings to my code + ActivityManager, everything else is silent
adb logcat --pid=$(adb shell pidof -s io.github.mtempleheald.lifeadmin) heald.lifeadmin:V ActivityManager:I *:S -v time,color
# find log entries by specific pattern
adb logcat -e mtempleheald
```


# Notes

Logging is not provided by Maui directly, so nothing written by me shows in logcat, hence temporary addition of LogMessages variables.

"SQLite-net is shipped as a NuGet package. You must add the sqlite-net-pcl package to your apps to use it. Use the NuGet package manager in Visual Studio. Additionally, if you want to run an app on Android, you must also add the SQLitePCLRaw.provider.dynamic_cdecl package." from [here](https://learn.microsoft.com/en-us/training/modules/store-local-data/3-store-data-locally-with-sqlite)
