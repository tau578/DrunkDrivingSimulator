using UnityEngine;

public class Fencemovement : MonoBehaviour
{
public Rigidbody rb;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
    gameObject.transform.localScale += new Vector3(0.0001f, 0.0001f, 0.0001f);    
    rb.linearVelocity = transform.forward * SpawnScript.playerSpeed;
    }
}
