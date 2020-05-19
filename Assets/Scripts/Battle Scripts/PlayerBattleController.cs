using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBattleController : MonoBehaviour
{
    public PlayerController player;

    //canvas control
    public Canvas battleCanvas;
    public Canvas mainCanvas;
    public bool inBattle = false;
    public Image endBattlePanel;
    public Image victoryImage;
    public Image defeatImage;

    //display numeric changes
    public Image playerHealthImage;
    public Image enemyhealthImage;
    public Image playerStaminaImage;

    //enemy control
    public float enemySelect;
    public bool shouldSelectEnemy = false;
    public float enemyMaxHealth;
    public float enemyHealth = 1f;
    public float enemyMaxStamina;
    public float enemyStamina;

    public bool startOfBattle = false;

    void Start()
    {
        player.Stamina = 0f;
        defeatImage.gameObject.SetActive(false);
        victoryImage.gameObject.SetActive(false);
    }
    void Update()
    {
        playerHealthImage.fillAmount = player.Health / player.maxHealth;
        enemyhealthImage.fillAmount = enemyHealth / enemyMaxHealth;
        playerStaminaImage.fillAmount = player.Stamina / player.maxStamina;

        if(inBattle)
        {
            battleCanvas.gameObject.SetActive(true);
            mainCanvas.gameObject.SetActive(false);
            ActiveTime();
            if(enemySelect == 0)
            {
                shouldSelectEnemy = true;
                startOfBattle = true;
            }
            else
            {
                shouldSelectEnemy = false;
            }
        }
        else
        {
            enemySelect = 0f;
            battleCanvas.gameObject.SetActive(false);
            mainCanvas.gameObject.SetActive(true);
        }

        if (shouldSelectEnemy)
        {
            enemySelect = Random.Range(1, 5);
        }

        //determine which enemy loads
        switch (enemySelect)
        {
            case 1:
                enemyMaxHealth = 150f;
                enemyMaxStamina = 70f;
                break;
            case 2:
                enemyMaxHealth = 60f;
                enemyMaxStamina = 50f;
                break;
            case 3:
                enemyMaxHealth = 120f;
                enemyMaxStamina = 10f;
                break;
            case 4:
                enemyMaxHealth = 80f;
                enemyMaxStamina = 90f;
                break;
        }

        if(startOfBattle)
            {
            if(enemyHealth != enemyMaxHealth)
            {
                enemyHealth = enemyMaxHealth;
            }
            }

        if(inBattle && enemyHealth <=0 && !startOfBattle)
        {
            endBattlePanel.gameObject.SetActive(true);
            victoryImage.gameObject.SetActive(true);
        }
        else if(inBattle && player.Health <= 0)
        {
            endBattlePanel.gameObject.SetActive(true);
            defeatImage.gameObject.SetActive(true);
        }
        else
        {
            endBattlePanel.gameObject.SetActive(false);
            defeatImage.gameObject.SetActive(false);
            victoryImage.gameObject.SetActive(false);
        }
    }

    void ActiveTime()
    {
        if (player.Stamina < player.maxStamina)
        {
            player.Stamina = player.Stamina + 5 * Time.deltaTime;
        }

        if (enemyStamina < enemyMaxStamina)
        {
            enemyStamina = enemyStamina + 2 * Time.deltaTime;
            startOfBattle = false;
        }
        else if(enemyStamina >= enemyMaxStamina)
        {
            EnemyAttack();
        }
    }

    void EnemyAttack()
    {
        player.Health = player.Health - enemyStamina;
        enemyStamina = 0f;

    }

    public void onSilverButton()
    {
        if(player.Stamina >= player.maxStamina)
        {
            if(enemySelect == 1)
            {
                enemyHealth = enemyHealth - 100f;
                player.Stamina = 0f;
            }
            else
            {
                enemyHealth = enemyHealth - 20f;
                player.Stamina = 50f;
            }

        }
    }

   public void onEtherButton()
    {
        if (player.Stamina >= player.maxStamina)
        {
            if (enemySelect == 2)
            {
                enemyHealth = enemyHealth - 100f;
                player.Stamina = 0f;
            }
            else
            {
                enemyHealth = enemyHealth - 20f;
                player.Stamina = 50f;
            }


        }
    }
    public void onFireButton()
    {
        if (player.Stamina >= player.maxStamina)
        {
            if (enemySelect == 3)
            {
                enemyHealth = enemyHealth - 100f;
                player.Stamina = 0f;
            }
            else
            {
                enemyHealth = enemyHealth - 20f;
                player.Stamina = 50f;
            }

        }
    }
   public void onBlessedButton()
    {
        if (player.Stamina >= player.maxStamina)
        {
            if (enemySelect == 4)
            {
                enemyHealth = enemyHealth - 100f;
                player.Stamina = 0f;
            }
            else
            {
                enemyHealth = enemyHealth - 20f;
                player.Stamina = 50f;
            }
        }
    }

    public void onFleeButton()
    {
        if(player.Stamina >= player.maxStamina * 0.75)
        {
            float attemptFlee = Random.Range(1, 5);

            if(attemptFlee == 1)
            {
                BattleReset();
            }
            else
            {
                return;
            }
        }

        player.Stamina = 0f;
    }

    public void BattleReset()
    {
        player.Stamina = 0f;
        enemyHealth = 0f;
        enemyStamina = 0f;
        enemySelect = 0f;
        inBattle = false;

    }
}
