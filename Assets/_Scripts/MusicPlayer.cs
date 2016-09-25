using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {

	public AudioClip splashSound;
	public AudioClip [] musicArray;
	public Image icon;
    public float splashScreenDelay = 2.5f;


    private AudioSource audioSource;
	private float volume;
		
	void Awake(){
		DontDestroyOnLoad(gameObject);
		Debug.Log("Don't destroy on load:" + name);

    }
	
	void Start(){
        volume = PlayerPrefsManager.GetMasterVolume();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = splashSound;
        audioSource.loop = false;
        if (volume == 0f)
        {
            icon.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
        audioSource.volume = volume;
        audioSource.Play();

        //Bit hacky but we will load the next screen after a delay automatically from here
        StartCoroutine(FirstLoad());

    }

    IEnumerator FirstLoad()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) //failsafe in case we want to load musiplayer on demand for testing
        {
            yield return new WaitForSeconds(splashScreenDelay);
            LevelManager.LoadNextLevel();
        }
    }

	public void SceneLoaded(int level){
		AudioClip currentlyPlaying = audioSource.clip;
		AudioClip music = musicArray[level];
		//Debug.Log ("Loaded level " + level);
		//Debug.Log ("Playing clip: " + music);
		if (music){
			if (currentlyPlaying != music){
				audioSource.clip = music;
				audioSource.loop = true;
				if (level == 6 || level == 7){ // Win or Lose screens - this should be made neater by checking level names
					audioSource.loop = false;
				}
				audioSource.Play();
			}
		}
		else{
			Debug.Log("No music found in musicArray. Trying to access entry: " + level);
		}
	}
	
	public void SetVolume(float vol){
		audioSource.volume = vol;
	}
	
}
