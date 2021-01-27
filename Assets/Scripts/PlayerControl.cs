using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public Transform movePoint;
    [SerializeField] public bool actionTaken = false;

    [SerializeField] public LayerMask movementStopper;
    [SerializeField] public ActionState actionStatus;
    GameSession gameSession;
    // Start is called before the first frame update
    void Start()
    {
        //this will mean movement can be separate from player entity, but can still be organized with player
        movePoint.parent = null;
        gameSession = FindObjectOfType<GameSession>();
        actionStatus = ActionState.DORMANT;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Move()
    {
        //Check if player has moved to point before moving again
        if (Vector3.Distance(this.transform.position, movePoint.position) == 0f && actionStatus == ActionState.DORMANT)
        {
            //will check if side button fully pressed
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                //checks if the box trying to move into is on collider layer
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, movementStopper))
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                    actionStatus = ActionState.STARTED;
                }
            }

            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, movementStopper))
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                    actionStatus = ActionState.STARTED;
                }
            }
        } 
        transform.position = Vector3.MoveTowards(this.transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(this.transform.position, movePoint.position) == 0f && actionStatus == ActionState.STARTED)
        {
            actionStatus = ActionState.FINISHED;
        }    
    }
}
