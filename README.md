# WowFac3D

## About
#### The project
Augmented and virtual reality application based on AugmentedFace subsystem of ARCore.
 
This app is a proposal of university project as a team work for the course in Information Visualization of Pof. Andrea Abate.

(Sorry for any beginner erros, but this is my first application with Unity and ARCore.)

#### Contributors
This project has been development with contribute of:

<img src="/readmedia/alex.png" height="80"> 

Alexander Minichino

<img src="/readmedia/attilio.png" height="80">

Attilio Della Greca

<img src="/readmedia/nello.png" height="80"> 

Aniello Florido


## Installation 
##### (This is a part of [official guide of Google Developers](https://developers.google.com/ar/develop/unity/quickstart-android ""))
### Requirements
##### Hardware
- An ARCore [supported device](https://developers.google.com/ar/discover/supported-devices)
- A USB cable to connect your device to your development machine

##### Software
- Unity 2017.4.34f1 or later
    - Make sure to include [Android Build Support](https://docs.unity3d.com/Manual/InstallingUnity.html) during installation
    - The Universal Render Pipeline (formerly known as Ligthweight Render Pipeline or LWRP) is not supported by the ARCore SDK for Unity
    - When using Unity 2019, the following Unity packages are required:
        1.  Multiplayer HLAPI
        2. XR Legacy Input Helper
- [ARCore SDK for Unity 1.15.0 or later](https://github.com/google-ar/arcore-unity-sdk/releases) (already present in the project)
Android SDK 7.0 (API Level 24) or later, installed using the SDK Manager in Android Studio

#### Project configuration

After cloning, you need to configure Unity to use ARCore, then, follow this steps:

 1. Go to File > [Build Settings](https://docs.unity3d.com/Manual/BuildSettings.html "Build Settings") to open the Build Settings window.
 2. Select Android and click Switch Platform.
 3. In the Build Settings window, click Player Settings.
 4. In the Settings window, configure the following:

 | Setting                                              | Value                                                                                             |
|------------------------------------------------------|---------------------------------------------------------------------------------------------------|
| Player Settings > Other Settings > Rendering         | Uncheck Auto Graphics APIIf Vulkan is listed under Graphics APIs, remove it.                      |
| Player Settings > Other Settings > Package Name      | Create a unique app ID using a Java package name format.For example, use com.example.helloAR      |
| Player Settings > Other Settings > Minimum API Level | Android 7.0 'Nougat' (API Level 24) or higher(For AR Optional apps, the Minimum API level is 14.) |
| Player Settings > XR Settings > ARCore Supported     | Enable                                                                                            |

For more info please read [here](https://developers.google.com/ar/develop/unity/quickstart-android#configure_project_settings "").

#### Build and run the app

1. [Enable developer options and USB debugging ](https://developer.android.com/studio/debug/dev-options.html#enable "") on your device.
2. Connect your device to your development machine.

3. In the Unity Build Settings window, click Build and Run.
Unity builds your project into an Android APK, installs it on your device, and launches it.

4. (Optional) Use Android logcat to view log messages or Android Device Monitor to analyze the device more comprehensively.

## Usage

If you want you can download [working apk](/WowFac3D.apk).

#### Augmented reality and virtual reality mode

The app allow to change both face attachment and background:

<img src="/readmedia/screen4.jpg" height="800">

<img src="/readmedia/screen3.jpg" height="800">

<img src="/readmedia/screen1.jpg" height="800">
 
<img src="/readmedia/screen2.jpg" height="800">

<img src="/readmedia/screen5.jpg" height="800">


### Gallery 

You can easily take a picture and it will be saved in gallery, where the pictures can be shown andmanaged:

Also you can share and delete them.

<img src="/readmedia/gallery2.jpg" height="800">
<img src="/readmedia/gallery1.jpg" height="800">

### Little demo
<img src="/readmedia/record.gif" height="800">

## Thanks to

Prof. Andrea Abate and Dipartimento di Informatica of University Of Salerno.

<img src="/readmedia/logo_dipartimento.png" height="80"> 
