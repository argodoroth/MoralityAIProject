using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] String sceneName;
    [SerializeField] String direction;
    GameSession gameSession;

    // Start is called before the first frame update

    private void Awake()
    {
        gameSession = FindObjectOfType<GameSession>();
        gameSession.SetPlayerPos();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameSession.SetDirection(direction);
        SceneManager.LoadScene(sceneName);
    }
}
