using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public int health = 2;
    private GameObject player;
    private NavMeshAgent agent;
    public int damage = 1;
    public float distanceToAttack = 2f;
    public float attackCooldown = 1f;
    private float attackTimer = 0f;
    private GameManager gameManager;
    public float speed = 1f;
    private bool isDead = false;
    public bool isAttacking = false;
    private Animator animator;
    private SoundEffectManager soundEffectManager;
    public bool canAttack = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        // agent = GetComponent<NavMeshAgent>();
        attackTimer = attackCooldown;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        animator = GetComponent<Animator>();
        soundEffectManager = GameObject.Find("SoundEffectManager").GetComponent<SoundEffectManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // agent.SetDestination(player.transform.position);
        if(isDead)
        {
            return;
        }
        attackTimer -= Time.deltaTime;

        LookAtPlayer();

        if(isAttacking) return;

        MoveTowardsPlayer();

        if(Vector3.Distance(transform.position, player.transform.position) < distanceToAttack)
        {
            if(attackTimer <= 0)
            {
                StartCoroutine(Attack());
            }
        }
    }
    public void TakeDamage(int damage)
    {
        if(isDead) return;
        health -= damage;
        if(health <= 0)
        {
            animator.applyRootMotion = true;
            isDead = true;
            animator.SetTrigger("Die");
            gameManager.EarnScorePoints(10);
            Destroy(gameObject,3f);
        }
    }
    void LookAtPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();
        transform.rotation = Quaternion.LookRotation(direction);
    }
    void MoveTowardsPlayer()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
    IEnumerator Attack()
    {
        if(!canAttack) yield break;
        isAttacking = true;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(1.75f);
        if(Vector3.Distance(transform.position, player.transform.position) < distanceToAttack && !isDead)
        {
            player.GetComponent<PlayerMovement>().TakeDamage(damage);
            soundEffectManager.PlaySoundEffect(5);
        }
            
        yield return new WaitForSeconds(0.25f);
        isAttacking = false;
        attackTimer = attackCooldown;
    }
}
