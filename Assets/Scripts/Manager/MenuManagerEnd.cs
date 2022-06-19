using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using SDD.Events;

[RequireComponent(typeof(AudioSource))]

public class MenuManagerEnd : MonoBehaviour,IEventHandler
{
    AudioSource audioData;

    void Start()
    {
        audioData = GetComponent<AudioSource>();
    }

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<GameMenuEvent>(GameMenu);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<GameMenuEvent>(GameMenu);
    }

    private void OnEnable()
    {
        SubscribeEvents();    
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    void GameMenu(GameMenuEvent e)
    {
        SceneManager.LoadScene(0);
    }

    public void MenuButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new MainMenuButtonClickedEvent());
    }
}