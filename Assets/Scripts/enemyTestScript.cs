using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class enemyTestScript : MonoBehaviour
{

    public float xSpeed, ySpeed, maxSpeed, acceleration, deceleration;
    private bool updateXPos, updateYPos, updateXNeg, updateYNeg;
    private GameObject playerObject;
    private Animator anim;
    private bool facingRight;
    public int enemyHealth;
    public static int level1enemiesdead = 0, level2enemiesdead = 0, level3enemiesdead = 0, level4enemiesdead = 0, level5enemiesdead = 0, morriswormdead = 0;   //for level requirements
    public static bool level1requirementmet = false, level2requirementmet = false, level3requirementmet = false, level4requirementmet = false, level5requirementmet = false, morriswormrequirement = false;  //level requirements
    public static bool level1beat = false, level2beat = false, level3beat = false, level4beat = false;
  


    void Start()
    {
        playerObject = GameObject.Find("XOR");  
        xSpeed = 0;
        ySpeed = 0;
        maxSpeed = 2;
        acceleration = 10;
        deceleration = 10;
        updateXNeg = true;
        updateXPos = true;
        updateYNeg = true;
        updateYPos = true;
        anim = GetComponent<Animator>();
        facingRight = true;
        //next 5 set all the health for different enemy types
        if (CompareTag("level1enemy"))
        {
            enemyHealth = 100;
        }
        else if (CompareTag("level2enemy"))
        {
            enemyHealth = 300;
        }
        else if (CompareTag("level3enemy"))
        {
            enemyHealth = 500;
        }
        else if (CompareTag("level4enemy"))
        {
            enemyHealth = 1000;
        }
        else if (CompareTag("level5enemy"))
        {
            enemyHealth = 1000;
            maxSpeed = 3;
        }
        else if (CompareTag("morriswormenemy"))
        {
            enemyHealth = 10000;
            maxSpeed = 1;
        }
    }
void Update()
{    //enemy movement
    Vector2 dir = (playerObject.transform.position - transform.position).normalized;

    if (xSpeed > 0f || xSpeed < 0f)
    {
            if (facingRight && xSpeed < 0f)
            {
                Vector3 localScale = this.transform.localScale;
                localScale.x *= -1f;
                this.transform.localScale = localScale;
                facingRight = false;
            }
            else if (!facingRight && xSpeed > 0f)
            {
                Vector3 localScale = this.transform.localScale;
                localScale.x *= -1f;
                this.transform.localScale = localScale;
                facingRight = true;
            }
    }

    }
    void FixedUpdate()
    {
        Vector2 dir = (playerObject.transform.position - transform.position).normalized;

        if ((dir.x < 0) && (xSpeed > -maxSpeed) && updateXNeg)
            xSpeed = xSpeed - acceleration * Time.deltaTime;
        else if ((dir.x > 0) && (xSpeed < maxSpeed) && updateXPos)
            xSpeed = xSpeed + acceleration * Time.deltaTime;
        else
        {
            if (xSpeed > deceleration * Time.deltaTime)
                xSpeed = xSpeed - deceleration * Time.deltaTime;
            else if (xSpeed < -deceleration * Time.deltaTime)
                xSpeed = xSpeed + deceleration * Time.deltaTime;
            else
                xSpeed = 0;
        }

        if ((dir.y < 0) && (ySpeed > -maxSpeed) && updateYPos)
            ySpeed = ySpeed - acceleration * Time.deltaTime;
        else if ((dir.y > 0) && (ySpeed < maxSpeed) && updateYNeg)
            ySpeed = ySpeed + acceleration * Time.deltaTime;
        else
        {
            if (ySpeed > deceleration * Time.deltaTime)
                ySpeed = ySpeed - deceleration * Time.deltaTime;
            else if (ySpeed < -deceleration * Time.deltaTime)
                ySpeed = ySpeed + deceleration * Time.deltaTime;
            else
                ySpeed = 0;
        }

        Vector2 moving = new Vector2(xSpeed, ySpeed);
        GetComponent<Rigidbody2D>().velocity = moving;

    }

    public void enemyHurt(int amount)   //takes away health by the specified amount in the bullet collision function call, and adds to the tracker that tracks the level requirements
    {
        enemyHealth = enemyHealth - amount;
        InformationManager.Instance.playerScore = InformationManager.Instance.playerScore + 100;

        if (enemyHealth <= 0)
        {
            if (CompareTag("level1enemy"))
            {
                level1enemiesdead++;
                Debug.Log("level1enemiesdead: " + level1enemiesdead);
            }
            else if (CompareTag("level2enemy"))
            {
                level2enemiesdead++;
                Debug.Log("level2enemiesdead: " + level2enemiesdead);

            }
            else if (CompareTag("level3enemy"))
            {
                level3enemiesdead++;
                Debug.Log("level3enemiesdead: " + level2enemiesdead);
            }
            else if (CompareTag("level4enemy"))
            {
                level4enemiesdead++;
                Debug.Log("level4enemiesdead: " + level2enemiesdead);
            }
            else if (CompareTag("level5enemy"))
            {
                level5enemiesdead++;
                Debug.Log("level5enemiesdead: " + level2enemiesdead);
            }
            else if (CompareTag("morriswormenemy"))
            {
                morriswormdead++;
                Debug.Log("morrisworm is dead");
            }
            deleteEnemy();
        }
    }

    void deleteEnemy()    //kills enemy if bullet hits them
    {
        Destroy(this.gameObject);
        InformationManager.Instance.UpdateScore();
        Debug.Log("Bullet killed enemy");
        int rng = Random.Range(1, 4); //gives a 1, 2, or 3 (used to play 1 of e of the death sound effects
        if (rng == 1)
        {
            AudioManager.Instance.PlaySoundEffect(4);
        }
        else if (rng == 2)
        {
            AudioManager.Instance.PlaySoundEffect(6);
        }
        else if (rng == 3)
        {
            AudioManager.Instance.PlaySoundEffect(7);
        }
    }
} 


