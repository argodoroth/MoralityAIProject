using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] string direction;

    //Makes a singleton object that can be passed through scenes
    void Awake()
    {
        {
            int gameStatusCount = FindObjectsOfType<GameSession>().Length;
            if (gameStatusCount > 1)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDirection(string dir)
    {
        direction = dir;
    }

    public void SetPlayerPos()
    {
        PlayerControl player = FindObjectOfType<PlayerControl>();
        if (direction == "Left")
        {
            player.transform.position = new Vector3(-7.5f, 0.5f, -5.5f);
        }
        if (direction == "Right")
        {
            player.transform.position = new Vector3(7.5f, 0.5f, -5.5f);
        }
        if (direction == "Up")
        {
            player.transform.position = new Vector3(0.5f, 4.5f, -5.5f);
        }
        if (direction == "Down")
        {
            player.transform.position = new Vector3(0.5f, -4.5f, -5.5f);
        }
    }

}
