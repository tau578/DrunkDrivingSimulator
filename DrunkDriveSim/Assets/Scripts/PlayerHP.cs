using UnityEngine;

public class PlayerHP : MonoBehaviour
{
public int hp;



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
}
