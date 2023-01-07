using System.Collections;
using UnityEngine;
using TMPro;

public class UIHelperText : MonoBehaviour
{
    public TextMeshProUGUI textMesh;

    private Coroutine fadeTextCoroutine;
    private float textFadeDuration = 0.1f;

    public void ShowText(string text)
    {
        ShowText(text, 3f);
    }

    public void ShowText(string text, float duration)
    {
        if(fadeTextCoroutine != null)
        {
            StopCoroutine(fadeTextCoroutine);
        }
        textMesh.color = Color.clear;
        textMesh.text = text;
        fadeTextCoroutine = StartCoroutine(FadeText(duration));
    }

    private IEnumerator FadeText(float duration)
    {
        float count = 0f;
        while(count < duration)
        {
            textMesh.color = Color.Lerp(textMesh.color, Color.white, textFadeDuration);
            count += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        while(!textMesh.color.Equals(Color.clear))
        {
            textMesh.color = Color.Lerp(textMesh.color, Color.clear, textFadeDuration);
            yield return new WaitForEndOfFrame();
        }
        textMesh.text = "";
    }
}
