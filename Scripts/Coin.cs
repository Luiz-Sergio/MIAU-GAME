using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    public Transform playerTransform;
    public float speed = 90f;
    private int time = 0;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<ObstacleScript>() != null)
        {
            Destroy(gameObject);
            return;
        }

        if(other.gameObject.name != "PLAYER")
        {
            
            return;
        }

        GameManager.Instance.ChangeScore();
        

        Destroy(gameObject);
        GameManager.Instance.playStarSound();
    }
    // Start is called before the first frame update
    void Start()
    {
       playerTransform = GetComponentInParent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.isGameStarted) {
            transform.Rotate(0, 0, speed * Time.deltaTime);
            Vector3 temp = transform.position;
            temp = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.001f);
            transform.position = temp;
        }
        
    }


    private void FixedUpdate()
    {
        
        if(playerTransform.position.z - transform.position.z > 20)
        {
            
            Destroy(gameObject);
        }
        
    }
}
