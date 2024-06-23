using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IntroSceneManager : MonoBehaviour
{
    [Header("All_Set")]
    [SerializeField] private GameObject intro;
    [SerializeField] private GameObject start_Fade;
    [SerializeField] private GameObject start_Load;
    [SerializeField] private GameObject loby;


    [Header("IntroSet")]
    [SerializeField] private Texture2D customCursorTexture;
    [SerializeField] private Vector2 hotSpot = Vector2.zero;
    [SerializeField] private Image start_Game_Set;
    public float fadeInDuration = 1.0f; // 페이드 인 시간
    private bool gameStart = false;

    [Header("Option")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button loby_Exit_Btn;
    [SerializeField] private GameObject OptionMenu;
    [SerializeField] private Dropdown resolution;

    [Header("BGM_Effect_Sound")]

    [SerializeField] private Slider BGM;
    [SerializeField] private Slider effect_Sound;
    [SerializeField] private Text bgmVolumeText; // BGM 볼륨 텍스트
    [SerializeField] private Text effectSoundVolumeText; // 효과음 볼륨 텍스트

    [Header("Loby_Exit")]
    [SerializeField] private GameObject exit_UI;
    [SerializeField] private GameObject ui_Wall;
    [SerializeField] private float upSpeed = 1000;
    [SerializeField] private float stopTime = 0.8f;
    private bool exit = false;
    private bool exit_down = false;

    [Header("Loby_Story_Mode")]
    [SerializeField] private GameObject story_Mode_UI;
    private bool story_Btn_Up = false;
    private bool story_Btn_Down = false;


    private void Start()
    {
        ChangeCursor(customCursorTexture, hotSpot);
        SetStart_Size();

        startButton.onClick.AddListener(OnStartButtonClicked);
        loby_Exit_Btn.onClick.AddListener(OnClick_Loby_Exit);
        

        // 슬라이더 초기값 설정 및 이벤트 리스너 추가
        BGM.value = Mathf.Pow(10, GameManager.Instance.GetBGMVolume() / 20);
        effect_Sound.value = Mathf.Pow(10, GameManager.Instance.GetSFXVolume() / 20);

        BGM.onValueChanged.AddListener(delegate { OnBGMVolumeChanged(); });
        effect_Sound.onValueChanged.AddListener(delegate { OnSFXVolumeChanged(); });


    }

    private void Update()
    {
        UpdateBGMVolumeText(BGM.value);
        UpdateSFXVolumeText(effect_Sound.value);
        StartCoroutine(UpExit());
        StartCoroutine(Story_Mode_Ready());
    }

    void SetStart_Size()
    {
        InitializeDropdown();
        // Dropdown의 onValueChanged 이벤트에 리스너 추가
        resolution.onValueChanged.AddListener(delegate { OnDropdownValueChanged(resolution); });
        OptionMenu.gameObject.SetActive(false);
    }

    void InitializeDropdown()
    {
        // Dropdown 옵션 설정
        resolution.options.Clear();
        resolution.options.Add(new Dropdown.OptionData("1920 x 1080"));
        resolution.options.Add(new Dropdown.OptionData("1280 x 720"));
        resolution.options.Add(new Dropdown.OptionData("800 x 600"));

        // 초기 선택값 설정 (옵션 인덱스)
        resolution.value = 0;
    }

    void OnDropdownValueChanged(Dropdown change)
    {
        int selectedOption = change.value;
        Debug.Log("Selected Option: " + selectedOption);

        // 선택된 옵션에 따라 다른 메서드 호출
        switch (selectedOption)
        {
            case 0:
                GameManager.Instance.OnClick_SetSize_First();
                break;
            case 1:
                GameManager.Instance.OnClick_SetSize_Scound();
                break;
            case 2:
                GameManager.Instance.OnClick_SetSize_Tread();
                break;
            default:
                Debug.LogError("Unknown Option");
                break;
        }
    }

    //Game Manger
    private void OnStartButtonClicked()
    {
        StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn()
    {
        if (start_Game_Set != null)
        {
            start_Fade.gameObject.SetActive(true);
            start_Game_Set.gameObject.SetActive(true);
            float elapsedTime = 0f;
            Color color = start_Game_Set.color;
            color.a = 0f; // 시작할 때 완전히 투명하게 설정
            start_Game_Set.color = color;

            while (elapsedTime < fadeInDuration)
            {
                elapsedTime += Time.deltaTime;
                color.a = Mathf.Clamp01(elapsedTime / fadeInDuration);
                start_Game_Set.color = color;
                yield return null;
            }

            // 페이드 인 완료 후 완전히 불투명하게 설정
            color.a = 1f;
            start_Game_Set.color = color;
            if (color.a == 1)
            {
                StartCoroutine(InLobyGo());
                
            }
        }

    }

    IEnumerator InLobyGo()
    {
        if (OptionMenu.activeSelf)
        { 
            OptionMenu.SetActive(false);    
        }
        intro.gameObject.SetActive(false);
        start_Load.gameObject.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        start_Load.gameObject.SetActive(false);
        loby.gameObject.SetActive(true);
    }

    public void OnBGMVolumeChanged()
    {
        GameManager.Instance.SetBGMVolume(BGM.value);
    }

    public void OnSFXVolumeChanged()
    {
        GameManager.Instance.SetSFXVolume(effect_Sound.value);
    }


    //Now Scene
    public void OnClick_Game_Start()
    {
        GameManager.Instance.OnClick_GameStart();
    }

    public void OnClick_OpenOption()
    {
        OptionMenu.gameObject.SetActive(true);
    }

    public void OnClick_CloseOption()
    {
        OptionMenu.gameObject.SetActive(false);
    }

    public void OnClick_Exit()
    {
        Application.Quit();
    }

    public void OnClick_Loby_Exit()
    {
        exit = true;
    }

    public void OnClick_Loby_Exit_Down()
    {
        exit_down = true;
    }

    public void OnClick_Story_Btn_Up()
    {
        story_Btn_Up = true;
    }

    public void OnClick_Story_Btn_Down()
    {
        story_Btn_Down = true;
    }

    public void OnClick_SetSize_One()
    {
        GameManager.Instance.OnClick_SetSize_First();
    }
    public void OnClick_SetSize_Two()
    {
        GameManager.Instance.OnClick_SetSize_Scound();
    }
    public void OnClick_SetSize_Three()
    {
        GameManager.Instance.OnClick_SetSize_Tread();
    }


    IEnumerator UpExit()
    {
        if (exit)
        {
           exit_UI.transform.Translate(Vector2.up * upSpeed * Time.deltaTime);
           ui_Wall.gameObject.SetActive(true);
           yield return new WaitForSeconds(stopTime);
           exit = false;
        }

        if (exit_down)
        {
            exit_UI.transform.Translate(Vector2.down * upSpeed * Time.deltaTime);
            yield return new WaitForSeconds(stopTime);
            ui_Wall.gameObject.SetActive(false);
            exit_down = false;
        }
    }

    IEnumerator Story_Mode_Ready()
    {
        if (story_Btn_Up)
        {
            loby.transform.Translate(Vector2.up * upSpeed * Time.deltaTime);
            yield return new WaitForSeconds(stopTime);
            
            story_Mode_UI.transform.Translate(Vector2.down * upSpeed * Time.deltaTime);
            yield return new WaitForSeconds(stopTime);
            story_Btn_Up = false;
        }

        if (story_Btn_Down)
        {
            loby.transform.Translate(Vector2.down * upSpeed * Time.deltaTime);
            yield return new WaitForSeconds(stopTime);

            story_Mode_UI.transform.Translate(Vector2.up * upSpeed * Time.deltaTime);
            yield return new WaitForSeconds(stopTime);
            story_Btn_Down = false;
        }
    }

    private void UpdateBGMVolumeText(float value)
    {
        int volumePercent = Mathf.RoundToInt(value * 100);
        bgmVolumeText.text = volumePercent + "%";
    }

    private void UpdateSFXVolumeText(float value)
    {
        int volumePercent = Mathf.RoundToInt(value * 100);
        effectSoundVolumeText.text = volumePercent + "%";
    }


    private void ChangeCursor(Texture2D cursorTexture, Vector2 hotSpot)
    {
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
    }

    private void OnDisable()
    {
       
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
