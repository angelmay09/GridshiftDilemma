using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    
    public AudioSource audioSource;
    public Slider volumeSlider;
    private const string VolumePref = "VolumePref";

    private void Start()
    {
        float volume = PlayerPrefs.GetFloat(VolumePref, 1.0f);
        audioSource.Play();

        if (volumeSlider != null)
        {
            volumeSlider.value = volume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat(VolumePref, volume);
        PlayerPrefs.Save();
    }
    
    public void ResetVolume() {
        SetVolume(1.0f);
        if (volumeSlider != null)
        {
            volumeSlider.value = 1.0f;
        }
    }
}
