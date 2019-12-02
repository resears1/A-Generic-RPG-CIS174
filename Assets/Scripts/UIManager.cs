using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Slider healthBar;
    public Text HPText;
    public PlayerHealthManager playerHealth;

    private PlayerStats playerStats;
    public Text XPText;
    public Text LvlText;
    public Slider XPBar;

    private static bool UIExist;

    // Start is called before the first frame update
    void Start()
    {
        if (!UIExist)
        {
            UIExist = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        playerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.maxValue = playerHealth.hpMax;
        healthBar.value = playerHealth.hpCurrent;
        XPBar.maxValue = playerStats.toNextLvl[playerStats.currentLvl];
        XPBar.value = playerStats.currentXP;
        HPText.text = "HP: " + playerHealth.hpCurrent + "/" + playerHealth.hpMax;
        XPText.text = "XP: " + playerStats.currentXP + "/" + playerStats.toNextLvl[playerStats.currentLvl];
        LvlText.text = "Level: " + playerStats.currentLvl;
    }
}
