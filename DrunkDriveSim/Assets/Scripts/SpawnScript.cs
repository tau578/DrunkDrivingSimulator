using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
public class SpawnScript : MonoBehaviour
{
    [SerializeField] public static int obstacleCount = 0;
    public static int maxObstacle = 20;
    public static float playerSpeed = 4;
    public List<GameObject> obstacles;
    public TextMeshProUGUI score;
    public static float points;

    public Renderer rend;

    void Awake()
    {
        maxObstacle = 20;
        playerSpeed = 4;
        obstacleCount = 0;
        points = 0;
        StartCoroutine(Suicide(Random.Range(1f, 1.5f)));
        rend = rend.GetComponent<Renderer>();
    }

    void Update()
    {
        points += Time.deltaTime;
        score.text = string.Format("{00}", points);
        if (playerSpeed < 10)
        {
            playerSpeed += 0.01f;
            float offset = SpawnScript.playerSpeed * 0.1f;
            rend.material.mainTextureOffset = new Vector2(-offset, 0f);
            if(offset <= -1f)
            {
                offset = 0;
            }
        }
    }

    IEnumerator Suicide(float delay)
    {

        yield return new WaitForSeconds(delay);
        if (obstacleCount < maxObstacle)
        {
            int currentPlanetInt = Random.Range(0, obstacles.Count);
            GameObject currentObstacle = obstacles[currentPlanetInt];
            var position = new Vector3(Random.Range(-7, 7), 0.3f, 70);
            Instantiate(currentObstacle, position, transform.rotation);
            obstacleCount += 1;
            StartCoroutine(Suicide(Random.Range(1f, 5f)));
        }
    }
}
