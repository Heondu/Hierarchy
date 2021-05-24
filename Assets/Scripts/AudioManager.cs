using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip clickClip;
    [SerializeField]
    private AudioSource source;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            source.clip = clickClip;
            source.Play();
        }
    }

    public void SetVolume(float value)
    {
        source.volume = value;
    }
}
