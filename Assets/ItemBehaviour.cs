using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public string itemType;
    public void interact()
    {
        if(itemType == "Bequille")
        {
            GameObject.Find("Player").GetComponent<PlayerMovement>().takeBequille();
            Destroy(gameObject);
        }
    }
}
