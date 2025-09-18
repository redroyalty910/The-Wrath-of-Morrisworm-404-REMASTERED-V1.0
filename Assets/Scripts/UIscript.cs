using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIscript : MonoBehaviour
{
    private static UIscript _instance;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);// destroy a duplicate UI, for whem switching scenes from game over to title screen
        }
        else
        {
            _instance = this;
        }
}

    void Start()
    {
        DontDestroyOnLoad(gameObject);//to not delete UI with each scene change(toggling on and off ther UI is done elsewhere)
    }

    void Update()   //this displays a level requirement on the top of the screen depending on what scene is loaded
    {
        if (SceneManager.Instance.sceneNum == 1)
        {
            InformationManager.Instance.levelRequirementUI.text = "COMPLETE!";   //shows for a single framw when loading back to hub world
        }
        if (SceneManager.Instance.sceneNum == 2)
        {
            InformationManager.Instance.levelRequirementUI.text = "Defeat 25 Enemies! " + enemyTestScript.level1enemiesdead + "/25 Enemies Defeated";
        }
        if (SceneManager.Instance.sceneNum == 3)
        {
            InformationManager.Instance.levelRequirementUI.text = "Defeat 20 Enemies! " + enemyTestScript.level2enemiesdead + "/20 Enemies Defeated";
        }
        if (SceneManager.Instance.sceneNum == 4)
        {
            InformationManager.Instance.levelRequirementUI.text = "Defeat 15 Enemies! " + enemyTestScript.level3enemiesdead + "/15 Enemies Defeated";
        }
        if (SceneManager.Instance.sceneNum == 5)
        {
            InformationManager.Instance.levelRequirementUI.text = "Defeat 10 Enemies! " + enemyTestScript.level4enemiesdead + "/10 Enemies Defeated";
        }
        if (SceneManager.Instance.sceneNum == 6)
        {
            InformationManager.Instance.levelRequirementUI.text = "Defeat Morrisworm-404 + 5 Enemies! " + enemyTestScript.level5enemiesdead+ "/5 Enemies Defeated";
        }
    }

}