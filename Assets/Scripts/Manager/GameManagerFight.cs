using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public class GameManagerFight : MonoBehaviour
{
<<<<<<< HEAD
    [SerializeField] GameObject m_Goblin1;
    [SerializeField] GameObject m_Goblin2;
    [SerializeField] GameObject m_Goblin3;
    [SerializeField] GameObject m_DragonSoulEater;
    [SerializeField] GameObject m_DragonTheNightmare;
    [SerializeField] GameObject m_DragonTerrorBringer;
    [SerializeField] GameObject m_DragonUsurper;

    List<GameObject> m_Enemy;
=======
    [SerializeField] ProgressBar pbHealth, pbMana;
    [SerializeField] ProgressBarRound pbClassOne, pbClassTwo;
    [SerializeField] float framerate=0.5f;
    [SerializeField] float classOneSpeed;
    [SerializeField] float classTwoSpeed;
>>>>>>> a964f9ec1b2a3f4cbb03c1b3079d60846fc59e28

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
            case GAMESTATE.confirmed:
                EventManager.Instance.Raise(new GameConfirmedEvent());
                break;
            case GAMESTATE.menu:
                EventManager.Instance.Raise(new GameMenuEvent());
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

        m_Enemy = new List<GameObject>() {m_Goblin1, m_Goblin2, m_Goblin3, m_DragonSoulEater, m_DragonTheNightmare, m_DragonTerrorBringer, m_DragonUsurper};
    }

    // Start is called before the first frame update
    void Start()
    {
        SetState(GAMESTATE.fight);
<<<<<<< HEAD

        if (PlayerPrefs.GetInt("classe") == 0)
        {
            //guerrier
        }
        else if (PlayerPrefs.GetInt("classe") == 1)
        {
            //archer
        }
        else
        {
            //mage
        }

        if (PlayerPrefs.GetInt("classe") == 0)
        {
            //guerrier
        }
        else if (PlayerPrefs.GetInt("classe") == 1)
        {
            //archer
        }
        else
        {
            //mage
        }

        if (PlayerPrefs.GetString("Ennemi") == "Gobelin")
        {
            EnemyAppears(null);
            m_Goblin1.SetActive(true);
            m_Goblin2.SetActive(true);
            m_Goblin3.SetActive(true);
        }
        else if (PlayerPrefs.GetString("Ennemi") == "SoulEater")
        {
            EnemyAppears(m_DragonSoulEater);
        }
        else if (PlayerPrefs.GetString("Ennemi") == "TheNightmare")
        {
            EnemyAppears(m_DragonTheNightmare);
        }
        else if (PlayerPrefs.GetString("Ennemi") == "TerrorBringer")
        {
            EnemyAppears(m_DragonTerrorBringer);
        }
        else
        {
            EnemyAppears(m_DragonUsurper);
        }
=======
        StartCoroutine(RoundOne());
        StartCoroutine(RoundTwo());
>>>>>>> a964f9ec1b2a3f4cbb03c1b3079d60846fc59e28
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            EventManager.Instance.Raise(new PauseHasBeenPressEvent());
        }
    }

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<ContinuePartyButtonClickedEvent>(ContinuPartyButtonClicked);
        EventManager.Instance.AddListener<FightButtonClickedEvent>(FightButtonClicked);
        EventManager.Instance.AddListener<ConfirmedButtonClickedEvent>(ConfirmedButtonClicked);
        EventManager.Instance.AddListener<MainMenuButtonClickedEvent>(MainMenuButtonClicked);
        EventManager.Instance.AddListener<SettingsButtonClickedEvent>(SettingsClicked);
        EventManager.Instance.AddListener<PauseHasBeenPressEvent>(PauseHasBeenPress);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<ContinuePartyButtonClickedEvent>(ContinuPartyButtonClicked);
        EventManager.Instance.RemoveListener<FightButtonClickedEvent>(FightButtonClicked);
        EventManager.Instance.RemoveListener<ConfirmedButtonClickedEvent>(ConfirmedButtonClicked);
        EventManager.Instance.RemoveListener<MainMenuButtonClickedEvent>(MainMenuButtonClicked);
        EventManager.Instance.RemoveListener<SettingsButtonClickedEvent>(SettingsClicked);
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

    void ConfirmedButtonClicked(ConfirmedButtonClickedEvent e)
    {
        Confirmed();
    }

    void MainMenuButtonClicked(MainMenuButtonClickedEvent e)
    {
        Menu();
    }

    void SettingsClicked(SettingsButtonClickedEvent e)
    {
        Settings();
    }

    void PauseHasBeenPress(PauseHasBeenPressEvent e)
    {
        Pause();
    }

    void EnemyAppears(GameObject enemy)
    {
        m_Enemy.ForEach(item => { if (item != null) item.SetActive(enemy == item); });
    }



    void Load()
    {
        SetState(GAMESTATE.load);
    }

    void Fight()
    {
        SetState(GAMESTATE.fight);
    }

    void Confirmed()
    {
        SetState(GAMESTATE.confirmed);
    }

    void Menu()
    {
        SetState(GAMESTATE.menu);
    }

    void Settings()
    {
        SetState(GAMESTATE.settings);
    }

    void Pause()
    {
        SetState(GAMESTATE.pause);
    }

    void GameOver()
    {
        SetState(GAMESTATE.gameover);
    }


    IEnumerator RoundOne()
    {
        while (pbClassOne.Val < 100)
        {
            pbClassOne.Val = pbClassOne.Val + classOneSpeed;
            yield return new WaitForSeconds(framerate);
        }
        GameObject.FindGameObjectWithTag("Player").GetComponent<player1mech>().isDead();
        GameObject.FindGameObjectWithTag("Player2").GetComponent<player1mech>().isDead();
        GameOver();
    }
    IEnumerator RoundTwo()
    {
        while (pbClassTwo.Val < 100)
        {
            pbClassTwo.Val = pbClassTwo.Val + classTwoSpeed;
            yield return new WaitForSeconds(framerate);
        }
        GameObject.FindGameObjectWithTag("Player").GetComponent<player1mech>().isDead();
        GameObject.FindGameObjectWithTag("Player2").GetComponent<player1mech>().isDead();
        GameOver();
    }

}
