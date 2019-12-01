using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Slider healthBar;
    public Text HPText;
    public PlayerHealthManager playerHealth;

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
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.maxValue = playerHealth.hpMax;
        healthBar.value = playerHealth.hpCurrent;
        HPText.text = "HP: " + playerHealth.hpCurrent + "/" + playerHealth.hpMax;
    }
}
