using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public List<string> actionsList = new List<string>();
    public List<string> tiredList = new List<string>();

    public GameObject monster;
    public GameObject ghost;
    public GameObject panelBulle;

    public Animator animator;

    public int attackDmg = 1;
    public int nbActionMax;

    private string _facingDirection;

    public bool isDead = false;

    private bool _isMoving;
    private bool _isGrounded;

    private int _health = 1;

    private float _speed = 2;
    private float targetPos;
    private float _delay = 0;


    void Awake () {
        animator = GetComponent<Animator>();
        panelBulle = transform.GetChild(0).gameObject;
    }
	

	void Update () {
        if(nbActionMax != 0)
        {
            switch (GameController._levelState)
            {
                case LevelState.dreaming:

                    ghost.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        print("MoveRight");
                        actionsList.Add("Right");
                        ghost.GetComponent<GhostController>().actionList.Add("Right");
                        --nbActionMax;
                    }

                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        print("MoveLeft");
                        actionsList.Add("Left");
                        ghost.GetComponent<GhostController>().actionList.Add("Left");
                        --nbActionMax;
                    }

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        print("Jump");
                        actionsList.Add("Jump");
                        ghost.GetComponent<GhostController>().actionList.Add("Jump");
                        --nbActionMax;
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        print("Attack");
                        actionsList.Add("Attack");
                        ghost.GetComponent<GhostController>().actionList.Add("Attack");
                        --nbActionMax;
                    }
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        GameController._levelState = LevelState.running;
                    }
                    break;
            }
        }
        else
        {
            GameController._levelState = LevelState.running;
        }

        if (_health<=0)
        {
            Death();
        }
        
    }

    public bool MoveRight()
    {
        _delay += Time.deltaTime;
        if (!_isMoving)
        {
            targetPos = transform.position.x + 1;
            _isMoving = true;
        }

        if (_delay < 8 && transform.position.x < targetPos)
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
            _facingDirection = "right";
            return false;
        }

        else
        {
            _facingDirection = "right";
            _isMoving = false;
            _delay = 0;
            return true;
        }
            

    }

    public bool MoveLeft()
    {
        _delay += Time.deltaTime;
        if (!_isMoving)
        {
            targetPos = transform.position.x - 1;
            _isMoving = true;
        }

        if (_delay < 8 && transform.position.x > targetPos)
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
            _facingDirection = "left";
            return false;
        }

        else
        {
            _facingDirection = "left";
            _isMoving = false;
            _delay = 0;
            return true;
        }
    }

    public bool Jump()
    {
        _delay += Time.deltaTime;
        if (_delay >= 1)
        {
            if(!_isMoving)
            {
                _isGrounded = false;
                _isMoving = true;
            }
            
            animator.SetBool("isJumping", false);
            print(_isGrounded);
            if (_facingDirection == "right" && !_isGrounded)
            {
                transform.Translate(1 * _speed * Time.deltaTime, 3.5f * _speed * Time.deltaTime, 0);
                return false;
            }
            else if (_facingDirection == "left" && !_isGrounded)
            {
                transform.Translate(-1 * _speed * Time.deltaTime, 3.5f * _speed * Time.deltaTime, 0);
                return false;
            }

            else
            {
                //animator.SetBool("isJump", false);
                _isMoving = false;
                _delay = 0;
                return true;
            }
        }

        return false;
    }

    public bool Attack()
    {
        if(monster != null)
        {
            monster.GetComponent<WalkingMonster>().TakeDamage();
        }
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Finish_Fight"))
        {
            animator.SetBool("isAttack", false);
            return true;
        }

        return false;

    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Monster")
        {
            monster = other.gameObject;
            if (actionsList.Count > 2 && actionsList[1] == "Attack")
            {
                Attack();
            }
            else
            {
                monster.GetComponent<WalkingMonster>().Attack();
            }
        }
        else if(other.collider.tag == "Ground")
        {
            print("Coucou");
            _isGrounded = true;
        }
    }

    public void Death()
    {
        print("YOU DIED!!");
        animator.SetBool("isDied", true);
        isDead = true;
    }

    IEnumerator DelayShowText()
    {
        yield return new WaitForSeconds(4);
        panelBulle.SetActive(false);
    }

    public void SaySomething(string text)
    {
        panelBulle.SetActive(true);
        panelBulle.transform.GetChild(1).GetComponent<Text>().text = text;
        StartCoroutine(DelayShowText());
    }

    
                                                       ////////////////////             \\\\\\\\\\\\\\\\\\\
                                             //////////////////////////////   GETTERS   \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
    public int GetHealth()
    {
        return _health;
    }

                                                  ////////////////////             \\\\\\\\\\\\\\\\\\\
                                        //////////////////////////////   SETTERS   \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

    public void SetHealth(int damage)
    {
        _health -= damage;
    }
}
