using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeypadController : MonoBehaviour
{
    private string keypadText;
    private int keypadLength;

    private InteractionController connectedObject;
    private string connectedCode;

    [SerializeField]
    private Canvas keypadUI;
    [SerializeField]
    private TMPro.TextMeshProUGUI keypadTextField;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void showKeypad(StorageController connected)
    {
        keypadText = "";

        connectedObject = connected;
        connectedCode = connectedObject.getNumCode().ToString();

        for (int i = 0; i < connectedCode.Length; i++)
        {
            keypadText += "_";
        }

        keypadLength = 0;
        keypadTextField.text = keypadText;
        keypadTextField.color = Color.black;

        keypadUI.enabled = true;
    }

    public void hideKeypad()
    {
        keypadUI.enabled = false;
    }

    public void addEntry(string c)
    {
        if (keypadLength < keypadText.Length)
        {
            keypadText = replaceChar(keypadText, keypadLength, c);
            keypadLength++;
        }

        keypadTextField.text = keypadText;
        keypadTextField.color = Color.black;
    }

    public void removeEntry()
    {
        if (keypadLength > 0 )
        {
            keypadText = replaceChar(keypadText, keypadLength-1, "_");
            keypadLength--;
        }

        keypadTextField.text = keypadText;
        keypadTextField.color = Color.black;
    }

    public void submitEntry()
    {
        if (keypadLength == keypadText.Length)
        {
            if (connectedCode== keypadText)
            {
                keypadTextField.color = Color.green;
                connectedObject.passCorrectCode();

            }
            else
            {
                keypadTextField.color = Color.red;
            }
        }
    }

    private string replaceChar(string s, int index, string c)
    {
        string newString = "";

        for (int i = 0;  i < s.Length; i++)
        {
            if (i == index)
            {
                newString += c;
            }
            else
            {
                newString += s[i];
            }
        }

        return newString;
    }
}
