using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioSlider : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private AudioManager audioManager;

    private void Start()
    {
        slider.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(float value)
    {
        text.text = Mathf.RoundToInt(value * 100).ToString();

        audioManager.SetVolume(value);
    }
}
