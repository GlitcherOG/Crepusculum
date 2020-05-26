using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseDisplayHealth : MonoBehaviour
{
    public Image healthBar;
    public TMP_Text healthText;
    void Update()
    {
        healthBar.fillAmount = PlayerController.instance.Health / PlayerController.instance.maxHealth;
        healthText.text = PlayerController.instance.Health.ToString() + "/" + PlayerController.instance.maxHealth.ToString();
    }
}
