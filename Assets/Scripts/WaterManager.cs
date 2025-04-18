using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterManager : MonoBehaviour
{
    public bool isCollecting;
    public bool isWatering;
    public bool isTouching;
    public int currentWater = 0;
    public int maxWater = 100;
    public float waitToUpdate = 1f;
    private int waterToChange = 10;
    [SerializeField]
    private int waterPercentage = 0;

    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCollecting && isTouching)
        {
            waitToUpdate -= Time.deltaTime;
            if (waitToUpdate <= 0){
                AddWater(waterToChange);
                waitToUpdate = 1f;
            }
        }
        else if (isWatering && isTouching)
        {
            waitToUpdate -= Time.deltaTime;
            if (waitToUpdate <= 0){
                RemoveWater(waterToChange);
                waitToUpdate = 1f;
            }
        }

        //Debug.Log("Collecting " + isCollecting);
        //Debug.Log("Touching " + isTouching);
        //Debug.Log("Water: " + currentWater);
    }

    public void AddWater(int waterToChange)
    {
        if (currentWater < maxWater && playerController.InventorySpaceLeft() > 0)
        {
            currentWater += waterToChange;
        }
    }

    public void RemoveWater(int waterToChange)
    {
        if (currentWater > 0)
        {
            currentWater -= waterToChange;
            waterPercentage += waterToChange;
        }
        if (waterPercentage == 100)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.collider.tag == "Player") 
    //     {
    //     }
    // }

    // private void OnCollisionStay2D(Collision2D other)
    // {
    //     if (other.collider.tag == "Player")
    //     {
    //         isTouching = true;
    //     }
    // }

    // private void OnCollisionExit2D(Collision2D other)
    // {
    //     if (other.collider.tag == "Player")
    //     {
    //         isTouching = false;
    //         waitToUpdate = 1;
    //     }
    // }
}
