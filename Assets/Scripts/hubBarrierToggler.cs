using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hubBarrierToggler: MonoBehaviour
{
    void Update()
    {
        if (SceneManager.Instance.sceneNum == 1)   //this scripts removes each barrier from the hub levels when each level requements are met
        {
            if (CompareTag("hubBarrier1") && enemyTestScript.level1beat)
            {
                gameObject.SetActive(false);
            }
            if (CompareTag("hubBarrier2") && enemyTestScript.level2beat)
            {
                gameObject.SetActive(false);
            }
            if (CompareTag("hubBarrier3") && enemyTestScript.level3beat)
            {
                gameObject.SetActive(false);
            }
            if (CompareTag("hubBarrier4") && enemyTestScript.level4beat)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
