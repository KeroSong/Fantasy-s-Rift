using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SDD.Events;

public class MenuManagerPlay : MonoBehaviour,IEventHandler
{
    [SerializeField] int m_ScenePause;
    [SerializeField] int m_SceneFight;

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<GamePauseEvent>(GamePause);
        EventManager.Instance.AddListener<GameFightEvent>(GameFight);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<GamePauseEvent>(GamePause);
        EventManager.Instance.RemoveListener<GameFightEvent>(GameFight);
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
}