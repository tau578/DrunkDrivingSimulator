using UnityEngine;

public class Wobble : MonoBehaviour
{

    public float wobbleSpeed = 2f;      
    public float wobbleAmount = 0.05f;  

    private Vector3 wobbleBaseScale;

    void Start()
    {
        wobbleBaseScale = transform.localScale;
    }

    void Update()
    {
    float wobbleOffset = Mathf.Sin(Time.time * wobbleSpeed) * wobbleAmount;

    float x = 1 + wobbleOffset;
    float y = 1 - wobbleOffset;

    transform.localScale = new Vector3(wobbleBaseScale.x * x, wobbleBaseScale.y * y, wobbleBaseScale.z);
    }
}