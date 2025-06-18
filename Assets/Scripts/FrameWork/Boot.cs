using UnityEngine;

public class Boot : UnitySingleton<Boot>
{
    public Camera uiCamera;
    //����
    public AudioSource audio;

    //ui�����㼶����ҪTransform 
    //����canvas canvas����camera��Ⱦ���� ͨ�����ھ�����Ʋ㼶
    //���������depthonly ���Ҹĳ���������
    public Transform BackRoot;
    public Transform UIRoot;
    public Transform TipRoot;


    void Start()
    {
        Init();
        uiCamera = transform.GetComponentInChildren<Camera>();
        UIManager.Instance.OpenWindow("LoginPanel");
        //GameScenesManager.Instance.LoadSceneAsync("Game", "PlayerPanel");
    }

    void Init()
    {
        gameObject.AddComponent<GameScenesManager>().Init(this);
        UIManager.Instance.Init(TipRoot, UIRoot, BackRoot);
    }

    public void ChangeAudio(AudioClip clip)
    {
        audio.clip = clip;
        audio.time = 0;
        audio.Play();
    }
}
