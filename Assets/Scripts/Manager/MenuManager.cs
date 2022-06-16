using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using SDD.Events;

public class MenuManager : MonoBehaviour,IEventHandler
{
    [SerializeField] GameObject m_MainMenuPanel;
    [SerializeField] GameObject m_LoadPanel;
    [SerializeField] GameObject m_PlayerSexePanel;
    [SerializeField] GameObject m_PlayerClassPanel;
    [SerializeField] GameObject m_SettingsPanel;

    [SerializeField] int m_ScenePlay;
    [SerializeField] int m_SceneFight;

    Resolution[] m_Resolutions;
    [SerializeField] Dropdown m_ResolutionDropdown;
    [SerializeField] AudioMixer m_AudioMixer;

    List<GameObject> m_Panels;

    private void Awake()
    {
        m_Panels = new List<GameObject>() {m_MainMenuPanel, m_LoadPanel, m_PlayerSexePanel, m_PlayerClassPanel, m_SettingsPanel};
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
    }

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<GameMenuEvent>(GameMenu);
        EventManager.Instance.AddListener<GameLoadEvent>(GameLoad);
        EventManager.Instance.AddListener<GameNewPartyEvent>(GameNewParty);
        EventManager.Instance.AddListener<GameSelectPlayerEvent>(GameSelectPlayer);
        EventManager.Instance.AddListener<GamePlayEvent>(GamePlay);
        EventManager.Instance.AddListener<GameSettingsEvent>(GameSetting);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<GameMenuEvent>(GameMenu);
        EventManager.Instance.RemoveListener<GameLoadEvent>(GameLoad);
        EventManager.Instance.RemoveListener<GameNewPartyEvent>(GameNewParty);
        EventManager.Instance.RemoveListener<GameSelectPlayerEvent>(GameSelectPlayer);
        EventManager.Instance.RemoveListener<GamePlayEvent>(GamePlay);
        EventManager.Instance.RemoveListener<GameSettingsEvent>(GameSetting);
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
        OpenPanel(m_LoadPanel);
    }

    void GameNewParty(GameNewPartyEvent e)
    {
        OpenPanel(m_PlayerSexePanel);
    }

    void GameSetting(GameSettingsEvent e)
    {
        OpenPanel(m_SettingsPanel);
    }

    void GameSelectPlayer(GameSelectPlayerEvent e)
    {
        OpenPanel(m_PlayerClassPanel);
    }

    void GamePlay(GamePlayEvent e)
    {
        OpenPanel(null);
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

    public void NewPartyHasBeenClicked()
    {
        EventManager.Instance.Raise(new NewPartyButtonClickedEvent());
    }

    public void SettingsHasBeenClicked()
    {
        EventManager.Instance.Raise(new SettingsButtonClickedEvent());
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
        EventManager.Instance.Raise(new PlayButtonClickedEvent());
    }

    public void MenuButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new MainMenuButtonClickedEvent());
    }

    /*public void ReplayButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new ReplayButtonClickedEvent());
    }*/



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

    public void SetSound(float sound)
    {
        m_AudioMixer.SetFloat("Sound", sound);
    }
}