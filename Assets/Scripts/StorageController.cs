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
                GameObject newItem = Instantiate(itemPrefab, inventory.transform);
                newItem.GetComponent<SpriteRenderer>().color = itemColor;
                newItem.GetComponent<ItemController>().setInfo(itemInfo);


                inventory.addItem(newItem.gameObject);
                containsItems = false;
            }
        }
        else if (inventory.getSelectedItem() != null && lockType == "key")
        {
            Color selectedColor = inventory.getSelectedItem().GetComponent<SpriteRenderer>().color;

            if (selectedColor == keyColor)
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

        keypad.hideKeypad();
    }

    public void setItem(GameObject item, Color color, string info = null)
    {
        itemPrefab = item;
        itemInfo = info;
        itemColor = color;

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
