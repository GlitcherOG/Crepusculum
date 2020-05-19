using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEditorInternal;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerBattleController battle;
    public float Health = 100;
    public float maxHealth = 100f;
    public float Stamina = 100;
    public float maxStamina = 100f;
    public Animator player;

    bool move = true;
    float Timer;
    int battleCooldown;
    private void Start()
    {

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
        if (battleCooldown == 0)
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
        if(move && Timer <= 0)
        {
            Timer = 0.4f;
            Check();
            RaycastHit2D hit;
            switch (Movement)
            {
                case 0:
                    hit = Physics2D.Raycast(transform.position, Vector2.up);
                    if (hit.distance > 1 || hit == false)
                    {
                        player.SetTrigger("WalkingDown");
                        gameObject.transform.position += new Vector3(0, 1, 0);
                    }
                    break;
                case 1:
                    hit = Physics2D.Raycast(transform.position, Vector2.right);
                    if (hit.distance > 1 || hit==false)
                    {
                        player.SetTrigger("WalkingRight");
                        gameObject.transform.position += new Vector3(1, 0, 0);
                    }
                    break;
                case 2:
                    hit = Physics2D.Raycast(transform.position, Vector2.down);
                    if (hit.distance > 1 || hit == false)
                    {
                        player.SetTrigger("Walking");
                        gameObject.transform.position += new Vector3(0, -1, 0);
                    }
                    break;
                case 3:
                    hit = Physics2D.Raycast(transform.position, Vector2.left);
                    if (hit.distance > 1 || hit == false)
                    {
                        player.SetTrigger("WalkingLeft");
                        gameObject.transform.position += new Vector3(-1, 0, 0);
                    }
                    break;
                default:
                    
                    break;
            }
        }
    }
}
