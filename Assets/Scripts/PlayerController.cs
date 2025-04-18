using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D myRB;
    private Animator myAnim;

    [SerializeField]
    private float speed;

    private float attackTime = 0.25f;
    private float attackCounter = 0.25f;
    private bool isAttacking;

    private UIManager uiMan;
    private WaterManager waterMan;
    private HealthManager healthMan;
    private bool canMove = true;


    public bool hasSword = false;
    public int potionCount = 0;
    public int inventoryCapacity = 10;
    public GameObject swordPrefab;
    public GameObject potionPrefab;

    public GameObject notifySprite;
    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        uiMan = FindObjectOfType<UIManager>();
        waterMan = FindObjectOfType<WaterManager>();
        healthMan = FindObjectOfType<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        


        if (canMove)
        {
            myRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed;
        } else
        {
            myRB.velocity = Vector2.zero;
        }


        myAnim.SetFloat("moveX", myRB.velocity.x);
        myAnim.SetFloat("moveY", myRB.velocity.y);

        if(Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1){
            myAnim.SetFloat("lastMoveX", Input.GetAxis("Horizontal"));
            myAnim.SetFloat("lastMoveY", Input.GetAxis("Vertical"));
        }

        if (isAttacking)
        {
            myRB.velocity = Vector2.zero;
            attackCounter -= Time.deltaTime;
            if (attackCounter <= 0)
            {
                myAnim.SetBool("isAttacking", false);
                isAttacking = false;
            }
        }

        if (!isAttacking && hasSword && (Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.Space)))
        {
            attackCounter = attackTime;
            myAnim.SetBool("isAttacking", true);
            isAttacking = true;
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {

            uiMan.ToggleInventory();

            if(uiMan.IsInventoryOpen())
            {
                canMove = false;
            } else
            {
                canMove = true;
            }
        }

        if(uiMan.IsInventoryOpen())
        {
            //Drop Item
            if ((Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.Space)))
            {
                UIManager.ItemType item = uiMan.GetItemSelected();
                switch(item)
                {
                    case UIManager.ItemType.Sword:
                        if(hasSword)
                        {
                            Instantiate(swordPrefab, transform.position, Quaternion.identity);
                            hasSword = false;
                            //uiMan.CloseInventory();
                            //canMove = true;
                        }
                        break;
                    case UIManager.ItemType.Potion:

                        if(potionCount > 0)
                        {
                            potionCount--;
                            Instantiate(potionPrefab, transform.position, Quaternion.identity);
                            //uiMan.CloseInventory();
                            //canMove = true;

                        }
                        break;
                    case UIManager.ItemType.Water:
                        waterMan.currentWater -= 10;
                        break;
                }
            }
        
            
            //Use Item
            if(Input.GetKeyDown(KeyCode.E))
            {
                UIManager.ItemType item = uiMan.GetItemSelected();
                if(item == UIManager.ItemType.Potion && healthMan.currentHealth < healthMan.maxHealth)
                {
                    healthMan.currentHealth += 20;
                    if(healthMan.currentHealth > healthMan.maxHealth)
                    {
                        healthMan.currentHealth = healthMan.maxHealth;
                    }
                    potionCount--;
                }
            }

        }
        

    }



    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.TryGetComponent<Item>(out Item i))
        {
            if(i.readyToPickup)
            {
                switch (col.gameObject.tag)
                {
                    case "Potion":
                        if (InventorySpaceLeft() > 0)
                        {
                            potionCount++;
                            col.gameObject.SetActive(false);
                        }
                        break;
                    case "Sword":
                        if (!hasSword && InventorySpaceLeft() > 0)
                        {
                            hasSword = true;
                            col.gameObject.SetActive(false);
                        }
                        break;
                }
            }
        }

        
        

    }

    public int InventorySpaceLeft()
    {
        return inventoryCapacity - (hasSword ? 1 : 0) - potionCount - (waterMan.currentWater / 10);
    }




}
