using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour {

	protected static ScreenFader mInstance;
	public static ScreenFader Instance{
		get{
			if (mInstance == null){
			
				GameObject tempObj = new GameObject();
				mInstance = tempObj.AddComponent<ScreenFader>();
				Destroy(tempObj);
			}
			return mInstance;
		}
	
	}
	public Canvas FadePrefab;
	public enum E_FadeType{
		FADE_IN,
		FADE_OUT
	}
	E_FadeType Type;
	
	string sceneName = null;
    int sceneNumber = -1;
	bool b_doFade = false;
	Canvas FadeSprite;
	float alpha = 1.0f;
	float fadeSpeed = 5.0f;
	
	void InstantiateFade(){
		if (FadePrefab != null){
			FadeSprite = Instantiate (FadePrefab, FadePrefab.transform.position, Quaternion.identity) as Canvas;
		}
	
	}
	
	void Start(){
		mInstance = this;
		StartFade(null, true);
	}
	
	void DoFade(bool mode){
		if (mode){
			if(FadeSprite.GetComponentInChildren<Image>().color.a > 0.0f){
				alpha -= Time.deltaTime * fadeSpeed;
			}
			else{
				Destroy (FadeSprite.gameObject);
				FadeSprite = null;
				b_doFade = false;
			}
		}
		else{
			if (FadeSprite.GetComponentInChildren<Image>().color.a < 1.0f){
				alpha += Time.deltaTime * fadeSpeed;
			}
			else{
				b_doFade = false;
                if (sceneName == "LoadNextLevel")
                {
                    sceneNumber = (SceneManager.GetActiveScene().buildIndex) + 1;
                    if (sceneNumber <= SceneManager.sceneCountInBuildSettings)
                    {
                        SceneManager.LoadScene(sceneNumber);
                    }
                    else
                    {
                        SceneManager.LoadScene("02aWin");
                    }
                }
                else
                {
                    SceneManager.LoadScene(sceneName);
                }
            }
		}
	}
	
	public void StartFade(string sceneName, bool mode = false){
		if(b_doFade){
			return;
		}
		this.sceneName = sceneName;
		b_doFade = true;
		
		if (FadeSprite == null){
			InstantiateFade();
		}
		
		Color col = FadeSprite.GetComponentInChildren<Image>().color;
		
		if (mode){
			this.Type = E_FadeType.FADE_IN;
			FadeSprite.GetComponentInChildren<Image>().color = new Color(col.r, col.g, col.b, 1.0f);
		}
		else{
			this.Type = E_FadeType.FADE_OUT;
			FadeSprite.GetComponentInChildren<Image>().color = new Color(col.r, col.g, col.b, 0.0f);
		}
	}

	
	void Update(){
		if (b_doFade){
			switch (Type){
				case E_FadeType.FADE_IN:
					DoFade(true);
					break;
				case E_FadeType.FADE_OUT:
					DoFade(false);
					break;
			}
			
			if (FadeSprite != null){
				Color col = FadeSprite.GetComponentInChildren<Image>().color;
				FadeSprite.GetComponentInChildren<Image>().color = new Color(col.r, col.g, col.b, alpha);
			}
		}
	}
}
