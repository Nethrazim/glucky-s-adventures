using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public List<GameObject> objectsToTrigger;
    private bool hasBeenTriggered = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasBeenTriggered)
        {
            if (collision.gameObject.tag == "Player")
            {
                foreach (GameObject gameObj in objectsToTrigger)
                {
                    gameObj.GetComponent<FloatingIsland>().execute = true;
                }
            }
        }
        
    }
}

