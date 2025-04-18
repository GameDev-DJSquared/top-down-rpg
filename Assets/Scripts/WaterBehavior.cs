using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBehavior : MonoBehaviour
{
    private WaterManager waterMan;
    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        waterMan = FindObjectOfType<WaterManager>();
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.tag == "Player" && other.transform.position.x < 0)
        {
            if(waterMan.currentWater < waterMan.maxWater)
            {
                playerController.notifySprite.SetActive(true);

            }
            waterMan.isCollecting = true;
            waterMan.isTouching = true;
        }
        else if (other.collider.tag == "Player" && other.transform.position.x > 0)
        {
            if(waterMan.currentWater > 0)
            {
                playerController.notifySprite.SetActive(true);

            }

            waterMan.isWatering = true;
            waterMan.isTouching = true;
            
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.tag == "Player" && other.transform.position.x < 0)
        {
            waterMan.isCollecting = false;
            waterMan.isTouching = false;
            waterMan.waitToUpdate = 1;
            playerController.notifySprite.SetActive(false);

        }
        else if(other.collider.tag == "Player" && other.transform.position.x > 0)
        {
            waterMan.isWatering = true;
            waterMan.isTouching = false;
            waterMan.waitToUpdate = 1;
            playerController.notifySprite.SetActive(false);

        }
    }
}
