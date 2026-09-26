using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MenuTemplates
{
}
public class BlinkingText : MonoBehaviour
{
    TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        StartBlinking();
    }

    IEnumerator Blink()

    {

        while (true)

        {

            switch (text.color.a.ToString())

            {

                case "0":

                    text.color = new Color(text.color.r, text.color.g, text.color.b, 1);

                    yield return new WaitForSeconds(0.5f);

                    break;

                case "1":

                    text.color = new Color(text.color.r, text.color.g, text.color.b, 0);

                    yield return new WaitForSeconds(0.5f);

                    break;

            }

        }

    }


    // Update is called once per frame
    void StartBlinking()
    {
        StopCoroutine("Blink");
        StartCoroutine("Blink");
    }
    void StopBlinking()
    {
        StopCoroutine("Blink");
    }
}
