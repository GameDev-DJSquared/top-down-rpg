using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private HealthManager healthMan;
    public Slider healthBar;
    public TextMeshProUGUI hpText;

    public WeaponManager weaponMan;
    public Slider weaponBar;
    public TextMeshProUGUI weaponText;

    private WaterManager waterMan;
    public Slider waterBar;
    public TextMeshProUGUI waterText;
    // Start is called before the first frame update
    void Start()
    {
        healthMan = FindObjectOfType<HealthManager>();
        waterMan = FindObjectOfType<WaterManager>();
        weaponMan = FindObjectOfType<WeaponManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update");
        healthBar.maxValue = healthMan.maxHealth;
        healthBar.value = healthMan.currentHealth;
        hpText.text = "HP: " + healthMan.currentHealth + "/" + healthMan.maxHealth;

        waterBar.maxValue = waterMan.maxWater;
        waterBar.value = waterMan.currentWater;
        Debug.Log("Bar Value: " + waterBar.value);

        weaponBar.maxValue = weaponMan.maxLevel;
        weaponBar.value = weaponMan.currentLevel;
        weaponText.text = "Weapon Level: " + weaponMan.currentLevel;
    }
}
