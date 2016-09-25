using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

	public Slider volumeSlider;	
	public Text volumeText;

	public static float vol;
	public static float timescale = 1f;
	
	private MusicPlayer musicplayer;

    public GameObject confirmResetDialog;

    void Start(){
		musicplayer = FindObjectOfType<MusicPlayer>();
		vol = musicplayer.GetComponent<AudioSource>().volume * 100;
	}
	
		
	//public bool mouseControl;
	public void ResetDefaults(){
		vol = 100f;
		PlayerPrefsManager.SetMasterVolume(vol/100);
		volumeSlider.value = vol;
		volumeText.text = "100";
	}
	

	public void VolumeSliderSlide(){
		vol = volumeSlider.value;
		volumeText.text = vol.ToString ("F0");
		musicplayer.SetVolume(vol / 100);
	}
	
	public void SetVolumeSlider(){
		musicplayer = FindObjectOfType<MusicPlayer>();
		vol = musicplayer.GetComponent<AudioSource>().volume * 100;
		volumeSlider.value = vol;
		volumeText.text = vol.ToString ("F0");
	}



	public void SaveAndExit(){
		PlayerPrefsManager.SetMasterVolume(volumeSlider.value/100);
		LevelManager levelman = FindObjectOfType<LevelManager>();
		levelman.LoadLevel("01aStartMenu");
	}

    public void EnableConfirmResetDialog()
    {
        if (!confirmResetDialog)
        {
            Debug.LogError("Could not find the confirm reset dialog gameobject!");
        }
        else
        {
            confirmResetDialog.SetActive(true);
        }
    }

    public void DisableConfirmResetDialog()
    {
        if (!confirmResetDialog)
        {
            Debug.LogError("Could not find the confirm reset dialog gameobject!");
        }
        else
        {
            confirmResetDialog.SetActive(false);
        }
    }

    public void ResetAllProgress()
    {
        PlayerPrefsManager.ClearPlayerPrefs();
        vol = 100f;
        PlayerPrefsManager.SetMasterVolume(vol / 100);
        musicplayer.SetVolume(1f);
        LevelManager levelman = FindObjectOfType<LevelManager>();
        levelman.LoadLevel("01bSettings");
    }


}
