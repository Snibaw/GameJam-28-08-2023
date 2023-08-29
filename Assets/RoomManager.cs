using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public int enemyCount = 0;
    private bool allEnemiesKilled = false;
    private bool isDoorLocked = false;
    public int doorNumber;
    private GameManager gameManager;
    public GameObject spawnPoint;
    public GameObject EnemiesPrefab;
    private SoundEffectManager soundEffectManager;
    // Start is called before the first frame update
    void Start()
    {
        soundEffectManager = GameObject.Find("SoundEffectManager").GetComponent<SoundEffectManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player" && !isDoorLocked)
        {
            isDoorLocked = true;
            soundEffectManager.PlaySoundEffect(4);
            gameManager.OpenCloseDoor(doorNumber);
            SpawnEnemies();
        }
    }
    private void SpawnEnemies()
    {
        enemyCount = spawnPoint.transform.childCount;
        foreach(Transform spawn in spawnPoint.transform)
        {
            Instantiate(EnemiesPrefab, spawn.transform.position, Quaternion.identity);
        }
    }
}
