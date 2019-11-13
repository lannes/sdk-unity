//
//  VIDUser.h
//  VtcSDK
//
//  Created by Kent Vu on 7/25/16.
//  Copyright © 2016 VTCIntecom. All rights reserved.
//

#import <Foundation/Foundation.h>

typedef NS_ENUM(NSInteger, AuthenType) {
    AuthenTypeNormal = 0,
//    AuthenTypeFacebook = 1,
//    AuthenTypeGoogle = 2,
    AuthenTypeYahoo = 3
};

@interface VIDUser : NSObject

@property (nonatomic) BOOL signedIn;
@property (nonatomic, copy) NSString *accountUsingMobile;
@property (nonatomic, copy) NSString *extend;
@property (nonatomic, copy) NSString *userId;
@property (nonatomic, copy) NSString *accessToken;
@property (nonatomic, copy) NSString *userName;
@property (nonatomic) NSTimeInterval expiration;
@property (nonatomic) AuthenType authenType;
//@property (nonatomic) NSInteger vcoinBalance;
@property (nonatomic, copy) NSString *avatarURL;
@property (nonatomic, copy) NSString *currentGameVer;
@property (nonatomic) long currentGameVerNumber;
@property (nonatomic, copy) NSString *email;
@property (nonatomic, copy) NSString *mobile;
@property (nonatomic) NSInteger userStatus;

+ (VIDUser *)currentUser;

- (void)loadData:(NSDictionary *)userInfo;
- (void)resetData;

- (NSDictionary *)exportUserInfo;

@end
