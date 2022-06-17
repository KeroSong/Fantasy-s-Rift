using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using SDD.Events;

public class MenuManagerFight : MonoBehaviour,IEventHandler
{
    [SerializeField] GameObject m_LoadPanel;
    [SerializeField] GameObject m_PausePanel;
    [SerializeField] GameObject m_ConfirmedPanel;
    [SerializeField] GameObject m_SettingsPanel;
    [SerializeField] GameObject m_GameOverPanel;
    [SerializeField] GameObject m_VictoryFightPanel;

    [SerializeField] int m_ScenePlay;
    [SerializeField] int m_SceneMenu;

    Resolution[] m_Resolutions;
    [SerializeField] Dropdown m_ResolutionDropdown;
    [SerializeField] Slider m_DifficultySlider;
    [SerializeField] AudioMixer m_AudioMixer;

    List<GameObject> m_Panels;

    private void Awake()
    {
        m_Panels = new List<GameObject>() {m_LoadPanel, m_PausePanel, m_ConfirmedPanel, m_SettingsPanel, m_GameOverPanel, m_VictoryFightPanel};
    }

    // Start is called before the first frame update
    void Start()
    {
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
        EventManager.Instance.AddListener<GameLoadEvent>(GameLoad);
        EventManager.Instance.AddListener<GameFightEvent>(GameFight);
        EventManager.Instance.AddListener<GameConfirmedEvent>(GameConfirmed);
        EventManager.Instance.AddListener<GameMenuEvent>(GameMenu);
        EventManager.Instance.AddListener<GameSettingsEvent>(GameSetting);
        EventManager.Instance.AddListener<GamePauseEvent>(GamePause);
        EventManager.Instance.AddListener<GameVictoryFightEvent>(GameVictoryFight);
        EventManager.Instance.AddListener<GameOverEvent>(GameOver);
        EventManager.Instance.AddListener<GamePlayEvent>(GamePlay);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<GameLoadEvent>(GameLoad);
        EventManager.Instance.RemoveListener<GameFightEvent>(GameFight);
        EventManager.Instance.RemoveListener<GameConfirmedEvent>(GameConfirmed);
        EventManager.Instance.RemoveListener<GameMenuEvent>(GameMenu);
        EventManager.Instance.RemoveListener<GameSettingsEvent>(GameSetting);
        EventManager.Instance.RemoveListener<GamePauseEvent>(GamePause);
        EventManager.Instance.RemoveListener<GameVictoryFightEvent>(GameVictoryFight);
        EventManager.Instance.RemoveListener<GameOverEvent>(GameOver);
        EventManager.Instance.RemoveListener<GamePlayEvent>(GamePlay);
    }

    private void OnEnable()
    {
        SubscribeEvents();    
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    void GameLoad(GameLoadEvent e)
    {
        OpenPanel(m_LoadPanel);
    }

    void GameFight(GameFightEvent e)
    {
        OpenPanel(null);
    }

    void GameConfirmed(GameConfirmedEvent e)
    {
        OpenPanel(m_ConfirmedPanel);
    }

    void GameMenu(GameMenuEvent e)
    {
        SceneManager.LoadScene(m_SceneMenu);
    }

    void GameSetting(GameSettingsEvent e)
    {
        OpenPanel(m_SettingsPanel);
    }

    void GamePause(GamePauseEvent e)
    {
        OpenPanel(m_PausePanel);
    }

    void GameVictoryFight(GameVictoryFightEvent e)
    {
        OpenPanel(m_VictoryFightPanel);
    }

    void GameOver(GameOverEvent e)
    {
        OpenPanel(m_GameOverPanel);
    }

    void GamePlay(GamePlayEvent e)
    {
        SceneManager.LoadScene(m_ScenePlay);
    }

    void OpenPanel(GameObject panel)
    {
        m_Panels.ForEach(item => { if (item != null) item.SetActive(panel == item); });
    }



    public void ContinuePartyHasBeenClicked()
    {
        EventManager.Instance.Raise(new ContinuePartyButtonClickedEvent());
    }

    public void LoadHasBeenClicked()
    {
        EventManager.Instance.Raise(new LoadButtonClickedEvent());
    }

    public void FightButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new FightButtonClickedEvent());
    }

    public void ConfirmedButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new ConfirmedButtonClickedEvent());
    }

    public void MenuButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new MainMenuButtonClickedEvent());
    }

    public void SettingsHasBeenClicked()
    {
        EventManager.Instance.Raise(new SettingsButtonClickedEvent());
    }

    public void PauseButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new PauseHasBeenPressEvent());
    }

    public void PlayHasBeenClicked()
    {
        EventManager.Instance.Raise(new PlayButtonClickedEvent());
    }

    public void Monster1Attack()
    {
        EventManager.Instance.Raise(new Button1Event());
    }

    public void Monster2Attack()
    {
        EventManager.Instance.Raise(new Button2Event());
    }

    public void Monster3Attack()
    {
        EventManager.Instance.Raise(new Button3Event());
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