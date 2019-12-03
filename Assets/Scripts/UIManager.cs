using Assets.Scripts;
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

    public Text newScore;
    public Text score1;
    public Text score2;
    public Text score3;
    public Text score4;
    public Text score5;
    public Text score6;
    public Text score7;
    public Text score8;
    public Text score9;
    public Text score10;

    public GameObject deathScreen;
    public static GameObject screenHolder;
    public static ArrayList scoreboard;

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
        scoreboard = new ArrayList();
        scoreboard.Add(score1);
        scoreboard.Add(score2);
        scoreboard.Add(score3);
        scoreboard.Add(score4);
        scoreboard.Add(score5);
        scoreboard.Add(score6);
        scoreboard.Add(score7);
        scoreboard.Add(score8);
        scoreboard.Add(score9);
        scoreboard.Add(score10);
        deathScreen.SetActive(false);
        screenHolder = deathScreen;
    }

    // Update is called once per frame
    void Update()
    {
        newScore.text = Score.score.ToString();
        healthBar.maxValue = playerHealth.hpMax;
        healthBar.value = playerHealth.hpCurrent;
        XPBar.maxValue = playerStats.toNextLvl[playerStats.currentLvl];
        XPBar.value = playerStats.currentXP;
        HPText.text = "HP: " + playerHealth.hpCurrent + "/" + playerHealth.hpMax;
        XPText.text = "XP: " + playerStats.currentXP + "/" + playerStats.toNextLvl[playerStats.currentLvl];
        LvlText.text = "Level: " + playerStats.currentLvl;
    }

    public static void CreateLeaderboard(ArrayList scores)
    {
        int i = 0;
        foreach(Text scoreBox in scoreboard)
        {
            scoreBox.text = scores[i].ToString();
            i++;
        }
        screenHolder.SetActive(true);
    }
}
