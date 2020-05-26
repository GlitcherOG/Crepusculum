using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEditorInternal;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public PlayerBattleController battle;
    public bool Debug;
    public float Health = 100;
    public float maxHealth = 100f;
    public float Stamina = 100;
    public float maxStamina = 100f;
    public float itemsHeld;
    public float maxItems = 5f;
    public Animator player;
    public GameObject mainCamera;
    Collider2D collisons;
    bool move = true;
    float Timer;
    int battleCooldown;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Start()
    {
        collisons = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (battle.inBattle == false)
        {
            if (Input.GetKey("w"))
            {
                movement(0);
            }
            if (Input.GetKey("d"))
            {
                movement(1);
            }
            if (Input.GetKey("s"))
            {
                movement(2);
            }
            if (Input.GetKey("a"))
            {
                movement(3);
            }
            if (Timer >= 0)
            {
                Timer -= Time.deltaTime;
            }
        }
    }

    public void Check()
    {
        int roll = Random.Range(1, 10);
        if (battleCooldown <= 0 && Debug)
        {
            if (roll == 1)
            {
                battleCooldown = 3;
                battle.inBattle = true;
            }
        }
        else
        {
            battleCooldown -= 1;
        }
    }

    public void movement(int Movement) // 0 up, 1 right, 2 down, 3 left
    {
        if (move && Timer <= 0)
        {
            collisons.enabled = false;
            Timer = 0.4f;
            Check();
            RaycastHit2D hit;
            //AnimationReset();
            //player.SetTrigger("Walking");
            switch (Movement)
            {
                case 0:
                    hit = Physics2D.Raycast(transform.position, Vector2.up);
                    if ((hit.distance > 1 || hit.transform.tag == "ScreenBounds" || hit.transform.GetComponent<Collider2D>().isTrigger) || hit == false)
                    {
                        //player.SetBool("WalkingDown", true);
                        gameObject.transform.position += new Vector3(0, 1, 0);
                    }
                    break;
                case 1:
                    hit = Physics2D.Raycast(transform.position, Vector2.right);
                    if ((hit.distance > 1 || hit.transform.tag == "ScreenBounds" || hit.transform.GetComponent<Collider2D>().isTrigger) || hit == false)
                    {
                        //player.SetBool("WalkingRight", true);
                        gameObject.transform.position += new Vector3(1, 0, 0);
                    }
                    break;
                case 2:
                    hit = Physics2D.Raycast(transform.position, Vector2.down);
                    if ((hit.distance > 1 || hit.transform.tag == "ScreenBounds" || hit.transform.GetComponent<Collider2D>().isTrigger) || hit == false)
                    {
                        //player.SetBool("WalkingUp", true);
                        gameObject.transform.position += new Vector3(0, -1, 0);
                    }
                    break;
                case 3:
                    hit = Physics2D.Raycast(transform.position, Vector2.left);
                    if ((hit.distance > 1 || hit.transform.tag == "ScreenBounds" || hit.transform.GetComponent<Collider2D>().isTrigger) || hit == false)
                    {
                        //player.SetBool("WalkingLeft", true);
                        gameObject.transform.position += new Vector3(-1, 0, 0);
                    }
                    break;
                default:
                    hit = Physics2D.Raycast(transform.position, Vector2.up);
                    break;
            }
            if (hit.distance <= 1 && hit.transform.tag == "ScreenBounds")
            {
                MoveCamera(Movement);
            }
            collisons.enabled = true;
        }
    }
    private void MoveCamera(int Movement)
    {
        switch (Movement)
        {
            case 0:
                mainCamera.transform.position += new Vector3(0, 11);
                break;
            case 1:
                mainCamera.transform.position += new Vector3(19, 0);
                break;
            case 2:
                mainCamera.transform.position += new Vector3(0, -11);
                break;
            case 3:
                mainCamera.transform.position += new Vector3(-19, 0);
                break;
            default:

                break;
        }
    }
    void AnimationReset()
        {
            player.SetBool("WalkingUp", false);
            player.SetBool("WalkingDown", false);
            player.SetBool("WalkingLeft", false);
            player.SetBool("WalkingRight", false);
        }
    }
