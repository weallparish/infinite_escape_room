using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private Canvas infoUI;
    [SerializeField]
    private TMPro.TextMeshProUGUI infoText;

    private List<GameObject> items;
    private int activeIndex;

    // Start is called before the first frame update
    void Start()
    {
        activeIndex = 0;

        items = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.x = pos.x - 0.1f;
        pos.y = pos.y - 0.1f;
        pos.z = 0;

        transform.position = Vector3.Lerp(transform.position, pos, 3f * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (activeIndex > 0)
            {
                activeIndex--;
            }
            else
            {
                activeIndex = items.Count - 1;
            }

            showSelectedItem();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (activeIndex < items.Count - 1)
            {
                activeIndex++;
            }
            else
            {
                activeIndex = 0;
            }

            showSelectedItem();
        }
    }

    private void showSelectedItem()
    {
        foreach (GameObject item in items)
        {
            if (items.IndexOf(item) == activeIndex)
            {
                item.SetActive(true);
            }
            else
            {
                item.SetActive(false);
            }
        }

        if (getSelectedItem() != null)
        {
            if (getSelectedItem().GetComponent<ItemController>().getInfo() == null)
            {
                infoUI.enabled = false;
            }
            else
            {
                infoText.text = getSelectedItem().GetComponent<ItemController>().getInfo();
                infoUI.enabled = true;
            }
        }
    }

    public void addItem(GameObject item)
    {

        items.Add(item);

        activeIndex = items.IndexOf(item);

        showSelectedItem();
    }

    public void removeItem(GameObject item)
    {
        items.Remove(item);
        Destroy(item.gameObject);

        if (activeIndex > 0)
        {
            activeIndex--;
        }
        else
        {
            activeIndex = items.Count - 1;
        }
    }

    public GameObject getSelectedItem()
    {
        if (items.Count > 0)
        {
            return items[activeIndex];
        }
        else
        {
            return null;
        }
    }
}
