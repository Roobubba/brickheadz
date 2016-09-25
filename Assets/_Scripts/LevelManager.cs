using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneWasLoaded;
    }

    void OnSceneWasLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        if (scene.name == "01bSettings")
        {
            Settings settings = FindObjectOfType<Settings>();
            settings.SetVolumeSlider();
        }
        if (scene.buildIndex > 0)
        {
            MusicPlayer musicPlayer = FindObjectOfType<MusicPlayer>();
            musicPlayer.SceneLoaded(scene.buildIndex);
        }
    }

    public void LoadLevel(string name)
    {
        ScreenFader.Instance.StartFade(name);
    }

    public static void LoadNextLevel()
    {
        ScreenFader.Instance.StartFade("LoadNextLevel");
    }

    public static void LoadStartMenu()
    {
        ScreenFader.Instance.StartFade("01aStartMenu");
    }

    public void QuitRequest()
    {
        Application.Quit();
    }

}
