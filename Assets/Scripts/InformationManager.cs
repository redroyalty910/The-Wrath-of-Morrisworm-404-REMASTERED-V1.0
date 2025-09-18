using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InformationManager : MonoBehaviour
{
    private static InformationManager _instance;

    public static InformationManager Instance { get { return _instance; } }

    public int playerHealth = 3, playerAmmo = 60, playerLives = 3, playerScore = 0; //set all player stats
    
    public Text playerScoreUI, playerHealthUI, playerAmmoUI, playerLivesUI, levelRequirementUI;   //player ui


    private void Awake()   //makes sure a duplicate is deleted if a new game is started.
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()  // makes sure the original isnt deleted
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        updateUI();
        if (enemyTestScript.level1enemiesdead == 25)     //these next 5 check if an enemy level requirements is met(ex. level 1 is kill 25 enemies) and if true,
                                                         //sets the requirement to true and the enemies kiled back to 0 so that if the player goes in the level again, they have to kill 25 enemies again.
        {
            enemyTestScript.level1requirementmet = true;
            enemyTestScript.level1enemiesdead = 0;
        }
        if (enemyTestScript.level2enemiesdead == 20)
        {
            enemyTestScript.level2requirementmet = true;
            enemyTestScript.level2enemiesdead = 0;
        }
        if (enemyTestScript.level3enemiesdead == 15)
        {
            enemyTestScript.level3requirementmet = true;
            enemyTestScript.level3enemiesdead = 0;
        }
        if (enemyTestScript.level4enemiesdead == 10)
        {
            enemyTestScript.level4requirementmet = true;
            enemyTestScript.level4enemiesdead = 0;
        }
        if (enemyTestScript.level5enemiesdead == 5)
        {
            enemyTestScript.level5requirementmet = true;
            enemyTestScript.level5enemiesdead = 0;
        }
        if (enemyTestScript.morriswormdead == 1)
        {
            enemyTestScript.morriswormrequirement = true;
            enemyTestScript.morriswormdead = 0;
        }

        if (Input.GetKeyDown(KeyCode.Escape))   //this will quit the game
        {
            Application.Quit();
        }

    }

    public void UpdateScore()   //add 100 to player score 
    {
        playerScore = playerScore + 100;
        Debug.Log("Score: " + playerScore);

    }

    public void UpdateAmmo()   //take 1 ammo away
    {
        playerAmmo = playerAmmo - 1;
    }
    public void updateUI()   //update the ui for the player
    {
        playerHealthUI.text = "" + playerHealth;
        playerAmmoUI.text = "" + playerAmmo;
        playerLivesUI.text = " x " + playerLives;
        playerScoreUI.text = "Score: " + playerScore;
    }

    public void resetPlayerStats()   //this is used when a player dies
    {
        playerHealth = 3;  //reset health
        playerAmmo = 60;  //reset ammo
        playerLives = playerLives - 1;
        Debug.Log("Player lost a life.");

    }
    public void newGame()   //reset everything for a fresh game when player runs out of lives
    {
        playerHealth = 3;
        playerAmmo = 60;
        playerLives = 3;
        playerScore = 0;
        enemyTestScript.level1requirementmet = false;
        enemyTestScript.level2requirementmet = false;
        enemyTestScript.level3requirementmet = false;
        enemyTestScript.level4requirementmet = false;
        enemyTestScript.level5requirementmet = false;
        enemyTestScript.morriswormrequirement = false;
        enemyTestScript.level1enemiesdead = 0;
        enemyTestScript.level2enemiesdead = 0;
        enemyTestScript.level3enemiesdead = 0;
        enemyTestScript.level4enemiesdead = 0;
        enemyTestScript.level5enemiesdead = 0;
        enemyTestScript.morriswormdead = 0;

    }
    public void playerHurt()   //take health off the player(is called elsewhere)
    {
        playerHealth = playerHealth - 1;
        Debug.Log("Health is: " + playerHealth);
    }


    public void ammoPowerUp(int ammo)   //give player max ammo whrn collecting a power up
    {
        playerAmmo = ammo;
        Debug.Log("Ammo is: " + playerAmmo);
    }
}
