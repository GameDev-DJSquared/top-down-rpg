using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Linq;

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

    public TextMeshProUGUI inventorySpaceText;

    public CanvasGroup inventoryGroup;
    public Button[] inventorySlots;
    public Image[] inventoryImages;
    public TextMeshProUGUI inventoryCapacity;
    public TextMeshProUGUI potionCount;
    public TextMeshProUGUI waterCount;

    public Sprite filledBucket;
    public Sprite emptyBucket;
    
    PlayerController playerController;

    public enum ItemType
    {
        Sword,
        Potion,
        Water
    }

    
    // Start is called before the first frame update
    void Start()
    {
        healthMan = FindObjectOfType<HealthManager>();
        waterMan = FindObjectOfType<WaterManager>();
        weaponMan = FindObjectOfType<WeaponManager>();
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Update");
        healthBar.maxValue = healthMan.maxHealth;
        healthBar.value = healthMan.currentHealth;
        hpText.text = "HP: " + healthMan.currentHealth + "/" + healthMan.maxHealth;

        waterBar.maxValue = waterMan.maxWater;
        waterBar.value = waterMan.currentWater;
        //Debug.Log("Bar Value: " + waterBar.value);

        weaponBar.maxValue = weaponMan.maxLevel;
        weaponBar.value = weaponMan.currentLevel;
        weaponText.text = "Weapon Level: " + weaponMan.currentLevel;


        inventorySpaceText.text = "Space Left: " + playerController.InventorySpaceLeft();

        if (IsInventoryOpen())
        {
            potionCount.text = playerController.potionCount.ToString();
            inventoryCapacity.text = playerController.InventorySpaceLeft().ToString();

            if (waterMan.currentWater > 0)
            {
                inventoryImages[2].sprite = filledBucket;
                waterCount.text = (waterMan.currentWater / 10).ToString();
            } else
            {
                inventoryImages[2].sprite = emptyBucket;

            }

            if(playerController.hasSword)
            {
                inventoryImages[0].gameObject.SetActive(true);
            } else
            {
                inventoryImages[0].gameObject.SetActive(false);

            }

            if(playerController.potionCount > 0)
            {
                inventoryImages[1].gameObject.SetActive(true);
                potionCount.text = playerController.potionCount.ToString();
            } else
            {
                inventoryImages[1].gameObject.SetActive(false);

            }
        }
        
    }


    public void ToggleInventory()
    {
        if(!inventoryGroup.gameObject.activeInHierarchy)
        {
            OpenInventory();
        }
        else
        {
            CloseInventory();
        }
    }

    public ItemType GetItemSelected()
    {
        GameObject itemSelected = EventSystem.current.currentSelectedGameObject;
        int index = inventorySlots.ToList().IndexOf(itemSelected.GetComponent<Button>());
        
        switch(index)
        {
            case 0:
                return ItemType.Sword;
            case 1:
                return ItemType.Potion;
            case 2:
                return ItemType.Water;

        }
        return ItemType.Water;
    } 

    public void OpenInventory()
    {


        inventoryGroup.gameObject.SetActive(true);
        inventoryGroup.alpha = 1.0f;
        EventSystem.current.SetSelectedGameObject(inventorySlots[1].gameObject);
        Time.timeScale = 0f;

    }

    public void CloseInventory()
    {
        inventoryGroup.gameObject.SetActive(false);
        Time.timeScale = 1f;

    }

    public bool IsInventoryOpen()
    {
        return inventoryGroup.gameObject.activeInHierarchy;
    }
}
