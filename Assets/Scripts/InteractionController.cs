using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void interact()
    {
        print("Interact");
    }

    public virtual int getNumCode()
    {
        return 0;
    }

    public virtual void passCorrectCode()
    {

    }
}
