using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageController : InteractionController
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private GameObject itemPrefab;
    [SerializeField]
    private GameObject lockSprite;
    [SerializeField]
    private Color itemColor;
    [SerializeField]
    private Color keyColor;
    [SerializeField]
    private bool isLocked;

    private InventoryController inventory;

    private bool containsItems;

    private void Start()
    {
        inventory = FindObjectOfType<InventoryController>();
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

                inventory.addItem(newItem.gameObject);
                containsItems = false;
            }
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

    public void setItem(GameObject item, Color color)
    {
        itemPrefab = item;
        itemColor = color;

        containsItems = true;
    }

    public void setLock(Color color)
    {
        isLocked = true;
        keyColor = color;

        lockSprite.GetComponent<SpriteRenderer>().color = keyColor;
    }
}
