using UnityEngine;

public class SpawnScript : MonoBehaviour
{
public static int obstacleCount = 0;
public static int maxObstacle = 3;
public static int playerSpeed;
public GameObject obstacle;

    void Update()
    {
        if(obstacleCount < maxObstacle)
        {
            var position = new Vector3(-8, 0, Random.Range(-4, 2));
            Instantiate(obstacle, position, Quaternion.identity);
            obstacleCount += 1;
        }
    }
}
