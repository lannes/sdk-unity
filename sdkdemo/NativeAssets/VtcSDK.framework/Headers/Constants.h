//
//  Constants.h
//  VtcSDK
//
//  Created by Kent Vu on 11/22/16.
//  Copyright © 2016 VTCIntecom. All rights reserved.
//

#import <UIKit/UIKit.h>

//! Project version number for VtcSDK.
#define VtcSDKVersionString @"2.1.8"

//#define VTCLog(FORMAT, ...) NSLog((@"[FUNCTION: %s] [VTCSDK-logging] [Line %d] " FORMAT), __PRETTY_FUNCTION__, __LINE__, ##__VA_ARGS__);
#define VTCLog(FORMAT, ...)

#define ColorFromHEX(rgbValue) [UIColor colorWithRed:((float)((rgbValue & 0xFF0000) >> 16))/255.0 green:((float)((rgbValue & 0xFF00) >> 8))/255.0 blue:((float)(rgbValue & 0xFF))/255.0 alpha:1.0]
#define ColorFromAlphaHEX(rgbValue, alphaValue) [UIColor colorWithRed:((float)((rgbValue & 0xFF0000) >> 16))/255.0 green:((float)((rgbValue & 0xFF00) >> 8))/255.0 blue:((float)(rgbValue & 0xFF))/255.0 alpha:(float)alphaValue]

#define VTCLocalizeString(string) [Utilities localizeStringForString:string]

static NSString *kUserDefaultLoadedBufferLink = @"UserDefaultLoadedBufferLink";
static NSString *kUserDefaultFirstOpen = @"UserDefaultFirstOpenKey";
static NSString *kUserDefaultCachedUser = @"UserDefaultCachedUser";
static NSString *kUserDefaultCachedUTM = @"UserDefaultCachedUTM";
static NSString *kUserDefaultCachedNewRegisteredUser = @"UserDefaultCachedNewRegisteredUser";
static NSString *kUserDefaultStackOfIAPPayment = @"UserDefaultStackOfIAPPayment";
static NSString *kUserDefaultStackOfIAPFinishLog = @"UserDefaultStackOfIAPFinishLog";

static NSString *kKeychainAdvertisingIdentify = @"Ads_Id";
static NSString *kKeychainLoggedInstall = @"cachedInstall";

static NSString *kNotificationCheckingWithFireBase = @"NotificationCheckingWithFireBase";
static NSString *kNotificationOpenBufferLink = @"NotificationOpenBufferLink";
static NSString *kNotificationGotFacebookGoogleAuthenConfig = @"NotificationGotFacebookGoogleAuthenConfig";

static NSString *generalDeviceType = @"3";

static CGFloat kWidthOfLandscapeSocialLoginButton = 50.0;

static NSString *kAPNSReceivedType = @"RECEIVED";
static NSString *kAPNSClickedType = @"CLICKED";

static NSString *kStartLogInApp = @"START_PAY_STORE";
static NSString *kFinishLogInApp = @"FINISH_PAY_STORE";

typedef NS_ENUM(NSInteger, SDKLanguage) {
    SDKLanguageViet = 0,
    SDKLanguageEnglish,
    SDKLanguagePhilipine,
    SDKLanguageThailand,
    SDKLanguageLaos,
    SDKLanguageCambodia,
    SDKLanguageMyanmar,
    SDKLanguageMalaysia,
    SDKLanguageIndonesia
};

typedef NS_ENUM(NSInteger, TrackingHitType) {
    TrackingHitTypeEvent = 0,
    TrackingHitTypeScreen
};

typedef NS_ENUM(NSInteger, GACategoryType) {
    GACategoryTypeInAppEvent = 0,
    GACategoryTypeAuthen,
    GACategoryTypeTracking,
    GACategoryTypePayment
};

typedef NS_ENUM(NSInteger, GAActionType) {
    GAActionTypeFirstOpen = 0,
    GAActionTypeOpenApp,
    GAActionTypeCloseApp,
    GAActionTypeCrashApp,
    GAActionTypeInstalledApp,
    GAActionTypeLogin,
    GAActionTypeRegister,
    GAActionTypeUTMCampaign,
    GAActionTypeUTMMedium,
    GAActionTypeUTMSource,
    GAActionTypeInGame
};

typedef NS_ENUM(NSInteger, GALabelType) {
    GALabelTypeNone = 0,
    GALabelTypeVTCID,
    GALabelTypeFacebook,
    GALabelTypeGoogle,
    GALabelTypeVTC,
    GALabelTypeUTM,
    GALabelTypeVTCPay,
    GALabelTypeInAppPurchase
};

typedef NS_ENUM(NSInteger, APICategoryType) {
    APICategoryTypeFirstOpen = 1,
    APICategoryTypeOpen = 2,
    APICategoryTypeCloseApp = 3,
    APICategoryTypeCrashApp = 4,
    APICategoryTypeClick = 5,
    APICategoryTypePayment = 6,
    APICategoryTypeInstall = 7,
    APICategoryTypeLoginVTC = 8,
    APICategoryTypeLoginGoogle = 9,
    APICategoryTypeLoginFacebook = 10,
    APICategoryTypeOther = 0
};

static NSString *kAppsFlyerEventTypeLogin = @"af_login";
static NSString *kAppsFlyerEventTypeGMOLogin = @"gmo_login";
static NSString *kAppsFlyerEventTypeRegister = @"af_complete_registration";
static NSString *kAppsFlyerEventTypeCompleteTutorial = @"af_tutorial_completion";
static NSString *kAppsFlyerEventTypeLevelAchieved = @"af_level_achieved";
static NSString *kAppsFlyerEventTypeInitialCheckout = @"af_initiated_checkout";
static NSString *kAppsFlyerEventTypePurchase = @"af_purchase";

// on facebook
static NSString *kAppsFlyerEventTypeFBRegister = @"af_fb_complete_registration";
// Google
static NSString *kAppsFlyerEventTypeGGRegister = @"af_gg_complete_registration";

static NSString *kAppsFlyerEventTypeLoginValueKey = @"af_login";
static NSString *kAppsFlyerEventTypeGMOLoginValueKey = @"gmo_login";
static NSString *kAppsFlyerEventTypeRegisterValueKey = @"af_complete_registration";
static NSString *kAppsFlyerEventTypeCompleteTutorialValueKey = @"af_tutorial_completion";
static NSString *kAppsFlyerEventTypeLevelAchievedValueKey = @"af_level_achieved";
static NSString *kAppsFlyerEventTypeInitialCheckoutValueKey = @"af_initiated_checkout";
static NSString *kAppsFlyerEventTypePurchaseValueKey = @"af_purchase";
