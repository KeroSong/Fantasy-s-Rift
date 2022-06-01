using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public class MenuManager : MonoBehaviour,IEventHandler
{
    [SerializeField] GameObject m_MainMenuPanel;
    [SerializeField] GameObject m_SavePanel;
    [SerializeField] GameObject m_SettingsPanel;
    [SerializeField] GameObject m_PausePanel;
    [SerializeField] GameObject m_VictoryBatlePanel;
    [SerializeField] GameObject m_GameOverPanel;

    List<GameObject> m_Panels;

    private void Awake()
    {
        m_Panels = new List<GameObject>() {m_MainMenuPanel, m_SavePanel, m_SettingsPanel, m_PausePanel, m_VictoryBatlePanel, m_GameOverPanel};
    }

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<GameMenuEvent>(GameMenu);
        EventManager.Instance.AddListener<GamePlayEvent>(GamePlay);
        EventManager.Instance.AddListener<GameSaveEvent>(GameSave);
        EventManager.Instance.AddListener<GameSettingsEvent>(GameSetting);
        /*EventManager.Instance.AddListener<GameVictoryEvent>(GameVictory);
        EventManager.Instance.AddListener<GameOverEvent>(GameOver);*/
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<GameMenuEvent>(GameMenu);
        EventManager.Instance.RemoveListener<GamePlayEvent>(GamePlay);
        EventManager.Instance.RemoveListener<GameSaveEvent>(GameSave);
        EventManager.Instance.RemoveListener<GameSettingsEvent>(GameSetting);
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

    void GamePlay(GamePlayEvent e)
    {
        OpenPanel(null);
    }

    void GameSave(GameSaveEvent e)
    {
        OpenPanel(m_SavePanel);
    }

    void GameSetting(GameSettingsEvent e)
    {
        OpenPanel(m_SettingsPanel);
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
        EventManager.Instance.Raise(new ContinuePartyClickedEvent());
    }

    public void NewPartyHasBeenClicked()
    {
        EventManager.Instance.Raise(new NewPartyClickedEvent());
    }

    public void SettingsHasBeenClicked()
    {
        EventManager.Instance.Raise(new SettingsClickedEvent());
    }

    public void QuitHasBeenClicked()
    {
        EventManager.Instance.Raise(new QuitClickedEvent());
    }

    /*public void ReplayButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new ReplayButtonClickedEvent());
    }*/

    /*public void MenuButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new MainMenuButtonClickedEvent());
    }*/
}
