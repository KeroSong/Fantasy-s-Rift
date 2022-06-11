using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public enum GAMESTATE {menu, load, menuPlayer, selectPlayer, play, settings, quit, pause, pauseFight, save, fight, gameover, victoryBatle}

public class GameManager : MonoBehaviour
{
    private static GameManager m_Instance;
    public static GameManager Instance { get {
            //if (m_Instance == null) m_Instance = CreateInstance(); // Impossible dans Unity
            return m_Instance; } }

    GAMESTATE m_State;
    public bool EnJeu { get { return m_State == GAMESTATE.play; } }

    //int m_Score;
    //[SerializeField] int m_VictoryScore;

    //float m_CountdownTimer;
    //[SerializeField] float m_GameDuration;

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
                EventManager.Instance.Raise(new GameNewPartyEvent());
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
            case GAMESTATE.gameover:
                EventManager.Instance.Raise(new GameOverEvent());
                break;
            case GAMESTATE.victoryBatle:
                EventManager.Instance.Raise(new GameVictoryEvent());
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<ContinuePartyButtonClickedEvent>(ContinuPartyButtonClicked);
        EventManager.Instance.AddListener<NewPartyButtonClickedEvent>(NewPartyClicked);
        EventManager.Instance.AddListener<SettingsButtonClickedEvent>(SettingsClicked);
        EventManager.Instance.AddListener<QuitButtonClickedEvent>(QuitClicked);
        EventManager.Instance.AddListener<SelectPlayerButtonClickedEvent>(SelectPlayerButtonClicked);
        EventManager.Instance.AddListener<PlayButtonClickedEvent>(PlayButtonClicked);
        EventManager.Instance.AddListener<PauseHasBeenPressEvent>(PauseHasBeenPress);
        /*EventManager.Instance.AddListener<ScoreHasBeenGainedEvent>(ScoreHasBeenGained);
        EventManager.Instance.AddListener<ReplayButtonClickedEvent>(ReplayButtonClicked);
        EventManager.Instance.AddListener<MainMenuButtonClickedEvent>(MainMenuButtonClicked);*/
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<ContinuePartyButtonClickedEvent>(ContinuPartyButtonClicked);
        EventManager.Instance.RemoveListener<NewPartyButtonClickedEvent>(NewPartyClicked);
        EventManager.Instance.RemoveListener<SettingsButtonClickedEvent>(SettingsClicked);
        EventManager.Instance.RemoveListener<QuitButtonClickedEvent>(QuitClicked);
        EventManager.Instance.RemoveListener<SelectPlayerButtonClickedEvent>(SelectPlayerButtonClicked);
        EventManager.Instance.RemoveListener<PlayButtonClickedEvent>(PlayButtonClicked);
        EventManager.Instance.RemoveListener<PauseHasBeenPressEvent>(PauseHasBeenPress);
        /*EventManager.Instance.RemoveListener<ScoreHasBeenGainedEvent>(ScoreHasBeenGained);
        EventManager.Instance.RemoveListener<ReplayButtonClickedEvent>(ReplayButtonClicked);
        EventManager.Instance.RemoveListener<MainMenuButtonClickedEvent>(MainMenuButtonClicked);*/
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

    void NewPartyClicked(NewPartyButtonClickedEvent e)
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

    void PauseHasBeenPress(PauseHasBeenPressEvent e)
    {
        Pause();
    }

    /*void ReplayButtonClicked(ReplayButtonClickedEvent e)
    {
        NewParty();
    }*/

    /*void MainMenuButtonClicked(MainMenuButtonClickedEvent e)
    {
        SetState(GAMESTATE.menu);
    }*/

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

    void Pause()
    {
        SetState(GAMESTATE.pause);
    }
}
