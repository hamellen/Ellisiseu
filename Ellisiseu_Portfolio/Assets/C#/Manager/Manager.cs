using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    static Manager manager;
    public static Manager GetManager() { Init(); return manager; }




    UIManager ui = new UIManager();//UI 관리
    ResourcesManager resourcesManager = new ResourcesManager();
    //SceneManagerEx scenemanager = new SceneManagerEx();//씬 전환
    SoundManager soundManager = new SoundManager();//소리 
    DataManager dataManager = new DataManager();//데이터 보관
    ItemManager itemManager = new ItemManager();//실질적 아이템 현황
    TokenManager tokenManager = new TokenManager();//UniTask 중단 설정
    //NetworkRunnerManager networkRunnerManager = new NetworkRunnerManager();//퓨전 관리 


    public static DataManager DATAMANAGER { get { return manager.dataManager; } }

    public static ItemManager ITEMMANAGER { get { return manager.itemManager; } }

    
    public static UIManager UI { get { return manager.ui; } }

    
    public static ResourcesManager RESOURCES { get { return manager.resourcesManager; } }

    //public static SceneManagerEx SCENEMANAGER { get { return manager.scenemanager; } }

    public static SoundManager SOUNDMANAGER { get { return manager.soundManager; } }

    public static TokenManager TOKENMANAGER { get { return manager.tokenManager; } }

   // public static NetworkRunnerManager NETWORKRUNNERMANAGER { get { return manager.networkRunnerManager; } }


    static void Init()
    {
       

        if (manager==null) {

           
            GameObject go = new GameObject { name = "Manager_Object" };
            go.AddComponent<Manager>();
            Debug.Log("매니저 생성");
            DontDestroyOnLoad(go);
            manager = go.GetComponent<Manager>();
        }

        SOUNDMANAGER.Init();
        //NETWORKRUNNERMANAGER.Init();
    }



    // Start is called before the first frame update
    void Start()
    {
        Init();


    }

    
}
