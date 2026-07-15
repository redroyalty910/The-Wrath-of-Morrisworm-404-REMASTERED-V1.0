using System.Collections; // unity collection
using System.Collections.Generic; // generic collection support
using System.Threading; // threading support
using UnityEngine; // main unity engine

public class SceneManager : MonoBehaviour // controls scene flow, UI visibility, music, death, and level completion
{
    private static SceneManager _instance; // stores the single active SceneManager instance
    public int sceneNum; // tracks the current scene using a number
    private GameObject finalUI;  // reference to the gameplay UI prefab

    public static SceneManager Instance { get { return _instance; } } // lets other scripts access the single SceneManager instance

    private void Awake() // runs before Start
    {
        if (Instance != null && Instance != this) // if another SceneManager already exists...
        {
            Destroy(this.gameObject); // destroy this duplicate manager
        } else // if no SceneManager exists...
        {
            _instance = this; // set this object as the active SceneManager instance
        }
    }
    void Start() // runs once when this object Starts
    {
        sceneNum = 0; // Start on the title screen
        DontDestroyOnLoad(this.gameObject); // Keep this manager alive between scenes
    }

    public static void Load(int sceneId) // loads a scene by its build index
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneId); // load the selected scene
    }

    private void Update() // runs once per frame
    {
        if (finalUI == null) // if UI reference is missing...
        {
            finalUI = GameObject.Find("FinalUIPREFAB"); // search for the UI prefab in the scene
        }
        if (finalUI != null) // if the UI prefab is found
        {
            if (sceneNum == 0 || sceneNum == 7 || sceneNum == 1 || sceneNum == 8 || sceneNum == 9)  // Hide UI in title, game over, hub, victory, and cutscene scenes
            {
                finalUI.SetActive(false); // hide gameplay UI
                XORMOVEMENTSCRIPT.ableToShoot = false; // Disable player shooting
            }
            else // if player is inside a gameplay level
            {
                finalUI.SetActive(true); // show the gameplay UI
                XORMOVEMENTSCRIPT.ableToShoot = true; // enable player shooting
            }
        }


        if (sceneNum == 0 && Input.GetKeyUp(KeyCode.Space)) // if title screen is active and space is pressed, load the cutscene
        { 
            sceneNum = 9; // set current scene to the intro cutscene
            Load(9); // load the intro cutscene
        }
        if (sceneNum == 9 && Input.GetKeyUp(KeyCode.Return))   // if intro cutscene is active and return / enter is pressed, load the hub
        {
            sceneNum = 1; // set scene #
            Load(1); // load hub world
           AudioManager.Instance.PlaySoundTrack(0); // and play the hub music
        }
        if (sceneNum == 7 && Input.GetKeyUp(KeyCode.Space))  // if game over screen is active and space is pressed, load the title screen
        {
            sceneNum = 0; // set scene #
            Load(0); // load title screen
        }
        else if (sceneNum == 1 && Input.GetKeyUp(KeyCode.Space) && XORMOVEMENTSCRIPT.hublevel1trigger == true)  // if in hub + level 1 trigger is active...
        {
            sceneNum = 2; // set scene #
            Load(2);
            AudioManager.Instance.StopSoundTrack(0);
            AudioManager.Instance.PlaySoundTrack(1); // play corresponding soundtrack
        }
        else if (sceneNum == 1 && Input.GetKeyUp(KeyCode.Space) && XORMOVEMENTSCRIPT.hublevel2trigger == true)  // if in hub + level 2 trigger is active...
        {
            sceneNum = 3; // set scene #
            Load(3);
            AudioManager.Instance.StopSoundTrack(0);
            AudioManager.Instance.PlaySoundTrack(2); // play corresponding soundtrack
        }
        else if (sceneNum == 1 && Input.GetKeyUp(KeyCode.Space) && XORMOVEMENTSCRIPT.hublevel3trigger == true)  // if in hub + level 3 trigger is active...
        {
            sceneNum = 4; // set scene #
            Load(4);
            AudioManager.Instance.StopSoundTrack(0);
            AudioManager.Instance.PlaySoundTrack(3); // play corresponding soundtrack
        }
        else if (sceneNum == 1 && Input.GetKeyUp(KeyCode.Space) && XORMOVEMENTSCRIPT.hublevel4trigger == true)  // if in hub + level 4 trigger is active...
        {
            sceneNum = 5; // set scene #
            Load(5);
            AudioManager.Instance.StopSoundTrack(0);
            AudioManager.Instance.PlaySoundTrack(4); // play corresponding soundtrack
        }
        else if (sceneNum == 1 && Input.GetKeyUp(KeyCode.Space) && XORMOVEMENTSCRIPT.hublevel5trigger == true)  // if in hub + level 5 trigger is active...
        {
            sceneNum = 6; // set scene #
            Load(6);
            AudioManager.Instance.StopSoundTrack(0);
            AudioManager.Instance.PlaySoundTrack(5); // play corresponding soundtrack
        }

        if (sceneNum == 8 && Input.GetKeyUp(KeyCode.Space))   // if on the victory screen and space is pressed, load the title screen
        {
            sceneNum = 0; // set scene #
            Load(0);
            enemyTestScript.level1beat = false; // reset beat flags
            enemyTestScript.level2beat = false;
            enemyTestScript.level3beat = false;
            enemyTestScript.level4beat = false;
        }

        if (sceneNum == 1)  // if player is in the hub world
        {
           enemyTestScript.level1enemiesdead = 0; // reset kill counts on all levels
           enemyTestScript.level2enemiesdead = 0;
           enemyTestScript.level3enemiesdead = 0;
           enemyTestScript.level4enemiesdead = 0;
           enemyTestScript.level5enemiesdead = 0;
           enemyTestScript.morriswormdead = 0;
           InformationManager.Instance.playerScore = 0;
            
        }


        if (InformationManager.Instance != null) // if InformationManager exists...
        {
            if (InformationManager.Instance.playerHealth <= 0 && InformationManager.Instance.playerLives > 0) // if player died but still has lives...
            {
                sceneNum = 1; // set scene #
                Load(1); // load hub world
                AudioManager.Instance.StopSoundTrack(1); // stop all level soundtracks and play the hub soundtrack
                AudioManager.Instance.StopSoundTrack(2);
                AudioManager.Instance.StopSoundTrack(3);
                AudioManager.Instance.StopSoundTrack(4);
                AudioManager.Instance.StopSoundTrack(5);
                AudioManager.Instance.PlaySoundTrack(0);
            }
            else if (InformationManager.Instance.playerHealth <= 0 && InformationManager.Instance.playerLives <= 0)   // if player dies with no lives left
            {
                AudioManager.Instance.StopSoundTrack(1); // stop all music
                AudioManager.Instance.StopSoundTrack(2);
                AudioManager.Instance.StopSoundTrack(3);
                AudioManager.Instance.StopSoundTrack(4);
                AudioManager.Instance.StopSoundTrack(5);

                InformationManager.Instance.newGame(); // reset the full game state

                sceneNum = 7; // set scene #
                Load(7); // load game over screen
                enemyTestScript.level1beat = false; // reset the level completion flags
                enemyTestScript.level2beat = false;
                enemyTestScript.level3beat = false;
                enemyTestScript.level4beat = false;

            }
        }

        if (enemyTestScript.level1requirementmet == true)   // if level 1 requirements are met...
        {
            enemyTestScript.level1beat = true; // mark level as completed
            Debug.Log("level 1 clear, going back to hub"); // console log for testing
            sceneNum = 1; // set scene #
            Load(1);
            AudioManager.Instance.PlaySoundTrack(0);
            enemyTestScript.level1requirementmet = false; // reset the level requirement flag
            enemyTestScript.level1enemiesdead = 0; // reset kill count
            InformationManager.Instance.playerHealth = 3;  //reset health
            InformationManager.Instance.playerAmmo = 60; //reset ammo

        }
        if (enemyTestScript.level2requirementmet == true) // if level 2 requirements are met...
        {
            enemyTestScript.level2beat = true; // mark level as completed
            Debug.Log("level 2 clear, going back to hub"); // console log for testing
            sceneNum = 1; // set scene #
            Load(1);
            AudioManager.Instance.PlaySoundTrack(0);
            enemyTestScript.level2requirementmet = false; // reset the level requirement flag
            enemyTestScript.level2enemiesdead = 0; // reset kill count
            InformationManager.Instance.playerHealth = 3;  //reset health
            InformationManager.Instance.playerAmmo = 60; //reset ammo
        }
        if (enemyTestScript.level3requirementmet == true) // if level 3 requirements are met...
        {
            enemyTestScript.level3beat = true; // mark level as completed
            Debug.Log("level 3 clear, going back to hub"); // console log for testing
            sceneNum = 1; // set scene #
            Load(1);
            AudioManager.Instance.PlaySoundTrack(0);
            enemyTestScript.level3requirementmet = false; // reset the level requirement flag
            enemyTestScript.level3enemiesdead = 0; // reset kill count
            InformationManager.Instance.playerHealth = 3;  //reset health
            InformationManager.Instance.playerAmmo = 60; //reset ammo
        }
        if (enemyTestScript.level4requirementmet == true) // if level 4 requirements are met...
        {
            enemyTestScript.level4beat = true; // mark level as completed
            Debug.Log("level 4 clear, going back to hub"); // console log for testing
            sceneNum = 1; // set scene #
            Load(1);
            AudioManager.Instance.PlaySoundTrack(0);
            enemyTestScript.level4requirementmet = false; // reset the level requirement flag
            enemyTestScript.level4enemiesdead = 0; // reset kill count
            InformationManager.Instance.playerHealth = 3;  //reset health
            InformationManager.Instance.playerAmmo = 60; //reset ammo
        }
        if ((enemyTestScript.level5requirementmet == true) && (enemyTestScript.morriswormrequirement == true))  // if level 5 requirements are met... AND Morrisworm has been defeated...
        {
            Debug.Log("level clear, going to win screen"); // console log for testing
            sceneNum = 8; // set scene #
            Load(8);
            AudioManager.Instance.StopSoundTrack(5);
            enemyTestScript.level5requirementmet = false; // reset the level requirement flag
            enemyTestScript.level5enemiesdead = 0; // reset kill count
            enemyTestScript.morriswormrequirement = false; // reset the boss requirement flag
            enemyTestScript.morriswormdead = 0; // reset boss count
            InformationManager.Instance.playerHealth = 3;  //reset health
            InformationManager.Instance.playerAmmo = 60; //reset ammo
        }
    }


}
