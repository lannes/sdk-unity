#import "UnityBridge.h"
#import <VtcSDK/VtcSDK.h>

const int SIGNIN_CODE = 100;
const int OPENSHOP_CODE = 200;

DelegateCallbackFunction delegate = NULL;

@interface UnityBridge : NSObject<SDKManagerDelegate>
@end
static UnityBridge *__delegate = nil;

void unitybridge_setDelegate(DelegateCallbackFunction callback) {
    delegate = callback;
}

void initStartSDK() {
    if (!__delegate) {
      __delegate = [[UnityBridge alloc] init];
    }

    [SDKManager defaultManager].delegate = __delegate;
}

void signIn() {
    [[SDKManager defaultManager] signIn];
}

void updateGameInfo(const char* sId, const char* sData) {
    NSString *areaId = [NSString stringWithUTF8String:sId];
    NSString *data = [NSString stringWithUTF8String:sData];

    [[SDKManager defaultManager] updateGameInfo:areaId extendData:data];
}

void openShop() {
    //[[SDKManager defaultManager] openShop];
}

void signOut() {
    [[SDKManager defaultManager] signOut];
}

@implementation UnityBridge
/*
   This method's revoked after user logged in successfully
   Return the logged in instance
*/
- (void)sdkManagerDidSignInWithUser:(VIDUser *)user {
    if (delegate != NULL) {
        delegate("sdkManagerDidSignInWithUser", SIGNIN_CODE);
    }
    NSLog(@"username = %@", user.userName);
    NSLog(@"user_id = %@", user.userId);
}

/*
   - This method's revoked after user logged out successfully
   - Remember to call sign-in method to re-open sign-in interface
*/
- (void)sdkManagerDidSignOut {
    // do something here, ex: back to root game screen
    // call method to open sign-in interface
    [[SDKManager defaultManager] signIn];
}

/*
   - This method's revoked after user purchased a payment package successfully
   - Normally, almost payment progress steps will be done on server side, even the adding virtual gold of game to an user, client side just do only one thing: re-update balance ingame and report last result to user.
*/
- (void)sdkManagerDidPurchaseSuccessfully {
    NSLog(@"Payment successfully");
    // do something here if need
    if (delegate != NULL) {
        delegate("", OPENSHOP_CODE);
    }
}
       
@end
