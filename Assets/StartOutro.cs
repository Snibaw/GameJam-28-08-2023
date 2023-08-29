using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOutro : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().StartOutro();
    }
}
