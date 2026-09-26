using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public Rigidbody rb;
    public float xspeed;


    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        gameObject.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
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
    gameObject.transform.localScale += new Vector3(0.0001f, 0.0001f, 0.0001f);    
    rb.linearVelocity = transform.forward * SpawnScript.playerSpeed;
    }
}
