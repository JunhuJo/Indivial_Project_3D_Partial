using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IntroSceneManager : MonoBehaviour
{
    [Header("IntroSet")]
    [SerializeField] private Image start_Game_Set;
    public float fadeInDuration = 1.0f; // 페이드 인 시간
    private bool gameStart = false;

    [Header("Option")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button loby_Exit_Btn;
    [SerializeField] private GameObject OptionMenu;

    [Header("BGM_Effect_Sound")]

    [SerializeField] private Slider BGM;
    [SerializeField] private Slider effect_Sound;
    [SerializeField] private Text bgmVolumeText; // BGM 볼륨 텍스트
    [SerializeField] private Text effectSoundVolumeText; // 효과음 볼륨 텍스트

    [Header("Loby_Exit")]
    [SerializeField] private GameObject exit_UI;
    [SerializeField] private GameObject ui_Wall;
    [SerializeField] private float upSpeed = 300;
    [SerializeField] private float stopTime = 1.0f;
    private bool exit = false;
    private bool exit_down = false;



    private void Start()
    {

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

                //GameManager.Instance.OnClick_GameStart();
            }
        }

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
}
