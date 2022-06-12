using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public class GameManagerPause : MonoBehaviour
{
    private static GameManagerPause m_Instance;
    public static GameManagerPause Instance { get {
            //if (m_Instance == null) m_Instance = CreateInstance(); // Impossible dans Unity
            return m_Instance; } }

    GAMESTATE m_State;

    void SetState(GAMESTATE newState)
    {
        m_State = newState;
        switch (m_State)
        {
            case GAMESTATE.save:
                EventManager.Instance.Raise(new GameSaveEvent());
                break;
            case GAMESTATE.play:
                EventManager.Instance.Raise(new GamePlayEvent());
                break;
            case GAMESTATE.confirmed:
                EventManager.Instance.Raise(new GameConfirmedEvent());
                break;
            case GAMESTATE.menu:
                EventManager.Instance.Raise(new GameMenuEvent());
                break;
            case GAMESTATE.pause:
                EventManager.Instance.Raise(new GamePauseEvent());
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
        SetState(GAMESTATE.pause);
    }

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<SavePartyButtonClickedEvent>(SaveButtonClicked);
        EventManager.Instance.AddListener<PlayButtonClickedEvent>(PlayButtonClicked);
        EventManager.Instance.AddListener<ConfirmedButtonClickedEvent>(ConfirmedButtonClicked);
        EventManager.Instance.AddListener<MainMenuButtonClickedEvent>(MainMenuButtonClicked);
        EventManager.Instance.AddListener<PauseHasBeenPressEvent>(PauseHasBeenPress);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<SavePartyButtonClickedEvent>(SaveButtonClicked);
        EventManager.Instance.RemoveListener<PlayButtonClickedEvent>(PlayButtonClicked);
        EventManager.Instance.RemoveListener<ConfirmedButtonClickedEvent>(ConfirmedButtonClicked);
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

    void SaveButtonClicked(SavePartyButtonClickedEvent e)
    {
        Save();
    }

    void PlayButtonClicked(PlayButtonClickedEvent e)
    {
        Play();
    }

    void ConfirmedButtonClicked(ConfirmedButtonClickedEvent e)
    {
        Confirmed();
    }

    void MainMenuButtonClicked(MainMenuButtonClickedEvent e)
    {
        Menu();
    }

    void PauseHasBeenPress(PauseHasBeenPressEvent e)
    {
        Pause();
    }

    void Save()
    {
        SetState(GAMESTATE.save);
    }

    void Play()
    {
        SetState(GAMESTATE.play);
    }

    void Confirmed()
    {
        SetState(GAMESTATE.confirmed);
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
