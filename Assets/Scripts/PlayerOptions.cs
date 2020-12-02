using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOptions : MonoBehaviour
{

    public AudioSource backgroundMusic;
    public Toggle muteToggle;
    public Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
        // temporary variable to get the results from playerprefs- if it's 1 then mute, otherwise don't
        bool muted = (PlayerPrefs.GetInt("Mute") == 1) ? true : false;
        float vol = PlayerPrefs.GetFloat("Volume");
        //pass this variable to actually mute
        MusicClicked(muted,vol);
        //make sure the toggle visual is updated
        muteToggle.isOn = muted;
        volumeSlider.value = vol;
    }

    public void Mute()
    {
        backgroundMusic.mute = !backgroundMusic.mute;
        int muted = backgroundMusic.mute ? 1 : 0;
        //save to playerprefs with the mute "key"
        PlayerPrefs.SetInt("Mute", muted);
        PlayerPrefs.Save();
    }

    public void MusicClicked(bool state,float volume)
    {
        //set the result of whether it is muted or not (this is set in Toggle's actions in editor)
        backgroundMusic.mute = state;
        Debug.Log(backgroundMusic.mute);
        Debug.Log(PlayerPrefs.GetInt("Mute"));
        //pause any music then play again because volume does not appear to be updating correctly
        backgroundMusic.Pause();
        backgroundMusic.Play();
        //turn into an int, Playerprefs doesn't support bool
        int muted = state ? 1 : 0;

        //save to playerprefs with the mute "key"
        PlayerPrefs.SetInt("Mute",muted);
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.Save();
    }

    //used by the slider's onchanged event in inspector
    public void volumeChanged()
    {
        backgroundMusic.volume = volumeSlider.value;
        backgroundMusic.Pause();
        backgroundMusic.Play();

        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.Save();
    }

    public void showHideOptions(GameObject panel)
    {
        panel.SetActive(!panel.activeInHierarchy);
    }
}
