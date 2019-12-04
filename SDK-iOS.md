# **SDK for Unity 5.6.x**

## **Table of Content**
* [1. Tools](#Tools)
* [2. Demo](#Demo)
* [3. iOS Plugin](#iOS-Plugin)
    * [3.1. Player Settings](#Player-Settings)
    * [3.2. Library](#Library)
    * [3.3. VTCSdk configuration](#VTCSdk-configuration)
    * [3.4. Export Project](#Export-Project)
* [4. Unity code](#Unity-code)
    * [4.1. InitStartSDK](#InitStartSDK)
    * [4.2. DelegateMessage](#DelegateMessage)
    * [4.3. SignIn](#SignIn)
    * [4.4. SignOut](#SignOut)
* [5. Xcode](#Xcode)
    * [5.1. didFinishLaunchingWithOptions](#didFinishLaunchingWithOptions)
    * [5.2. openURL](#openURL)
* [6. Q & A](#Q-&-A)

### <a name="Tools">1. Tools</a>

* [Unity **5.6.5p1**](https://unity3d.com/unity/qa/patch-releases/5.6.5p1)
* [Xcode **11.2**](https://apps.apple.com/app/xcode/id497799835)

### <a name="Demo">2. Demo</a>
Checkout Git: [https://github.com/lannes/sdk-unity.git](https://github.com/lannes/sdk-unity.git)

Open `sdkdemo` project in the Unity 5.6.x.

### <a name="iOS-Plugin">3. iOS Plugin</a>

* #### <a name="Player-Settings">3.1. Player Settings</a>
    * Target minimum iOS version: 9.0

* #### <a name="Library">3.2. Library</a>
    * Copy `UnityBridge.h`, `UnityBridge.mm` into the `Assets/Plugins/iOS` folder.
    * Copy [BuildPostProcessor.cs](./sdkdemo/Assets/Scripts/Editor/BuildPostProcessor.cs) into the `Scripts/Editor` folder.
    * Copy `NativeAssets` directory to the Unity project.
    * Folder structure:
    ```
    + Assest
    |   + Plugins
    |   |   + Android
    |   |   + iOS
    |   |   |   - UnityBridge.h
    |   |   |   - UnityBridge.mm
    |   + Scripts
    |   |   + Editor
    |   |   |   - BuildPostProcessor.cs
    |   |   - SDKManager.cs
    + Library
    + NativeAssets
    |   + VtcSDK.framework
    |   + VtcSDKResource.bundle
    |   - VtcSDK-Info.plist
    |   - sdkconfig.xml
    + ProjectSettings
    ```
* #### <a name="VTCSdk-configuration">3.3. VTCSdk configuration</a>

    The `VtcSDK-Info.plist` file in `NativeAssets` directory.

    ```xml
    <?xml version="1.0" encoding="UTF-8"?>
    <!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
    <plist version="1.0">
    <dict>
        <key>APPSFLYER_DEV_KEY</key>
        <string>4toAa4UsXTiSELM98xse83</string>
        <key>APPSTORE_ID</key>
        <string>1465214241</string>
        <key>VTC_APP_NAME</key>
        <string>FinalBlade</string>
        <key>UTM_STRING</key>
        <string></string>
        <key>DIRECT_VERSION</key>
        <false/>
        <key>VTC_APP_ID</key>
        <string></string>
        <key>VTC_APP_SECRET</key>
        <string></string>
        <key>LANGUAGE</key>
        <string>vie</string>
        <key>VTC_APP_ID_EN</key>
        <string></string>
    </dict>
    </plist>
    ```

    Please contact us to get the values below:
    * VTC_APP_ID
    * VTC_APP_SECRET
    * VTC_APP_ID_EN

    Set field values before use Unity to build Xcode project.

* #### <a name="Export-Project">Export Project</a>

    ![](./iOS.png)

### <a name="Unity-code">4. Unity code</a>

Copy [SDKManager.cs](./sdkdemo/Assets/Scripts/SDKManager.cs) into the `Scripts` folder.

* #### <a name="InitStartSDK">4.1. InitStartSDK</a>
    ```cs
    void StartSDK() {
        #if UNITY_IOS
        // You must call set environment in UnityAppController.mm
        SDKManager.InitStartSDK();
        #endif
    }
    ```
 
* #### <a name="DelegateMessage">4.2. DelegateMessage</a>
    ```cs
    public class Main : MonoBehaviour {
        #if UNITY_IOS
        [MonoPInvokeCallback(typeof(SDKManager.DelegateMessage))] 
        public static void onMessage(string message, int requestCode) {
        }
        #endif
    }
    ```

* #### <a name="SignIn">4.3. SignIn</a>
    ```cs
    #if UNITY_IOS
	[MonoPInvokeCallback(typeof(SDKManager.DelegateMessage))] 
 	public static void onMessage(string message, int requestCode) {
		if (requestCode == SDKManager.SIGNIN_CODE) {            
            SDKManager.vtcUser = VTCUser.CreateFromJSON(message);
            
            Debug.Log("ACCOUNT NAME: " + SDKManager.vtcUser.accountName);
            Debug.Log("ACCOUNT ID: " + SDKManager.vtcUser.accountId);
            Debug.Log("VCOIN BALANCE: " + SDKManager.vtcUser.vcoinBalance);
		}
 	}
    #endif

    public void SignIn() {
        #if UNITY_IOS
        SDKManager.SignIn(onMessage);
        #endif
    }
    ```

* #### <a name="SignOut">4.4. SignOut</a>
    ```cs
    public void SignOut() {
        #if UNITY_IOS
        SDKManager.SignOut();
        #endif
    }
    ```

### <a name="Xcode">5. Xcode<a>

Edit `UnityAppController.mm` as below:

* #### <a name="didFinishLaunchingWithOptions">5.1. didFinishLaunchingWithOptions</a>

    ```objc
    #import <VtcSDK/VtcSDK.h>

    - (BOOL)application:(UIApplication*)application didFinishLaunchingWithOptions:(NSDictionary*)launchOptions
    {
        // ...

        [SDKManager defaultManager].isSandbox = YES;
        [SDKManager defaultManager].ignoreCaptcha = NO;
        [SDKManager defaultManager].isSaveAccessToken = YES;
        [SDKManager handleApplication:application didFinishLaunchingWithOptions:launchOptions];
        return YES;
    }
    ```
        
    Environment
    * **Sandbox**: `[SDKManager defaultManager].isSandbox = YES`
    * **Live**: `[SDKManager defaultManager].isSandbox = NO`
    
* #### <a name="openURL">5.2. openURL</a>

    ```objc
    - (BOOL)application:(UIApplication*)application openURL:(NSURL*)url sourceApplication:(NSString*)sourceApplication annotation:(id)annotation
    {
        return [SDKManager handleApplication:application openURL:url sourceApplication:sourceApplication annotation:annotation];
    }
    ```

### <a name="Q-&-A">6. Q & A</a>

1. **Q:** What is `BuildPostProcessor.cs` file? 

    `BuildPostProcessor.cs` is a build script, it will support you add libraries and frameworks necessary automatic. It also add some setting for `Xcode` project.

2. **Q:** How to use `BuildPostProcessor.cs` file?

    `BuildPostProcessor` support methods as below:
    * `void AddFramework(string framework)`
    
        e.g. AddFramework("CoreData.framework");
    * `void AddUsrLib(string framework)`
    
        e.g. AddUsrLib("libz.dylib");
    * `void AddExternalFramework(string path, string framework)`
    
        e.g. AddExternalFramework(Application.dataPath + "/../NativeAssets", "VtcSDK.framework");
    * `void AddFile(string path, string file)`
    
        e.g. AddFile(Application.dataPath + "/../NativeAssets", "VtcSDK-Info.plist");
    * `void UpdatePlist()`
    
        e.g. rootDict.CreateArray("UIBackgroundModes").AddString("remote-notification");
    
    * `void AddFrameworkToEmbed(string path, string frameworkName)`
        
        e.g. AddFrameworkToEmbed(buildPath, "VtcSDK.framework");

3. **Q:** How to disable dark mode?

    This section applies to **Xcode 11** usage ([Reference](https://developer.apple.com/documentation/appkit/supporting_dark_mode_in_your_interface/choosing_a_specific_interface_style_for_your_ios_app)).
    
    Add code in `UpdatePlist()` method as below:
    ```cs
	rootDict.SetString("UIUserInterfaceStyle", "Light");
	```

4. **Q:** Is `VTCSdk` run on simulator?

    Please contact us to get `VTCSdk` version for simulator.

5. **Q** How to solve if occur error `MapFileParser.sh: Permission denied` while build with `Xcode`?

    Run command below:
    ```sh
    chmod +x /path/to/MapFileParser
    ```
