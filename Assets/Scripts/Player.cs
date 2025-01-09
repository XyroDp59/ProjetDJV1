using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent (typeof(HealthSystem))]
public class Player : MonoBehaviour
{
    public static Transform playerHitBox;

    [SerializeField] float initSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] Animator anim;

    float spin = 0f;
    float speed;
    float vy;
    Vector3 dir;

    [SerializeField] private HealthSystem healthSystem;
    CharacterController controller;
    Collider col;
    Camera _mainCamera;
    [SerializeField] GameObject Mesh;


    [SerializeField] private HarmfulProjectile bullet;

    private void Awake()
    {
        playerHitBox = transform.GetChild(0);
    }


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController> ();
        _mainCamera = Camera.main;
        healthSystem.deathEvent.AddListener(Die);
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        dir = Vector3.zero;
        if (spin <= 0f)
        {
            if (Input.GetKey(KeyCode.W)) dir += transform.forward;

            if (Input.GetKey(KeyCode.A)) dir -= transform.right;

            if (Input.GetKey(KeyCode.D)) dir += transform.right;

            if (Input.GetKey(KeyCode.S)) dir -= transform.forward;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 2f * initSpeed;
            }
            else speed = initSpeed;

            if (Input.GetKey(KeyCode.Space) && controller.isGrounded)
            {
                vy = jumpSpeed;
            }
            else if (!controller.isGrounded)
            {
                vy -= 9.81f * Time.deltaTime;
            }

            

            if (Input.GetMouseButtonDown(0))
            {
                Shoot(bullet);
            }

            //Start spinning
            if (Input.GetMouseButtonDown(1))
            {
                spin = 0.5f;
                anim.SetTrigger("Spin");
                col.enabled = false;
            }
            col.enabled = true;
        }
        else
        {
            // spinning
            vy = 0f;
            dir = new Vector3(transform.forward.x,0, transform.forward.z);
            speed = 1 * initSpeed;
        }
        controller.Move((dir + vy * Vector3.up) * speed * Time.deltaTime);
        if (spin > -2f) spin -= Time.deltaTime;
    }

    private void Shoot(HarmfulProjectile bullet)
    {
        Vector3 bulletPos = bullet.transform.position;

        HarmfulProjectile b = Instantiate(bullet, bulletPos, bullet.transform.rotation);

        RaycastHit hit;
        var ray = _mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2,Screen.height /2));
        Vector3 dir;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 objectHit = hit.point;

            dir = objectHit - bulletPos;
        } else
        {
            dir = transform.forward;
        }

        b.direction = dir.normalized;
        b.gameObject.SetActive(true);

    }

    public void Die()
    {
        HUD.singleton.GameOver("YOU DIED");
        Mesh.SetActive(false);
    }
}
