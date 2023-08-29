using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public string itemType;

    private GameManager gameManager;
    private void Start() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void interact()
    {
        if(itemType == "Bequille")
        {
            GameObject.Find("Player").GetComponent<PlayerMovement>().takeBequille();
            Destroy(gameObject);
        }
        else if(itemType == "Ascenseur")
        {
            transform.parent.GetComponent<AscenseurBehaviour>().OpenDoors();
        }
        else if(itemType == "Pilule")
        {
            gameManager.PickUpPil();
            Destroy(gameObject);
        }
        else if(itemType == "Gobelet")
        {
           gameManager.StartEndGame();
            Destroy(gameObject);
        }
        else if(itemType == "BlocNote")
        {
            gameManager.PickUpBlocNote();
            Destroy(gameObject);
        }
    }
}
