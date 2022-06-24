using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameController : MonoBehaviour
{
    [SerializeField] private string configFileName;
    public LocalDataManager localDataManager;

    [SerializeField] private int playerLivesCount = 1;
    public float playerSafeTime = 1f;
    public int playerScore = 0;

    public AudioSource musicSource;
    public AudioSource soundSource;

    
    public AudioClip gameMusic;
    public AudioClip menuMusic;
    public AudioClip carBreak;
    public AudioClip carStart;

    public float minGameSpeed = 10f;
    public float maxGameSpeed = 250f;
    public float currentGameSpeed = 10f;
    public float timeUntilMaxSpeed = 1000f;
    private float lastGameSpeedChangedTime = 0f;

    
    [System.NonSerialized] public bool gameRunning = false;


    public static GameController Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    private void OnEnable()
    {
        Debug.Assert(configFileName != "", "Config file name is not defined. Define it in the inspector.");
        localDataManager = new LocalDataManager(configFileName);
    }


    void Start()
    {        
        gameObject.GetComponent<GUIController>().OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        lastGameSpeedChangedTime = Time.timeSinceLevelLoad;
        musicSource = gameObject.AddComponent<AudioSource>();
        soundSource = gameObject.AddComponent<AudioSource>();
        SetUpAudioSources();
    }


    private void SetUpAudioSources()
    {
        musicSource.mute = localDataManager.ReadDataFromFile().isMusicMuted;
        musicSource.loop = true;
        musicSource.clip = menuMusic;
        musicSource.Play();

        soundSource.mute = localDataManager.ReadDataFromFile().isSoundMuted;
    }


    public void UpdatePlayerLiveCount(int val)
    {
        playerLivesCount = val;
        gameObject.GetComponent<GUIController>().TryChangeTextValue("Live Counter", "Lives: " + playerLivesCount.ToString());
    }


    public int GetPlayerLiveCount()
    {
        return playerLivesCount;
    }


    public void GameOver()
    {
        gameRunning = false;
        SceneManager.LoadScene("GameOverScene");
        musicSource.Stop();
        musicSource.clip = menuMusic;
        musicSource.Play();
    }


    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        gameRunning = false;
        gameObject.GetComponent<GUIController>().LockControlButtons();
    }


    public void UnpauseGame()
    {
        Time.timeScale = 1.0f;
        gameRunning = true;
        gameObject.GetComponent<GUIController>().UnlockControlButtons();
    }


    public void StartOver()
    {
        musicSource.Stop();
        musicSource.clip = gameMusic;        
        SceneManager.sceneLoaded += OnSceneLoaded;
        playerScore = 0;
        playerLivesCount = 3; 
        currentGameSpeed = minGameSpeed;
        SceneManager.LoadScene("SampleScene");        
        gameRunning = true;
        musicSource.Play();
        soundSource.PlayOneShot(carStart);
    }


    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdatePlayerLiveCount(playerLivesCount);
        UpdatePlayerScore(playerScore);
        lastGameSpeedChangedTime = Time.timeSinceLevelLoad;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    // Start is called before the first frame update
    


    public void SwitchSoundMute()
    {
        soundSource.mute = !soundSource.mute;
    }


    public void SwitchMusicMute()
    {
        musicSource.mute = !musicSource.mute;
    }


    private void UpdatePlayerScore(int val)
    {
        playerScore = val;
        gameObject.GetComponent<GUIController>().TryChangeTextValue("Score Text", "SCORE: " + val.ToString());
    }


    private bool IsTimeToUpComplexity()
    {
        return Time.timeSinceLevelLoad - lastGameSpeedChangedTime > 0.7f ? true : false;
    }


    IEnumerator ChangeSpeed(float v_start, float v_end, float duration)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            currentGameSpeed = Mathf.Lerp(v_start, v_end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        currentGameSpeed = v_end;
    }


    private void ChangeGameSpeed()
    {
        if (currentGameSpeed < maxGameSpeed)
        {
            StartCoroutine(ChangeSpeed(currentGameSpeed, currentGameSpeed + 0.25f, 2f));            
            lastGameSpeedChangedTime = Time.timeSinceLevelLoad;
        }
    }


    void FixedUpdate()
    {
        if (gameRunning)
        {
            if (IsTimeToUpComplexity())
            {
                ChangeGameSpeed();
            }
            UpdatePlayerScore(playerScore + 13 + (int)(10 * currentGameSpeed));
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
