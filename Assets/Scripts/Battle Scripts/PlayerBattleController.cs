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
    public Image enemySprite;
    public Button itemButton;
    public Text lootText;
    public Image gameEndPanel;

    //display numeric changes
    public Image playerHealthImage;
    public Image enemyhealthImage;
    public Image playerStaminaImage;
    public Text combatLogText;

    //enemy control
    public float enemySelect;
    public bool shouldSelectEnemy = false;
    public float enemyMaxHealth;
    public float enemyHealth = 1;
    public float enemyMaxStamina;
    public float enemyStamina;
    public Sprite vampireSprite;
    public Sprite spiritSprite;
    public Sprite zombieSprite;
    public Sprite werewolfSprite;

    public bool startOfBattle = false;
    public float lootChance = 0;

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

            if(player.itemsHeld > 0)
            {
                itemButton.gameObject.SetActive(true);
            }
            else
            {
                itemButton.gameObject.SetActive(false);
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
                enemyMaxHealth = 150;
                enemyMaxStamina = 70;
                break;
            case 2:
                enemyMaxHealth = 60;
                enemyMaxStamina = 50;
                break;
            case 3:
                enemyMaxHealth = 120;
                enemyMaxStamina = 10;
                break;
            case 4:
                enemyMaxHealth = 80;
                enemyMaxStamina = 90;
                break;
            case 5:
                enemyMaxHealth = 320;
                enemyMaxStamina = 40;
                break;
        }

        if(startOfBattle)
            {
            if(enemyHealth != enemyMaxHealth)
            {
                enemyHealth = enemyMaxHealth;
            }
            switch (enemySelect)
            {
                case 1:
                    combatLogText.text = "A beastly Werewolf approaches!";
                    enemySprite.sprite = werewolfSprite;
                    break;
                case 2:
                    combatLogText.text = "The Spirits are awoken!";
                    enemySprite.sprite = spiritSprite;
                    break;
                case 3:
                    combatLogText.text = "The Dead have come!";
                    enemySprite.sprite = zombieSprite;
                    break;
                case 4:
                    combatLogText.text = "Arrogant Vampire! You will not see another night!";
                    enemySprite.sprite = vampireSprite;
                    break;
                case 5:
                    combatLogText.text = "The Horror. My final prize!";
                    enemySprite.enabled = false;
                    break;
            }
            }

        if(inBattle && enemyHealth <=0 && !startOfBattle)
        {
            endBattlePanel.gameObject.SetActive(true);
            victoryImage.gameObject.SetActive(true);
            player.Stamina = 0;
            enemyStamina = 0;
            if (lootChance == 0)
            {
                lootChance = Random.Range(1, 3);
                if (lootChance == 1 && player.itemsHeld != player.maxItems)
                {
                    player.itemsHeld += 1f;
                    lootText.enabled = true;
                    lootChance = 8;
                }
            }
            if(enemySelect == 5)
            {
                gameEndPanel.enabled = true;
            }
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
            enemyStamina += 2 * Time.deltaTime;
            startOfBattle = false;
        }
        else if(enemyStamina >= enemyMaxStamina)
        {
            EnemyAttack();
        }
    }

    void EnemyAttack()
    {
        player.Health = player.Health - enemyMaxStamina;
        combatLogText.text = "The foe deals " + enemyMaxStamina + " damage!";
        enemyStamina = 0;
    }

    public void onSilverButton()
    {
        if(player.Stamina >= player.maxStamina)
        {
            if(enemySelect == 1 || enemySelect == 5)
            {
                enemyHealth = enemyHealth - 100;
                player.Stamina = 0f;
                combatLogText.text = "Silver scalds it's wicked flesh!";
            }
            else
            {
                enemyHealth = enemyHealth - 20;
                player.Stamina = 50f;
                combatLogText.text = "Perhaps not the most effective tool...";
            }

        }
        else if(player.Stamina < player.maxStamina)
        {
            combatLogText.text = "I must gather my strength!";
        }
    }

   public void onEtherButton()
    {
        if (player.Stamina >= player.maxStamina)
        {
            if (enemySelect == 2 || enemySelect == 5)
            {
                enemyHealth = enemyHealth - 100;
                player.Stamina = 0f;
                combatLogText.text = "Haunt this place no longer!";
            }
            else
            {
                enemyHealth = enemyHealth - 20;
                player.Stamina = 50f;
                combatLogText.text = "Perhaps not the most effective tool...";
            }


        }
        else if (player.Stamina < player.maxStamina)
        {
            combatLogText.text = "I must gather my strength!";
        }
    }
    public void onFireButton()
    {
        if (player.Stamina >= player.maxStamina)
        {
            if (enemySelect == 3 || enemySelect == 5)
            {
                enemyHealth = enemyHealth - 100;
                player.Stamina = 0f;
                combatLogText.text = "Be purged in righteous flames!";
            }
            else
            {
                enemyHealth = enemyHealth - 20;
                player.Stamina = 50f;
                combatLogText.text = "Perhaps not the most effective tool...";
            }

        }
        else if (player.Stamina < player.maxStamina)
        {
            combatLogText.text = "I must gather my strength!";
        }
    }
   public void onBlessedButton()
    {
        if (player.Stamina >= player.maxStamina)
        {
            if (enemySelect == 4 || enemySelect == 5)
            {
                enemyHealth = enemyHealth - 100;
                player.Stamina = 0f;
                combatLogText.text = "You will not corrupt this world!";
            }
            else
            {
                enemyHealth = enemyHealth - 20;
                player.Stamina = 50f;
                combatLogText.text = "Perhaps not the most effective tool...";
            }
        }
        else if (player.Stamina < player.maxStamina)
        {
            combatLogText.text = "I must gather my strength!";
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
                combatLogText.text = "I have escaped, for now.";
            }
            else if (attemptFlee != 1)
            {
                combatLogText.text = "The enemy has me trapped!";
                return;
            }
        }

        player.Stamina = 0f;
    }

    public void onItemButton()
    {
        if(player.itemsHeld > 0 && player.Health < player.maxHealth)
        {
            player.Health += 60;
            if(player.Health > player.maxHealth)
            {
                player.Health = player.maxHealth;
            }
            combatLogText.text = "Sure wish that tasted better...";
        }
        player.itemsHeld -= 1f;
    }

    public void BattleReset()
    {
        player.Stamina = 0f;
        enemyHealth = 0;
        enemyStamina = 0;
        enemySelect = 0f;
        combatLogText.text = "";
        lootChance = 0;
        inBattle = false;

        if(player.Health <= 0)
        {
            GameReset();
        }
    }

    public void GameReset()
    {
        player.Health = player.maxHealth;
        player.itemsHeld = 0;
        player.transform.position = new Vector3(0.5f, 0.5f, 0f);
    }
}
