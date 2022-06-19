using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public class GameManagerEnd : MonoBehaviour
{
    private static GameManagerEnd m_Instance;
    public static GameManagerEnd Instance { get {
            return m_Instance; } }

    GAMESTATE m_State;

    void SetState(GAMESTATE newState)
    {
        m_State = newState;
        switch (m_State)
        {
            case GAMESTATE.menu:
                EventManager.Instance.Raise(new GameMenuEvent());
                break;
            default:
                break;
        }
    }

    void Awake()
    {
        if (m_Instance == null) m_Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    { 
        if (Input.GetKeyDown("escape"))
        {
            Menu();
        }
    }

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<MainMenuButtonClickedEvent>(MainMenuButtonClicked);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<MainMenuButtonClickedEvent>(MainMenuButtonClicked);
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    void MainMenuButtonClicked(MainMenuButtonClickedEvent e)
    {
        Menu();
    }

    void Menu()
    {
        SetState(GAMESTATE.menu);
    }
}
