using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundTile;
    Vector3 nextSpawnPosition;

    public void SpawnTile(bool spawnObsAndCoins)
    {
        GameObject temp = Instantiate(groundTile,nextSpawnPosition, Quaternion.identity,transform);
        nextSpawnPosition = temp.transform.GetChild(1).transform.position;
        nextSpawnPosition.z += 5;

        if(spawnObsAndCoins)
        {
            temp.GetComponent<GroundTile>().SpawnCoin();
            temp.GetComponent<GroundTile>().SpawnObstacle();
        }

    }

    void Start()
    {
        for(int i = 0; i < 15; i++)
        {
            if(i<2 )
            {
                SpawnTile(false);
            }
            else if(i%3==0)
            {
                SpawnTile(true);
            }
            else
            {
                SpawnTile(false);
            }
                
        }
        
    }

}
