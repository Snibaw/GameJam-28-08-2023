using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AscenseurBehaviour : MonoBehaviour
{
    public bool isOpen = false;
    public bool justTped = false;
    public Transform tpPosition;
    public AscenseurBehaviour otherDoor;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player" && !justTped)
        {
            if(!isOpen)
            {
                OpenDoors();
            }
            else
            {
                other.gameObject.transform.position = tpPosition.position;
                GameObject.Find("Player").transform.rotation = GameObject.Find("Player").transform.rotation * Quaternion.Euler(0,180,0);
                otherDoor.TpToThis();
                CloseDoors();
            }
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player")
        {
            if(isOpen)
            {
                CloseDoors();
            }
            justTped = false;
        }
    }

    public void OpenDoors()
    {
        if(isOpen) return;
        transform.GetChild(0).GetComponent<Animator>().SetTrigger("Open");
        isOpen = true;
    }
    public void CloseDoors()
    {
        if(!isOpen) return;
        transform.GetChild(0).GetComponent<Animator>().SetTrigger("Close");
        isOpen = false;
    }
    public void TpToThis()
    {
        justTped = true;
        OpenDoors();
    }
}
