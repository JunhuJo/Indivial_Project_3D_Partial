using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManageMent : MonoBehaviour
{
    [Header("Cursor Settings")]
    [SerializeField] private Texture2D customCursorTexture; // Ŀ�� �̹���
    [SerializeField] private Vector2 hotSpot = Vector2.zero; // Ŀ�� �ֽ��� (�̹����� �߽��� �������� Ŀ�� ��ġ�� ����)

    [Header("CharacterCreate")]
    [SerializeField] private CinemachineVirtualCamera virtual_Camera;
    [SerializeField] private GameObject Player_Prefap;


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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("�޴�â �����ϱ�");
        }
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
