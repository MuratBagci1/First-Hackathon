using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WavePopUp : MonoBehaviour
{
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    public float fadeTime = 0.5f;
    public float fadeDelay = 0.5f;
    private float timeElapsed = 0;
    private float fadeDelayElapsed = 0f;

    public IEnumerator Fade()
    {
        if (fadeDelay > fadeDelayElapsed)
        {
            fadeDelayElapsed += Time.deltaTime;
        }
        else
        {
            timeElapsed += Time.deltaTime;

        }
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    public void PopUp(string level)
    {
        text.text = level;
        text.transform.localScale = Vector3.one;
        text.transform.DOScale(2, 1f);
        timeElapsed = 0;
        StartCoroutine(Fade());
    }
}