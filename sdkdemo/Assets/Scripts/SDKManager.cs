using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VTCUser {
	private string _accessToken;
	private string _billingAccessToken;
	private string _accountName;
	private string _accountId;
	private int _vcoinBalance;
	private string _email;

	public string accessToken {
		get { return _accessToken; }
		set { _accessToken = value; }
	}

    public string billingAccessToken {
		get { return _billingAccessToken; }
		set { _billingAccessToken = value; }
	}

	public string accountName {
		get { return _accountName; }
		set { _accountName = value; }
	}
	
	public string accountId {
		get { return _accountId; }
		set { _accountId = value; }
	}

	public int vcoinBalance {
		get { return _vcoinBalance; }
		set { _vcoinBalance = value; }
	}

	public string email {
		get { return _email; }
		set { _email = value; }
	}
}

public interface IOnActivityResult {
	void onMessage(string message, int requestCode);
}

public class SDKManager: MonoBehaviour {
	public static int ENVIRONMENT_SANDBOX = 0;
	public static int ENVIRONMENT_REAL = 1;
	public static int ALLOW_CHANGE_ACCOUNT = 0;
	public static string VI = "vi";
	public static string ENG = "eng";
	public static VTCUser vtcUser = new VTCUser();

	#if UNITY_ANDROID

	public static int SIGNIN_CODE = 100;
	public static int OPENSHOP_CODE = 200;
	public static int SIGNOUT_CODE = 300;

	public const string CORE_PACKAGE = "com.strategy.intecom.vtc.tracking";
	public const string SDK_MANAGER = CORE_PACKAGE + ".SDKManager";
	public const string MAIN_PACKAGE = "com.software.intecom.vtc.unity";
	public const string UNITY_SDK_MANAGER = MAIN_PACKAGE + ".UnitySDKManager";
	public static IOnActivityResult m_iOnActivityResult = null;
 
	/*
	class SignOutCallback: AndroidJavaProxy {
		public SignOutCallback() : base(SDK_MANAGER + "$onSignOutCallBack") { }

		public void onSignout() {
			Debug.Log("onSignout");
		}
	}

	class DidPurchaseSuccessfullyCallback: AndroidJavaProxy {
		public DidPurchaseSuccessfullyCallback() : base(SDK_MANAGER + "$DidPurchaseSuccessfully") { }

		public void onDidPurchaseSuccessfully() {
			Debug.Log("onDidPurchaseSuccessfully");
		}
	}
	*/

	class AndroidPluginCallback: AndroidJavaProxy {
		public AndroidPluginCallback(): base(MAIN_PACKAGE + ".PluginCallback") {}

		public void onMessage(string message, int requestCode) {
			if (requestCode == SIGNIN_CODE) {
				using (AndroidJavaObject sdkManager = new AndroidJavaObject(SDK_MANAGER)) {
					using (AndroidJavaObject obj = sdkManager.GetStatic<AndroidJavaObject>("userModel")) {
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

	public static void SetEnvironment(int env) {
		#if UNITY_ANDROID

		using (AndroidJavaClass unitySdkManager = new AndroidJavaClass(UNITY_SDK_MANAGER)) {
			unitySdkManager.CallStatic("setEnviroment", env);
		}

		#endif
	}

	public static void InitStartSDK(AndroidJavaObject activity) {
		#if UNITY_ANDROID

		using (AndroidJavaClass unitySdkManager = new AndroidJavaClass(UNITY_SDK_MANAGER)) {
			unitySdkManager.CallStatic("initStartSDK", activity);
		}
  		
		#endif
	}

	public static void SetClientId(string clientId) {
		#if UNITY_ANDROID
		
		using (AndroidJavaClass VTCString = new AndroidJavaClass("com.strategy.intecom.vtc.common.VTCString")) {
			VTCString.SetStatic("CLIENT_ID", clientId);
		}

		#endif
	}

	public static void SetClientSecret(string secretId) {
		#if UNITY_ANDROID

		using (AndroidJavaClass VTCString = new AndroidJavaClass("com.strategy.intecom.vtc.common.VTCString")) {
			VTCString.SetStatic("CLIENT_SECRET", secretId);
		}

		#endif
	}

	public static void SignIn(AndroidJavaObject activity, IOnActivityResult iOnActivityResult) {
		#if UNITY_ANDROID
		m_iOnActivityResult = iOnActivityResult;

		using (AndroidJavaClass unitySdkManager = new AndroidJavaClass(UNITY_SDK_MANAGER)) {
        	unitySdkManager.CallStatic("SignIn", activity, new AndroidPluginCallback());
		}

		#endif
	}

	public static void UpdateGameInfo(string id, string data) {
		#if UNITY_ANDROID
		
		using (AndroidJavaClass unitySdkManager = new AndroidJavaClass(UNITY_SDK_MANAGER)) {
			unitySdkManager.CallStatic("updateGameInfo", id, data);
		}	
		
		#endif
	}

	public static void OpenShop(AndroidJavaObject activity, IOnActivityResult iOnActivityResult) {
		#if UNITY_ANDROID
		m_iOnActivityResult = iOnActivityResult;

		/*
		using (AndroidJavaClass sdkManager = new AndroidJavaClass(SDK_MANAGER)) {
			sdkManager.CallStatic("setDidPurchaseSuccessfully", new DidPurchaseSuccessfullyCallback());
		}
		*/

		using (AndroidJavaClass unitySdkManager = new AndroidJavaClass(UNITY_SDK_MANAGER)) {
			unitySdkManager.CallStatic("openShop", activity);
		}

		#endif
	}

	public static void CloseShop() {
		#if UNITY_ANDROID
		
		using (AndroidJavaClass unitySdkManager = new AndroidJavaClass(UNITY_SDK_MANAGER)) {
			unitySdkManager.CallStatic("closeShop");
		}

		#endif
	}

	public static void SignOut() {
		#if UNITY_ANDROID

		/*
		using (AndroidJavaClass sdkManager = new AndroidJavaClass(SDK_MANAGER)) {
			sdkManager.CallStatic("setOnSignOutCallBack", new SignOutCallback());
		}
		*/
		
		using (AndroidJavaClass unitySdkManager = new AndroidJavaClass(UNITY_SDK_MANAGER)) {
			unitySdkManager.CallStatic("SignOut");
		}
		
		#endif
	}
}
