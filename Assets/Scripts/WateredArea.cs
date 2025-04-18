using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WateredArea : MonoBehaviour
{
    public static WateredArea instance;
    Tilemap tilemap;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        tilemap = GetComponent<Tilemap>();
    }

    public void SetAlpha(float alpha)
    {
        tilemap.color = new Color(1, 1, 1, alpha);
    }
}
