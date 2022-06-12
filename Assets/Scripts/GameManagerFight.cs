using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public class GameManagerFight : MonoBehaviour
{
    private static GameManagerFight m_Instance;
    public static GameManagerFight Instance { get {
            //if (m_Instance == null) m_Instance = CreateInstance(); // Impossible dans Unity
            return m_Instance; } }

    GAMESTATE m_State;

    void SetState(GAMESTATE newState)
    {
        m_State = newState;
        switch (m_State)
        {
            case GAMESTATE.load:
                EventManager.Instance.Raise(new GameLoadEvent());
                break;
            case GAMESTATE.fight:
                EventManager.Instance.Raise(new GameFightEvent());
                break;
            case GAMESTATE.menu:
                EventManager.Instance.Raise(new GameMenuEvent());
                break;
            case GAMESTATE.pause:
                EventManager.Instance.Raise(new GamePauseEvent());
                break;
            case GAMESTATE.gameover:
                EventManager.Instance.Raise(new GameOverEvent());
                break;
            case GAMESTATE.victoryFight:
                EventManager.Instance.Raise(new GameVictoryFightEvent());
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

    // Start is called before the first frame update
    void Start()
    {
        SetState(GAMESTATE.fight);
    }

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<ContinuePartyButtonClickedEvent>(ContinuPartyButtonClicked);
        EventManager.Instance.AddListener<FightButtonClickedEvent>(FightButtonClicked);
        EventManager.Instance.AddListener<MainMenuButtonClickedEvent>(MainMenuButtonClicked);
        EventManager.Instance.AddListener<PauseHasBeenPressEvent>(PauseHasBeenPress);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<ContinuePartyButtonClickedEvent>(ContinuPartyButtonClicked);
        EventManager.Instance.RemoveListener<FightButtonClickedEvent>(FightButtonClicked);
        EventManager.Instance.RemoveListener<MainMenuButtonClickedEvent>(MainMenuButtonClicked);
        EventManager.Instance.RemoveListener<PauseHasBeenPressEvent>(PauseHasBeenPress);
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }



    void ContinuPartyButtonClicked(ContinuePartyButtonClickedEvent e)
    {
        Load();
    }

    void FightButtonClicked(FightButtonClickedEvent e)
    {
        Fight();
    }

    void MainMenuButtonClicked(MainMenuButtonClickedEvent e)
    {
        Menu();
    }

    void PauseHasBeenPress(PauseHasBeenPressEvent e)
    {
        Pause();
    }



    void Load()
    {
        SetState(GAMESTATE.load);
    }

    void Fight()
    {
        SetState(GAMESTATE.fight);
    }

    void Menu()
    {
        SetState(GAMESTATE.menu);
    }

    void Pause()
    {
        SetState(GAMESTATE.pause);
    }
}
