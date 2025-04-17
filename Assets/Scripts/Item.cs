using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool readyToPickup = false;


    private void OnTriggerExit2D(Collider2D col)
    {
        if (!readyToPickup && col.gameObject.tag == "Player")
        {
            readyToPickup = true;
            Debug.Log("item ready for pickup");
        }
    }
}
