using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SDD.Events;

public class MenuManagerPauseFight : MonoBehaviour,IEventHandler
{
    [SerializeField] GameObject m_LoadPanel;
    [SerializeField] GameObject m_PausePanelFight;

    [SerializeField] int m_SceneMenu;
    [SerializeField] int m_SceneFight;

    List<GameObject> m_Panels;

    private void Awake()
    {
        m_Panels = new List<GameObject>() {m_LoadPanel, m_PausePanelFight};
    }

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<GameLoadEvent>(GameLoad);
        EventManager.Instance.AddListener<GameFightEvent>(GameFight);
        EventManager.Instance.AddListener<GameMenuEvent>(GameMenu);
        EventManager.Instance.AddListener<GamePauseFightEvent>(GamePauseFight);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<GameLoadEvent>(GameLoad);
        EventManager.Instance.RemoveListener<GameFightEvent>(GameFight);
        EventManager.Instance.RemoveListener<GameMenuEvent>(GameMenu);
        EventManager.Instance.RemoveListener<GamePauseFightEvent>(GamePauseFight);
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
        SceneManager.LoadScene(m_SceneFight);
    }

    void GameMenu(GameMenuEvent e)
    {
        SceneManager.LoadScene(m_SceneMenu);
    }

    void GamePauseFight(GamePauseFightEvent e)
    {
        OpenPanel(m_PausePanelFight);
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

    public void MenuFightButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new MainMenuButtonClickedEvent());
    }

    public void PauseFightButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new PauseFightHasBeenPressEvent());
    }
}