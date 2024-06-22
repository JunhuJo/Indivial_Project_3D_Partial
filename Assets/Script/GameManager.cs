using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    // �ν��Ͻ��� �����ϱ� ���� ������Ƽ
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

                    // �̱��� ������Ʈ�� �ٸ� �������� �����ǵ��� ����
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

    private void Start()
    {
        SetBGMVolume(1.0f); // �ʱ� ���� �� ���� (1.0�� �ִ� ����)
        SetSFXVolume(1.0f); // �ʱ� ���� �� ���� (1.0�� �ִ� ����)
    }


    public void OnClick_GameStart()
    {
        SceneManager.LoadScene("Main_Play");
    }

    public void SetBGMVolume(float volume)
    {
        // volume�� 0�� ���� ����Ͽ� �ּ� ���� ����
        if (volume <= 0.0001f) volume = 0.0001f;
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        // volume�� 0�� ���� ����Ͽ� �ּ� ���� ����
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
