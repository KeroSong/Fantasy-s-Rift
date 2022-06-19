using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using SDD.Events;

[RequireComponent(typeof(AudioSource))]

public class MenuManager : MonoBehaviour,IEventHandler
{
    AudioSource audioData;
    [SerializeField] GameObject m_MainMenuPanel;
    [SerializeField] GameObject m_LoadPanel;
    [SerializeField] GameObject m_PlayerSexePanel;
    [SerializeField] GameObject m_PlayerClassPanel;
    [SerializeField] GameObject m_SettingsPanel;

    Resolution[] m_Resolutions;
    [SerializeField] Dropdown m_ResolutionDropdown;
    [SerializeField] Slider m_DifficultySlider;
    [SerializeField] AudioMixer m_AudioMixer;

    List<GameObject> m_Panels;

    private void Awake()
    {
        m_Panels = new List<GameObject>() {m_MainMenuPanel, m_LoadPanel, m_PlayerSexePanel, m_PlayerClassPanel, m_SettingsPanel};
    }

    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        m_Resolutions = Screen.resolutions.Select(resolution => new Resolution {width = resolution.width, height = resolution.height}).Distinct().ToArray();
        m_ResolutionDropdown.ClearOptions();

        List<string> Options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i = 0; i < m_Resolutions.Length; i++)
        {
            string option = m_Resolutions[i].width + "x" + m_Resolutions[i].height;
            Options.Add(option);

            if (m_Resolutions[i].width == Screen.width && m_Resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        m_ResolutionDropdown.AddOptions(Options);
        m_ResolutionDropdown.value = currentResolutionIndex;
        m_ResolutionDropdown.RefreshShownValue();

        Screen.fullScreen = true;

        m_DifficultySlider.value = PlayerPrefs.GetInt("Difficulté");
    }

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<GameMenuEvent>(GameMenu);
        EventManager.Instance.AddListener<GameLoadEvent>(GameLoad);
        EventManager.Instance.AddListener<GameNewGameEvent>(GameNewGame);
        EventManager.Instance.AddListener<GameSelectPlayerEvent>(GameSelectPlayer);
        EventManager.Instance.AddListener<GamePlayEvent>(GamePlay);
        EventManager.Instance.AddListener<GameSettingsEvent>(GameSetting);
        EventManager.Instance.AddListener<GameEndEvent>(GameEnd);
        EventManager.Instance.AddListener<GameCineEvent>(GameCine);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<GameMenuEvent>(GameMenu);
        EventManager.Instance.RemoveListener<GameLoadEvent>(GameLoad);
        EventManager.Instance.RemoveListener<GameNewGameEvent>(GameNewGame);
        EventManager.Instance.RemoveListener<GameSelectPlayerEvent>(GameSelectPlayer);
        EventManager.Instance.RemoveListener<GamePlayEvent>(GamePlay);
        EventManager.Instance.RemoveListener<GameSettingsEvent>(GameSetting);
        EventManager.Instance.RemoveListener<GameEndEvent>(GameEnd);
        EventManager.Instance.RemoveListener<GameCineEvent>(GameCine);
    }

    private void OnEnable()
    {
        SubscribeEvents();    
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    void GameMenu(GameMenuEvent e)
    {
        OpenPanel(m_MainMenuPanel);
    }

    void GameLoad(GameLoadEvent e)
    {
        audioData.Play(0);
        Destroy(m_LoadPanel.GetComponent<Image>());
        OpenPanel(m_LoadPanel);
    }

    void GameNewGame(GameNewGameEvent e)
    {
        audioData.Play(0);
        OpenPanel(m_PlayerSexePanel);
    }

    void GameSetting(GameSettingsEvent e)
    {
        audioData.Play(0);
        Destroy(m_SettingsPanel.GetComponent<Image>());
        OpenPanel(m_SettingsPanel);
    }

    void GameSelectPlayer(GameSelectPlayerEvent e)
    {
        audioData.Play(0);
        OpenPanel(m_PlayerClassPanel);
    }

    void GamePlay(GamePlayEvent e)
    {
        audioData.Play(0);
        SceneManager.LoadScene(1);
    }

    void GameEnd(GameEndEvent e)
    {
        SceneManager.LoadScene(4);
    }

    void GameCine(GameCineEvent e)
    {
        SceneManager.LoadScene(5);
    }

    void OpenPanel(GameObject panel)
    {
        m_Panels.ForEach(item => { if (item != null) item.SetActive(panel == item); });
    }

    

    public void ContinuePartieHasBeenClicked()
    {
        EventManager.Instance.Raise(new ContinuePartieButtonClickedEvent());
    }

    public void NewGameHasBeenClicked()
    {
        EventManager.Instance.Raise(new NewGameButtonClickedEvent());
    }

    public void SettingsHasBeenClicked()
    {
        EventManager.Instance.Raise(new SettingsButtonClickedEvent());
    }

    public void CreditHasBeenClicked()
    {
        EventManager.Instance.Raise(new CreditButtonClickedEvent());
    }

    public void QuitHasBeenClicked()
    {
        EventManager.Instance.Raise(new QuitButtonClickedEvent());
    }

    public void LoadHasBeenClicked()
    {
        EventManager.Instance.Raise(new LoadButtonClickedEvent());
    }

    public void SexeButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new SelectPlayerButtonClickedEvent());
    }

    public void ClassButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new CineButtonClickedEvent());
    }

    public void MenuButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new MainMenuButtonClickedEvent());
    }



    //settings parameters
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = m_Resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetDifficulty(float difficulte)
    {
        PlayerPrefs.SetInt("Difficulté", (int)difficulte);
    }

    public void SetSound(float sound)
    {
        m_AudioMixer.SetFloat("Sound", sound);
    }
}