using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnState { START, PLAYERTURN, ENEMYTURN}
public class TurnSystem : MonoBehaviour
{
    public TurnState state;
    public PlayerControl player;
    public NPC[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        state = TurnState.START;
        StartCoroutine(Setup());
    }

    IEnumerator Setup()
    {
        //player = FindObjectOfType<PlayerControl>();
        //enemies = FindObjectsOfType<NPC>();
        yield return new WaitForSeconds(1f);
        state = TurnState.PLAYERTURN;
    }

    IEnumerator TurnSwitch()
    {
        yield return new WaitForSeconds(1f);
        if (state == TurnState.ENEMYTURN)
        {
            state = TurnState.PLAYERTURN;
        } else if (state == TurnState.PLAYERTURN)
        {
            state = TurnState.ENEMYTURN;
        }
    }

    private void Update()
    {
        if (state == TurnState.PLAYERTURN)
        {
            //player.movePoint.position = player.transform.position + new Vector3(0, -1, 0);
            //player.transform.position = Vector3.MoveTowards(this.transform.position, player.movePoint.position, player.moveSpeed * Time.deltaTime);
            PlayerTurn();
        } else if (state == TurnState.ENEMYTURN)
        {
            EnemyTurn();
        }
    }
    void PlayerTurn()
    {
        //player.movePoint.position = player.transform.position + new Vector3(0, -1, 0);
        //player.transform.position = Vector3.MoveTowards(this.transform.position, player.movePoint.position, player.moveSpeed * Time.deltaTime);
        player.Move();
        if (enemies.Length > 0 && player.actionStatus == ActionState.FINISHED)
        {
            state = TurnState.ENEMYTURN;
            player.actionStatus = ActionState.DORMANT;
        }
    }

    void EnemyTurn()
    {
        bool Allfinished = true;
        foreach (NPC enemy in enemies)
        {
            enemy.Move();
            if (enemy.actionStatus != ActionState.FINISHED)
            {
                Allfinished = false;
            }
        }
        if (Allfinished)
        {
            state = TurnState.PLAYERTURN;
            foreach (NPC enemy in enemies)
            {
                enemy.actionStatus = ActionState.DORMANT;
            }
        }
    }
}
