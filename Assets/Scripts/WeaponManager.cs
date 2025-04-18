using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public int currentLevel = 1;
    public int maxLevel = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseLevel()
    {
        if (currentLevel < maxLevel)
        {
            currentLevel += 1;
        }
    }
}
