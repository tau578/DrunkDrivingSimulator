using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Trigger"))
        {
            SpawnScript.obstacleCount -= 1;
            Destroy(this.gameObject);
        }
    }
}
