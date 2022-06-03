using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public enum GAMESTATE {menu, menuPlayer, play, save, settings, pause, gameover, victoryBatle}

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
            case GAMESTATE.menuPlayer:
                EventManager.Instance.Raise(new GameNewPartyEvent());
                break;
            case GAMESTATE.play:
                EventManager.Instance.Raise(new GamePlayEvent());
                break;
            case GAMESTATE.save:
                EventManager.Instance.Raise(new GameSaveEvent());
                break;
            case GAMESTATE.settings:
                EventManager.Instance.Raise(new GameSettingsEvent());
                break;
            case GAMESTATE.pause:
                EventManager.Instance.Raise(new GamePauseEvent());
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
        EventManager.Instance.AddListener<ContinuePartyClickedEvent>(ContinuPartyClicked);
        EventManager.Instance.AddListener<NewPartyClickedEvent>(NewPartyClicked);
        EventManager.Instance.AddListener<SettingsClickedEvent>(SettingsClicked);
        EventManager.Instance.AddListener<QuitClickedEvent>(QuitClicked);
        //EventManager.Instance.AddListener<ScoreHasBeenGainedEvent>(ScoreHasBeenGained);
        //EventManager.Instance.AddListener<ReplayButtonClickedEvent>(ReplayButtonClicked);
        //EventManager.Instance.AddListener<MainMenuButtonClickedEvent>(MainMenuButtonClicked);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<ContinuePartyClickedEvent>(ContinuPartyClicked);
        EventManager.Instance.RemoveListener<NewPartyClickedEvent>(NewPartyClicked);
        EventManager.Instance.RemoveListener<SettingsClickedEvent>(SettingsClicked);
        EventManager.Instance.RemoveListener<QuitClickedEvent>(QuitClicked);
        //EventManager.Instance.RemoveListener<ScoreHasBeenGainedEvent>(ScoreHasBeenGained);
        //EventManager.Instance.RemoveListener<ReplayButtonClickedEvent>(ReplayButtonClicked);
        //EventManager.Instance.RemoveListener<MainMenuButtonClickedEvent>(MainMenuButtonClicked);
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    void ContinuPartyClicked(ContinuePartyClickedEvent e)
    {
        Save();
    }

    void NewPartyClicked(NewPartyClickedEvent e)
    {
        NewParty();
    }

    void SettingsClicked(SettingsClickedEvent e)
    {
        Settings();
    }

    void QuitClicked(QuitClickedEvent e)
    {
        return;
    }

    /*void ReplayButtonClicked(ReplayButtonClickedEvent e)
    {
        NewParty();
    }*/

    /*void MainMenuButtonClicked(MainMenuButtonClickedEvent e)
    {
        SetState(GAMESTATE.menu);
    }*/

    void Save()
    {
        SetState(GAMESTATE.save);
    }

    void NewParty()
    {
        SetState(GAMESTATE.menuPlayer);
    }

    void Settings()
    {
        SetState(GAMESTATE.settings);
    }

    void Game()
    {
        SetState(GAMESTATE.play);
    }
}
