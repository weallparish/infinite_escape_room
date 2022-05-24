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
        colorList = new List<Color>() { Color.red, Color.yellow, Color.green, Color.blue, Color.cyan, Color.magenta};

        Generate();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Generate()
    {
        puzzleAmount = 5;

        GameObject newStorage;
        StorageController storageInfo;

        Color lastLockColor = colorList[Random.Range(0, colorList.Count - 1)];
        colorList.Remove(lastLockColor);

        door.setLock(lastLockColor);

        for (int i = 0; i < puzzleAmount-1;  i++)
        {
            newStorage = Instantiate(storage,room.transform);
            newStorage.transform.position = new Vector3(0.5f*(3*i-5), 0.5f, 0);

            storageInfo = newStorage.GetComponent<StorageController>();

            storageInfo.setItem(key, lastLockColor);

            lastLockColor = colorList[Random.Range(0, colorList.Count - 1)];
            colorList.Remove(lastLockColor);

            storageInfo.setLock(lastLockColor);
        }

        newStorage = Instantiate(storage, room.transform);
        newStorage.transform.position = new Vector3(0.5f * (3 * (puzzleAmount -1) - 5), 0.5f, 0);

        storageInfo = newStorage.GetComponent<StorageController>();

        storageInfo.setItem(key, lastLockColor);
        //storageInfo.setItem(paper, Color.white, "'Code: 99999'");

        storageInfo.setLock(99999);
    }

    private void ChooseLockType()
    {
        int randomNum = Random.Range(0, 1);
    }
}
