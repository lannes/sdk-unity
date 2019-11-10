# **SDK for Unity 5.6.x**

## **Android Platform**

### **Tools**

* Unity **5.6.5p1**
* Android Studio **3.5.x**

### **Source Unity**

Copy `SDKManager.cs` into the `Scripts` folder.

Set enviroment
```cs
SDKManager.setEnviroment (SDKManager.ENVIRONMENT_SANDBOX);
```

Init SDK
```cs
#if UNITY_ANDROID
using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
    using (AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) {        
        SDKManager.initStartSDK (activity);
    }
}
#endif
```

SignIn
```csharp
#if UNITY_ANDROID
using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
    using (AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) {        
        SDKManager.SignIn (activity);
    }
}
#endif
```

### **Setting**

Choose Minimum API Level: Android 4.1
Choose Target API Level: Automatic (highest installed)

![](Identification.png)

### **Android Plugin**

Copy 4 files: `AndroidManifest.xml`, `mainTemplate.gradle`, `unity.aar`, `VTCSdk.aar` into the `Assets/Plugin/Android` folder as the below image:

![](./plugin-android.png)

#### **Manifest**
```xml
<?xml version="1.0" encoding="utf-8"?>
<manifest xmlns:android="http://schemas.android.com/apk/res/android" xmlns:tools="http://schemas.android.com/tools" android:versionName="1.0" android:versionCode="1">
  <supports-screens android:smallScreens="true" android:normalScreens="true" android:largeScreens="true" android:xlargeScreens="true" android:anyDensity="true" />
  <application android:name="com.strategy.intecom.vtc.tracking.SDKManager"
      android:theme="@style/UnityThemeSelector" android:icon="@drawable/app_icon" android:label="@string/app_name" android:isGame="true" android:banner="@drawable/app_banner">
    <activity android:label="@string/app_name" android:screenOrientation="fullSensor" android:launchMode="singleTask" android:configChanges="mcc|mnc|locale|touchscreen|keyboard|keyboardHidden|navigation|orientation|screenLayout|uiMode|screenSize|smallestScreenSize|fontScale|layoutDirection|density" android:name=".UnityPlayerActivity">
      <intent-filter>
        <action android:name="android.intent.action.MAIN" />
        <category android:name="android.intent.category.LAUNCHER" />
        <category android:name="android.intent.category.LEANBACK_LAUNCHER" />
      </intent-filter>
      <meta-data android:name="unityplayer.UnityActivity" android:value="true" />
    </activity>
    <activity android:name="com.software.intecom.vtc.unity.UnityActivity" android:theme="@android:style/Theme.Translucent.NoTitleBar"></activity>
  
    <activity
            android:name="com.strategy.intecom.vtc.login.Authen"
            android:configChanges="orientation|keyboard|keyboardHidden|screenLayout|screenSize"
            android:label="@string/app_name"
            android:screenOrientation="landscape"
            android:theme="@style/Theme.AppCompat.NoActionBar"
            android:windowSoftInputMode="stateAlwaysHidden" />

        <activity
            android:name="com.strategy.intecom.vtc.login.Login"
            android:configChanges="orientation|keyboard|keyboardHidden|screenLayout|screenSize"
            android:label="@string/app_name"
            android:screenOrientation="landscape"
            android:theme="@android:style/Theme.Translucent.NoTitleBar.Fullscreen"
            android:windowSoftInputMode="stateAlwaysHidden" />

        <activity
            android:name="com.strategy.intecom.vtc.login.RegisterNew"
            android:configChanges="orientation|keyboard|keyboardHidden|screenLayout|screenSize"
            android:label="@string/app_name"
            android:screenOrientation="landscape"
            android:theme="@android:style/Theme.Translucent.NoTitleBar.Fullscreen"
            android:windowSoftInputMode="stateAlwaysHidden" />

        <activity
            android:name="com.strategy.intecom.vtc.login.ActiveOtp"
            android:configChanges="orientation|keyboard|keyboardHidden|screenLayout|screenSize"
            android:label="@string/app_name"
            android:screenOrientation="landscape"
            android:theme="@android:style/Theme.Translucent.NoTitleBar.Fullscreen"
            android:windowSoftInputMode="stateAlwaysHidden" />
        <activity
            android:name="com.strategy.intecom.vtc.login.resetpassword.ResetPassword"
            android:configChanges="keyboard|keyboardHidden|screenLayout|screenSize|orientation"
            android:label="@string/app_name"
            android:screenOrientation="landscape"
            android:theme="@android:style/Theme.Translucent.NoTitleBar.Fullscreen"
            android:windowSoftInputMode="stateAlwaysHidden" />
        <activity
            android:name="com.strategy.intecom.vtc.login.ReLogin"
            android:configChanges="keyboard|keyboardHidden|screenLayout|screenSize|orientation"
            android:label="@string/app_name"
            android:screenOrientation="landscape"
            android:theme="@android:style/Theme.Translucent.NoTitleBar.Fullscreen"
            android:windowSoftInputMode="stateAlwaysHidden" />
        <activity
            android:name="com.strategy.intecom.vtc.login.LoginOtp"
            android:configChanges="keyboard|keyboardHidden|screenLayout|screenSize|orientation"
            android:label="@string/app_name"
            android:screenOrientation="landscape"
            android:theme="@android:style/Theme.Translucent.NoTitleBar.Fullscreen"
            android:windowSoftInputMode="stateAlwaysHidden" />
        <activity
            android:name="com.strategy.intecom.vtc.login.LoginOtpNew"
            android:configChanges="keyboard|keyboardHidden|screenLayout|screenSize|orientation"
            android:label="@string/app_name"
            android:screenOrientation="landscape"
            android:theme="@android:style/Theme.Translucent.NoTitleBar.Fullscreen"
            android:windowSoftInputMode="stateAlwaysHidden" />
        <activity
            android:name="com.strategy.intecom.vtc.login.register.Register"
            android:configChanges="keyboard|keyboardHidden|screenLayout|screenSize|orientation"
            android:label="@string/app_name"
            android:screenOrientation="landscape"
            android:theme="@android:style/Theme.Translucent.NoTitleBar.Fullscreen"
            android:windowSoftInputMode="stateAlwaysHidden" />
        <activity
            android:name="com.strategy.intecom.vtc.login.changepassword.ChangePassword"
            android:configChanges="keyboard|keyboardHidden|screenLayout|screenSize|orientation"
            android:label="@string/app_name"
            android:screenOrientation="landscape"
            android:theme="@android:style/Theme.Translucent.NoTitleBar.Fullscreen"
            android:windowSoftInputMode="stateAlwaysHidden" />
        <activity
            android:name="com.strategy.intecom.vtc.login.resetpassword.ResetPasswordType"
            android:configChanges="keyboard|keyboardHidden|screenLayout|screenSize|orientation"
            android:label="@string/app_name"
            android:screenOrientation="landscape"
            android:theme="@android:style/Theme.Translucent.NoTitleBar.Fullscreen"
            android:windowSoftInputMode="stateAlwaysHidden" />
        <activity
            android:name="com.strategy.intecom.vtc.login.resetpassword.ResetPasswordOTP"
            android:configChanges="keyboard|keyboardHidden|screenLayout|screenSize|orientation"
            android:screenOrientation="landscape"
            android:theme="@android:style/Theme.Translucent.NoTitleBar.Fullscreen"
            android:windowSoftInputMode="stateAlwaysHidden" />

        <activity
            android:name="com.strategy.intecom.vtc.login.password.RegisterPassword"
            android:configChanges="keyboard|keyboardHidden|screenLayout|screenSize|orientation"
            android:screenOrientation="landscape"
            android:theme="@android:style/Theme.Translucent.NoTitleBar.Fullscreen"
            android:windowSoftInputMode="stateAlwaysHidden" />

        <activity
            android:name="com.strategy.intecom.vtc.login.password.TermUse"
            android:configChanges="keyboard|keyboardHidden|screenLayout|screenSize|orientation"
            android:screenOrientation="landscape"
            android:theme="@android:style/Theme.Translucent.NoTitleBar.Fullscreen"
            android:windowSoftInputMode="stateAlwaysHidden" />

        <activity
            android:name="com.strategy.intecom.vtc.login.UpdateAccount"
            android:configChanges="keyboard|keyboardHidden|screenLayout|screenSize|orientation"
            android:screenOrientation="landscape"
            android:theme="@style/Theme.AppCompat.Light.NoActionBar"
            tools:replace="android:theme"
            android:windowSoftInputMode="stateAlwaysHidden" />
  </application>
  <uses-feature android:name="android.software.leanback" android:required="false" />

  <uses-feature android:glEsVersion="0x00020000" />
  <uses-feature android:name="android.hardware.vulkan" android:required="false" />
  <uses-feature android:name="android.hardware.touchscreen" android:required="false" />
  <uses-feature android:name="android.hardware.touchscreen.multitouch" android:required="false" />
  <uses-feature android:name="android.hardware.touchscreen.multitouch.distinct" android:required="false" />
</manifest>
```

#### **Gradle**
```js
buildscript {
	repositories {
		maven { url "https://maven.google.com/"}
		jcenter()
		google()
	}

	dependencies {
		classpath 'com.android.tools.build:gradle:3.1.4'
		classpath 'com.google.gms:google-services:3.2.0'
	}
}

allprojects {
	repositories {
        maven { url 'https://maven.google.com' }
        jcenter()
        flatDir {
            dirs 'libs'
        }
	}
}

apply plugin: 'com.android.application'

dependencies {
	compile fileTree(dir: 'libs', include: ['*.jar'])
	//   implementation(':VTCSdkLibNoTelco')

    implementation('de.keyboardsurfer.android.widget:crouton:1.8.5@aar') {
        // exclusion is not necessary, but generally a good idea.
        exclude group: 'com.google.android', module: 'support-v4'
    }
//
//    compile 'com.google.android.gms:play-services:12.0.1'
    implementation 'com.google.android.gms:play-services-safetynet:12.0.1'
    implementation 'com.google.android.gms:play-services-analytics:12.0.1'
    implementation 'com.google.android.gms:play-services-identity:12.0.1'
    implementation 'com.google.android.gms:play-services-plus:12.0.1'
    implementation 'com.google.android.gms:play-services-ads:12.0.1'
    implementation 'com.google.android.gms:play-services-auth:12.0.1'
    implementation 'com.google.android.gms:play-services-gcm:12.0.1'
//
    implementation 'com.android.support:multidex:1.0.1'
    implementation 'com.android.support:appcompat-v7:27.1.1'
    implementation 'com.android.support:design:27.1.1'
    implementation 'com.github.bumptech.glide:glide:3.7.0'
    implementation 'com.squareup.okhttp:okhttp:2.6.0'
    implementation 'com.appsflyer:af-android-sdk:4.8.1'
    implementation 'com.google.code.gson:gson:2.7'
    implementation 'com.android.support.constraint:constraint-layout:1.0.2'
    testImplementation 'junit:junit:4.12'

    // retrofit, gson
    implementation 'com.squareup.retrofit2:retrofit:2.0.2'
    implementation 'com.squareup.retrofit2:converter-gson:2.0.2'
**DEPS**}

android {
	compileSdkVersion **APIVERSION**
	buildToolsVersion '**BUILDTOOLS**'

	defaultConfig {
		minSdkVersion 16
		multiDexEnabled true		
		targetSdkVersion **TARGETSDKVERSION**
		applicationId '**APPLICATIONID**'
	}

	lintOptions {
		abortOnError false
	}

	aaptOptions {
		noCompress '.unity3d', '.ress', '.resource', '.obb'
	}

**SIGN**
	buildTypes {
		debug {
			jniDebuggable true
		}
		release {
			// Set minifyEnabled to true if you want to run ProGuard on your project
			//minifyEnabled false
			proguardFiles getDefaultProguardFile('proguard-android.txt'), 'proguard-unity.txt'
			**SIGNCONFIG**
		}
	}
}
```

Please without modify variables in the `**` symbols, else Unity will export the `build.gradle` file incorrect.

### **Q&A**

1. **Q:** Why need export to Android project?

    **A:** VTCSdk library need new version of gradle with more features than version 2.1.0 default of Unity.

2. **Q:** How to add new gradle version ?
    
    **A:** Link reference: https://docs.unity3d.com/560/Documentation/Manual/android-gradle-overview.html

3. **Q:** Why use gradle version `3.1.4`?
    
    **A:** Because it help avoid conflict with Android manifest file generated by the Unity 5.6.x.

4. **Q:** Why need use `unity.aar` with `VTCSdk.aar` together?
    
    **A:** `com.software.intecom.vtc.unity.UnityActivity` help the `unity.aar` library receive `onActivityResult` from `VTCSdk.aar` then send message to Unity. It don't require must extends or modify `UnityPlayerActivity` class.

    Note: Remember add declare the line `<activity android:name="com.software.intecom.vtc.unity.UnityActivity" android:theme="@android:style/Theme.Translucent.NoTitleBar"></activity>` in the `AndroidManifest.xml` file.

5. **Q:** Why need choose Target API Level is Automatic?

    **A:** Because the Unity 5.6.x only support max level is 25 (`Android 7.1 (API Level 25)`) to select, in while Google Play require must target at least [Android 9 (API level 28)](https://developer.android.com/distribute/best-practices/develop/target-sdk)
