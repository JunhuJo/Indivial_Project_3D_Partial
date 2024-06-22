using UnityEngine;
using UnityEngine.UI;

public class IntroSceneManager : MonoBehaviour
{
    [Header("Option")]
    [SerializeField] private Button startButton;
    [SerializeField] private GameObject OptionMenu;

    [Header("BGM_Effect_Sound")]
    
    [SerializeField] private Slider BGM;
    [SerializeField] private Slider effect_Sound;
    [SerializeField] private Text bgmVolumeText; // BGM 볼륨 텍스트
    [SerializeField] private Text effectSoundVolumeText; // 효과음 볼륨 텍스트
    private float bgmValue;
    private float eftValue;

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
        GameManager.Instance.OnClick_GameStart();
    }

    public void OnBGMVolumeChanged()
    {
        GameManager.Instance.SetBGMVolume(BGM.value);
        
    }

    public void OnSFXVolumeChanged()
    {
        GameManager.Instance.SetSFXVolume(eftValue);
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
