using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempoAscenseur : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.transform.position = new Vector3(-8.44f,1.23f,-12.33f);
        }
    }
}
