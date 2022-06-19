using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using SDD.Events;

public class MenuManagerPause : MonoBehaviour,IEventHandler
{
    [SerializeField] GameObject m_SavePanel;
    [SerializeField] GameObject m_PausePanel;
    [SerializeField] GameObject m_ConfirmedPanel;
    [SerializeField] GameObject m_SettingsPanel;

    [SerializeField] int m_ScenePlay;
    [SerializeField] int m_SceneMenu;

    Resolution[] m_Resolutions;
    [SerializeField] Dropdown m_ResolutionDropdown;
    [SerializeField] Slider m_DifficultySlider;
    [SerializeField] AudioMixer m_AudioMixer;

    List<GameObject> m_Panels;

    private void Awake()
    {
        m_Panels = new List<GameObject>() {m_SavePanel, m_PausePanel, m_ConfirmedPanel, m_SettingsPanel};
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
        EventManager.Instance.AddListener<GameSaveEvent>(GameSave);
        EventManager.Instance.AddListener<GamePlayEvent>(GamePlay);
        EventManager.Instance.AddListener<GameConfirmedEvent>(GameConfirmed);
        EventManager.Instance.AddListener<GameMenuEvent>(GameMenu);
        EventManager.Instance.AddListener<GameSettingsEvent>(GameSetting);
        EventManager.Instance.AddListener<GamePauseEvent>(GamePause);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<GameSaveEvent>(GameSave);
        EventManager.Instance.RemoveListener<GamePlayEvent>(GamePlay);
        EventManager.Instance.RemoveListener<GameConfirmedEvent>(GameConfirmed);
        EventManager.Instance.RemoveListener<GameMenuEvent>(GameMenu);
        EventManager.Instance.RemoveListener<GameSettingsEvent>(GameSetting);
        EventManager.Instance.RemoveListener<GamePauseEvent>(GamePause);
    }

    private void OnEnable()
    {
        SubscribeEvents();    
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    void GameSave(GameSaveEvent e)
    {
        OpenPanel(m_SavePanel);
    }

    void GamePlay(GamePlayEvent e)
    {
        OpenPanel(null);
        SceneManager.LoadScene(m_ScenePlay);
    }

    void GameConfirmed(GameConfirmedEvent e)
    {
        OpenPanel(m_ConfirmedPanel);
    }

    void GameMenu(GameMenuEvent e)
    {
        OpenPanel(null);
        SceneManager.LoadScene(m_SceneMenu);
    }

    void GameSetting(GameSettingsEvent e)
    {
        Destroy(m_SettingsPanel.GetComponent<Image>());
        OpenPanel(m_SettingsPanel);
    }

    void GamePause(GamePauseEvent e)
    {
        OpenPanel(m_PausePanel);
    }

    void OpenPanel(GameObject panel)
    {
        m_Panels.ForEach(item => { if (item != null) item.SetActive(panel == item); });
    }

    

    public void SavePartieHasBeenClicked()
    {
        EventManager.Instance.Raise(new SavePartieButtonClickedEvent());
    }

    public void LoadHasBeenClicked()
    {
        EventManager.Instance.Raise(new LoadButtonClickedEvent());
    }

    public void SaveHasBeenClicked()
    {
        EventManager.Instance.Raise(new SaveButtonClickedEvent());
    }

    public void PlayButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new PlayButtonClickedEvent());
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