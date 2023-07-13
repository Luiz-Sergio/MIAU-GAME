using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource hitSound;
    [SerializeField] private AudioSource slideSound;

    private float playerSpeed;
    private float maxplayerSpeed=40;
    public float changeLaneSpeed;
    public float jumpSpeed;
    private bool isJumping = false;
    private bool isSliding = false;
    private float startOfJump;
    public float lengthOfJump = 10f;
    public float heightOfJump = 2;
    private int currentLane = 0;
    private Vector3 positionTargeted;
    private Animator animator;
    private float startOfSlide;
    public float LengthOfSlide;
    private BoxCollider boxCollider;
    private Vector3 boxColliderSize;
    private bool alive = true;
    public GameObject Panel;
    public GameObject PressAnyKeyPane;
    // Start is called before the first frame update
    void Start()
    {
        playerSpeed = GameManager.Instance.playerSpeed;
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        animator.Play("Cat_Idle2");
        boxCollider = GetComponent<BoxCollider>();
        boxColliderSize = boxCollider.size;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (GameManager.isGameStarted)
        {
            animator.SetBool("idleStart", false);
            PressAnyKeyPane.SetActive(false);
            if (alive)
            {

                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    changeLane(-3);
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    changeLane(3);
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("Hey");
                    jumpSound.Play();
                    Jump();
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    
                    Slide();
                }

                if (isJumping)
                {

                    float ratio = (transform.position.z - startOfJump) / lengthOfJump;

                    if (ratio >= 1f)
                    {
                        isJumping = false;
                        animator.SetBool("Jumping", false);
                    }
                    else
                    {
                        positionTargeted.y = Mathf.Sin(ratio * Mathf.PI) * heightOfJump;
                    }

                }
                else
                {
                    positionTargeted.y = Mathf.MoveTowards(positionTargeted.y, 0, 5 * Time.deltaTime);
                }

                if (isSliding)
                {
                    float ratio = (transform.position.z - startOfSlide) / LengthOfSlide;
                    if (ratio >= 1f)
                    {
                        isSliding = false;
                        animator.SetBool("Sliding", false);
                        boxCollider.size = boxColliderSize;
                    }
                }

                Vector3 newPosition = new Vector3(positionTargeted.x, positionTargeted.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, newPosition, changeLaneSpeed);
            }
        }
        else
        {
            animator.SetBool("idleStart", true);
        }
        
     
        
    }

    public void Jump()
    {
        if (!isJumping)
        {
            startOfJump = transform.position.z;
            animator.SetFloat("JumpSpeed", playerSpeed / lengthOfJump);
            animator.SetBool("Jumping",true);
            isJumping = true;
        }
    }

    public void Slide()
    {
        if(!isJumping && !isSliding)
        {
            slideSound.Play();
            startOfSlide = transform.position.z;
            animator.SetFloat("JumpSpeed", playerSpeed / LengthOfSlide);
            animator.SetBool("Sliding", true);
            Vector3 newSize = boxCollider.size;
            newSize.y = newSize.y / 2;
            boxCollider.size = newSize;
            isSliding = true;
        }
    }


    private void FixedUpdate()
    {
        if(GameManager.isGameStarted) {
            if(playerSpeed < maxplayerSpeed)
            {
                playerSpeed += 0.005f;
                
            }
            rb.velocity = Vector3.forward * playerSpeed;
        }
        Debug.Log("pspeed: "+playerSpeed);
    }

    public void Die()
    {
        hitSound.Play();
        alive = false;
        Panel.SetActive(true);
        float temp = playerSpeed;
        playerSpeed = 0;
        rb.isKinematic = true;
        animator.SetTrigger("Hit");
        animator.SetBool("Dead", true);
        GameManager.Instance.Menu();
        //Invoke("Restart", 1.5f);
        
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void changeLane(int direction)
    {
        int line = currentLane + direction;

        if(line >=-3 && line <= 3)
        {
            currentLane = line;
            positionTargeted = new Vector3(line,0,0);
        }

    }
}
