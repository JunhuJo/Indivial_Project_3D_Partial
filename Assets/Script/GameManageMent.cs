using Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManageMent : MonoBehaviour
{
    
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
    [SerializeField] private bool gamePlayeScene = false;

    [Header("Menu")]
    [SerializeField] private GameObject escMenu;
    //[SerializeField] private Slider BGM;
    //[SerializeField] private Slider effect_Sound;




    private void Start()
    {
        escMenu.gameObject.SetActive(false);

        //GameManager.Instance.SetBGMVolume(GameManager.Instance.GetBGMVolume());
        //GameManager.Instance.SetSFXVolume(GameManager.Instance.GetSFXVolume());

        ChangeCursor(customCursorTexture, hotSpot);
        GameObject Player = Instantiate(Player_Prefap);
        virtual_Camera.Follow = Player.transform;
        miniMap.player = Player.transform;
        player_Info = Player.GetComponent<PlayerInfo>();
    }

    private void Update()
    {
        EscMeunOpen();
        //OnBGMVolumeChanged();
        //OnSFXVolumeChanged();

        if (gamePlayeScene)
        {
            PlayerInfoUpdate();
        }
    }

    //public void OnBGMVolumeChanged()
    //{
    //    GameManager.Instance.SetBGMVolume(BGM.value);
    //}
    //
    //public void OnSFXVolumeChanged()
    //{
    //    GameManager.Instance.SetSFXVolume(effect_Sound.value);
    //}


    private void EscMeunOpen()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!escMenu.activeSelf)
            {
                escMenu.gameObject.SetActive(true);
            }

            if (escMenu.activeSelf)
            {
                escMenu.gameObject.SetActive(false);
            }
        }
    }

    private void PlayerInfoUpdate()
    {
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
