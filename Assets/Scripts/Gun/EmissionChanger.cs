using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionChanger : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;
    [SerializeField] private float transitionDuration;
    [SerializeField] private float startIntensity;
    [SerializeField] private float endIntensity;

    private float elapsedTime = 0f;
    private Coroutine transitionCoroutine;

    private void Start()
    {
        Discharge();
    }

    public void StartTransition()
    {
        if (transitionCoroutine != null)
        {
            StopCoroutine(transitionCoroutine);
        }

        transitionCoroutine = StartCoroutine(TransitionCoroutine());
    }

    public void Discharge()
    {
        material.SetColor("_EmissionColor", startColor);
        material.SetFloat("_EmissionIntensity", startIntensity);
    }

    public void StopTransition()
    {
        if (transitionCoroutine != null)
        {
            StopCoroutine(transitionCoroutine);
        }
    }

    private IEnumerator TransitionCoroutine()
    {
        elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;

            float t = elapsedTime / transitionDuration;
            Color lerpedColor = Color.Lerp(startColor, endColor, t);
            float lerpedIntensity = Mathf.Lerp(startIntensity, endIntensity, t);

            material.SetColor("_EmissionColor", lerpedColor * lerpedIntensity);

            yield return null;
        }

        material.SetColor("_EmissionColor", endColor);
        material.SetFloat("_EmissionIntensity", endIntensity);

        transitionCoroutine = null;
    }

    private void OnDisable()
    {
        Discharge();
    }
}
