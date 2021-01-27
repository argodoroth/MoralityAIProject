using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionState { DORMANT, STARTED, PROCESSING, FINISHED}

public class NPC : MonoBehaviour
{
    [SerializeField] Transform movePoint;
    [SerializeField] int moveSpeed;
    [SerializeField] public LayerMask movementStopper;
    [SerializeField] public ActionState actionStatus;
    PlayerControl player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerControl>();
        actionStatus = ActionState.DORMANT;
        movePoint.parent = null;
    }

    public void Move()
    {
        Debug.Log(actionStatus);
        if (Vector3.Distance(this.transform.position, movePoint.position) == 0f && actionStatus == ActionState.DORMANT)
        {
            //checks if the box trying to move into is on collider layer
            if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(-1, 0f, 0f), .2f, movementStopper))
            {
                actionStatus = ActionState.STARTED;
                movePoint.position += new Vector3(-1, 0f, 0f);
            }
        }
        if (Vector3.Distance(this.transform.position, movePoint.position) > 0f && actionStatus == ActionState.STARTED)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        } else if (Vector3.Distance(this.transform.position, movePoint.position) == 0f)
        {
            actionStatus = ActionState.FINISHED;
        }
    }
}
