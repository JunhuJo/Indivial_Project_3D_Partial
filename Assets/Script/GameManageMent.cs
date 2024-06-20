using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameManageMent : MonoBehaviour
{
    
    private static GameManageMent instance;

    // �ν��Ͻ��� �����ϱ� ���� ������Ƽ
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

                    // �̱��� ������Ʈ�� �ٸ� �������� �����ǵ��� ����
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return instance;
        }
    }


    [Header("Cursor_Settings")]
    [SerializeField] private Texture2D customCursorTexture; // Ŀ�� �̹���
    [SerializeField] private Vector2 hotSpot = Vector2.zero; // Ŀ�� �ֽ��� (�̹����� �߽��� �������� Ŀ�� ��ġ�� ����)

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
        // �̹� �ν��Ͻ��� �����ϴ� ���, �ߺ��Ǵ� ������Ʈ�� �ı�
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
    //// ���÷� �߰��� �̱����� ��� �޼���
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
            Debug.Log("�޴�â �����ϱ�");
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
        // ��ũ��Ʈ�� ��Ȱ��ȭ�� �� �⺻ Ŀ���� ����
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
