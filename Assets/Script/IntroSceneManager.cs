using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class IntroSceneManager : MonoBehaviour
{
    [Header("Option")]
    [SerializeField] private Button startButton;
    [SerializeField] private GameObject OptionMenu;

    [Header("BGM_Effect_Sound")]
    
    [SerializeField] private Slider BGM;
    [SerializeField] private Slider effect_Sound;
    private float bgmValue;
    private float eftValue;

    private void Start()
    {
       
        startButton.onClick.AddListener(OnStartButtonClicked);
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
}
