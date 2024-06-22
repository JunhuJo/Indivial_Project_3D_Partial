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
    [SerializeField] private Text bgmVolumeText; // BGM ���� �ؽ�Ʈ
    [SerializeField] private Text effectSoundVolumeText; // ȿ���� ���� �ؽ�Ʈ
    private float bgmValue;
    private float eftValue;

    private void Start()
    {
       
        startButton.onClick.AddListener(OnStartButtonClicked);

        // �����̴� �ʱⰪ ���� �� �̺�Ʈ ������ �߰�
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
