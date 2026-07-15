/* think of this file as a global stat-keeper. It keeps track of all the players' most useful stats... globally, instead of level-to-level
* if a new scene loads, and another InformationManager.cs appears, it will destroy itself to ensure that only ONE exists at a time.
* It regularly updates the user-interface as well. puts stat values on the screen and such.
* level completion requirements are also tracked within this file.
*/

using System.Collections; // unity collection
using System.Collections.Generic; // generic collection
using UnityEngine; // main unity engine library
using UnityEngine.UI; // allows this script to use UI text object
public class InformationManager : MonoBehaviour // manages player stats and UI, also checks if level requirements are met
{
    private static InformationManager _instance; // stores the single active InformationManager instance

    public static InformationManager Instance { get { return _instance; } }

    public int playerHealth = 3, playerAmmo = 60, playerLives = 3, playerScore = 0; // set all player stats
    
    public Text playerScoreUI, playerHealthUI, playerAmmoUI, playerLivesUI, levelRequirementUI;   // player ui text reference


    private void Awake()   // runs before start
    {
        if (_instance != null && _instance != this) // if another InformationManager already exists...
        {
            Destroy(this.gameObject); // destroy this duplicate manager to ensure that only ONE exists at a time...
        }
        else // if NO manager exists yet...
        {
            _instance = this; // set this object as the active InformationManager instance
        }
    }

    void Start()  // runs once when this object starts
    {
        DontDestroyOnLoad(gameObject); // keep this manager alive between scene loads
    }

    void Update() // runs once per frame (updates)
    {
        updateUI(); // refresh the player UI every frame
        if (enemyTestScript.level1enemiesdead == 25) // if level 1 kill requirement is met
                                                         
        {
            enemyTestScript.level1requirementmet = true; // mark level 1 requirement as met
            enemyTestScript.level1enemiesdead = 0; // reset level 1 kill-counter to 0 so it can be used again if the player dies and restarts the level
        }
        if (enemyTestScript.level2enemiesdead == 20) // if level 2 kill requirement is met
        {
            enemyTestScript.level2requirementmet = true; // mark level 2 requirement as met
            enemyTestScript.level2enemiesdead = 0; // reset level 2 kill-counter to 0 so it can be used again if the player dies and restarts the level
        }
        if (enemyTestScript.level3enemiesdead == 15) // if level 3 kill requirement is met
        {
            enemyTestScript.level3requirementmet = true; // mark level 3 requirement as met
            enemyTestScript.level3enemiesdead = 0; // reset level 3 kill-counter to 0 so it can be used again if the player dies and restarts the level
        }
        if (enemyTestScript.level4enemiesdead == 10) // if level 4 kill requirement is met
        {
            enemyTestScript.level4requirementmet = true; // mark level 4 requirement as met
            enemyTestScript.level4enemiesdead = 0; // reset level 4 kill-counter to 0 so it can be used again if the player dies and restarts the level
        }
        if (enemyTestScript.level5enemiesdead == 5) // if level 5 kill requirement is met
        {
            enemyTestScript.level5requirementmet = true; // mark level 5 requirement as met
            enemyTestScript.level5enemiesdead = 0; // reset level 5 kill-counter to 0 so it can be used again if the player dies and restarts the level
        }
        if (enemyTestScript.morriswormdead == 1) // if level 5 kill requirement is met
        {
            enemyTestScript.morriswormrequirement = true; // mark morrisworm requirement as met
            enemyTestScript.morriswormdead = 0;  // reset level 5 kill-counter to 0 so it can be used again if the player dies and restarts the level
        }

        if (Input.GetKeyDown(KeyCode.Escape))   // if the escape key is pressed
        {
            Application.Quit(); // ya quit the game
        }

    }

    public void UpdateScore()   // adds points to the player score
    {
        playerScore = playerScore + 100; // adds 100 points to the playerScore
        Debug.Log("Score: " + playerScore); // console message

    }

    public void UpdateAmmo()   // removes ammo when the player shoots
    {
        playerAmmo = playerAmmo - 1; // subtract one ammo
    }
    public void updateUI()   // update the ui text for the player
    {
        playerHealthUI.text = "" + playerHealth; // show current health
        playerAmmoUI.text = "" + playerAmmo; // show current ammo
        playerLivesUI.text = " x " + playerLives; // show current lives
        playerScoreUI.text = "Score: " + playerScore; // show current score
    }

    public void resetPlayerStats()   // resets the player stats when the player dies and loses a life
    {
        playerHealth = 3;  // reset health
        playerAmmo = 60;  // reset ammo
        playerLives = playerLives - 1; // remove a life
        Debug.Log("Player lost a life."); // console message

    }
    public void newGame()   // resets the game back to a fresh state
    {
        playerHealth = 3; // reset health
        playerAmmo = 60; // reset ammo
        playerLives = 3; // reset lives
        playerScore = 0; // reset score
        enemyTestScript.level1requirementmet = false; // reset level 1 clear flag
        enemyTestScript.level2requirementmet = false; // reset level 2 clear flag
        enemyTestScript.level3requirementmet = false; // reset level 3 clear flag
        enemyTestScript.level4requirementmet = false; // reset level 4 clear flag
        enemyTestScript.level5requirementmet = false; // reset level 5 clear flag
        enemyTestScript.morriswormrequirement = false; // reset level MORRISWORM clear flag

        enemyTestScript.level1enemiesdead = 0; // reset level 1 kills
        enemyTestScript.level2enemiesdead = 0; // reset level 2 kills
        enemyTestScript.level3enemiesdead = 0; // reset level 3 kills
        enemyTestScript.level4enemiesdead = 0; // reset level 4 kills
        enemyTestScript.level5enemiesdead = 0; // reset level 5 kills
        enemyTestScript.morriswormdead = 0; // reset level MORRISWORM kills

    }
    public void playerHurt()   // damages XOR
    {
        playerHealth = playerHealth - 1; // subtract one health
        Debug.Log("Health is: " + playerHealth); // console message
    }


    public void ammoPowerUp(int ammo)   // refills the ammo from a powerup
    {
        playerAmmo = ammo; // set ammo to the given amount
        Debug.Log("Ammo is: " + playerAmmo); // console message
    }
}
