using UnityEngine;
using System.Collections;
using Enum = System.Enum;

public class PlayerPrefsManager : MonoBehaviour {

	const string MASTER_VOLUME_KEY = "m_vol";
	const string DIFFICULTY_KEY = "diff";
	const string LEVEL_KEY = "level"; // we will use powers of 2 to store these in a single int
	const string UNITS_KEY = "units"; // friendly units will be unlocked in a set order
	const string ENEMIES_KEY = "enemies"; // enemy units will be unlocked in a set order
    const string TUTORIAL_KEY = "tutorial"; // Use an int in place of a bool


    public enum TestFlags{
		LEVEL_1 = 1,
		LEVEL_2 = 2,
		LEVEL_3 = 4,
		LEVEL_4 = 8,
		LEVEL_5 = 16,
		LEVEL_6 = 32,
		LEVEL_7 = 64,
		LEVEL_8 = 128,
		LEVEL_9 = 256,
		LEVEL_10 = 512,
		LEVEL_11 = 1024,
		LEVEL_12 = 2048
	}
	

	//Global functions on playerprefs
		
	public static void ClearPlayerPrefs(){
		Debug.Log ("Delete all Player Prefs called");
		PlayerPrefs.DeleteAll();
	}


	// Player volume settings

	public static void SetMasterVolume(float vol){
		if (vol >= 0f && vol <= 1f){
			PlayerPrefs.SetFloat (MASTER_VOLUME_KEY, vol);
		}
		else{
			Debug.LogError ("Master volume out of range: " + vol);
		}
	}
	
	public static float GetMasterVolume(){
		return PlayerPrefs.GetFloat (MASTER_VOLUME_KEY);
	}

	
	
	//Level progress and related:

	public static void CompleteLevel (TestFlags level){
		int currLevel = GetLevel();
		PlayerPrefs.SetInt (LEVEL_KEY, currLevel | (int)level);
	}
	
	public static int GetLevel(){
		return PlayerPrefs.GetInt (LEVEL_KEY);
	}
	
	public static bool IsLevelCompleted(TestFlags testLevel){
		int currLevel = GetLevel(); 
		if ((currLevel & (int)testLevel) == (int)testLevel){
			return true;
		}
		return false;
	}
	
	public static bool IsLevelCompletedFast(TestFlags testLevel, int currLevel){
		if ((currLevel & (int)testLevel) == (int)testLevel){
			return true;
		}
		return false;
	}
	
	public static void ResetAllLevels(){
		Debug.Log("Command to reset ALL level progress received");
		PlayerPrefs.SetInt(LEVEL_KEY, 0);
	}
	
	public static void ResetLevel(TestFlags level){
		int currLevel = GetLevel();
		if (IsLevelCompletedFast (level, currLevel)){
			currLevel -= (int)level; 
			PlayerPrefs.SetInt (LEVEL_KEY, currLevel);
			Debug.Log("Level Reset Command issued for level: " + level + " = " + (int)level);
		}
	}
	
	public static bool GetIsLevelUnlockedFast(TestFlags level, int currLevel){ // use to determine which levels are accessible to the player

		if ((int)level <= currLevel){
			return true;
		}
		else{
			bool foundLevel = false;
			int firstUnlockedLevel = 0;
			var values = Enum.GetValues(typeof(TestFlags));
			foreach (TestFlags flag in values){
				if (!foundLevel){
					if ((int)flag > currLevel){
					firstUnlockedLevel = (int)flag;
					foundLevel = true;
					break;
					}
				}
			}
			if (firstUnlockedLevel == (int)level){
				return true;
			}
		}
		return false;
	}
	
	//Difficulty Settings Here
	
	public static void SetDifficulty(int diff){
		if(diff >= 0 && diff <=3){
			PlayerPrefs.SetInt (DIFFICULTY_KEY, diff);
		}
		else{
			Debug.LogError("Difficulty out of range: " + diff);
		}
	}
		
	public static int GetDifficulty(){
		return PlayerPrefs.GetInt (DIFFICULTY_KEY);
	}
	
	public static void SetUnlockedUnits(int units){
		if (units >=0 && units <=5){
			PlayerPrefs.SetInt (UNITS_KEY, units);
		}
		else{
			Debug.LogError ("Unlocked units out of range: " + units); 
		}
	}
	
	public static int GetUnits(){
		return PlayerPrefs.GetInt (UNITS_KEY);
	}
	
	public static void SetUnlockedEnemies(int enemies){
		if (enemies >=1 && enemies <=5){
			PlayerPrefs.SetInt (ENEMIES_KEY, enemies);
		}
		else{
			Debug.LogError ("Unlocked enemies out of range: " + enemies); 
		}
	}
	
	public static int GetEnemies(){
		return PlayerPrefs.GetInt (ENEMIES_KEY);
	}
	
    public static void SetTutorialLevelComplete(bool completed)
    {
        if (completed)
        {
            PlayerPrefs.SetInt(TUTORIAL_KEY, 1);
        }
        else
        {
            PlayerPrefs.SetInt(TUTORIAL_KEY, 0);
        }

    }

    public static bool GetIsTutorialLevelComplete()
    {
        int tutorialStatus = PlayerPrefs.GetInt(TUTORIAL_KEY);
        if (tutorialStatus == 0)
        {
            return false;
        }
        else // any value other than 0 will do
        {
            return true;
        }
    }
}
