using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitboxBehaviour : MonoBehaviour
{
    public float knockBackStrength = 10f;
    private Collider[] enemiesColliders = new Collider[10];

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Enemy")
        {
            for(int i = 0; i < enemiesColliders.Length; i++)
            {
                if(enemiesColliders[i] == null)
                {
                    enemiesColliders[i] = other;
                    break;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Enemy")
        {
            for(int i = 0; i < enemiesColliders.Length; i++)
            {
                if(enemiesColliders[i] == other)
                {
                    enemiesColliders[i] = null;
                    break;
                }
            }
        }
    }

    public void HitEnemiesInArea()
    {
        foreach(Collider collider in enemiesColliders)
        {
            if(collider == null) continue;
            collider.gameObject.GetComponent<EnemyBehaviour>().TakeDamage(1);
            KnockBack(collider.gameObject);
        }
    }
    private void KnockBack(GameObject enemy)
    {
        Vector3 direction = enemy.transform.position - GameObject.Find("Player").transform.position + new Vector3(0,0.3f,0);
        direction.Normalize();
        enemy.GetComponent<Rigidbody>().AddForce(direction * knockBackStrength, ForceMode.Impulse);
    }
}
