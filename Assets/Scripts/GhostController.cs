using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour {

    public List<string> actionList = new List<string>();

    private float _speed = 1;
    private float targetPos;

    private string _facingDirection;
    private bool _isMoving;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(actionList.Count != 0)
        {
            DoPlayerAction();
        }
	}

    public bool MoveRightGhost()
    {
        if (!_isMoving)
        {
            targetPos = transform.position.x + 1;
            _isMoving = true;
        }

        if (transform.position.x < targetPos)
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
            _facingDirection = "right";
            return false;
        }

        else
        {
            _facingDirection = "right";
            _isMoving = false;
            return true;
        }

    }

    public bool MoveLeftGhost()
    {
        if (!_isMoving)
        {
            targetPos = transform.position.x - 1;
            _isMoving = true;
        }

        if (transform.position.x > targetPos)
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
            _facingDirection = "left";
            return false;
        }

        else
        {
            _facingDirection = "left";
            _isMoving = false;
            return true;
        }

    }

    // Effectue la liste d'action des players
    public void DoPlayerAction()
    {
        bool actionFinish = false;
        switch (actionList[0])
        {
            case "Right":
                actionFinish = MoveRightGhost();
                break;
            case "Left":
                actionFinish = MoveLeftGhost();
                break;
            case "Jump":
                JumpGhost();
                break;
        }
        if (actionFinish)
        {
            actionList.RemoveAt(0);
        }

    }

    public void JumpGhost()
    {
        transform.Translate(2, 2, 0);
    }

}
