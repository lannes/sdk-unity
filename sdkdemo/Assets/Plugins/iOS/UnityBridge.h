#ifdef __cplusplus
extern "C" {
#endif

typedef void(*DelegateCallbackFunction)(const char *message, int requestCode);
void unitybridge_setDelegate(DelegateCallbackFunction callback);

void initStartSDK();
void signIn();
void updateGameInfo(const char* sId, const char* sData);
void openShop();
void signOut();
 
#ifdef __cplusplus
}
#endif