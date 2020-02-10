using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public Image creditsBackground;
    public Text creditsText;
    // By default they're set to be completely transparent
    void Start()
    {
        creditsBackground.canvasRenderer.SetAlpha(0f);
        creditsText.canvasRenderer.SetAlpha(0f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            creditsBackground.CrossFadeAlpha(1, 1, false);
            creditsText.CrossFadeAlpha(1, 1, false);
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            creditsBackground.CrossFadeAlpha(0, 1, false);
            creditsText.CrossFadeAlpha(0, 1, false);
        }
    }
}
