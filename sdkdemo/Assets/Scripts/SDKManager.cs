using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices; 
using UnityEngine;

[System.Serializable]
public class VTCUser {
	public string accountName;
	public string accountId;
	public string accessToken;
	public string billingAccessToken;
	public int vcoinBalance;
	public string email;

	public static VTCUser CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<VTCUser>(jsonString);
    }
}

public interface IOnActivityResult {
	void onMessage(string message, int requestCode);
}

public class SDKManager: MonoBehaviour {
	public static int ENVIRONMENT_SANDBOX = 0;
	public static int ENVIRONMENT_LIVE = 1;
	public static int ALLOW_CHANGE_ACCOUNT = 0;
	public static string VI = "vi";
	public static string ENG = "eng";
	public static VTCUser vtcUser = null;

	public static int SIGNIN_CODE = 100;
	public static int OPENSHOP_CODE = 200;

	#if UNITY_IOS
	
	public delegate void DelegateMessage(string message, int requestCode);

	[DllImport("__Internal")]
 	private static extern void unitybridge_setDelegate(DelegateMessage callback);

	[DllImport ("__Internal")]
	private static extern void initStartSDK();

	[DllImport ("__Internal")]
	private static extern void signIn();
	
	[DllImport ("__Internal")]
  	private static extern void updateGameInfo(string sId, string sData);

	[DllImport ("__Internal")]
	private static extern void openShop();

	[DllImport ("__Internal")]
	private static extern void signOut();

	#endif

	public static IOnActivityResult m_iOnActivityResult = null;

	#if UNITY_ANDROID

	public const string CORE_PACKAGE = "com.strategy.intecom.vtc.tracking";
	public const string SDK_MANAGER = CORE_PACKAGE + ".SDKManager";

	public const string MAIN_PACKAGE = "com.software.intecom.vtc.unity";
	public const string UNITY_SDK_MANAGER = MAIN_PACKAGE + ".UnitySDKManager";
 
	class AndroidPluginCallback: AndroidJavaProxy {
		public AndroidPluginCallback(): base(MAIN_PACKAGE + ".PluginCallback") {}

		public void onMessage(string message, int requestCode) {
			if (requestCode == SIGNIN_CODE) {
				using (AndroidJavaObject sdkManager = new AndroidJavaObject(SDK_MANAGER)) {
					using (AndroidJavaObject obj = sdkManager.GetStatic<AndroidJavaObject>("userModel")) {
						SDKManager.vtcUser = new VTCUser();
						SDKManager.vtcUser.accessToken = obj.Call<string>("getAccessToken");
						SDKManager.vtcUser.billingAccessToken = obj.Call<string>("getBillingAccessToken");
						SDKManager.vtcUser.accountName = obj.Call<string>("getAccountName");
						SDKManager.vtcUser.accountId = obj.Call<string>("getAccountId");
						SDKManager.vtcUser.vcoinBalance = obj.Call<int>("getVcoinBalance");
						SDKManager.vtcUser.email = obj.Call<string>("getEmail");						
					}
				}
			}

			if (requestCode == OPENSHOP_CODE) {				
			}

			if (SDKManager.m_iOnActivityResult != null) {
				SDKManager.m_iOnActivityResult.onMessage(message, requestCode);
			}
		}
	}

	#endif

	#if UNITY_ANDROID
	public static void SetEnvironment(int env) {
		using (AndroidJavaClass unitySdkManager = new AndroidJavaClass(UNITY_SDK_MANAGER)) {
			unitySdkManager.CallStatic("setEnviroment", env);
		}
	}
	#endif

	#if UNITY_IOS
	public static void InitStartSDK() {
		initStartSDK();
	}
	#endif

	#if UNITY_ANDROID
	public static void InitStartSDK(AndroidJavaObject activity) {
		using (AndroidJavaClass unitySdkManager = new AndroidJavaClass(UNITY_SDK_MANAGER)) {
			unitySdkManager.CallStatic("initStartSDK", activity);
		}
	}
	#endif
	
	#if UNITY_ANDROID
	public static void SetClientId(string clientId) {	
		using (AndroidJavaClass VTCString = new AndroidJavaClass("com.strategy.intecom.vtc.common.VTCString")) {
			VTCString.SetStatic("CLIENT_ID", clientId);
		}
	}

	public static void SetClientSecret(string secretId) {
		using (AndroidJavaClass VTCString = new AndroidJavaClass("com.strategy.intecom.vtc.common.VTCString")) {
			VTCString.SetStatic("CLIENT_SECRET", secretId);
		}
	}
	#endif

	#if UNITY_IOS
	public static void SignIn(DelegateMessage callback) {
		unitybridge_setDelegate(callback);
		signIn();
	}
	#endif

	#if UNITY_ANDROID
	public static void SignIn(AndroidJavaObject activity, IOnActivityResult iOnActivityResult) {
		m_iOnActivityResult = iOnActivityResult;

		using (AndroidJavaClass unitySdkManager = new AndroidJavaClass(UNITY_SDK_MANAGER)) {
        	unitySdkManager.CallStatic("SignIn", activity, new AndroidPluginCallback());
		}
	}
	#endif

	public static void UpdateGameInfo(string id, string data) {
		#if UNITY_ANDROID
		using (AndroidJavaClass unitySdkManager = new AndroidJavaClass(UNITY_SDK_MANAGER)) {
			unitySdkManager.CallStatic("updateGameInfo", id, data);
		}	
		#endif

		#if UNITY_IOS
		updateGameInfo(id, data);
		#endif
	}

	public static void OpenShop(AndroidJavaObject activity, IOnActivityResult iOnActivityResult) {
		#if UNITY_ANDROID
		m_iOnActivityResult = iOnActivityResult;

		using (AndroidJavaClass unitySdkManager = new AndroidJavaClass(UNITY_SDK_MANAGER)) {
			unitySdkManager.CallStatic("openShop", activity);
		}
		#endif
	}
	
	public static void SignOut() {
		#if UNITY_ANDROID
		using (AndroidJavaClass unitySdkManager = new AndroidJavaClass(UNITY_SDK_MANAGER)) {
			unitySdkManager.CallStatic("SignOut");
		}		
		#endif

		#if UNITY_IOS
		signOut();
		#endif
	}
}
