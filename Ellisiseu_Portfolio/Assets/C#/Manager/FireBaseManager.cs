using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Extensions;
using Google;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Fusion;

public class FirebaseManager : MonoBehaviour
{

    public static FirebaseManager firebaseManager;
    public static NetworkRunnerManager networkRunnerManager;
    public static NetworkSceneManagerDefault networkSceneManager;

    public static FirebaseManager GetFireBaseManager() { Init(); return firebaseManager; }
    public static NetworkSceneManagerDefault GetNetworkSceneManager() {  return networkSceneManager; }

    public string GoogleAPI = "71589248415-gje6sp8mr16bpsn17lr0ld067p8ha2qk.apps.googleusercontent.com";

    private GoogleSignInConfiguration configuration;

    Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;
    Firebase.Auth.FirebaseAuth auth;
    Firebase.Auth.FirebaseUser user;

    public Text Username, UserEmail;

    public GameObject LoginScreen, ProfileScreen;

    private void Awake()
    {
        configuration = new GoogleSignInConfiguration
        {
            WebClientId = GoogleAPI,
            RequestIdToken = true,
        };
    }

    private void Start()
    {
        Init();

        InitFirebase();
    }

    void InitFirebase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            if (task.Result == DependencyStatus.Available)
            {
                auth = FirebaseAuth.DefaultInstance;
                Debug.Log("Firebase 초기화 성공!");
            }
            else
            {
                Debug.LogError("Firebase 초기화 실패: " + task.Result);
            }
        });
    }

    public void GoogleSignInClick()
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        GoogleSignIn.Configuration.RequestEmail = true;

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnGoogleAuthenticatedFinished);
    }

    void OnGoogleAuthenticatedFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            Debug.LogError("Faulted");
        }
        else if (task.IsCanceled)
        {
            Debug.LogError("Cancelled");
        }
        else
        {
            Firebase.Auth.Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential(task.Result.IdToken, null);

            auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(task => {
                if (task.IsCanceled)
                {
                    return;
                }

                if (task.IsFaulted)
                {
                    Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                    return;
                }

                user = auth.CurrentUser;

                Username.text = user.DisplayName;
                UserEmail.text = user.Email;

                LoginScreen.SetActive(false);
                ProfileScreen.SetActive(true);

                // StartCoroutine(LoadImage(CheckImageUrl(user.PhotoUrl.ToString())));
            });
        }
    }

    static void Init() {

        if (firebaseManager == null) {

            GameObject go = new GameObject { name = "FireBaseManager" };
            go.AddComponent<FirebaseManager>();
            go.AddComponent<NetworkRunnerManager>();
            go.AddComponent<NetworkSceneManagerDefault>();
            Debug.Log("파이어베이스 매니저 생성");
            DontDestroyOnLoad(go);
            firebaseManager = go.GetComponent<FirebaseManager>();
            networkRunnerManager= go.GetComponent<NetworkRunnerManager>();
            networkSceneManager= go.GetComponent<NetworkSceneManagerDefault>();
        }
    
    }

    // private string CheckImageUrl(string url) {
    //     if (!string.IsNullOrEmpty(url)) {
    //         return url;
    //     }
    //     return imageUrl;
    // }

    // IEnumerator LoadImage(string imageUri) {
    //     WWW www = new WWW(imageUri);
    //     yield return www;

    //     UserProfilePic.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
    // }
}