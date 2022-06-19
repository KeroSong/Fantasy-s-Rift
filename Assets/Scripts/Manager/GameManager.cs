using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public enum GAMESTATE {menu, load, menuPlayer, selectPlayer, play, settings, quit, pause, inventory, equipment, save, fight, confirmed, shop, inn, gameover, victoryFight, end}

public class GameManager : MonoBehaviour
{
    private static GameManager m_Instance;
    public static GameManager Instance { get {
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
            case GAMESTATE.load:
                EventManager.Instance.Raise(new GameLoadEvent());
                break;
            case GAMESTATE.menuPlayer:
                EventManager.Instance.Raise(new GameNewGameEvent());
                break;
            case GAMESTATE.selectPlayer:
                EventManager.Instance.Raise(new GameSelectPlayerEvent());
                break;
            case GAMESTATE.play:
                EventManager.Instance.Raise(new GamePlayEvent());
                break;
            case GAMESTATE.settings:
                EventManager.Instance.Raise(new GameSettingsEvent());
                break;
            case GAMESTATE.quit:
                EventManager.Instance.Raise(new GameQuitEvent());
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
        SetState(GAMESTATE.menu);
    }

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<ContinuePartieButtonClickedEvent>(ContinuPartieButtonClicked);
        EventManager.Instance.AddListener<NewGameButtonClickedEvent>(NewGameClicked);
        EventManager.Instance.AddListener<SettingsButtonClickedEvent>(SettingsClicked);
        EventManager.Instance.AddListener<QuitButtonClickedEvent>(QuitClicked);
        EventManager.Instance.AddListener<SelectPlayerButtonClickedEvent>(SelectPlayerButtonClicked);
        EventManager.Instance.AddListener<PlayButtonClickedEvent>(PlayButtonClicked);
        EventManager.Instance.AddListener<MainMenuButtonClickedEvent>(MainMenuButtonClicked);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<ContinuePartieButtonClickedEvent>(ContinuPartieButtonClicked);
        EventManager.Instance.RemoveListener<NewGameButtonClickedEvent>(NewGameClicked);
        EventManager.Instance.RemoveListener<SettingsButtonClickedEvent>(SettingsClicked);
        EventManager.Instance.RemoveListener<QuitButtonClickedEvent>(QuitClicked);
        EventManager.Instance.RemoveListener<SelectPlayerButtonClickedEvent>(SelectPlayerButtonClicked);
        EventManager.Instance.RemoveListener<PlayButtonClickedEvent>(PlayButtonClicked);
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

    void ContinuPartieButtonClicked(ContinuePartieButtonClickedEvent e)
    {
        Load();
    }

    void NewGameClicked(NewGameButtonClickedEvent e)
    {
        NewParty();
    }

    void SettingsClicked(SettingsButtonClickedEvent e)
    {
        Settings();
    }

    void QuitClicked(QuitButtonClickedEvent e)
    {
        Application.Quit();
    }

    void SelectPlayerButtonClicked(SelectPlayerButtonClickedEvent e)
    {
        SelectPlayer();
    }

    void PlayButtonClicked(PlayButtonClickedEvent e)
    {
        Play();
    }

    void MainMenuButtonClicked(MainMenuButtonClickedEvent e)
    {
        Menu();
    }

    void Load()
    {
        SetState(GAMESTATE.load);
    }

    void NewParty()
    {
        SetState(GAMESTATE.menuPlayer);
    }

    void Settings()
    {
        SetState(GAMESTATE.settings);
    }

    void SelectPlayer()
    {
        SetState(GAMESTATE.selectPlayer);
    }

    void Play()
    {
        SetState(GAMESTATE.play);
    }

    void Menu()
    {
        SetState(GAMESTATE.menu);
    }
}
