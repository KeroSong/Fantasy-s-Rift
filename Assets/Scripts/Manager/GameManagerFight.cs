using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public class GameManagerFight : MonoBehaviour
{
    [SerializeField] GameObject m_Goblin1;
    [SerializeField] GameObject m_Goblin2;
    [SerializeField] GameObject m_Goblin3;
    [SerializeField] GameObject m_DragonSoulEater;
    [SerializeField] GameObject m_DragonTheNightmare;
    [SerializeField] GameObject m_DragonTerrorBringer;
    [SerializeField] GameObject m_DragonUsurper;
    [SerializeField] GameObject m_Choice1;
    [SerializeField] GameObject m_Player1Monster1;
    [SerializeField] GameObject m_Player1Monster2;
    [SerializeField] GameObject m_Player1Monster3;
    [SerializeField] GameObject m_Choice2;
    [SerializeField] GameObject m_Player2Monster1;
    [SerializeField] GameObject m_Player2Monster2;
    [SerializeField] GameObject m_Player2Monster3;
    [SerializeField] GameObject m_MechWarrior;
    [SerializeField] GameObject m_MechArcher;
    [SerializeField] GameObject m_MechWizard;
    List<GameObject> m_ListMeck;

    List<GameObject> m_Enemy;
    [SerializeField] ProgressBar PbHealthMech1;
    [SerializeField] ProgressBar PbHealthMech2;
    [SerializeField] ProgressBar pbHealthMonster1, pbHealthMonster2, pbHealthMonster3;
    [SerializeField] GameObject HealthMonster1, HealthMonster2, HealthMonster3;
    [SerializeField] ProgressBarRound ProgressRoundMonster1, ProgressRoundMonster2, ProgressRoundMonster3;
    [SerializeField] ProgressBarRound pbClassOne, pbClassTwo;
    [SerializeField] float MonsterSpeed = 1;
    [SerializeField] float framerate=0.5f;
    [SerializeField] float classOneSpeed;
    [SerializeField] float classTwoSpeed;
    List<GameObject> m_Buttons;

    private static GameManagerFight m_Instance;

    public float Monster1;
    public float Monster2;
    public float Monster3;

    public bool IsFighting { get { return m_State == GAMESTATE.fight; } }
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
            case GAMESTATE.play:
                EventManager.Instance.Raise(new GamePlayEvent());
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
        m_Buttons = new List<GameObject>() { m_Player1Monster1, m_Player1Monster2, m_Player1Monster3, m_Player2Monster1, m_Player2Monster2, m_Player2Monster3 };
        m_ListMeck = new List<GameObject>() { m_MechWarrior, m_MechArcher, m_MechWizard };
    }

    // Start is called before the first frame update
    void Start()
    {
        
        PlayerPrefs.SetFloat("JaugeMecha1", 0);
        PlayerPrefs.SetFloat("JaugeMecha2", 0);
        PlayerPrefs.SetFloat("Monster1", 0);
        PlayerPrefs.SetFloat("Monster2", 0);
        PlayerPrefs.SetFloat("Monster3", 0);

        SetState(GAMESTATE.fight);
        //StartCoroutine(RoundOne());
        //StartCoroutine(RoundTwo());
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
            HealthMonster1.SetActive(true);
            HealthMonster2.SetActive(true);
            HealthMonster3.SetActive(true);
            m_Goblin1.SetActive(true);
            m_Goblin2.SetActive(true);
            m_Goblin3.SetActive(true);
        }
        else if (PlayerPrefs.GetString("Ennemi") == "SoulEater")
        {
            this.LOG(PlayerPrefs.GetString("Ennemi").ToString());
            EnemyAppears(m_DragonSoulEater);
            HealthMonster1.SetActive(true);
        }
        else if (PlayerPrefs.GetString("Ennemi") == "TheNightmare")
        {
            EnemyAppears(m_DragonTheNightmare);
            HealthMonster1.SetActive(true);
        }
        else if (PlayerPrefs.GetString("Ennemi") == "TerrorBringer")
        {
            EnemyAppears(m_DragonTerrorBringer);
            HealthMonster1.SetActive(true);
        }
        else
        {
            EnemyAppears(m_DragonUsurper);
        }
        ButtonAttack(null);
        if (PlayerPrefs.GetInt("classe") == 0)
        {
            Meck(m_MechWarrior);
        }
        else if (PlayerPrefs.GetInt("classe") == 1)
        {
            Meck(m_MechArcher);
        }
        else
        {
            Meck(m_MechWizard);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (IsFighting) { 
            if (Input.GetKeyDown("p"))
            {
                EventManager.Instance.Raise(new PauseHasBeenPressEvent());
            }

            float jaugeMecha1 = PlayerPrefs.GetFloat("JaugeMecha1");
            float jaugeMecha2 = PlayerPrefs.GetFloat("JaugeMecha2");

            float jaugeMonster1 = PlayerPrefs.GetFloat("Monster1");
            float jaugeMonster2 = PlayerPrefs.GetFloat("Monster2");
            float jaugeMonster3 = PlayerPrefs.GetFloat("Monster3");

            pbClassOne.Val = jaugeMecha1;
            pbClassTwo.Val = jaugeMecha2;
            ProgressRoundMonster1.Val = jaugeMonster1;
            ProgressRoundMonster2.Val = jaugeMonster2;
            ProgressRoundMonster3.Val = jaugeMonster3;


            if (pbClassOne.Val == 100 || pbClassTwo.Val == 100 || ProgressRoundMonster1.Val == 100 || ProgressRoundMonster2.Val == 100 || ProgressRoundMonster3.Val ==100)
            {
                if(pbClassOne.Val == 100)
                {
                
                    m_Choice1.SetActive(true);
                    m_Player1Monster1.SetActive(true);
                    m_Player1Monster2.SetActive(true);
                    m_Player1Monster3.SetActive(true);

                }
                else if (pbClassTwo.Val == 100)
                {
                    m_Choice2.SetActive(true);
                    m_Player2Monster1.SetActive(true);
                    m_Player2Monster2.SetActive(true);
                    m_Player2Monster3.SetActive(true);
                }
                else if (ProgressRoundMonster1.Val == 100)
                {
                    GameObject.FindGameObjectWithTag("Monster1").GetComponent<IAFight>().attack();
                    PlayerPrefs.SetFloat("Monster1", 0);
                }
                else if (ProgressRoundMonster2.Val == 100)
                {
                    GameObject.FindGameObjectWithTag("Monster2").GetComponent<IAFight>().attack();
                    PlayerPrefs.SetFloat("Monster2", 0);
                }
                else if (ProgressRoundMonster3.Val == 100)
                {
                    GameObject.FindGameObjectWithTag("Monster3").GetComponent<IAFight>().attack();
                    PlayerPrefs.SetFloat("Monster2", 0);
                }
            }
            else
            {
                if (PlayerPrefs.GetInt("Difficulté") == 0)
                {
                    PlayerPrefs.SetFloat("JaugeMecha1", (jaugeMecha1 + PlayerPrefs.GetFloat("VitesseJaugePlayer"))*1);
                    PlayerPrefs.SetFloat("JaugeMecha2", (jaugeMecha2 + PlayerPrefs.GetFloat("VitesseJaugeAI2"))*1);
                }
                else if (PlayerPrefs.GetInt("Difficulté") == 1)
                {
                    PlayerPrefs.SetFloat("JaugeMecha1", (jaugeMecha1 + PlayerPrefs.GetFloat("VitesseJaugePlayer")));
                    PlayerPrefs.SetFloat("JaugeMecha2", (jaugeMecha2 + PlayerPrefs.GetFloat("VitesseJaugeAI2")));
                }
                else
                {
                    PlayerPrefs.SetFloat("JaugeMecha1", (jaugeMecha1 + PlayerPrefs.GetFloat("VitesseJaugePlayer")) * 0.5f);
                    PlayerPrefs.SetFloat("JaugeMecha2", (jaugeMecha2 + PlayerPrefs.GetFloat("VitesseJaugeAI2")) * 0.5f);
                }

                PlayerPrefs.SetFloat("Monster1", (jaugeMonster1 + MonsterSpeed));
                PlayerPrefs.SetFloat("Monster2", (jaugeMonster2 + MonsterSpeed));
                PlayerPrefs.SetFloat("Monster3", (jaugeMonster3 + MonsterSpeed));
                ProgressRoundMonster1.Val += MonsterSpeed;
                ProgressRoundMonster1.Val += MonsterSpeed;

                if (PbHealthMech1.Val == 0 && PbHealthMech1.Val ==0)
                {
                    GameOver();
                }
                else if (pbHealthMonster1.Val == 0 && pbHealthMonster2.Val == 0 && pbHealthMonster3.Val == 0 )
                {
                    Victory();
                }
            }
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
        EventManager.Instance.AddListener<PlayButtonClickedEvent>(PlayButtonClicked);
        EventManager.Instance.AddListener<Button1Event>(Button1);
        EventManager.Instance.AddListener<Button2Event>(Button2);
        EventManager.Instance.AddListener<Button3Event>(Button3);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<ContinuePartyButtonClickedEvent>(ContinuPartyButtonClicked);
        EventManager.Instance.RemoveListener<FightButtonClickedEvent>(FightButtonClicked);
        EventManager.Instance.RemoveListener<ConfirmedButtonClickedEvent>(ConfirmedButtonClicked);
        EventManager.Instance.RemoveListener<MainMenuButtonClickedEvent>(MainMenuButtonClicked);
        EventManager.Instance.RemoveListener<SettingsButtonClickedEvent>(SettingsClicked);
        EventManager.Instance.RemoveListener<PauseHasBeenPressEvent>(PauseHasBeenPress);
        EventManager.Instance.RemoveListener<PlayButtonClickedEvent>(PlayButtonClicked);
        EventManager.Instance.RemoveListener<Button1Event>(Button1);
        EventManager.Instance.RemoveListener<Button2Event>(Button2);
        EventManager.Instance.RemoveListener<Button3Event>(Button3);
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

    void PlayButtonClicked(PlayButtonClickedEvent e)
    {
        Play();
    }

    void EnemyAppears(GameObject enemy)
    {
        m_Enemy.ForEach(item => { if (item != null) item.SetActive(enemy == item); });
    }

    void ButtonAttack(GameObject button)
    {
        m_Buttons.ForEach(item => { if (item != null) item.SetActive(button == item); });
    }

    void Button1(Button1Event e)
    {
        if(PlayerPrefs.GetFloat("JaugeMecha1") >= 100) 
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<player1mech>().attack();
            GameObject.FindGameObjectWithTag("Player").GetComponent<player1mech>().DamageToMonster1();
            PlayerPrefs.SetFloat("JaugeMecha1", 0);
            m_Choice1.SetActive(false);

        }
        else
        {
            GameObject.FindGameObjectWithTag("Player2").GetComponent<player2mech>().attack();
            GameObject.FindGameObjectWithTag("Player2").GetComponent<player2mech>().DamageToMonster1();
            PlayerPrefs.SetFloat("JaugeMecha2", 0);
            m_Choice2.SetActive(false);
        }

    }

    void Button2(Button2Event e)
    {
        if (PlayerPrefs.GetFloat("JaugeMecha1") >= 100)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<player1mech>().attack();
            GameObject.FindGameObjectWithTag("Player").GetComponent<player1mech>().DamageToMonster2();

            PlayerPrefs.SetFloat("JaugeMecha1", 0);
            m_Choice1.SetActive(false);
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player2").GetComponent<player2mech>().attack();
            GameObject.FindGameObjectWithTag("Player2").GetComponent<player2mech>().DamageToMonster2();
            PlayerPrefs.SetFloat("JaugeMecha2", 0);
            m_Choice2.SetActive(false);
        }
    }

    void Button3(Button3Event e)
    {
        if (PlayerPrefs.GetFloat("JaugeMecha1") >= 100)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<player1mech>().attack();
            GameObject.FindGameObjectWithTag("Player").GetComponent<player1mech>().DamageToMonster3();
            PlayerPrefs.SetFloat("JaugeMecha1", 0);
            m_Choice1.SetActive(false);
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player2").GetComponent<player2mech>().attack();
            GameObject.FindGameObjectWithTag("Player2").GetComponent<player2mech>().DamageToMonster3();
            PlayerPrefs.SetFloat("JaugeMecha2", 0);
            m_Choice2.SetActive(false);
        }
    }

    void Meck(GameObject meck)
    {
        m_ListMeck.ForEach(item => { if (item != null) item.SetActive(meck == item); });
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
    void Victory()
    {
        SetState(GAMESTATE.victoryFight);
    }

    void Play()
    {
        SetState(GAMESTATE.play);
    }

    IEnumerator RoundOne()
    {
        while (pbClassOne.Val < 100)
        {
            pbClassOne.Val = pbClassOne.Val + classOneSpeed;
            yield return new WaitForSeconds(framerate);
        }
        /*GameObject.FindGameObjectWithTag("Player").GetComponent<player1mech>().isDead();
        GameObject.FindGameObjectWithTag("Player2").GetComponent<player1mech>().isDead();
        GameOver();*/
    }
    IEnumerator RoundTwo()
    {
        while (pbClassTwo.Val < 100)
        {
            pbClassTwo.Val = pbClassTwo.Val + classTwoSpeed;
            yield return new WaitForSeconds(framerate);
        }
        /*GameObject.FindGameObjectWithTag("Player").GetComponent<player1mech>().isDead();
        GameObject.FindGameObjectWithTag("Player2").GetComponent<player1mech>().isDead();
        GameOver();*/
    }

}
