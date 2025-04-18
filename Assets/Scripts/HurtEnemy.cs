using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    public int damageToGive;
    public WeaponManager weaponMan;

    // Start is called before the first frame update
    void Start()
    {
        weaponMan = FindObjectOfType<WeaponManager>();
        damageToGive = weaponMan.currentLevel;
    }

    // Update is called once per frame
    void Update()
    {
        damageToGive = weaponMan.currentLevel;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            EnemyHealthManager eHealthMan;
            eHealthMan = other.gameObject.GetComponent<EnemyHealthManager>();
            eHealthMan.HurtEnemy(damageToGive);
        }
    }
}
