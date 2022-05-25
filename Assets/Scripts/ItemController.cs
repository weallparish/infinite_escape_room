using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private string storedInfo;
    private int amountCreated;
    private int amountStored;

    private List<string> hiddenChars;

    [SerializeField]
    private Sprite[] spriteList;

    // Start is called before the first frame update
    void Start()
    {
        amountStored = 1;

        hiddenChars = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
    }

    // Update is called once per frame
    void Update()
    {
        if (amountCreated == 1)
        {
            GetComponent<SpriteRenderer>().sprite = spriteList[spriteList.Length - 1];
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = spriteList[amountStored - 1];
        }
    }

    public void setInfo(string text, int num)
    {
        storedInfo = text;

        amountCreated = num;
    }

    public void addItem()
    {
        amountStored++;
    }

    public bool hasAmount()
    {
        return amountStored == amountCreated;
    }

    public string getInfo(bool forDisplay)
    {
        if (storedInfo != null)
        {
            if (hasAmount() || !forDisplay)
            {
                return storedInfo;
            }
            else
            {
                string n = "";

                for (int i = 0; i < amountStored; i++)
                {
                    n += storedInfo[i];
                }
                for ( int i = amountStored; i < storedInfo.Length; i++)
                {
                    n += hiddenChars[Random.Range(0,hiddenChars.Count - 1)];
                }

                return n;
            }
        }
        else
        {
            return null;
        }
    }
}
