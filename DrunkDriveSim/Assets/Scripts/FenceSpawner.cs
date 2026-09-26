using UnityEngine;
using System.Collections;
public class FenceSpawner : MonoBehaviour
{
    public GameObject fence1, fence2;
    void Start()
    {
        StartCoroutine(FenceSuicide1(0.8f * SpawnScript.playerSpeed));
    }

    // Update is called once per frame
    IEnumerator FenceSuicide1(float delay)
    {

        yield return new WaitForSeconds(delay);
        var position = new Vector3(11.4f, -1.94f, 69.64508f);
        Instantiate(fence1, position, transform.rotation);
        var position2 = new Vector3(-14.71f, -1.94f, 69.64508f);
        Instantiate(fence2, position2, transform.rotation);
        StartCoroutine(FenceSuicide1(0.8f * SpawnScript.playerSpeed));
        }
    }

