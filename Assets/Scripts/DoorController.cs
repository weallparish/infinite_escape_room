using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : InteractionController
{
    [SerializeField]
    private GameObject lockSprite;
    [SerializeField]
    private Color keyColor;
    [SerializeField]
    private bool isLocked;

    private InventoryController inventory;


    private void Start()
    { 

        inventory = FindObjectOfType<InventoryController>();
    }

    public override void interact()
    {
        if (!isLocked)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            Color selectedColor = inventory.getSelectedItem().GetComponent<SpriteRenderer>().color;

            if (selectedColor == keyColor)
            {
                isLocked = false;
                lockSprite.SetActive(false);

                inventory.removeItem(inventory.getSelectedItem().gameObject);
            }
        }
    }

    public void setLock(Color color)
    {
        isLocked = true;
        keyColor = color;

        lockSprite.GetComponent<SpriteRenderer>().color = keyColor;
    }
}
