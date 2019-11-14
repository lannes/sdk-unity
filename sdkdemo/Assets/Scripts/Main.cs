﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using AOT;

#if UNITY_ANDROID
public class Main : MonoBehaviour, IOnActivityResult {
#else
public class Main : MonoBehaviour {
#endif
	#if UNITY_IOS
	[MonoPInvokeCallback(typeof(SDKManager.DelegateMessage))] 
 	public static void onMessage(string message, int requestCode) {
		Debug.Log("Message received: " + message);

		if (requestCode == SDKManager.SIGNIN_CODE) {
			try {
				SDKManager.vtcUser = VTCUser.CreateFromJSON(message);
				Debug.Log("ACCOUNT NAME: " + SDKManager.vtcUser.accountName);
				Debug.Log("ACCOUNT ID: " + SDKManager.vtcUser.accountId);
				Debug.Log("VCOIN BALANCE: " + SDKManager.vtcUser.vcoinBalance);
			} catch (Exception e) {
				
			}
		}
 	}
 	#endif

	#if UNITY_ANDROID
	public void onMessage(string message, int requestCode) {
		if (requestCode == SDKManager.SIGNIN_CODE) {
			Debug.Log("ACCOUNT NAME: " + SDKManager.vtcUser.accountName);
			Debug.Log("ACCOUNT ID: " + SDKManager.vtcUser.accountId);
			Debug.Log("VCOIN BALANCE: " + SDKManager.vtcUser.vcoinBalance);

			// If you use VTC's payment please contact us to get more information.
			// SDKManager.UpdateGameInfo(id, data);
		}
	}
	#endif

  	void StartSDK() {
		#if UNITY_IOS
		// You must call set environment in UnityAppController.mm
		SDKManager.InitStartSDK();
		#endif

		#if UNITY_ANDROID
		SDKManager.SetEnvironment(SDKManager.ENVIRONMENT_SANDBOX);

		using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
			using (AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) {
				SDKManager.InitStartSDK(activity);
			}
		}

		SDKManager.SetClientId ("23d4c59d0fb261b2d711c14784f69f6b");
		SDKManager.SetClientSecret ("9c104e12f38bb9afe26c1b814cd2a2e1");
		#endif
  	}

	public void SignIn() {
		#if UNITY_IOS
		SDKManager.SignIn(onMessage);
		#endif

		#if UNITY_ANDROID
		using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
			using (AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) {
				SDKManager.SignIn(activity, this);
			}
		}
		#endif
	}

	public void SignOut() {
		#if UNITY_IOS || UNITY_ANDROID
		SDKManager.SignOut();		
		#endif
	}

	// Use this for initialization
	void Start () {
		StartSDK();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
