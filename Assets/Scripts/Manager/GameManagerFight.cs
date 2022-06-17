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

    List<GameObject> m_Enemy;
    [SerializeField] ProgressBar pbHealth, pbMana;
    [SerializeField] ProgressBarRound pbClassOne, pbClassTwo;
    [SerializeField] float framerate=0.5f;
    [SerializeField] float classOneSpeed;
    [SerializeField] float classTwoSpeed;
    List<GameObject> m_Buttons;

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
        m_Buttons = new List<GameObject>() { m_Player1Monster1, m_Player1Monster2, m_Player1Monster3, m_Player2Monster1, m_Player2Monster2, m_Player2Monster3 };
    }

    // Start is called before the first frame update
    void Start()
    {
        
        PlayerPrefs.SetFloat("JaugeMecha1", 0);
        PlayerPrefs.SetFloat("JaugeMecha2", 0);

        SetState(GAMESTATE.fight);
        StartCoroutine(RoundOne());
        StartCoroutine(RoundTwo());
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
            this.LOG(PlayerPrefs.GetString("Ennemi").ToString());
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
        ButtonAttack(null);


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            EventManager.Instance.Raise(new PauseHasBeenPressEvent());
        }

        float jaugeMecha1 = PlayerPrefs.GetFloat("JaugeMecha1");
        float jaugeMecha2 = PlayerPrefs.GetFloat("JaugeMecha2");

        pbClassOne.Val = jaugeMecha1;
        pbClassTwo.Val = jaugeMecha2;

        if(pbClassTwo.Val == 100 || pbClassTwo.Val == 100)
        {
            if(pbClassTwo.Val == 100)
            {
                
                m_Choice1.SetActive(true);
                m_Player1Monster1.SetActive(true);
                m_Player1Monster2.SetActive(true);
                m_Player1Monster3.SetActive(true);

            }
            else
            {
                m_Choice2.SetActive(true);
                m_Player2Monster1.SetActive(true);
                m_Player2Monster2.SetActive(true);
                m_Player2Monster3.SetActive(true);
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("Difficulté") == 0)
            {
                PlayerPrefs.SetFloat("JaugeMecha1", (jaugeMecha1 + PlayerPrefs.GetFloat("VitesseJaugePlayer"))*2);
                PlayerPrefs.SetFloat("JaugeMecha2", (jaugeMecha2 + PlayerPrefs.GetFloat("VitesseJaugeAI2"))*2);
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
            

        }
        Debug.Log(pbClassTwo.Val);
    }

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<ContinuePartyButtonClickedEvent>(ContinuPartyButtonClicked);
        EventManager.Instance.AddListener<FightButtonClickedEvent>(FightButtonClicked);
        EventManager.Instance.AddListener<ConfirmedButtonClickedEvent>(ConfirmedButtonClicked);
        EventManager.Instance.AddListener<MainMenuButtonClickedEvent>(MainMenuButtonClicked);
        EventManager.Instance.AddListener<SettingsButtonClickedEvent>(SettingsClicked);
        EventManager.Instance.AddListener<PauseHasBeenPressEvent>(PauseHasBeenPress);
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
            GameObject.FindGameObjectWithTag("Player").GetComponent<player1mech>().stopAttack();
            PlayerPrefs.SetFloat("JaugeMecha1", 0);
            m_Choice1.SetActive(false);

        }
        else
        {
            GameObject.FindGameObjectWithTag("Player2").GetComponent<player1mech>().attack();
            GameObject.FindGameObjectWithTag("Player2").GetComponent<player1mech>().stopAttack();
            PlayerPrefs.SetFloat("JaugeMecha2", 0);
            m_Choice2.SetActive(false);
        }

    }

    void Button2(Button2Event e)
    {
        if (PlayerPrefs.GetFloat("JaugeMecha1") >= 100)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<player1mech>().attack();
            GameObject.FindGameObjectWithTag("Player").GetComponent<player1mech>().stopAttack();
            PlayerPrefs.SetFloat("JaugeMecha1", 0);
            m_Choice1.SetActive(false);
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player2").GetComponent<player1mech>().attack();
            GameObject.FindGameObjectWithTag("Player2").GetComponent<player1mech>().stopAttack();
            PlayerPrefs.SetFloat("JaugeMecha2", 0);
            m_Choice2.SetActive(false);
        }
    }

    void Button3(Button3Event e)
    {
        if (PlayerPrefs.GetFloat("JaugeMecha1") >= 100)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<player1mech>().attack();
            GameObject.FindGameObjectWithTag("Player").GetComponent<player1mech>().stopAttack();
            PlayerPrefs.SetFloat("JaugeMecha1", 0);
            m_Choice1.SetActive(false);
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player2").GetComponent<player1mech>().attack();
            GameObject.FindGameObjectWithTag("Player2").GetComponent<player1mech>().stopAttack();
            PlayerPrefs.SetFloat("JaugeMecha2", 0);
            m_Choice2.SetActive(false);
        }
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
