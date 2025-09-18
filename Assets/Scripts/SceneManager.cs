using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    private static SceneManager _instance;
    public int sceneNum;
    private GameObject finalUI;  //reference the ui(used for toggling on and off)

    public static SceneManager Instance { get { return _instance; } }

    private void Awake()
    {
        //if there is already a SceneManager and it is NOT this object:
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        } else
        {
            _instance = this;       //first time Awake is called.
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        sceneNum = 0;
        DontDestroyOnLoad(this.gameObject);
    }

    public static void Load(int sceneId)
    {
        //AudioManager.Instance.PlaySoundEffect(0);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneId);
    }

    private void Update()
    {

        if (finalUI == null)
        {
            finalUI = GameObject.Find("FinalUIPREFAB");
        }

        if (finalUI != null)
        {
            if (sceneNum == 0 || sceneNum == 7 || sceneNum == 1 || sceneNum == 8 || sceneNum == 9)  // hide UI in title screen, hub world, game over screen, win screen, and intro cutscene 
            {
                finalUI.SetActive(false);
                XORMOVEMENTSCRIPT.ableToShoot = false;
            }
            else
            {
                finalUI.SetActive(true);
                XORMOVEMENTSCRIPT.ableToShoot = true;
            }
        }


        if (sceneNum == 0 && Input.GetKeyUp(KeyCode.Space))   //if on title screen and space is pressed, load cutscene
        { 
            sceneNum = 9;
            Load(9);
        }
        if (sceneNum == 9 && Input.GetKeyUp(KeyCode.Return))   //if on cutscene and enter is pressed, load hub world
        {
            sceneNum = 1;
            Load(1);
           AudioManager.Instance.PlaySoundTrack(0);
        }
        if (sceneNum == 7 && Input.GetKeyUp(KeyCode.Space))  //if on game over screen and space is presseed, load title screen
        {
            sceneNum = 0;
            Load(0);
        }
        else if (sceneNum == 1 && Input.GetKeyUp(KeyCode.Space) && XORMOVEMENTSCRIPT.hublevel1trigger == true)  //load level 1 from hub
        {
            sceneNum = 2;
            Load(2);
            AudioManager.Instance.StopSoundTrack(0);
            AudioManager.Instance.PlaySoundTrack(1);
        }
        else if (sceneNum == 1 && Input.GetKeyUp(KeyCode.Space) && XORMOVEMENTSCRIPT.hublevel2trigger == true)  //load level 2 from hub
        {
            sceneNum = 3;
            Load(3);
            AudioManager.Instance.StopSoundTrack(0);
            AudioManager.Instance.PlaySoundTrack(2);
        }
        else if (sceneNum == 1 && Input.GetKeyUp(KeyCode.Space) && XORMOVEMENTSCRIPT.hublevel3trigger == true)  //load level 3 from hub
        {
            sceneNum = 4;
            Load(4);
            AudioManager.Instance.StopSoundTrack(0);
            AudioManager.Instance.PlaySoundTrack(3);
        }
        else if (sceneNum == 1 && Input.GetKeyUp(KeyCode.Space) && XORMOVEMENTSCRIPT.hublevel4trigger == true)  //load level 4 from hub
        {
            sceneNum = 5;
            Load(5);
            AudioManager.Instance.StopSoundTrack(0);
            AudioManager.Instance.PlaySoundTrack(4);
        }
        else if (sceneNum == 1 && Input.GetKeyUp(KeyCode.Space) && XORMOVEMENTSCRIPT.hublevel5trigger == true)  //load level 5 from hub
        {
            sceneNum = 6;
            Load(6);
            AudioManager.Instance.StopSoundTrack(0);
            AudioManager.Instance.PlaySoundTrack(5);
        }

        if (sceneNum == 8 && Input.GetKeyUp(KeyCode.Space))   //if on victory screen and spaced is pressed, loadtitle screen
        {
            sceneNum = 0;
            Load(0);
            enemyTestScript.level1beat = false;
            enemyTestScript.level2beat = false;
            enemyTestScript.level3beat = false;
            enemyTestScript.level4beat = false;
        }

        if (sceneNum == 1)  //sets all enemies dead in eaec level back to 0, so if a player dies and say they have 5 enemies killed in level 1, they will still have to kill 25 again when they load back in. If this wasnt done, and say a player killed 5 enemies and died, the next time they load in theyd only have to kill 20 (the requirement for beating the lvevl is 25)
        {
           enemyTestScript.level1enemiesdead = 0;
           enemyTestScript.level2enemiesdead = 0;
           enemyTestScript.level3enemiesdead = 0;
           enemyTestScript.level4enemiesdead = 0;
           enemyTestScript.level5enemiesdead = 0;
           enemyTestScript.morriswormdead = 0;
           InformationManager.Instance.playerScore = 0;
            
        }


        if (InformationManager.Instance != null)
        {
            if (InformationManager.Instance.playerHealth <= 0 && InformationManager.Instance.playerLives > 0)  //load player back to hub if they have lives when they die
            {
                sceneNum = 1;
                Load(1);
                AudioManager.Instance.StopSoundTrack(1);
                AudioManager.Instance.StopSoundTrack(2);
                AudioManager.Instance.StopSoundTrack(3);
                AudioManager.Instance.StopSoundTrack(4);
                AudioManager.Instance.StopSoundTrack(5);
                AudioManager.Instance.PlaySoundTrack(0);
            }
            else if (InformationManager.Instance.playerHealth <= 0 && InformationManager.Instance.playerLives <= 0)   //load game over scene when player is out of lives. resets all flags and stops the soundtrack of whatever level was loaded at death
            {
                AudioManager.Instance.StopSoundTrack(1);
                AudioManager.Instance.StopSoundTrack(2);
                AudioManager.Instance.StopSoundTrack(3);
                AudioManager.Instance.StopSoundTrack(4);
                AudioManager.Instance.StopSoundTrack(5);
                InformationManager.Instance.newGame();
                sceneNum = 7;
                Load(7);
                enemyTestScript.level1beat = false;
                enemyTestScript.level2beat = false;
                enemyTestScript.level3beat = false;
                enemyTestScript.level4beat = false;

            }
        }

        if (enemyTestScript.level1requirementmet == true)   //for the following, if the player meets the requirements required to beat the level, they are brought back to the hub world
        {
            enemyTestScript.level1beat = true;
            Debug.Log("level 1 clear, going back to hub");
            sceneNum = 1;
            Load(1);
            AudioManager.Instance.PlaySoundTrack(0);
            enemyTestScript.level1requirementmet = false;
            enemyTestScript.level1enemiesdead = 0;
            InformationManager.Instance.playerHealth = 3;  //reset health
            InformationManager.Instance.playerAmmo = 60; //reset ammo

        }
        if (enemyTestScript.level2requirementmet == true)
        {
            enemyTestScript.level2beat = true;
            Debug.Log("level 2 clear, going back to hub");
            sceneNum = 1;
            Load(1);
            AudioManager.Instance.PlaySoundTrack(0);
            enemyTestScript.level2requirementmet = false;
            enemyTestScript.level2enemiesdead = 0;
            InformationManager.Instance.playerHealth = 3;  //reset health
            InformationManager.Instance.playerAmmo = 60; //reset ammo
        }
        if (enemyTestScript.level3requirementmet == true)
        {
            enemyTestScript.level3beat = true;
            Debug.Log("level 3 clear, going back to hub");
            sceneNum = 1;
            Load(1);
            AudioManager.Instance.PlaySoundTrack(0);
            enemyTestScript.level3requirementmet = false;
            enemyTestScript.level3enemiesdead = 0;
            InformationManager.Instance.playerHealth = 3;  //reset health
            InformationManager.Instance.playerAmmo = 60; //reset ammo
        }
        if (enemyTestScript.level4requirementmet == true)
        {
            enemyTestScript.level4beat = true;
            Debug.Log("level 4 clear, going back to hub");
            sceneNum = 1;
            Load(1);
            AudioManager.Instance.PlaySoundTrack(0);
            enemyTestScript.level4requirementmet = false;
            enemyTestScript.level4enemiesdead = 0;
            InformationManager.Instance.playerHealth = 3;  //reset health
            InformationManager.Instance.playerAmmo = 60; //reset ammo
        }
        if ((enemyTestScript.level5requirementmet == true) && (enemyTestScript.morriswormrequirement == true))  //load player into win screen if all conditions are met
        {
            Debug.Log("level clear, going to win screen");
            sceneNum = 8;
            Load(8);
            AudioManager.Instance.StopSoundTrack(5);
            enemyTestScript.level5requirementmet = false;
            enemyTestScript.level5enemiesdead = 0;
            enemyTestScript.morriswormrequirement = false;
            enemyTestScript.morriswormdead = 0;
            InformationManager.Instance.playerHealth = 3;  //reset health
            InformationManager.Instance.playerAmmo = 60; //reset ammo
        }
    }


}
