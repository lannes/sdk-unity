using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour, IOnActivityResult {
	public void onMessage(string message, int requestCode) {
		if (requestCode == SDKManager.SIGNIN_CODE) {
			Debug.Log("ACCOUNT NAME: " + SDKManager.vtcUser.accountName);
			Debug.Log("ACCOUNT ID: " + SDKManager.vtcUser.accountId);
			Debug.Log("VCOIN BALANCE: " + SDKManager.vtcUser.vcoinBalance);
		}
	}

  	void CallNativePlugin() {
		#if UNITY_ANDROID

		SDKManager.SetEnviroment (SDKManager.ENVIRONMENT_SANDBOX);

		using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
			using (AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) {
				SDKManager.InitStartSDK (activity);
				SDKManager.SetClientId ("23d4c59d0fb261b2d711c14784f69f6b");
				SDKManager.SetClientSecret ("9c104e12f38bb9afe26c1b814cd2a2e1");
			}
		}

		#endif
  	}

	public void SignIn() {
		#if UNITY_ANDROID

		using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
			using (AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) {
				SDKManager.SignIn (activity, this);
			}
		}

		#endif
	}

	public void SignOut() {
		#if UNITY_ANDROID
		
		SDKManager.SignOut();		
		
		#endif
	}

	// Use this for initialization
	void Start () {
		CallNativePlugin();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
