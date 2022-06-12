using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public class GameManagerPlay : MonoBehaviour
{
    private static GameManagerPlay m_Instance;
    public static GameManagerPlay Instance { get {
            //if (m_Instance == null) m_Instance = CreateInstance(); // Impossible dans Unity
            return m_Instance; } }

    GAMESTATE m_State;
    public bool IsPlaying { get { return m_State == GAMESTATE.play; } }

    //int m_Score;
    //[SerializeField] int m_VictoryScore;

    //float m_CountdownTimer;
    //[SerializeField] float m_GameDuration;

    void SetState(GAMESTATE newState)
    {
        m_State = newState;
        switch (m_State)
        {
            case GAMESTATE.play:
                break;
            case GAMESTATE.pause:
                EventManager.Instance.Raise(new GamePauseEvent());
                break;
            case GAMESTATE.fight:
                EventManager.Instance.Raise(new GameFightEvent());
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
        SetState(GAMESTATE.play);
    }

    // Update is called once per frame
    void Update()
    {
        if(IsPlaying)
        {
            /*SetScoreAndTimer(m_Score,Mathf.Max(m_CountdownTimer - Time.deltaTime, 0));
            if (m_CountdownTimer == 0)
                GameOver();*/

            if (Input.GetKeyDown("p"))
            {
                Pause();
            }
        }
    }

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<PlayButtonClickedEvent>(PlayButtonClicked);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<PlayButtonClickedEvent>(PlayButtonClicked);
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    void PlayButtonClicked(PlayButtonClickedEvent e)
    {
        Play();
    }

    void Play()
    {
        SetState(GAMESTATE.play);
    }

    void Pause()
    {
        SetState(GAMESTATE.pause);
    }

    void Fight()
    {
        SetState(GAMESTATE.fight);
    }
}
