using UnityEditor;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
public int hp = 4;
public GameObject glass1, glass2, glass3;


    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
        hp -= 1;
        SpawnScript.playerSpeed = 1;
        }
        if(other.CompareTag("Victim"))
        {
        SpawnScript.points += 20;
        }
    }

    void Update()
    {
        if(hp == 0)
        {
            Time.timeScale = 0;
        }
        if(hp == 3)
        {
            glass1.SetActive(true);
            glass2.SetActive(false);
            glass3.SetActive(false);
        }
        if(hp == 2)
        {
            glass1.SetActive(false);
            glass2.SetActive(true);
            glass3.SetActive(false);
        }
        if(hp == 1)
        {
            glass1.SetActive(false);
            glass2.SetActive(false);
            glass3.SetActive(true);
        }
    }
}
