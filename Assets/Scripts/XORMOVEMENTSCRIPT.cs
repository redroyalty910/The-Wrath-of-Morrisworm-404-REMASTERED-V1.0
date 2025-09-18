using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XORMOVEMENTSCRIPT : MonoBehaviour
{
    public float xSpeed, ySpeed, maxSpeed, acceleration, deceleration, shootForce, endTime, duration, speedTimer, shootTimer, invincibilityTimer;
    //public float shootDelayTimer, lastShootTimer;
    private bool updateXPos, updateYPos, updateXNeg, updateYNeg, speedActive, invincibilityActive, gameOver;
    [SerializeField]
    Transform player1spawnpoint;
    private Transform spawnPoint;
    [SerializeField]
    Transform player1gameOverSpaqn;
    private Transform gameOverSpawnPoint;
    public bool shootIndicator;
    public GameObject projectile;
    private Animator anim;
    public Transform projectileSpawnPoint;
    public static bool hublevel1trigger, hublevel2trigger, hublevel3trigger, hublevel4trigger, hublevel5trigger, ableToShoot;  //bools
    private bool isShooting;  //for animation
    private string shootDirection;   //for animation


    void Start()
    {
        spawnPoint = player1spawnpoint;
        this.transform.position = spawnPoint.position;   //spawn point
        shootIndicator = false;
        ableToShoot = false;
        isShooting = false;
        xSpeed = 0;
        ySpeed = 0;
        maxSpeed = 7;
        acceleration = 10;
        deceleration = 10;
        updateXNeg = true;
        updateXPos = true;
        updateYNeg = true;
        updateYPos = true;
        duration = 15f;   //for power ups
        speedActive = false;
        invincibilityActive = false;
        speedTimer = 0f;
        shootTimer = 0f;
        invincibilityTimer = 0f;
        gameOver = false;
      //  shootDelayTimer = .5f;
       // lastShootTimer = Time.time;
        anim = GetComponent<Animator>();
        hublevel1trigger = false;
        hublevel2trigger = false;
        hublevel3trigger = false;
        hublevel4trigger = false;
        hublevel5trigger = false;
        shootDirection = "";
    }

    void Update()
    {
        //shooting control
        if (ableToShoot == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && InformationManager.Instance.playerAmmo > 0) //&& (Time.time >= (lastShootTimer + shootDelayTimer)) 
            {
                shootIndicator = true;
                isShooting = true;
                shootTimer = .4f;   //sets the duration so that the animation only plays for this long(animation length is very close to this)
                Debug.Log("shooting left");
                shootDirection = "left";
                //lastShootTimer = Time.time;
                InformationManager.Instance.UpdateAmmo();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && InformationManager.Instance.playerAmmo > 0) //&& (Time.time >= (lastShootTimer + shootDelayTimer))
            {
                shootIndicator = true;
                isShooting = true;
                shootTimer = .4f;
                Debug.Log("shooting right");
                shootDirection = "right";
                // lastShootTimer = Time.time;
                InformationManager.Instance.UpdateAmmo();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && InformationManager.Instance.playerAmmo > 0)  //&& (Time.time >= (lastShootTimer + shootDelayTimer))
            {
                shootIndicator = true;
                isShooting = true;
                shootTimer = .4f;
                Debug.Log("shooting shooting down");
                shootDirection = "down";
                // lastShootTimer = Time.time;
                InformationManager.Instance.UpdateAmmo();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && InformationManager.Instance.playerAmmo > 0)  //&& (Time.time >= (lastShootTimer + shootDelayTimer)) 
            {
                shootIndicator = true;
                isShooting = true;
                shootTimer = .4f;
                Debug.Log("shooting up");
                shootDirection = "up";
                //lastShootTimer = Time.time;
                InformationManager.Instance.UpdateAmmo();
            }
            else if ((Input.GetKeyDown(KeyCode.LeftArrow) || (Input.GetKeyDown(KeyCode.RightArrow) || (Input.GetKeyDown(KeyCode.DownArrow) || (Input.GetKeyDown(KeyCode.UpArrow)) && InformationManager.Instance.playerAmmo <= 0)))) // && (Time.time >= (lastShootTimer + shootDelayTimer))
            {
                Debug.Log("Ammo empty");
                AudioManager.Instance.PlaySoundEffect(0);   //play empty ammo noise
            }
            if (shootIndicator == true)
            {
                int rng = Random.Range(1, 4); //gives a 1, 2, or 3 (used to play 1 of  the shooting sound effects
                if (rng == 1)
                {
                    AudioManager.Instance.PlaySoundEffect(1);
                }
                else if (rng == 2)
                {
                    AudioManager.Instance.PlaySoundEffect(8);
                }
                else if (rng == 3)
                {
                    AudioManager.Instance.PlaySoundEffect(9);
                }
                Instantiate(projectile, projectileSpawnPoint.position, Quaternion.identity);
                shootIndicator = false;
            }
        }
        
       
        //timers for the power ups
        if (speedActive == true)
        {
            speedTimer = speedTimer - Time.deltaTime;
            if (speedTimer <= 0)
            {
                Debug.Log("Power up depleted");
                acceleration = 10;
                maxSpeed = 7;
                speedActive = false;
            }
        }
        if (isShooting == true)   //timer for the animations
        {
            shootTimer = shootTimer - Time.deltaTime;
            if (shootTimer <= 0f)
            {
                isShooting = false;
                shootDirection = "";
            }
        }
        if (invincibilityActive)  //invincibility frames
        {
            invincibilityTimer = invincibilityTimer - Time.deltaTime;
            if (invincibilityTimer <= 0)
            {
                Debug.Log("Invincibility time is up");
                invincibilityActive = false;
            }

        }
        
        if (InformationManager.Instance.playerHealth <= 0)
        {
            //sfx.PlayOneShot(soundEffects[4]);
            this.transform.position = spawnPoint.position;   //respawn player and give him full health 
            InformationManager.Instance.resetPlayerStats();
            AudioManager.Instance.PlaySoundEffect(5);  //player death sound



        }
        else if (InformationManager.Instance.playerLives < 0)     //debug
        {
            //this.transform.position = gameOverSpawnPoint.position;
            gameOver = true;
            Debug.Log("Game Over :(");

        }

    }

    void FixedUpdate()
    {

//player movement 
        if ((Input.GetKey(KeyCode.A)) && (xSpeed > -maxSpeed) && updateXNeg)
        {
            xSpeed = xSpeed - acceleration * Time.deltaTime;
        }
        else if ((Input.GetKey(KeyCode.D)) && (xSpeed < maxSpeed) && updateXPos)
        {
            xSpeed = xSpeed + acceleration * Time.deltaTime;
        }
        else
        {
            if (xSpeed > deceleration * Time.deltaTime)
            {
                xSpeed = xSpeed - deceleration * Time.deltaTime;
            }
            else if (xSpeed < -deceleration * Time.deltaTime)
            {
                xSpeed = xSpeed + deceleration * Time.deltaTime;
            }
            else
            {
                xSpeed = 0;
            }
        }

        if (isShooting == true)
        {
            if (shootDirection == "left")
            {
                anim.Play("XORSHOOTLEFT");
            }
            else if (shootDirection == "right")
            {
                anim.Play("XORSHOOTRIGHT");
            }
            else if (shootDirection == "down")
            {
                anim.Play("XORSHOOTRIGHT");
            }
            else if (shootDirection == "up")
            {
                anim.Play("XORSHOOTLEFT");
            }
        }
        else
        {
            if (xSpeed > 0)
            {
                anim.Play("XORRUNRIGHT");
            }
            if (xSpeed < 0)
            {
                anim.Play("XORRUNLEFT");
            }
            if (xSpeed == 0)
            {
                anim.Play("XORIDLE");
            }
        }



        if ((Input.GetKey(KeyCode.S)) && (ySpeed > -maxSpeed) && updateYPos)
        {
            ySpeed = ySpeed - acceleration * Time.deltaTime;
        }
        else if ((Input.GetKey(KeyCode.W)) && (ySpeed < maxSpeed) && updateYNeg)
        {
            ySpeed = ySpeed + acceleration * Time.deltaTime;
        }
        else
        {
            if (ySpeed > deceleration * Time.deltaTime)
            {
                ySpeed = ySpeed - deceleration * Time.deltaTime;
            }
            else if (ySpeed < -deceleration * Time.deltaTime)
            {
                ySpeed = ySpeed + deceleration * Time.deltaTime;
            }
            else
            {
                ySpeed = 0;
            }
        }

        Vector2 moving = new Vector2(xSpeed, ySpeed);
        GetComponent<Rigidbody2D>().velocity = moving;
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //for whern enemies hit the player (incvludes invincibility frames
        if (((collision.gameObject.tag == "level1enemy") || (collision.gameObject.tag == "level2enemy") || (collision.gameObject.tag == "level3enemy") || (collision.gameObject.tag == "level4enemy") || (collision.gameObject.tag == "level5enemy") || (collision.gameObject.tag == "morriswormenemy")) && (!invincibilityActive))
        {
         Debug.Log("Player was hit, invincibility active for 1.5 seconds");
         InformationManager.Instance.playerHurt();
         invincibilityTimer = 2f;
         invincibilityActive = true;
            if (InformationManager.Instance.playerHealth > 0)
            {
                if (InformationManager.Instance.playerHealth == 2)
                {
                    AudioManager.Instance.PlaySoundEffect(2);  //play hurt sound 1
                }
                else if (InformationManager.Instance.playerHealth == 1)
                {
                    AudioManager.Instance.PlaySoundEffect(10);  //play hurt sound 2
                }
            }
            else if (InformationManager.Instance.playerHealth == 0)
            {
                AudioManager.Instance.PlaySoundEffect(11);  //play hurt sound 3
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "speedPowerUp")  
        {
            Debug.Log("Speed Increased for 5 seconds");
            Destroy(collision.gameObject);
            acceleration = 20;
            maxSpeed = 14;
            speedTimer = 5f;
            speedActive = true;
            AudioManager.Instance.PlaySoundEffect(3); //powerup sound
        }
        if (collision.gameObject.tag == "ammoPowerUp")
        {
            Debug.Log("ammo replenished");
            Destroy(collision.gameObject);
            InformationManager.Instance.ammoPowerUp(60);
            AudioManager.Instance.PlaySoundEffect(3); //powerup sound

        }
        if ((collision.gameObject.tag == "level1trigger"))   //all triggers fore whrn player is in the load zone for a level in the hub world
        {
            hublevel1trigger = true;
            Debug.Log("player entered level1 load zone");
        }


        if ((collision.gameObject.tag == "level2trigger"))
        {
            hublevel2trigger = true;
            Debug.Log("player entered level2 load zone");
        }


        if ((collision.gameObject.tag == "level3trigger"))
        {
            hublevel3trigger = true;
            Debug.Log("player entered level3 load zone");
        }


        if ((collision.gameObject.tag == "level4trigger"))
        {
            hublevel4trigger = true;
            Debug.Log("player entered level4 load zone");
        }


        if ((collision.gameObject.tag == "level5trigger"))
        {
            hublevel5trigger = true;
            Debug.Log("player entered level5 load zone");
        }



    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("level1trigger"))    //all turn off the trigger that was turned on whrn plauer leaves a level load zone 
        {
            hublevel1trigger = false;
            Debug.Log("player exited level1 load zone");
        }
        if (collision.gameObject.CompareTag("level2trigger"))
        {
            hublevel2trigger = false;
            Debug.Log("player exited level2 load zone");
        }
        if (collision.gameObject.CompareTag("level3trigger"))
        {
            hublevel3trigger = false;
            Debug.Log("player exited level3 load zone");
        }
        if (collision.gameObject.CompareTag("level4trigger"))
        {
            hublevel4trigger = false;
            Debug.Log("player exited level4 load zone");
        }
        if (collision.gameObject.CompareTag("level5trigger"))
        {
            hublevel5trigger = false;
            Debug.Log("player exited level5 load zone");
        }
    }

    public void Respawn()   //set player back to spawn(not even used since player is sent back to hub world whrn dead
    {
        transform.position = player1spawnpoint.position;
        xSpeed = 0;
        ySpeed = 0;
    }

 
}
