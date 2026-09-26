using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public Rigidbody rb;
    public float xspeed;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Trigger"))
        {
            SpawnScript.obstacleCount -= 1;
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
    rb.linearVelocity = transform.forward * SpawnScript.playerSpeed;
    }
}
