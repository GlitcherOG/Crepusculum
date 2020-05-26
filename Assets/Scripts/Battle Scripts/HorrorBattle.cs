using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorrorBattle : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerBattleController theHorror = collision.GetComponentInParent<PlayerBattleController>();
        theHorror.enemySelect = 5;
        theHorror.inBattle = true;
        theHorror.startOfBattle = true;
        theHorror.enemyHealth = 320;
        theHorror.combatLogText.text = "The Horror. My final prize!";
    }

}
