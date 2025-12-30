**MyCloset**

MyCloset is a small MAUI app that demonstrates a simple item list and state handling. It includes UI pages and services for adding and viewing items. This repository builds for macOS (Mac Catalyst) and Android.

**Prerequisites**
- **.NET SDK:** Install a matching .NET SDK that supports MAUI (check with `dotnet --list-sdks`).
- **MAUI workloads:** Install workloads if missing: `dotnet workload install maui-maccatalyst maui-android maui-ios`.
- **macOS:** To run locally on Mac use the Mac Catalyst target (`net10.0-maccatalyst`).
- **Android (optional):** If you plan to build for Android, install the Android SDK (Android Studio is recommended).

**Quick Run (macOS — Mac Catalyst)**
1. Open a terminal in the repo root:
   - `cd /Users/sagarluitel/Desktop/Projects/MyCloset`
2. Restore NuGet packages:
   - `dotnet restore`
3. Build for Mac Catalyst:
   - `dotnet build -f net10.0-maccatalyst -c Debug`
4. Run the app:
   - `dotnet run -f net10.0-maccatalyst -c Debug`

Notes:
- If the app does not appear, try running with diagnostic output: `dotnet run -f net10.0-maccatalyst -c Debug -v diag`.
- You can also open the solution in Visual Studio for Mac (or Visual Studio) and run the `MyCloset` project with the Mac Catalyst configuration.

**Android (optional)**
- Install Android Studio (recommended) or the Android command-line tools and SDK into `~/Library/Android/sdk`.
- After SDK is installed, set environment variables (add to `~/.zshrc`):
  - `export ANDROID_SDK_ROOT=$HOME/Library/Android/sdk`
  - `export ANDROID_HOME=$HOME/Library/Android/sdk`
  - `export PATH=$PATH:$ANDROID_SDK_ROOT/cmdline-tools/latest/bin:$ANDROID_SDK_ROOT/platform-tools`
- Accept licenses:
  - `$ANDROID_SDK_ROOT/cmdline-tools/latest/bin/sdkmanager --licenses`
- Build/run for Android (example):
  - `dotnet restore`
  - `dotnet build -f net10.0-android -c Debug`
  - `dotnet run -f net10.0-android -c Debug`
- If the Android SDK is installed in a custom location, pass it to MSBuild:
  - `dotnet build -f net10.0-android -p:AndroidSdkDirectory=/path/to/android/sdk`

**Troubleshooting**
- Error: `The Android SDK directory could not be found` — ensure the Android SDK is installed and `ANDROID_SDK_ROOT`/`ANDROID_HOME` point to the SDK folder (typically `~/Library/Android/sdk`).
- Use `dotnet workload list` to verify MAUI workloads are installed.
- If build fails for a specific target, try building that target explicitly: `dotnet build -f net10.0-maccatalyst -c Debug`.
- Inspect logs for details: run with `-v diag` or check macOS Console for runtime errors.

**Todo / Steps Summary**
- **Check .NET SDK + MAUI workloads:** `dotnet --list-sdks` and `dotnet workload list`.
- **Restore NuGet packages:** `dotnet restore`.
- **Build the project:** `dotnet build -c Debug` (or target with `-f`).
- **Run for macOS (Mac Catalyst):** `dotnet run -f net10.0-maccatalyst -c Debug`.
- **Run for Android emulator (optional):** install Android SDK, then `dotnet run -f net10.0-android -c Debug`.
- **Open in Visual Studio for Mac and Run (optional).**

**Where to look**
- App entry: `App.xaml.cs` (creates the main window).
- Main UI: `MainPage.xaml` and `MainPage.xaml.cs`.
- Services: `Services/ItemStateService.cs`.

If you want, I can try running the Mac Catalyst target here and capture logs, or help install the Android SDK — tell me which you'd like next.
# MyCloset
