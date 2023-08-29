using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfIntro : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject blockNote;
    public Transform blockNoteSpawn;
    public GameObject Medecin;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.OpenCloseDoor(0);
        Instantiate(blockNote, blockNoteSpawn.position, Quaternion.identity);
        GameObject.Find("Player").GetComponent<PlayerMovement>().canMove = true;
        Destroy(blockNoteSpawn.gameObject);
        Medecin.SetActive(false);
    }
}
