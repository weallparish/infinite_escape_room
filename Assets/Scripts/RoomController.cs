using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField]
    private GameObject room1;
    [SerializeField]
    private GameObject room2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (room1.activeInHierarchy)
        {
            room1.SetActive(false);
            room2.SetActive(true);
        }
        else
        {
            room1.SetActive(true);
            room2.SetActive(false);
        }
    }
}
