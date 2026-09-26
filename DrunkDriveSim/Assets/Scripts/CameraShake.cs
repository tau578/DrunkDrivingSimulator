using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    //public GameObject cam;
    public GameObject target;
    public float basePosX = 0;
    public float basePosY = 1;
    public float basePosZ = 0.36f;

    public bool isShake = false;

    void Update()
    {
        /*if(isShake == false)
        {
            transform.position = new Vector3(0f, 1f, 0.36f);
        }*/

        transform.position = new Vector3(target.transform.position.x, 1f, 0.36f);
    }
    /*void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Border")
        {
            StartCoroutine(CamShake(0.2f, 1f));
        }
        Debug.Log("collided");
    }*/
    public void StartShake(float duration, float magnitude)
    {
        StartCoroutine(CamShake(duration, magnitude));
    }
    public IEnumerator CamShake(float duration, float magnitude)
    {
        float elapsed = 0.0f;
        isShake = true;

        //basePosX = cam.transform.position.x;
        //basePosY = cam.transform.position.y;

        while (elapsed < duration)
        {
            float y = Random.Range(transform.position.y - 0.15f, transform.position.y + 0.15f) * magnitude;
            float x = Random.Range(transform.position.x - 0.15f, transform.position.x + 0.15f) * magnitude;
            //float z = Random.Range(basePosZ + 0f, basePosZ - 0f) * magnitude;

            transform.position = new Vector2(x, y);

            elapsed += Time.deltaTime;
            yield return null;
        }
        isShake = false;
        transform.position = new Vector3(target.transform.position.x, 1f, 0.36f);
    }
}
