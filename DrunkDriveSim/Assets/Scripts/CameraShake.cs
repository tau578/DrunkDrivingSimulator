using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public GameObject cam;
    public float basePosX = 0;
    public float basePosY = 1;
    public float basePosZ = 0.36f;

    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Border")
        {
            StartCoroutine(CamShake(0.5f, 1f));
        }
    }
    public IEnumerator CamShake(float duration, float magnitude)
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float y = Random.Range(basePosY - 0.15f, basePosY + 0.15f) * magnitude;
            float x = Random.Range(basePosX - 0.15f, basePosX + 0.15f) * magnitude;
            float z = Random.Range(basePosZ + 0f, basePosZ - 0f) * magnitude;

            cam.transform.position = new Vector3(x, y, z);

            elapsed += Time.deltaTime;
            yield return null;
        }

         cam.transform.position = new Vector3(basePosX, basePosY, basePosZ);
    }
}
