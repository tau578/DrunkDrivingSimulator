using UnityEngine;

public class TextureScroll : MonoBehaviour
{
    public SpawnScript spawnScr;
    public Renderer rend;
    void Start()
    {
        rend = rend.GetComponent<Renderer>();
        spawnScr = spawnScr.GetComponent<SpawnScript>();
    }

    // Update is called once per frame
    void Update()
    {
        float offset = SpawnScript.playerSpeed * 0.1f;
        rend.material.mainTextureOffset = new Vector2(-offset, 0f);
    }
}
