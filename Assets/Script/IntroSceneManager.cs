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
    [SerializeField] private GameObject OptionMenu;

    [Header("BGM_Effect_Sound")]
    
    [SerializeField] private Slider BGM;
    [SerializeField] private Slider effect_Sound;
    [SerializeField] private Text bgmVolumeText; // BGM 볼륨 텍스트
    [SerializeField] private Text effectSoundVolumeText; // 효과음 볼륨 텍스트
    

    private void Start()
    {
       
        startButton.onClick.AddListener(OnStartButtonClicked);

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
                GameManager.Instance.OnClick_GameStart();
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
