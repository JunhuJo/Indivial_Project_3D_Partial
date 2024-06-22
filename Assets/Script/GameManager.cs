using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<GameManager>();
                    singletonObject.name = typeof(GameManager).ToString() + " (Singleton)";

                    
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return instance;
        }
    }

    [Header("Audio")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private const float sfxVolumeOffset = 10.0f;


    private void Awake()
    {
        
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

    private void Start()
    {
        SetBGMVolume(1.0f); 
        SetSFXVolume(1.0f); 
    }


    public void OnClick_GameStart()
    {
        SceneManager.LoadScene("Main_Play");
    }

    public void SetBGMVolume(float volume)
    {
        // volume이 0일 때를 대비하여 최소 값을 설정
        if (volume <= 0.0001f) volume = 0.0001f;
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        // volume이 0일 때를 대비하여 최소 값을 설정
        if (volume <= 0.0001f) volume = 0.0001f;
        audioMixer.SetFloat("EffectSound", Mathf.Log10(volume) * 20 + sfxVolumeOffset);
    }

    public float GetBGMVolume()
    {
        float value;
        audioMixer.GetFloat("BGM", out value);
        return value;
    }

    public float GetSFXVolume()
    {
        float value;
        audioMixer.GetFloat("EffectSound", out value);
        return value - sfxVolumeOffset;
    }

    public void OnClick_SetSize_First()
    {
        
        Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
    }

    public void OnClick_SetSize_Scound()
    {
        
        Screen.SetResolution(1280, 720, FullScreenMode.Windowed);
    }

    public void OnClick_SetSize_Tread()
    {
        
        Screen.SetResolution(800, 600, FullScreenMode.Windowed);
    }
}
