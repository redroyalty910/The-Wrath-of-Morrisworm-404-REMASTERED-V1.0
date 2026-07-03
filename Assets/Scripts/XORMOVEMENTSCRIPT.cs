using System.Collections;  // package for unity collections
using System.Collections.Generic; // package for gerneric collections
using UnityEngine; // main unity engine library

public class XORMOVEMENTSCRIPT : MonoBehaviour // class that controls XOR player behavior
{
    public float xSpeed, ySpeed, maxSpeed, acceleration, deceleration, shootForce, endTime, duration, speedTimer, shootTimer, invincibilityTimer; // movement, shooting, timer
    
    private bool updateXPos, updateYPos, updateXNeg, updateYNeg, speedActive, invincibilityActive, gameOver; // movement locks and state flags
    
    [SerializeField]
    Transform player1spawnpoint; // assigned spawn location

    private Transform spawnPoint; // active spawn point reference

    [SerializeField]
    Transform player1gameOverSpawn; // game-over spawn reference

    public bool shootIndicator; // is TRUE when a shot SHOULD spawn
    public GameObject projectile; // bullet prefab
    private Animator anim; // player animator
    public Transform projectileSpawnPoint; // where bullets will spawn from

    public static bool hublevel1trigger, hublevel2trigger, hublevel3trigger, hublevel4trigger, hublevel5trigger, ableToShoot;  // global hub / shoot flags

    private bool isShooting;  // tracks shooting animation state
    private string shootDirection;   // stores shot direction for animation


    void Start()
    {
        spawnPoint = player1spawnpoint; // use player1 spawn for spawnPoint
        this.transform.position = spawnPoint.position;   // move XOR to spawn

        shootIndicator = false; // no shot is queued, hence why it is "false"
        ableToShoot = false; // shooting starts disabled
        isShooting = false; // not shooting yet

        xSpeed = 0; // no horizontal speed
        ySpeed = 0; // no vertical speed

        maxSpeed = 7; // XOR's normal max speed
        acceleration = 10; // XOR's default acceleration
        deceleration = 10; // XOR's default deceleration

        updateXNeg = true; // Allow left movement
        updateXPos = true; // Allow right movement
        updateYNeg = true; // Allow up movement
        updateYPos = true; // Allow down movement

        duration = 15f;   // general power-up duraton

        speedActive = false; // speed boost inactive
        invincibilityActive = false; // invincibility inactive

        speedTimer = 0f; // reset speed timer
        shootTimer = 0f; // reset shoot timer
        invincibilityTimer = 0f; // reset invincibility timer
        gameOver = false; // game is not over

        anim = GetComponent<Animator>(); // get XOR's animator component

        hublevel1trigger = false; // not in level 1 zone
        hublevel2trigger = false; // not in level 2 zone
        hublevel3trigger = false; // not in level 3 zone
        hublevel4trigger = false; // not in level 4 zone
        hublevel5trigger = false; // not in level 5 zone

        shootDirection = ""; // no shoot direction YET
    }

    void Update() // update runs once per frame
    {
        if (ableToShoot == true) // if ableToShoot is true....
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && InformationManager.Instance.playerAmmo > 0) // shoot left
            {
                shootIndicator = true; // queue bullet spawn
                isShooting = true; // start shoot animation
                shootTimer = .4f;   // shoot animation time
                Debug.Log("shooting left"); // console message
                shootDirection = "left"; // set shoot direction for animation
                InformationManager.Instance.UpdateAmmo(); // spend ammo in the process
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && InformationManager.Instance.playerAmmo > 0) // shoot right
            {
                shootIndicator = true; // queue bullet spawn
                isShooting = true; // queue bullet spawn
                shootTimer = .4f; // shoot animation time
                Debug.Log("shooting right"); // console message
                shootDirection = "right"; // set shoot direction for animation
                InformationManager.Instance.UpdateAmmo(); // spend ammo in the process
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && InformationManager.Instance.playerAmmo > 0)  // shoot down
            {
                shootIndicator = true; // queue bullet spawn
                isShooting = true; // queue bullet spawn
                shootTimer = .4f; // shoot animation time
                Debug.Log("shooting shooting down"); // console message
                shootDirection = "down"; // set shoot direction for animation
                InformationManager.Instance.UpdateAmmo(); // spend ammo in the process
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && InformationManager.Instance.playerAmmo > 0)  // shoot up
            {
                shootIndicator = true; // queue bullet spawn
                isShooting = true; // queue bullet spawn
                shootTimer = .4f; // shoot animation time
                Debug.Log("shooting up"); // console message
                shootDirection = "up"; // set shoot direction for animation
                InformationManager.Instance.UpdateAmmo(); // spend ammo in the process
            }
            else if ((Input.GetKeyDown(KeyCode.LeftArrow) || (Input.GetKeyDown(KeyCode.RightArrow) || (Input.GetKeyDown(KeyCode.DownArrow) || (Input.GetKeyDown(KeyCode.UpArrow)) && InformationManager.Instance.playerAmmo <= 0))))
                // if the player tries to shoot but has no ammo...
            {
                Debug.Log("Ammo empty"); // console message
                AudioManager.Instance.PlaySoundEffect(0);   // play empty ammo noise
            }
            if (shootIndicator == true) // if a shot was queued
            {
                int rng = Random.Range(1, 4); // picks between either 1, 2 or 3
                if (rng == 1)
                {
                    AudioManager.Instance.PlaySoundEffect(1); // play sound 1
                }
                else if (rng == 2)
                {
                    AudioManager.Instance.PlaySoundEffect(8); // sound 2
                }
                else if (rng == 3)
                {
                    AudioManager.Instance.PlaySoundEffect(9); // or sound 3
                }
                Instantiate(projectile, projectileSpawnPoint.position, Quaternion.identity); // spawn the bullet
                shootIndicator = false; // and clear the shot queue
            }
        }
        
       
        //timers for the power ups
        if (speedActive == true) // if speed boost is active
        {
            speedTimer = speedTimer - Time.deltaTime; // count down timer for the power-up
            if (speedTimer <= 0) // if the speed-boost ended...
            {
                Debug.Log("Power up depleted"); // console message
                acceleration = 10; // default
                maxSpeed = 7; // default
                speedActive = false; // default
            }
        }
        if (isShooting == true) // if the shooting animation is active
        {
            shootTimer = shootTimer - Time.deltaTime; // count down timer for the shooting animation
            if (shootTimer <= 0f) // if the animation time has ended
            {
                isShooting = false; // cease the shooting state
                shootDirection = ""; // clear direction
            }
        }
        if (invincibilityActive)  // if player is invincible
        {
            invincibilityTimer = invincibilityTimer - Time.deltaTime; // coiunt down the timer
            if (invincibilityTimer <= 0) // if invincibility time is up
            {
                Debug.Log("Invincibility time is up"); // console message
                invincibilityActive = false; // player can be hit again
            }

        }
        
        if (InformationManager.Instance.playerHealth <= 0) // if the player is dead
        {
            this.transform.position = spawnPoint.position; // respawn player and give them full health 
            InformationManager.Instance.resetPlayerStats(); // reset the players stats
            AudioManager.Instance.PlaySoundEffect(5);  // player death sound



        }
        else if (InformationManager.Instance.playerLives < 0)     // if lives are below zero
        {
            gameOver = true; // it's game over
            Debug.Log("Game Over :("); // and this is the console message

        }

    }

    void FixedUpdate()
    {
        if ((Input.GetKey(KeyCode.A)) && (xSpeed > -maxSpeed) && updateXNeg) // move left
        {
            xSpeed = xSpeed - acceleration * Time.deltaTime; // accelerate left
        }
        else if ((Input.GetKey(KeyCode.D)) && (xSpeed < maxSpeed) && updateXPos) // move right
        {
            xSpeed = xSpeed + acceleration * Time.deltaTime; // accelerate right
        }
        else // no horizontal input
        {
            if (xSpeed > deceleration * Time.deltaTime) // if moving right
            {
                xSpeed = xSpeed - deceleration * Time.deltaTime; // slow right movement
            }
            else if (xSpeed < -deceleration * Time.deltaTime) // if moving left
            {
                xSpeed = xSpeed + deceleration * Time.deltaTime; // slow left movement
            }
            else // if almost stopped
            {
                xSpeed = 0; // stop horizontal movement
            }
        }

        if (isShooting == true) // prioritize shooting animations
        {
            if (shootDirection == "left") // shooting left
            {
                anim.Play("XORSHOOTLEFT"); // play left shoot animation
            }
            else if (shootDirection == "right") // shooting right
            {
                anim.Play("XORSHOOTRIGHT"); // play right shoot animation
            }
            else if (shootDirection == "down") // shooting down
            {
                anim.Play("XORSHOOTRIGHT"); // reuses right shoot animation
            }
            else if (shootDirection == "up") // shooting up
            {
                anim.Play("XORSHOOTLEFT"); // reuses left shoot animation
            }
        }
        else // if not shooting
        {
            if (xSpeed > 0) // moving right
            {
                anim.Play("XORRUNRIGHT"); // play right run animation
            }

            if (xSpeed < 0) // moving left
            {
                anim.Play("XORRUNLEFT"); // play left run animation
            }

            if (xSpeed == 0) // not moving horizontally
            {
                anim.Play("XORIDLE"); // play idle animation
            }
        }

        if ((Input.GetKey(KeyCode.S)) && (ySpeed > -maxSpeed) && updateYPos) // move down
        {
            ySpeed = ySpeed - acceleration * Time.deltaTime; // accelerate down
        }
        else if ((Input.GetKey(KeyCode.W)) && (ySpeed < maxSpeed) && updateYNeg) // move up
        {
            ySpeed = ySpeed + acceleration * Time.deltaTime; // accelerate up
        }
        else // No vertical input
        {
            if (ySpeed > deceleration * Time.deltaTime) // if moving up
            {
                ySpeed = ySpeed - deceleration * Time.deltaTime; // slow upward movement
            }
            else if (ySpeed < -deceleration * Time.deltaTime) // if moving down
            {
                ySpeed = ySpeed + deceleration * Time.deltaTime; // slow downward movement
            }
            else // if almost stopped
            {
                ySpeed = 0; // stop vertical movement
            }
        }

        Vector2 moving = new Vector2(xSpeed, ySpeed); // store movement vector
        GetComponent<Rigidbody2D>().velocity = moving; // apply movement to Rigidbody2D
    }


    private void OnCollisionEnter2D(Collision2D collision) // runs on physical collision
    {
        // when enemies hit the player
        if (((collision.gameObject.tag == "level1enemy") || (collision.gameObject.tag == "level2enemy") || (collision.gameObject.tag == "level3enemy") || (collision.gameObject.tag == "level4enemy") || (collision.gameObject.tag == "level5enemy") || (collision.gameObject.tag == "morriswormenemy")) && (!invincibilityActive))
        {
         Debug.Log("Player was hit, invincibility active for 1.5 seconds"); // console message
         InformationManager.Instance.playerHurt(); // damage the player
         invincibilityTimer = 2f; // start invincibility timer
         invincibilityActive = true; // enable invincibility
            if (InformationManager.Instance.playerHealth > 0) // if XOR is still alive ...
            {
                if (InformationManager.Instance.playerHealth == 2) // first hurt state
                {
                    AudioManager.Instance.PlaySoundEffect(2);  // play hurt sound 1
                }
                else if (InformationManager.Instance.playerHealth == 1) // secpnd hurt state
                {
                    AudioManager.Instance.PlaySoundEffect(10);  // play hurt sound 2
                }
            }
            else if (InformationManager.Instance.playerHealth == 0) // if health reached zero
            {
                AudioManager.Instance.PlaySoundEffect(11);  // play hurt sound 3
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) // runs when entering trigger
    {
        if (collision.gameObject.tag == "speedPowerUp")  // if player collides with speed powerup
        {
            Debug.Log("Speed Increased for 5 seconds"); // console message
            Destroy(collision.gameObject); // remove pickup
            acceleration = 20; // increase acceleartion
            maxSpeed = 14; // boost speed
            speedTimer = 5f; // set speed boost timer
            speedActive = true; // set speed boost active
            AudioManager.Instance.PlaySoundEffect(3); // speed-powerup sound
        }
        if (collision.gameObject.tag == "ammoPowerUp") // if player collides with ammo powerup
        {
            Debug.Log("ammo replenished"); // console message
            Destroy(collision.gameObject); // remove the pickup
            InformationManager.Instance.ammoPowerUp(60); // add ammo
            AudioManager.Instance.PlaySoundEffect(3); // powerup sound

        }
        if ((collision.gameObject.tag == "level1trigger"))   // level 1 zone
        {
            hublevel1trigger = true; // enable level 1 trigger
            Debug.Log("player entered level1 load zone"); // console message
        }


        if ((collision.gameObject.tag == "level2trigger")) // level 2 zone
        {
            hublevel2trigger = true; // enable level 2 trigger
            Debug.Log("player entered level2 load zone"); // console message
        }


        if ((collision.gameObject.tag == "level3trigger")) // level 3 zone
        {
            hublevel3trigger = true; // enable level 3 trigger
            Debug.Log("player entered level3 load zone"); // console message
        }


        if ((collision.gameObject.tag == "level4trigger")) // level 4 zone
        { 
            hublevel4trigger = true; // enable level 4 trigger
            Debug.Log("player entered level4 load zone"); // console message
        }


        if ((collision.gameObject.tag == "level5trigger")) // level 5 zone
        {
            hublevel5trigger = true; // enable level 5 trigger
            Debug.Log("player entered level5 load zone"); // console message
        }



    }

    private void OnTriggerExit2D(Collider2D collision) // runs when exiting trigger
    {
        if (collision.gameObject.CompareTag("level1trigger"))    // leave level 1 zone
        {
            hublevel1trigger = false; // disable level 1 trigger
            Debug.Log("player exited level1 load zone"); // console message
        }
        if (collision.gameObject.CompareTag("level2trigger")) // leave level 1 zone
        {
            hublevel2trigger = false; // disable level 1 trigger
            Debug.Log("player exited level2 load zone"); // console message
        }
        if (collision.gameObject.CompareTag("level3trigger")) // leave level 1 zone
        {
            hublevel3trigger = false; // disable level 1 trigger
            Debug.Log("player exited level3 load zone"); // console message
        }
        if (collision.gameObject.CompareTag("level4trigger")) // leave level 1 zone
        {
            hublevel4trigger = false; // disable level 1 trigger
            Debug.Log("player exited level4 load zone"); // console message
        }
        if (collision.gameObject.CompareTag("level5trigger")) // leave level 1 zone
        {
            hublevel5trigger = false; // disable level 1 trigger
            Debug.Log("player exited level5 load zone"); // console message
        }
    }

    public void Respawn()   //set player back to spawn(not even used since player is sent back to hub world whrn dead
    {
        transform.position = player1spawnpoint.position;
        xSpeed = 0;
        ySpeed = 0;
    }

 
}
