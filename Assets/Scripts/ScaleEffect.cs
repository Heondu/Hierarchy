using System.Collections;
using UnityEngine;

public class ScaleEffect : MonoBehaviour
{
    [SerializeField]
    private float resizeTime;
    [SerializeField]
    private float scale;
    [SerializeField]
    private AnimationCurve resizeCurve;
    private Vector3 originScale;

    private void Start()
    {
        originScale = transform.localScale;

        StartCoroutine(Resize());
    }

    private IEnumerator Resize()
    {
        while (true)
        {
            yield return StartCoroutine(Scale(1, 0));

            yield return StartCoroutine(Scale(0, 1));
        }
    }

    private IEnumerator Scale(float start, float end)
    {
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / resizeTime;

            transform.localScale = originScale + Vector3.one * Mathf.Lerp(start, end, resizeCurve.Evaluate(percent));

            yield return null;
        }
    }
}
