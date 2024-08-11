using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    public TextMeshProUGUI text;
    public bool isRunning = false;

    private void Start()
    {
        GameManager.Instance.popUp = gameObject.GetComponent<PopUp>();
        GameManager.Instance.gameRestarted = true;
    }
    public IEnumerator LevelPopUp(string level, string wave)
    {
        if (isRunning)
        {
            //Debug.Log("LevelPopUp is already running.");
            yield break;
        }

        isRunning = true;
        //Debug.Log("LevelPopUp started.");

        text.text = "Level " + level;
        text.transform.localScale = Vector3.one;
        text.transform.DOScale(2, 1f);
        yield return new WaitForSeconds(1f);

        //Debug.Log("Starting WavePopUp.");
        yield return StartCoroutine(WavePopUp(wave));

        isRunning = false;
        gameObject.SetActive(false);
        //Debug.Log("LevelPopUp finished.");
    }

    public IEnumerator WavePopUp(string wave)
    {
        //Debug.Log("WavePopUp started.");

        text.text = "Wave " + wave;
        text.transform.localScale = Vector3.one;
        text.transform.DOScale(2, 1f);
        yield return new WaitForSeconds(1f);
        if(!isRunning)
        {
            gameObject.SetActive(false);
        }
        //Debug.Log("WavePopUp finished.");
    }
}