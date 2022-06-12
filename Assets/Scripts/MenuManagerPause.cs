using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SDD.Events;

public class MenuManagerPause : MonoBehaviour,IEventHandler
{
    [SerializeField] GameObject m_SavePanel;
    [SerializeField] GameObject m_PausePanel;

    [SerializeField] int m_ScenePlay;
    [SerializeField] int m_SceneMenu;

    List<GameObject> m_Panels;

    private void Awake()
    {
        m_Panels = new List<GameObject>() {m_SavePanel, m_PausePanel};
    }

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<GameSaveEvent>(GameSave);
        EventManager.Instance.AddListener<GamePlayEvent>(GamePlay);
        EventManager.Instance.AddListener<GameMenuEvent>(GameMenu);
        EventManager.Instance.AddListener<GamePauseEvent>(GamePause);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<GameSaveEvent>(GameSave);
        EventManager.Instance.RemoveListener<GamePlayEvent>(GamePlay);
        EventManager.Instance.RemoveListener<GameMenuEvent>(GameMenu);
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

    void GameMenu(GameMenuEvent e)
    {
        OpenPanel(null);
        SceneManager.LoadScene(m_SceneMenu);
    }

    void GamePause(GamePauseEvent e)
    {
        OpenPanel(m_PausePanel);
    }

    void OpenPanel(GameObject panel)
    {
        m_Panels.ForEach(item => { if (item != null) item.SetActive(panel == item); });
    }

    

    public void SavePartyHasBeenClicked()
    {
        EventManager.Instance.Raise(new SavePartyButtonClickedEvent());
    }

    public void SaveHasBeenClicked()
    {
        EventManager.Instance.Raise(new SaveButtonClickedEvent());
    }

    public void PlayButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new PlayButtonClickedEvent());
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