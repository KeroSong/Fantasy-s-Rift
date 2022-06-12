using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SDD.Events;

public class MenuManagerFight : MonoBehaviour,IEventHandler
{
    [SerializeField] GameObject m_LoadPanel;
    [SerializeField] GameObject m_PausePanel;
    [SerializeField] GameObject m_ConfirmedPanel;
    [SerializeField] GameObject m_GameOverPanel;
    [SerializeField] GameObject m_VictoryFightPanel;

    [SerializeField] int m_ScenePlay;
    [SerializeField] int m_SceneMenu;

    List<GameObject> m_Panels;

    private void Awake()
    {
        m_Panels = new List<GameObject>() {m_LoadPanel, m_PausePanel, m_ConfirmedPanel, m_GameOverPanel, m_VictoryFightPanel};
    }

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<GameLoadEvent>(GameLoad);
        EventManager.Instance.AddListener<GameFightEvent>(GameFight);
        EventManager.Instance.AddListener<GameConfirmedEvent>(GameConfirmed);
        EventManager.Instance.AddListener<GameMenuEvent>(GameMenu);
        EventManager.Instance.AddListener<GamePauseEvent>(GamePause);
        EventManager.Instance.AddListener<GameVictoryFightEvent>(GameVictoryFight);
        EventManager.Instance.AddListener<GameOverEvent>(GameOver);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<GameLoadEvent>(GameLoad);
        EventManager.Instance.RemoveListener<GameFightEvent>(GameFight);
        EventManager.Instance.RemoveListener<GameConfirmedEvent>(GameConfirmed);
        EventManager.Instance.RemoveListener<GameMenuEvent>(GameMenu);
        EventManager.Instance.RemoveListener<GamePauseEvent>(GamePause);
        EventManager.Instance.RemoveListener<GameVictoryFightEvent>(GameVictoryFight);
        EventManager.Instance.RemoveListener<GameOverEvent>(GameOver);
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

    public void PauseButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new PauseHasBeenPressEvent());
    }
}