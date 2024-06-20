using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameManageMent : MonoBehaviour
{
    
    private static GameManageMent instance;

    // 인스턴스에 접근하기 위한 프로퍼티
    public static GameManageMent Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManageMent>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<GameManageMent>();
                    singletonObject.name = typeof(GameManageMent).ToString() + " (Singleton)";

                    // 싱글톤 오브젝트가 다른 씬에서도 유지되도록 설정
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return instance;
        }
    }


    [Header("Cursor_Settings")]
    [SerializeField] private Texture2D customCursorTexture; // 커서 이미지
    [SerializeField] private Vector2 hotSpot = Vector2.zero; // 커서 핫스팟 (이미지의 중심을 기준으로 커서 위치를 설정)

    [Header("Character_Create")]
    [SerializeField] private CinemachineVirtualCamera virtual_Camera;
    [SerializeField] private GameObject Player_Prefap;
    [SerializeField] private PlayerInfo player_Info;
    [SerializeField] private Slider player_Hp;
    [SerializeField] private Slider player_Ep;


    [SerializeField] private Text playerNickName_Text;
    [SerializeField] private Text playerLevel_Text;
    [SerializeField] private Text playerHp_Text;
    [SerializeField] private Text playerEp_Text;
    [SerializeField] private Text playerAp_Text;
    [SerializeField] private Text playerDp_Text;


    [Header("MiniMap")]
    [SerializeField] private MinMapFlow miniMap;

    [Header("Sound_Manager")]
    [SerializeField] private AudioMixerGroup audioMixerGroup;



    private void Awake()
    {
        // 이미 인스턴스가 존재하는 경우, 중복되는 오브젝트를 파괴
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    //public void IntroStart()
    //{
    //    SceneManager.LoadScene("Intro_Scene");
    //}
    //
    //// 예시로 추가한 싱글톤의 기능 메서드
    //public void DoSomething()
    //{
    //    Debug.Log("Singleton instance is working!");
    //}


    private void Start()
    {
        //IntroStart();
        ChangeCursor(customCursorTexture, hotSpot);
        GameObject Player = Instantiate(Player_Prefap);
        virtual_Camera.Follow = Player.transform;
        miniMap.player = Player.transform;
        player_Info = Player.GetComponent<PlayerInfo>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("메뉴창 오픈하기");
        }

        playerNickName_Text.text = $"{player_Info.playerNickName}";
        playerLevel_Text.text = $"Lv. {player_Info.playerLevel}";
        playerHp_Text.text = $"HP : {player_Info.playerNowHp} / {player_Info.playerMaxHp}";
        playerEp_Text.text = $"HP : {player_Info.playerNowEp} / {player_Info.playerMaxEp}";
        playerAp_Text.text = $"AP : {player_Info.playerAp}";
        playerDp_Text.text = $"DP : {player_Info.playerDp}";

        player_Hp.value = player_Info.playerNowHp / player_Info.playerMaxHp;
        player_Ep.value = player_Info.playerNowEp / player_Info.playerMaxEp;

    }

    private void ChangeCursor(Texture2D cursorTexture, Vector2 hotSpot)
    {
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
    }

    private void OnDisable()
    {
        // 스크립트가 비활성화될 때 기본 커서로 복원
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
