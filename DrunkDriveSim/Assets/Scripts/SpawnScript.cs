using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
public class SpawnScript : MonoBehaviour
{
[SerializeField] public static int obstacleCount = 0;
public static int maxObstacle = 20;
public static float playerSpeed = 4;
public List<GameObject> obstacles;
public TextMeshProUGUI score;
public float points;
    void Awake()
    {
        maxObstacle = 20;
        playerSpeed = 4;
        obstacleCount = 0;
        StartCoroutine(Suicide(Random.Range(1f, 1.5f)));
    }
    void Update()
    {
        points += Time.deltaTime;   
        score.text = string.Format("{00}", points);
        if(playerSpeed < 10)
        {
            playerSpeed += 0.01f;
        }
    }

     IEnumerator Suicide(float delay)
    {
        
        yield return new WaitForSeconds(delay);
        if(obstacleCount < maxObstacle)
        {
            int currentPlanetInt = Random.Range(0, obstacles.Count);
            GameObject currentObstacle = obstacles[currentPlanetInt];
            var position = new Vector3(-8, 1, Random.Range(-4, 2));
            Instantiate(currentObstacle, position, transform.rotation);
            obstacleCount += 1;
            StartCoroutine(Suicide(Random.Range(1f, 5f)));
        }
    }
}
