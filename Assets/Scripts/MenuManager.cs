using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SDD.Events;

public class MenuManager : MonoBehaviour,IEventHandler
{
    [SerializeField] GameObject m_MainMenuPanel;
    [SerializeField] GameObject m_LoadPanel;
    [SerializeField] GameObject m_PlayerSexePanel;
    [SerializeField] GameObject m_PlayerClassPanel;
    [SerializeField] GameObject m_SettingsPanel;
    [SerializeField] GameObject m_PausePanel;
    [SerializeField] GameObject m_SavePanel;
    [SerializeField] GameObject m_VictoryBatlePanel;
    [SerializeField] GameObject m_GameOverPanel;

    [SerializeField] int m_ScenePlay;
    [SerializeField] int m_SceneMenu;
    [SerializeField] int m_SceneFight;

    List<GameObject> m_Panels;

    private void Awake()
    {
        m_Panels = new List<GameObject>() {m_MainMenuPanel, m_LoadPanel, m_PlayerSexePanel, m_PlayerClassPanel, m_SettingsPanel, m_PausePanel, m_SavePanel, m_VictoryBatlePanel, m_GameOverPanel};
    }

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<GameMenuEvent>(GameMenu);
        EventManager.Instance.AddListener<GameLoadEvent>(GameLoad);
        EventManager.Instance.AddListener<GameNewPartyEvent>(GameNewParty);
        EventManager.Instance.AddListener<GameSelectPlayerEvent>(GameSelectPlayer);
        EventManager.Instance.AddListener<GamePlayEvent>(GamePlay);
        EventManager.Instance.AddListener<GameSettingsEvent>(GameSetting);
        EventManager.Instance.AddListener<GamePauseEvent>(GamePause);
        EventManager.Instance.AddListener<GameSaveEvent>(GameSave);
        /*EventManager.Instance.AddListener<GameVictoryEvent>(GameVictory);
        EventManager.Instance.AddListener<GameOverEvent>(GameOver);*/
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<GameMenuEvent>(GameMenu);
        EventManager.Instance.RemoveListener<GameLoadEvent>(GameLoad);
        EventManager.Instance.RemoveListener<GameNewPartyEvent>(GameNewParty);
        EventManager.Instance.RemoveListener<GameSelectPlayerEvent>(GameSelectPlayer);
        EventManager.Instance.RemoveListener<GamePlayEvent>(GamePlay);
        EventManager.Instance.RemoveListener<GameSettingsEvent>(GameSetting);
        EventManager.Instance.RemoveListener<GamePauseEvent>(GamePause);
        EventManager.Instance.RemoveListener<GameSaveEvent>(GameSave);
        /*EventManager.Instance.RemoveListener<GameVictoryEvent>(GameVictory);
        EventManager.Instance.RemoveListener<GameOverEvent>(GameOver);*/
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

    void GamePause(GamePauseEvent e)
    {
        SceneManager.LoadScene(m_SceneMenu);
        OpenPanel(m_PausePanel);
    }

    void GameSave(GameSaveEvent e)
    {
        OpenPanel(m_SavePanel);
    }

    /*void GameVictory(GameVictoryEvent e)
    {
        OpenPanel(m_VictoryBatlePanel);
    }*/

    /*void GameOver(GameOverEvent e)
    {
        OpenPanel(m_GameOverPanel);
    }*/

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

    /*public void PauseHasBeenPress()
    {
        EventManager.Instance.Raise(new );
    }*/

    public void MenuButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new MainMenuButtonClickedEvent());
    }

    public void SaveHasBeenClicked()
    {
        EventManager.Instance.Raise(new SaveButtonClickedEvent());
    }

    /*public void ReplayButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new ReplayButtonClickedEvent());
    }*/
}