using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Attach to the player's collider object. Owns only the beer HUD;
/// reads the existing score without changing scoring, health or obstacles.
/// </summary>
[DisallowMultipleComponent]
public sealed class BeerCanStack : MonoBehaviour
{
    [Header("HUD")]
    [SerializeField] private RectTransform hud;
    [SerializeField] private Texture2D[] canTextures;
    [SerializeField, Min(1)] private int pointsPerCan = 10;
    [Header("Growing pile (units at 1920 x 1080)")]
    [SerializeField] private Vector2 canSize = new Vector2(180, 300);
    [SerializeField, Min(1)] private float columnStep = 135;
    [SerializeField, Min(0)] private float rightInset = 155;
    [SerializeField, Min(0)] private float bottomInset = 110;
    [SerializeField, Min(1)] private float spreadResistance = 115;
    [SerializeField, Range(0.3f, 1)] private float pileRise = 0.55f;
    [SerializeField, Range(0, 80)] private float maxTilt = 55;

    [Header("Crash animation (canvas units)")]
    [SerializeField, Min(0.1f)] private float flightDuration = 1.6f;
    [SerializeField] private Vector2 horizontalSpeed = new Vector2(180, 480);
    [SerializeField] private Vector2 upwardSpeed = new Vector2(250, 550);
    [SerializeField, Min(0)] private float gravity = 850;

    private readonly List<RawImage> stacked = new List<RawImage>();
    private readonly List<FlyingCan> flying = new List<FlyingCan>();
    private readonly BeerCanProgress progress = new BeerCanProgress();
    private RectTransform area;
    private Vector2 lastAreaSize;
    private bool initialized;

    public int CanCount => stacked.Count;

    private sealed class FlyingCan
    {
        public RawImage image;
        public Vector2 velocity;
        public float spin;
        public float age;
    }

    private void Start()
    {
        if (hud == null || canTextures == null || canTextures.Length == 0 ||
            System.Array.Exists(canTextures, texture => texture == null))
        {
            Debug.LogError("BeerCanStack needs a HUD and beer textures.", this);
            enabled = false;
            return;
        }

        area = new GameObject("Beer Can Stack", typeof(RectTransform)).GetComponent<RectTransform>();
        area.gameObject.layer = hud.gameObject.layer;
        area.SetParent(hud, false);
        area.anchorMin = Vector2.zero;
        area.anchorMax = Vector2.one;
        area.offsetMin = area.offsetMax = Vector2.zero;
        area.pivot = Vector2.zero;
        initialized = true;
        SynchronizeScore();
    }

    private void LateUpdate()
    {
        if (!initialized) return;
        SynchronizeScore();
        if (lastAreaSize != area.rect.size) LayoutStack();

        // Finish scattering even if the existing game-over code pauses time.
        float dt = Time.unscaledDeltaTime;
        for (int i = flying.Count - 1; i >= 0; i--)
        {
            FlyingCan can = flying[i];
            can.age += dt;
            if (can.age >= flightDuration)
            {
                Destroy(can.image.gameObject);
                flying.RemoveAt(i);
                continue;
            }
            can.velocity.y -= gravity * dt;
            can.image.rectTransform.anchoredPosition += can.velocity * dt;
            can.image.rectTransform.Rotate(0, 0, can.spin * dt);
            float alpha = 1f - Mathf.InverseLerp(flightDuration * 0.6f, flightDuration, can.age);
            can.image.color = new Color(1, 1, 1, alpha);
        }
    }

    private void SynchronizeScore()
    {
        int desired = progress.Observe(SpawnScript.points, pointsPerCan);
        if (desired < stacked.Count)
        {
            foreach (RawImage image in stacked) Destroy(image.gameObject);
            stacked.Clear();
        }
        if (desired == stacked.Count) return;
        while (stacked.Count < desired)
        {
            var image = new GameObject("Beer Can", typeof(RectTransform), typeof(CanvasRenderer), typeof(RawImage))
                .GetComponent<RawImage>();
            image.gameObject.layer = area.gameObject.layer;
            image.transform.SetParent(area, false);
            image.texture = canTextures[stacked.Count % canTextures.Length];
            image.raycastTarget = false;
            image.rectTransform.anchorMin = image.rectTransform.anchorMax = Vector2.zero;
            image.rectTransform.pivot = new Vector2(0.5f, 0.5f);
            stacked.Add(image);
        }
        LayoutStack();
    }

    private void LayoutStack()
    {
        lastAreaSize = area.rect.size;
        // Size depends only on the viewport, never on the number of cans.
        // A virtual sloping surface makes the pile grow upward at the right,
        // then spread left across the windshield instead of shrinking to fit.
        float scale = Mathf.Max(0.01f, lastAreaSize.y / 1080f);
        float step = Mathf.Max(1, columnStep) * scale;
        int columns = Mathf.Max(1, Mathf.FloorToInt((lastAreaSize.x - rightInset * scale) / step) + 1);
        var heights = new float[columns];
        for (int i = 0; i < stacked.Count; i++)
        {
            int column = 0;
            float lowest = float.MaxValue;
            for (int c = 0; c < columns; c++)
            {
                float slope = Mathf.Max(0, Mathf.Abs(c - 1) - 1) * spreadResistance * scale;
                float level = heights[c] + slope;
                if (level < lowest)
                {
                    lowest = level;
                    column = c;
                }
            }
            RectTransform rect = stacked[i].rectTransform;
            var texture = stacked[i].texture;
            float variation = Mathf.Lerp(0.92f, 1.08f, PileNoise(i, 1));
            float fit = Mathf.Min(canSize.x / texture.width, canSize.y / texture.height) * scale * variation;
            rect.sizeDelta = new Vector2(texture.width, texture.height) * fit;
            float angle = Mathf.Lerp(25, maxTilt, PileNoise(i, 2)) * (i % 3 == 1 ? -1 : 1);
            rect.localRotation = Quaternion.Euler(0, 0, angle);
            float jitterX = Mathf.Lerp(-18, 18, PileNoise(i, 3)) * scale;
            float jitterY = Mathf.Lerp(-12, 12, PileNoise(i, 4)) * scale;
            rect.anchoredPosition = new Vector2(
                lastAreaSize.x - rightInset * scale - column * step + jitterX,
                bottomInset * scale + heights[column] + jitterY);
            float radians = angle * Mathf.Deg2Rad;
            float height = Mathf.Abs(Mathf.Cos(radians)) * rect.sizeDelta.y +
                Mathf.Abs(Mathf.Sin(radians)) * rect.sizeDelta.x;
            heights[column] += height * pileRise;
        }
    }

    private static float PileNoise(int index, uint salt)
    {
        // Stable variation: existing cans never shuffle when the next one arrives.
        unchecked
        {
            uint hash = (uint)(index + 1) * 747796405u + salt * 2891336453u;
            hash = (hash ^ (hash >> 16)) * 2246822519u;
            return (hash & 0xffff) / 65535f;
        }
    }

    private void OnTriggerEnter(Collider other) => HandleObstacle(other);
    private void OnCollisionEnter(Collision collision) => HandleObstacle(collision.collider);

    private void HandleObstacle(Collider other)
    {
        if (!isActiveAndEnabled || !initialized) return;
        if (other.GetComponentInParent<ObstacleScript>() != null)
            ScatterAndRestart();
    }

    public void ScatterAndRestart()
    {
        if (!initialized) return;
        SynchronizeScore();
        for (int i = 0; i < stacked.Count; i++)
        {
            RawImage image = stacked[i];
            // Alternate directions so a group visibly bursts both ways.
            float direction = i % 2 == 0 ? -1f : 1f;
            flying.Add(new FlyingCan
            {
                image = image,
                velocity = new Vector2(direction * Random.Range(horizontalSpeed.x, horizontalSpeed.y),
                    Random.Range(upwardSpeed.x, upwardSpeed.y)),
                spin = direction * Random.Range(180f, 540f)
            });
            image.transform.SetAsLastSibling();
        }
        stacked.Clear();
        progress.Restart(SpawnScript.points);
    }

    private void OnDestroy()
    {
        if (area != null) Destroy(area.gameObject);
    }
}

// Pure score bookkeeping, independent of frame rate and Unity lifecycle.
public sealed class BeerCanProgress
{
    private float baseline;
    private float previousScore;

    public int Observe(float score, int pointsPerCan)
    {
        if (score < previousScore) baseline = score;
        previousScore = score;
        return (int)System.Math.Floor(System.Math.Max(0, score - baseline) / System.Math.Max(1, pointsPerCan));
    }

    public void Restart(float score)
    {
        baseline = previousScore = score;
    }
}
