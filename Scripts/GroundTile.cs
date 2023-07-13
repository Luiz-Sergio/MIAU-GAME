using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    GroundSpawner groundSpawner;
    public GameObject obstaclePrefab;
    public GameObject obstaclePrefab2;
    public GameObject obstaclePrefab3;
    public GameObject corruptStar;
    public GameObject coinPrefab;
    private static int counter = 0;

    bool spawnSlideObs = true;

    // Start is called before the first frame update
    void Start()
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
  
        //SpawnObstacle();
        //SpawnCoin();
    }

    public void SpawnCoin()
    {
        int[] hPos = { -3, 0, 3 };
        int idx = Random.Range(0, 3);
        int coinsToSpawn;
        for(int i = 0; i < 7 ; i++)
        {
            GameObject temp = Instantiate(coinPrefab, transform);
            temp.transform.position = GetRandomPointInCollider(GetComponent<Collider>(),idx,hPos);
        }
    }

    Vector3 GetRandomPointInCollider(Collider collider, int idx, int[] hPos)
    {
        //int[] hPos = { -3, 0, 3 };
        //int idx = Random.Range(0, 3);
        Vector3 point = new Vector3
            (
            hPos[idx],
            Random.Range(collider.bounds.min.y + 1, collider.bounds.max.y - 1),
            Random.Range(collider.bounds.min.z + 1, collider.bounds.max.z - 1)
            );
        if(point != collider.ClosestPoint(point))
        {
            point = GetRandomPointInCollider (collider, idx, hPos);
        }
        point.y = 0.5f;
        return point;

    }

    private void OnTriggerExit(Collider other)
    {
        counter++;
        Debug.Log("sai do tile");
        groundSpawner.SpawnTile(counter%3==0);
        GameManager.Instance.ChangeDistance();
        Debug.Log("DISTANCE::::::::"+ GameManager.Instance.Distance);
        Destroy(gameObject, 1);
    }


    public void SpawnObstacle()
    {
        bool[] ocupied = { false, false, false };
        bool[] ocupiedAir = { false, false, false };
        GameObject[] obsPreFabs = { obstaclePrefab, obstaclePrefab2, obstaclePrefab3,corruptStar };
        int obstacleIndex;
        int obstacleIndexAir;
        
        for (int i = 0; i < 3; i++)
        {
            do
            {
                obstacleIndex = Random.Range(2, 5);
                
            } while (ocupied[obstacleIndex-2]);

            

            Transform obstaclePoint = transform.GetChild(obstacleIndex).transform;


            if (Random.Range(0, 3) == 2 && i ==2)
            {
                Instantiate(obsPreFabs[1], obstaclePoint.position, Quaternion.identity, transform);
                
            }
            else
            {
                Instantiate(obsPreFabs[i], obstaclePoint.position, Quaternion.identity, transform);
                
            }
            

            ocupied[obstacleIndex-2] = true;
        }
        spawnSlideObs = !spawnSlideObs;
        obstacleIndexAir = Random.Range(7, 10);
        Transform obstaclePointAir = transform.GetChild(obstacleIndexAir).transform;
        
        Instantiate(obsPreFabs[3], obstaclePointAir.position, Quaternion.identity, transform);
    }
}
