using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int puzzleAmount;

    private List<Color> colorList;

    [SerializeField]
    private DoorController door;
    [SerializeField]
    private GameObject room;

    [SerializeField]
    private GameObject storage;
    [SerializeField]
    private GameObject key;
    [SerializeField]
    private GameObject paper;

    // Start is called before the first frame update
    void Start()
    {
        colorList = new List<Color>() { Color.red, Color.yellow, Color.green, Color.blue, Color.cyan, Color.magenta };
        setupCustomColors();

        Generate();
    }

    private void setupCustomColors()
    {
        Color orange = new Color32(224, 121, 31, 255);
        colorList.Add(orange);

        Color burntOrange = new Color32(224, 64, 31, 255);
        colorList.Add(burntOrange);

        Color teal = new Color32(31, 224, 131, 255);
        colorList.Add(teal);

        Color skyBlue = new Color32(31, 186, 224, 255);
        colorList.Add(skyBlue);

        Color purple = new Color32(131, 31, 224, 255);
        colorList.Add(purple);

        Color fuschia = new Color32(224, 31, 93, 255);
        colorList.Add(fuschia);
    }

    private void Generate()
    {

        //Info from the last lock generated
        int lastLockType;
        Color lastLockColor = Color.clear;
        int lastLockNumber = 0;
        int lastLockAmount = 1;

        int randInt = 0;

        //Generate a random lock type (0 = key, 1 = keypad num)
        lastLockType = Random.Range(0, 2);

        //If the lock type is key
        if (lastLockType == 0)
        {
            //Pick a random color from the color list
            lastLockColor = colorList[Random.Range(0, colorList.Count - 1)];
            colorList.Remove(lastLockColor);

            //Set the door's lock to the picked color
            door.setLock(lastLockColor);
        }
        //If the lock type is a keypad num
        else if (lastLockType == 1)
        {
            randInt = Random.Range(3, 6);

            if (randInt == 3)
            {
                //Pick a random number for the keypad code
                lastLockNumber = Random.Range(100, 999);
            }
            else if (randInt == 4)
            {
                //Pick a random number for the keypad code
                lastLockNumber = Random.Range(1000, 9999);
            }
            else if (randInt == 5)
            {
                //Pick a random number for the keypad code
                lastLockNumber = Random.Range(10000, 99999);
            }
            else if (randInt == 6)
            {
                //Pick a random number for the keypad code
                lastLockNumber = Random.Range(100000, 999999);
            }

            //Set the door's lock to the picked number
            door.setLock(lastLockNumber);
        }

        //Decide whether the last lock has 1 or 3 pieces
        randInt = Random.Range(0, 4);
        if (randInt < 2)
        {
            lastLockAmount = 1;
        }
        else
        {
            lastLockAmount = 3;
        }

        for (int i = 0; i < lastLockAmount; i++)
        {
            CreateStorage(lastLockType, lastLockColor, lastLockNumber, lastLockAmount, Random.Range(1,6), 12, 2f*i - 3);
        }
    }

    private void CreateStorage(int lastLockType, Color lastLockColor, int lastLockNumber, int lastLockAmount, int branchLength, float xPos, float yPos)
    {
        if (branchLength > 1)
        {
            //Create a new storage to hold the item that leads to the following storage/door
            GameObject newStorage = Instantiate(storage, room.transform);

            //(TEMPORARY) - Place the storage to the left of the previous storage
            newStorage.transform.position = new Vector3(xPos, yPos, 0);

            //Get the storagecontroller from the new storage
            StorageController storageInfo = newStorage.GetComponent<StorageController>();

            //If the lock type of the following puzzle is a key
            if (lastLockType == 0)
            {
                //Place the key inside the storage
                storageInfo.setItem(key, lastLockColor, lastLockAmount);
            }
            //If the lock type of the following puzzle is a keypad num
            else if (lastLockType == 1)
            {
                //Place a paper scrap with the code written on it inside the storage
                storageInfo.setItem(paper, Color.white, lastLockAmount, lastLockNumber.ToString());
            }

            //
            // CREATE LOCK FOR STORAGE
            //

            //Generate a random lock type (0 = key, 1 = keypad num)
            lastLockType = Random.Range(0, 2);

            int randInt = 5;

            //If the lock type is key
            if (lastLockType == 0)
            {
                //Pick a random color from the color list
                lastLockColor = colorList[Random.Range(0, colorList.Count - 1)];
                colorList.Remove(lastLockColor);

                //Set the storage's lock to the picked color
                storageInfo.setLock(lastLockColor);
            }
            //If the lock type is a keypad num
            else if (lastLockType == 1)
            {
                randInt = Random.Range(3, 6);

                if (randInt == 3)
                {
                    //Pick a random number for the keypad code
                    lastLockNumber = Random.Range(100, 999);
                }
                else if (randInt == 4)
                {
                    //Pick a random number for the keypad code
                    lastLockNumber = Random.Range(1000, 9999);
                }
                else if (randInt == 5)
                {
                    //Pick a random number for the keypad code
                    lastLockNumber = Random.Range(10000, 99999);
                }

                //Set the storage's lock to the picked number
                storageInfo.setLock(lastLockNumber);
            }

            lastLockAmount = 1;

            CreateStorage(lastLockType, lastLockColor, lastLockNumber, lastLockAmount, branchLength - 1, xPos-2, yPos);
        }
        else
        {
            //Create starting storage
            GameObject newStorage = Instantiate(storage, room.transform);
            //(TEMPORARY) - Place the storage to the right of the previous storage
            newStorage.transform.position = new Vector3(xPos, yPos, 0);

            //Get storagecontroller component of storage
            StorageController storageInfo = newStorage.GetComponent<StorageController>();

            //If the lock type of the following puzzle is a key
            if (lastLockType == 0)
            {
                //Place the key inside the storage
                storageInfo.setItem(key, lastLockColor, lastLockAmount);
            }
            //If the lock type of the following puzzle is a keypad num
            else if (lastLockType == 1)
            {
                //Place a paper scrap with the code written on it inside the storage
                storageInfo.setItem(paper, Color.white, lastLockAmount, lastLockNumber.ToString());
            }

            }
        }
    
}
