using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : InteractionController
{
    private InventoryController inventory;
    private KeypadController keypad;

    [SerializeField]
    private GameObject lockSprite;
    [SerializeField]
    private Sprite keypadLockImg;
    [SerializeField]
    private Sprite keyLockImg;

    private Color keyColor;

    private bool isLocked;
    private int keypadNum;
    private string lockType;



    private void Start()
    { 
        inventory = FindObjectOfType<InventoryController>();
        keypad = FindObjectOfType<KeypadController>();
    }

    public override void interact()
    {
        if (!isLocked)
        {
            this.gameObject.SetActive(false);
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
