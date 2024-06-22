using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    // 인스턴스에 접근하기 위한 프로퍼티
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

                    // 싱글톤 오브젝트가 다른 씬에서도 유지되도록 설정
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
        // 이미 인스턴스가 존재하는 경우, 중복되는 오브젝트를 파괴
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
        SetBGMVolume(1.0f); // 초기 볼륨 값 설정 (1.0은 최대 볼륨)
        SetSFXVolume(1.0f); // 초기 볼륨 값 설정 (1.0은 최대 볼륨)
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
}
