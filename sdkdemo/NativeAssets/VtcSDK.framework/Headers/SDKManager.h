//
//  SDKManager.h
//  VtcSDK
//
//  Created by Kent Vu on 7/25/16.
//  Copyright © 2016 VTCIntecom. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import "Constants.h"

@class VIDUser;

@protocol SDKManagerDelegate <NSObject>

@required
/*!
 @brief method to tells the application that user has signed in.
 @param user current user, who has just signed in
 @note require!
 */
- (void)sdkManagerDidSignInWithUser:(VIDUser *)user;
/*!
 @brief method to tells the application that user has signed out.
 @note require!
 */
- (void)sdkManagerDidSignOut;
/*!
 @brief method to tells the application that user has purchased a payment package successfully.
 @note require!
 */
- (void)sdkManagerDidPurchaseSuccessfully;

@end

@interface SDKManager : NSObject

@property (nonatomic, weak) id<SDKManagerDelegate> delegate;

@property (nonatomic) BOOL isShowCloseButtonInAuthenVC; // default NO
@property (nonatomic) BOOL allowRotationInLoginView; // default NO
@property (nonatomic) UIInterfaceOrientationMask loginViewOrientationMask; // default is UIInterfaceOrientationMaskPortrait

@property (nonatomic) BOOL allowRotationInPaymentView; // default YES
@property (nonatomic) UIInterfaceOrientationMask paymentViewOrientationMask; // default is UIInterfaceOrientationMaskAll

@property (nonatomic) BOOL isSandbox; //default NO
@property (nonatomic) BOOL ignoreCaptcha; //default NO
@property (nonatomic) BOOL isSaveAccessToken; // default NO
@property (nonatomic) NSString* sitekeyRecapchaIos;

/*!
 @brief shared instance of SDKManager
 */
+ (SDKManager *)defaultManager NS_AVAILABLE_IOS(8_0);

// MARK: handle application delegate methods

/*!
 @brief The very first method need to add to project. It will init the SDK and handle the most important delegate method of application.
 @param application shared application instance
 @param launchOptions object contains all launching configuration
 @note require
 */

+ (void)handleApplication:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions NS_AVAILABLE_IOS(8_0);
/*!
 @brief method to handle the open URL to the app
 @param application shared application instance
 @param url the open URL
 @param sourceApplication The bundle ID of the app that is requesting your app to open the URL (url).
 @param annotation A property list object supplied by the source app to communicate information to the receiving app.
 @note require
 */
+ (BOOL)handleApplication:(UIApplication *)application openURL:(NSURL *)url sourceApplication:(NSString *)sourceApplication annotation:(id)annotation NS_AVAILABLE_IOS(8_0);


/**
 

 @param application <#application description#>
 @param url <#url description#>
 @param options <#options description#>
 @return <#return value description#>
 */
+ (BOOL)handleApplication:(UIApplication *)application openURL:(NSURL *)url options:(nonnull NSDictionary<NSString *,id> *)options;

// MARK: methods
/*!
 @brief method to open sign in interface.
 @note require
 */
- (void)signIn NS_AVAILABLE_IOS(8_0);

// MARK: methods
/*!
 @brief method to open sign in interface.
 @param parentController the view controller where sign-in controller presented
 @note require
 */
- (void)signInFromViewController:(UIViewController *)parentController NS_AVAILABLE_IOS(8_0);

/*!
 @brief method to open register interface.
 @note optional
 */
- (void)registerNow NS_AVAILABLE_IOS(8_0);

/*!
 @brief method to open register interface.
 @param parentController the view controller where register controller presented
 @note optional
 */
- (void)registerFromViewController:(UIViewController *)parentController NS_AVAILABLE_IOS(8_0);

/*!
 @brief method to clear the current session and logged-in user info.
 @note require
 */
- (void)signOut NS_AVAILABLE_IOS(8_0);
// MARK:
/*!
 
 @brief method to open update player info.
 */
- (void)openUpdateInfoForm:(NSString *)hashAccount NS_AVAILABLE_IOS(8_0);
/*!
 
 @brief method to open purchase shop.
 */
//- (void)openShop NS_AVAILABLE_IOS(8_0);
/*!
 @brief method to open purchase shop.
 @param parentController the view controller where shop controller presented
 */
//- (void)openShopFromViewController:(UIViewController *)parentController NS_AVAILABLE_IOS(8_0);

/*!
 @brief method to close purchase shop.
 */
//- (void)closeShop NS_AVAILABLE_IOS(8_0);
/*!
 @brief method to update game server and character info.
 @param areaId game server id where containt player character. Must be not nil.
 @param dataString whatever extend data server game want to inspire to sdk server.
  @note require
 */
- (void)updateGameInfo:(NSString *)areaId extendData:(NSString *)dataString NS_AVAILABLE_IOS(8_0);

/*!
 @brief method to hit custom event to GA.
 @param activity type of activity
 @param extend name of undefine activity. Ex: if you want to log when user spend some coins, sao, gold,... you can log an activity with extend data is "TIEU_SAO" "TIEU_GOLD" "TIEU_COIN" ... 
        extend extend string must be uppercase and not include 'space' character
 @param userName logged in user name
 @param userId logged in user id
 @note optional
 */
- (void)hitActivity:(APICategoryType)activity extendData:(NSString *)extend forUser:(NSString *)userName userId:(NSString *)userId completion:(void (^)(BOOL status, id responsedObject, NSError *error))completionBlock  NS_AVAILABLE_IOS(8_0);
/*!
 @brief method to hit custom event to GA.
 @param activity type of activity
 @param extend name of undefine activity. Ex: if you want to log when user spend some coins, sao, gold,... you can log an activity with extend data is "TIEU_SAO" "TIEU_GOLD" "TIEU_COIN" ...
 extend extend string must be uppercase and not include 'space' character
 @param amountNumber quantity of extend parameter. Ex: gold, coins, ...
 @param userName logged in user name
 @param userId logged in user id
 @note optional
 */
- (void)hitActivity:(APICategoryType)activity extendData:(NSString *)extend amount:(NSInteger)amountNumber forUser:(NSString *)userName userId:(NSString *)userId completion:(void (^)(BOOL status, id responsedObject, NSError *error))completionBlock NS_AVAILABLE_IOS(8_0);

/*!
 @brief method to hit custom event to GA.
 @param activity type of activity
 @param category like Google Analytics
 @param action like Google Analytics
 @param label like Google Analytics
 @param value like Google Analytics
 @note optional
 */
- (void)hitCustomActivity:(NSInteger)activity
                 category:(NSString *)category
                   action:(NSString *)action
                    label:(NSString *)label
                    value:(NSNumber *)value
               completion:(void (^)(BOOL status, id responsedObject, NSError *error))completionBlock;
/*!
 @brief method to hit custom event to AppsFlyer.
 @param eventName event name
 @param valueOfEvent value for event
 @note optional
 */
- (void)logAppsFlyerCustomInGameEvent:(NSString *)eventName values:(NSDictionary *)valueOfEvent;
/*!
 @brief method to hit custom event to AppsFlyer when current user has finished newbie mission.
 @note optional
 */
- (void)logAppsFlyerInGameEventCompleteTutorial;
/*!
 @brief method to hit custom event to AppsFlyer when current user passed level 1.
 @note optional
 */
- (void)logAppsFlyerInGameEventLevelAchieved;

/*!
 @brief method to change main color on navigation bar.
 @param mainColor desired color.
 @note optional
 */
- (void)changeSDKMainColor:(UIColor *)mainColor;

/*!
 @brief method to show the notification message in the notification bar.
 @param message the message will be shown.
 @note optional
 */
- (void)showNotificationMessage:(NSString *)message;

/*!
 @brief hard code the utm source and campaign for direct download version.
 @param utmString the utm string.
 @note optional
 */
- (void)hardcodeUTM:(NSString *)utmString;

/*!
 @brief get the device token for APNS.
 @note optional
 */
- (NSString *)getDeviceToken;

/**
 checking uninstall With AF
 
 @param deviceToken <#deviceToken description#>
 */
+ (void)handleCheckingUninstallWithAF:(NSData *)deviceToken;


/**
 Shared Text to facebook

 @param link <#link description#>
 */
+ (void)facebookShareWithLink:(NSString *_Nonnull)link;


/**
 Shared Images to facebook

 @param images [UIImage]
 */
+ (void)faceBookShareWithImage:(NSArray *_Nonnull)images;

@end
