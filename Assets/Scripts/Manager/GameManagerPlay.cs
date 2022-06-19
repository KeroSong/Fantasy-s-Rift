using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public class GameManagerPlay : MonoBehaviour
{
    [SerializeField] GameObject Player;

    [SerializeField] GameObject m_ManWarrior;
    [SerializeField] GameObject m_ManArcher;
    [SerializeField] GameObject m_ManWizard;
    [SerializeField] GameObject m_WomanWarrior;
    [SerializeField] GameObject m_WomanArcher;
    [SerializeField] GameObject m_WomanWizard;

    [SerializeField] GameObject m_DragonSoulEater;
    [SerializeField] GameObject m_ImageDragonSoulEater;
    [SerializeField] GameObject m_DragonTheNightmare;
    [SerializeField] GameObject m_ImageDragonTheNightmare;
    [SerializeField] GameObject m_DragonTerrorBringer;
    [SerializeField] GameObject m_ImageDragonTerrorBringer;
    [SerializeField] GameObject m_DragonUsurper;
    [SerializeField] GameObject m_ImageDragonUsurper;

    [SerializeField] GameObject m_MapCamera;

    List<GameObject> m_Characters;

    Dragon dragon;
    List<bool> m_ListeDragon;

    private static GameManagerPlay m_Instance;
    public static GameManagerPlay Instance { get {
            //if (m_Instance == null) m_Instance = CreateInstance(); // Impossible dans Unity
            return m_Instance; } }

    GAMESTATE m_State;
    public bool IsPlaying { get { return m_State == GAMESTATE.play; } }
    public bool IsInventory { get { return m_State == GAMESTATE.inventory; } }
    public bool IsEquipment { get { return m_State == GAMESTATE.equipment; } }

    void SetState(GAMESTATE newState)
    {
        m_State = newState;
        switch (m_State)
        {
            case GAMESTATE.play:
                EventManager.Instance.Raise(new GamePlayEvent());
                break;
            case GAMESTATE.pause:
                EventManager.Instance.Raise(new GamePauseEvent());
                break;
            case GAMESTATE.inventory:
                EventManager.Instance.Raise(new GameInventoryEvent());
                break;
            case GAMESTATE.equipment:
                EventManager.Instance.Raise(new GameEquipmentEvent());
                break;
            case GAMESTATE.fight:
                EventManager.Instance.Raise(new GameFightEvent());
                break;
            case GAMESTATE.shop:
                EventManager.Instance.Raise(new GameShopEvent());
                break;
            case GAMESTATE.inn:
                EventManager.Instance.Raise(new GameInnEvent());
                break;
            case GAMESTATE.end:
                EventManager.Instance.Raise(new GameEndEvent());
                break;
            default:
                break;
        }
    }
    void SelectCharacter(GameObject character)
    {
        m_Characters.ForEach(item => { if (item != null) item.SetActive(character == item); });
    }

    void Awake()
    {
        m_Characters = new List<GameObject>() {m_ManWarrior, m_ManArcher, m_ManWizard, m_WomanWarrior, m_WomanArcher, m_WomanWizard};

        if (m_Instance == null) m_Instance = this;
        else Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetState(GAMESTATE.play);

        Vector3 position = new Vector3(
            PlayerPrefs.GetFloat("PositionX"),
            PlayerPrefs.GetFloat("PositionY"),
            PlayerPrefs.GetFloat("PositionZ"));
        Player.transform.position = position;

        if (PlayerPrefs.GetInt("sexe") == 0)
        {
            if (PlayerPrefs.GetInt("classe") == 0)
            {
                SelectCharacter(m_ManWarrior);
            }
            else if (PlayerPrefs.GetInt("classe") == 1)
            {
                SelectCharacter(m_ManArcher);
            }
            else if (PlayerPrefs.GetInt("classe") == 2)
            {
                SelectCharacter(m_ManWizard);
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("classe") == 0)
            {
                SelectCharacter(m_WomanWarrior);
            }
            else if (PlayerPrefs.GetInt("classe") == 1)
            {
                SelectCharacter(m_WomanArcher);
            }
            else if (PlayerPrefs.GetInt("classe") == 2)
            {
                SelectCharacter(m_WomanWizard);
            }
        }

        m_MapCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        m_ListeDragon = ListeLoad();

        if(IsPlaying || IsInventory || IsEquipment)
        {
            PlayerPrefs.SetFloat("PositionX", (float)Player.transform.position.x);
            PlayerPrefs.SetFloat("PositionY", (float)Player.transform.position.y);
            PlayerPrefs.SetFloat("PositionZ", (float)Player.transform.position.z);

            if (Input.GetKeyDown("p"))
            {
                Pause();
            }

            if (Input.GetKeyDown("i"))
            {
                Inventory();
            }

            if (Input.GetKeyDown("c"))
            {
                Equipment();
            }

            if (Input.GetKeyDown("m"))
            {
                Map();
            }
        }

        if (m_ListeDragon[0])
        {
            Destroy(m_DragonSoulEater.GetComponent<Collider>());
            Destroy(m_ImageDragonSoulEater);
        }
        if (m_ListeDragon[1])
        {
            Destroy(m_DragonTheNightmare.GetComponent<Collider>());
            Destroy(m_ImageDragonTheNightmare);
        }
        if (m_ListeDragon[2])
        {
            Destroy(m_DragonTerrorBringer.GetComponent<Collider>());
            Destroy(m_ImageDragonTerrorBringer);
        }
        if (m_ListeDragon[3])
        {
            Destroy(m_DragonUsurper.GetComponent<Collider>());
            Destroy(m_ImageDragonUsurper);
        }
        int i = 0;
        foreach(bool allDragonDead in m_ListeDragon)
        {
            if (allDragonDead == true)
            i++;
        }
        if (i==4)
        {
            End();
        }

    }

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<PlayButtonClickedEvent>(PlayButtonClicked);
        EventManager.Instance.AddListener<FightCollisionEvent>(FightCollision);
        EventManager.Instance.AddListener<FightSoulEaterCollisionEvent>(FightSoulEaterCollision);
        EventManager.Instance.AddListener<FightTheNightmareCollisionEvent>(FightTheNightmareCollision);
        EventManager.Instance.AddListener<FightTerrorBringerCollisionEvent>(FightTerrorBringerCollision);
        EventManager.Instance.AddListener<FightUsurperCollisionEvent>(FightUsurperCollision);
        EventManager.Instance.AddListener<ShopCollisionEvent>(ShopCollision);
        EventManager.Instance.AddListener<InnCollisionEvent>(InnCollision);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<PlayButtonClickedEvent>(PlayButtonClicked);
        EventManager.Instance.RemoveListener<FightCollisionEvent>(FightCollision);
        EventManager.Instance.RemoveListener<FightSoulEaterCollisionEvent>(FightSoulEaterCollision);
        EventManager.Instance.RemoveListener<FightTheNightmareCollisionEvent>(FightTheNightmareCollision);
        EventManager.Instance.RemoveListener<FightTerrorBringerCollisionEvent>(FightTerrorBringerCollision);
        EventManager.Instance.RemoveListener<FightUsurperCollisionEvent>(FightUsurperCollision);
        EventManager.Instance.RemoveListener<ShopCollisionEvent>(ShopCollision);
        EventManager.Instance.RemoveListener<InnCollisionEvent>(InnCollision);
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

    void FightCollision(FightCollisionEvent e)
    {
        PlayerPrefs.SetString("Ennemi", "Gobelin");
        Fight();
    }

    void FightSoulEaterCollision(FightSoulEaterCollisionEvent e)
    {
        PlayerPrefs.SetString("Ennemi", "SoulEater");
        Fight();
    }

    void FightTheNightmareCollision(FightTheNightmareCollisionEvent e)
    {
        PlayerPrefs.SetString("Ennemi", "TheNightmare");
        Fight();
    }

    void FightTerrorBringerCollision(FightTerrorBringerCollisionEvent e)
    {
        PlayerPrefs.SetString("Ennemi", "TerrorBringer");
        Fight();
    }

    void FightUsurperCollision(FightUsurperCollisionEvent e)
    {
        PlayerPrefs.SetString("Ennemi", "Usurper");
        Fight();
    }

    void ShopCollision(ShopCollisionEvent e)
    {
        Shop();
    }

    void InnCollision(InnCollisionEvent e)
    {
        Inn();
    }

    void Play()
    {
        SetState(GAMESTATE.play);
    }

    void Pause()
    {
        SetState(GAMESTATE.pause);
    }

    void Inventory()
    {
        SetState(GAMESTATE.inventory);
    }

    void Equipment()
    {
        SetState(GAMESTATE.equipment);
    }

    void Fight()
    {
        SetState(GAMESTATE.fight);
    }

    void Shop()
    {
        SetState(GAMESTATE.shop);
    }

    void Inn()
    {
        SetState(GAMESTATE.inn);
    }

    void Map()
    {
        if (!m_MapCamera.activeSelf)
        {
            m_MapCamera.SetActive(true);
        }
        else
        {
            m_MapCamera.SetActive(false);
        }
    }

    void End()
    {
        SetState(GAMESTATE.end);
    }

    List<bool> ListeLoad()
    {
        string filePath = Application.persistentDataPath + "/Dragon.json";
        string data = System.IO.File.ReadAllText(filePath);
        dragon = JsonUtility.FromJson<Dragon>(data);

        List<bool> m_Liste = dragon.listDragon;
        return m_Liste;
    }

    void ListeSave(List<bool> m_Liste)
    {
        dragon.listDragon = m_Liste;

        string filePath = Application.persistentDataPath + "/Dragon.json";
        string data = JsonUtility.ToJson(dragon);
        System.IO.File.WriteAllText(filePath, data);
    }
}
