using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool canShoot = false;
    public bool canHit = false;

    [Header("Hit")]
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
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        bulletReloadTimer = bulletReloadTime;
        hitReloadTimer = hitReloadTime;
        bequille.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        bulletReloadTimer -= Time.deltaTime;
        hitReloadTimer -= Time.deltaTime;

        MoveThePlayer();
        RotateThePlayer();


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
            if(Physics.Raycast(transform.position, transform.forward, out hit, 2f))
            {
                if(hit.collider.gameObject.tag == "Item")
                {
                    hit.collider.gameObject.GetComponent<ItemBehaviour>().interact();
                }
            }
        }

    }
    void MoveThePlayer()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
    
        Vector3 move = transform.right * x + transform.forward * z;

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? speed * 2 : speed;

        transform.position += move * currentSpeed * Time.deltaTime;
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
        GameObject bullet = Instantiate(bulletPrefab, canon.transform.position, transform.rotation * Quaternion.Euler(90, 0, 0));
        Destroy(bullet, 3f);
    }
    void Attack()
    {
        hitReloadTimer = hitReloadTime;
        bequille.GetComponent<Animator>().SetTrigger("Attack");
    }
    public void takeBequille()
    {
        bequille.SetActive(true);
        canHit = true;
    }
}
