using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageController : InteractionController
{
    private InventoryController inventory;
    private KeypadController keypad;

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private GameObject lockSprite;
    [SerializeField]
    private Sprite keypadLockImg;
    [SerializeField]
    private Sprite keyLockImg;

    private GameObject itemPrefab;
    private string itemInfo;
    private int itemAmount;
    private Color itemColor;

    private Color keyColor;
    private int keypadNum;

    private bool containsItems;
    private string lockType;
    private bool isLocked;

    private void Start()
    {
        inventory = FindObjectOfType<InventoryController>();
        keypad = FindObjectOfType<KeypadController>();
    }

    public override void interact()
    {
        if (!isLocked)
        {
            animator.SetTrigger("OpenChest");

            if (containsItems)
            {
                bool itemAdded = false;

                foreach (GameObject item in inventory.getAllItems())
                {
                    if (item.GetComponent<SpriteRenderer>().color == itemColor && item.GetComponent<ItemController>().getInfo(false) == itemInfo)
                    {
                        item.GetComponent<ItemController>().addItem();
                        itemAdded = true;

                        break;
                    }
                }

                if (!itemAdded)
                {
                    GameObject newItem = Instantiate(itemPrefab, inventory.transform);
                    newItem.GetComponent<SpriteRenderer>().color = itemColor;
                    newItem.GetComponent<ItemController>().setInfo(itemInfo, itemAmount);

                    inventory.addItem(newItem.gameObject);
                    containsItems = false;
                }
            }
        }
        else if (inventory.getSelectedItem() != null && lockType == "key")
        {
            Color selectedColor = inventory.getSelectedItem().GetComponent<SpriteRenderer>().color;

            if (selectedColor == keyColor && inventory.getSelectedItem().GetComponent<ItemController>().hasAmount())
            {
                isLocked = false;
                lockSprite.SetActive(false);

                inventory.removeItem(inventory.getSelectedItem().gameObject);
            }
        }
        else if (lockType == "keypad num")
        {
            keypad.showKeypad(this);
        }
    }

    public override void passCorrectCode()
    {
        isLocked = false;
        lockSprite.SetActive(false);

        foreach (GameObject item in inventory.getAllItems()) 
        {
            if (item.GetComponent<ItemController>().getInfo(false) == keypadNum.ToString())
            {
                inventory.removeItem(item);
                break;
            }
        }

        keypad.hideKeypad();
    }

    public void setItem(GameObject item, Color color, int amount, string info = null)
    {
        itemPrefab = item;
        itemInfo = info;
        itemColor = color;
        itemAmount = amount;

        containsItems = true;
    }

    public void setLock(Color color)
    {
        isLocked = true;
        lockType = "key";
        keyColor = color;

        lockSprite.GetComponent<SpriteRenderer>().color = keyColor;
        lockSprite.GetComponent<SpriteRenderer>().sprite = keyLockImg;
    }

    public void setLock(int num)
    {
        isLocked = true;
        lockType = "keypad num";
        keypadNum = num;

        lockSprite.GetComponent<SpriteRenderer>().color = Color.white;
        lockSprite.GetComponent<SpriteRenderer>().sprite = keypadLockImg;
    }

    public override int getNumCode()
    {
        if (lockType == "keypad num")
        {
            return keypadNum;
        }
        else
        {
            return 0;
        }
    }
}
