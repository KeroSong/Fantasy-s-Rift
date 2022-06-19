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
    
    List<GameObject> m_Panels;

    private void Awake()
    {
        m_Panels = new List<GameObject>() {m_InventoryPanel, m_EquipmentPanel, m_ShopPanel, m_InnPanel};
    }

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<GamePauseEvent>(GamePause);
        EventManager.Instance.AddListener<GameFightEvent>(GameFight);
        EventManager.Instance.AddListener<GameInventoryEvent>(GameInventory);
        EventManager.Instance.AddListener<GameEquipmentEvent>(GameEquipment);
        EventManager.Instance.AddListener<GameShopEvent>(GameShop);
        EventManager.Instance.AddListener<GameInnEvent>(GameInn);
        EventManager.Instance.AddListener<GamePlayEvent>(GamePlay);
        EventManager.Instance.AddListener<GameEndEvent>(GameEnd);
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
        EventManager.Instance.RemoveListener<GameEndEvent>(GameEnd);
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
        SceneManager.LoadScene(2);
    }

    void GameFight(GameFightEvent e)
    {
        SceneManager.LoadScene(3);
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

    void GameEnd(GameEndEvent e)
    {
        SceneManager.LoadScene(4);
    }

    void OpenPanel(GameObject panel)
    {
        m_Panels.ForEach(item => { if (item != null) item.SetActive(panel == item); });
    }

    public void PlayButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new PlayButtonClickedEvent());
    }
}