# Tauri mobile app

## Installation

WSL Debian configuration [docs](https://v2.tauri.app/start/prerequisites)

```bash
sudo apt update
sudo apt install libwebkit2gtk-4.1-dev \
  build-essential \
  curl \
  wget \
  file \
  libxdo-dev \
  libssl-dev \
  libayatana-appindicator3-dev \
  librsvg2-dev
```

```bash
curl --proto '=https' --tlsv1.2 https://sh.rustup.rs -sSf | sh
```

Find linux 64bit download of [android studio](https://developer.android.com/studio)

Download this in WSL (probably best to do this in home, not the project folder)
`wget https://redirector.gvt1.com/edgedl/android/studio/ide-zips/2024.3.1.14/android-studio-2024.3.1.14-linux.tar.gz`

If checking the download
`sha256sum android-studio-2024.3.1.14-linux.tar.gz`

extract
`tar -xvf android-studio-2024.3.1.14-linux.tar.gz`

set JAVA_HOME
`export JAVA_HOME=/opt/android-studio/jbr`

launch/setup
`./android-studio/bin/studio.sh`

install tools within android studio
- Android SDK Platform
- Android SDK Platform-Tools
- NDK (Side by side)
- Android SDK Build-Tools
- Android SDK Command-line Tools

set env vars
```bash
export ANDROID_HOME="$HOME/Android/Sdk"
export NDK_HOME="$ANDROID_HOME/ndk/$(ls -1 $ANDROID_HOME/ndk)"
```

```bash
rustup target add aarch64-linux-android armv7-linux-androideabi i686-linux-android x86_64-linux-android
```

install tauri CLI
```bash
cargo install tauri-cli --version "^2.0.0" --locked
```

## Project setup

Create project (will prompt for name and other params)
```bash
cargo install create-tauri-app --locked
cargo create-tauri-app
```

Selected `lifeadmin`, `io.github.mtempleheald.lifeadmin`, `.NET`

```bash
cd lifeadmin
cargo tauri dev
```

Will open in Android window - yay!

### Fixing issues

Runtime errors
```
libEGL warning: failed to open /dev/dri/renderD128: Permission denied

libEGL warning: failed to open /dev/dri/card0: Permission denied
```

Fixed in https://github.com/tauri-apps/tauri/discussions/7879 by setting
`export WEBKIT_DISABLE_COMPOSITING_MODE=1`
