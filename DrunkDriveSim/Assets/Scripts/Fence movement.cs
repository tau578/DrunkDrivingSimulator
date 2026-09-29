using UnityEngine;

public class Fencemovement : MonoBehaviour
{
    public Rigidbody rb;
    public float xspeed;
   

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
    rb.linearVelocity = transform.forward * SpawnScript.playerSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Finish"))
        {
       transform.position = new Vector3(transform.position.x,transform.position.y , 72);
        }
    }
}
