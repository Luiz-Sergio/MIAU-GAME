using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerScript playerScript;
    void Start()
    {
        playerScript =  GameObject.FindObjectOfType<PlayerScript>();
        if (gameObject.name == "Eagle_Elite(Clone)")
        {
            transform.Rotate(0f, 180.0f, 0.0f, Space.Self);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "PLAYER")
        {
            playerScript.Die();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("name of obstacle "+gameObject.name);
        
        
        
    }
    private void FixedUpdate()
    {
        if (GameManager.isGameStarted)
        {
            if (gameObject.name == "Eagle_Elite(Clone)")
            {
                Vector3 temp = transform.position;
                //temp = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.11f);
                transform.position = temp;
            }
        }
    }
}
