using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using Padrao.Core.Singleton;

public class EffectsManager : Singleton<EffectsManager>
{
    public PostProcessVolume processVolume;
    public Color flashColor = Color.red;
    public float flashIntensity = 0.5f;
    public float flashDuration = 0.3f;

    [SerializeField]
    private Vignette _vignette;

    public void ChangeVignette()
    {
        Vignette tmp;
        if(processVolume.profile.TryGetSettings<Vignette>(out tmp))
        {
            _vignette = tmp;
            StartCoroutine(FlashColorVignette());
        }
    }

    private IEnumerator FlashColorVignette()
    {       
        float time = 0f;
        while(time < flashDuration)
        {
            ColorParameter color = new ColorParameter();

            color.value = Color.Lerp(Color.white, Color.red, time / flashDuration);

            _vignette.color.Override(color);
            _vignette.intensity.Override(flashIntensity * time / flashDuration);

            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
        time = 0f;
        while(time < flashDuration)
        {
            ColorParameter color = new ColorParameter();

            color.value = Color.Lerp(Color.red, Color.white, time / flashDuration);

            _vignette.color.Override(color);            
            _vignette.intensity.Override(flashIntensity * (1 - time / flashDuration));

            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
