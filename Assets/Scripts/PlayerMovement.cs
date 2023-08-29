using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public bool canMove = true;
    public bool canShoot = false;
    public bool canHit = false;
    private Transform camTransform;
    private GameManager gameManager;

    [Header("Health")]
    public int maxHealth = 100;
    public Image healthBar;
    private int currentHealth = 100;

    [Header("Hit")]
    public AttackHitboxBehaviour hitbox;
    public GameObject bequille;
    public float hitReloadTime = 0.5f;
    private float hitReloadTimer = 0f;

    [Header("Bullet")]

    public float bulletReloadTime = 0.5f;
    private float bulletReloadTimer = 0f;
    public GameObject canon;
    public GameObject bulletPrefab;
    [Header("Player Movement")]
    public float speed = 5f;
    public float sensitivity = -1f;
    private Vector3 rotate;
    private SoundEffectManager soundEffectManager;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        bulletReloadTimer = bulletReloadTime;
        hitReloadTimer = hitReloadTime;
        bequille.SetActive(false);
        camTransform = Camera.main.transform;
        sensitivity = PlayerPrefs.GetFloat("Sensitivity", -1f);

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentHealth = maxHealth;
        healthBar.fillAmount = (float)currentHealth / (float)maxHealth;

        soundEffectManager = GameObject.Find("SoundEffectManager").GetComponent<SoundEffectManager>();
    }

    // Update is called once per frame
    void Update()
    {
        bulletReloadTimer -= Time.deltaTime;
        hitReloadTimer -= Time.deltaTime;

        RotateThePlayer();

        if(!canMove) return;
        
        MoveThePlayer();
        


        if(Input.GetMouseButton(0))
        {
            if(canShoot && bulletReloadTimer <= 0f)
                ShootBullet();
            else if(canHit && hitReloadTimer <= 0f)
                Attack();
        }

        //If player press E, draw a raycast to see if there is an item in front of him
        if(Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if(Physics.Raycast(camTransform.position, transform.forward, out hit, 5f))
            {
                Debug.Log(hit.collider.gameObject.name);
                if(hit.collider.gameObject.tag == "Item")
                {
                    hit.collider.gameObject.GetComponent<ItemBehaviour>().interact();
                }
            }
            
        }

    }
    void MoveThePlayer()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        float yRotation = transform.eulerAngles.y;
        Vector3 move = Quaternion.Euler(0f, yRotation, 0f) * new Vector3(x, 0f, z);

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? speed * 1.5f : speed;

        transform.Translate(move.normalized * currentSpeed * Time.deltaTime, Space.World);
    }
    void RotateThePlayer()
    {
        float y = Input.GetAxis("Mouse X");
        float x = Input.GetAxis("Mouse Y");
    
        Vector3 rotation = new Vector3(x, y*sensitivity, 0f);
    
        transform.eulerAngles = transform.eulerAngles - rotation;

        //Rotation max in x is 70 and -70
        float currentX = transform.eulerAngles.x;
        if(currentX > 180f)
        {
            currentX -= 360f;
        }
        currentX = Mathf.Clamp(currentX, -70f, 70f);
        transform.eulerAngles = new Vector3(currentX, transform.eulerAngles.y, transform.eulerAngles.z);
        
    }
    void ShootBullet()
    {
        bulletReloadTimer = bulletReloadTime;
        soundEffectManager.PlaySoundEffect(1);
        GameObject bullet = Instantiate(bulletPrefab, canon.transform.position, transform.rotation * Quaternion.Euler(90, 0, 0));
        Destroy(bullet, 3f);
    }
    void Attack()
    {
        hitReloadTimer = hitReloadTime;
        soundEffectManager.PlaySoundEffect(0);
        bequille.GetComponent<Animator>().SetTrigger("Attack");
        hitbox.HitEnemiesInArea();
    }
    public void takeBequille()
    {
        soundEffectManager.PlaySoundEffect(2);
        bequille.SetActive(true);
        canMove = false;
        StartCoroutine(gameManager.GetDrunk());
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.fillAmount = (float)currentHealth / (float)maxHealth;
        if(currentHealth <= 0)
        {
            gameManager.GameOver();
        }
    }
    public IEnumerator FromSwordToPistol()
    {
        canHit=false;
        bequille.GetComponent<Animator>().SetTrigger("FromSwordToPistol");
        yield return new WaitForSeconds(4.5f);
        canShoot = true;
        bequille.transform.position =new Vector3(0.54f,0.21f,0.947f);
        bequille.transform.rotation = Quaternion.Euler(-454.75f,7.74f,-8.3f);
    }
}
