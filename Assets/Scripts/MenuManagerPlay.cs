using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SDD.Events;

public class MenuManagerPlay : MonoBehaviour,IEventHandler
{
    [SerializeField] GameObject m_ShopPanel;
    [SerializeField] GameObject m_InnPanel;

    [SerializeField] int m_ScenePause;
    [SerializeField] int m_SceneFight;

    List<GameObject> m_Panels;

    private void Awake()
    {
        m_Panels = new List<GameObject>() {m_ShopPanel, m_InnPanel};
    }

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<GamePauseEvent>(GamePause);
        EventManager.Instance.AddListener<GameFightEvent>(GameFight);
        EventManager.Instance.AddListener<GameShopEvent>(GameShop);
        EventManager.Instance.AddListener<GameInnEvent>(GameInn);
        EventManager.Instance.AddListener<GamePlayEvent>(GamePlay);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<GamePauseEvent>(GamePause);
        EventManager.Instance.RemoveListener<GameFightEvent>(GameFight);
        EventManager.Instance.RemoveListener<GameShopEvent>(GameShop);
        EventManager.Instance.RemoveListener<GameInnEvent>(GameInn);
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

    void GamePause(GamePauseEvent e)
    {
        SceneManager.LoadScene(m_ScenePause);
    }

    void GameFight(GameFightEvent e)
    {
        SceneManager.LoadScene(m_SceneFight);
    }

    void GameShop(GameShopEvent e)
    {
        OpenPanel(m_ShopPanel);
    }

    void GameInn(GameInnEvent e)
    {
        OpenPanel(m_InnPanel);
    }

    void GamePlay(GamePlayEvent e)
    {
        OpenPanel(null);
    }

    void OpenPanel(GameObject panel)
    {
        m_Panels.ForEach(item => { if (item != null) item.SetActive(panel == item); });
    }
}