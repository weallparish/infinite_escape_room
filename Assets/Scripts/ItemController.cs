using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{

    private string storedInfo;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setInfo(string text)
    {
        storedInfo = text;
    }

    public string getInfo()
    {
        if (storedInfo != null)
        {
            print(storedInfo);
            return storedInfo;
        }
        else
        {
            return null;
        }
    }
}
