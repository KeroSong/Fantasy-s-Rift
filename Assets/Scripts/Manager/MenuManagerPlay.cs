using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SDD.Events;

public class MenuManagerPlay : MonoBehaviour,IEventHandler
{
    [SerializeField] GameObject m_InventoryPanel;
    [SerializeField] GameObject m_EquipmentPanel;
    [SerializeField] GameObject m_ShopPanel;
    [SerializeField] GameObject m_InnPanel;

    [SerializeField] int m_ScenePause;
    [SerializeField] int m_SceneFight;

    List<GameObject> m_Panels;

    private void Awake()
    {
        m_Panels = new List<GameObject>() {m_InventoryPanel, m_EquipmentPanel, m_ShopPanel, m_InnPanel};
    }

    /*void Update()
    {
            this.LOG("cou");
        for (int i = 0; i < 25; i++)
        {
            int test = m_InventoryPanel.transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<ItemOnObject>().item.itemID;
            this.LOG(test.ToString());
            this.LOG("cou");
        }
    }*/

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<GamePauseEvent>(GamePause);
        EventManager.Instance.AddListener<GameFightEvent>(GameFight);
        EventManager.Instance.AddListener<GameInventoryEvent>(GameInventory);
        EventManager.Instance.AddListener<GameEquipmentEvent>(GameEquipment);
        EventManager.Instance.AddListener<GameShopEvent>(GameShop);
        EventManager.Instance.AddListener<GameInnEvent>(GameInn);
        EventManager.Instance.AddListener<GamePlayEvent>(GamePlay);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<GamePauseEvent>(GamePause);
        EventManager.Instance.RemoveListener<GameFightEvent>(GameFight);
        EventManager.Instance.RemoveListener<GameInventoryEvent>(GameInventory);
        EventManager.Instance.RemoveListener<GameEquipmentEvent>(GameEquipment);
        EventManager.Instance.RemoveListener<GameShopEvent>(GameShop);
        EventManager.Instance.RemoveListener<GameInnEvent>(GameInn);
        EventManager.Instance.RemoveListener<GamePlayEvent>(GamePlay);
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    void GamePause(GamePauseEvent e)
    {
        SceneManager.LoadScene(m_ScenePause);
    }

    void GameFight(GameFightEvent e)
    {
        SceneManager.LoadScene(m_SceneFight);
    }

    void GameInventory(GameInventoryEvent e)
    {
        if (!m_InventoryPanel.activeSelf)
        {
            m_InventoryPanel.SetActive(true);

        }
        else
        {
            m_InventoryPanel.SetActive(false);
        }
    }

    void GameEquipment(GameEquipmentEvent e)
    {
        if (!m_EquipmentPanel.activeSelf)
        {
            m_EquipmentPanel.SetActive(true);
            m_EquipmentPanel.transform.GetChild(0).gameObject.SetActive(true);
            m_EquipmentPanel.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            m_EquipmentPanel.SetActive(false);
            m_EquipmentPanel.transform.GetChild(0).gameObject.SetActive(false);
            m_EquipmentPanel.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    void GameShop(GameShopEvent e)
    {
        OpenPanel(m_ShopPanel);
    }

    void GameInn(GameInnEvent e)
    {
        OpenPanel(m_InnPanel);
    }

    void GamePlay(GamePlayEvent e)
    {
        OpenPanel(null);
    }

    void OpenPanel(GameObject panel)
    {
        m_Panels.ForEach(item => { if (item != null) item.SetActive(panel == item); });
    }
}