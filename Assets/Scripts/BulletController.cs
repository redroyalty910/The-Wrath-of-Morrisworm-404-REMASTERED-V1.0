using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private Rigidbody2D body;

    private Vector2 velocity;

    void Start()   //is fot the direction the bullet will fly depending on the key pressed
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && InformationManager.Instance.playerAmmo > 0)
        {
            velocity = new Vector2(-1, 0);
            speed = 2000f;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)  && InformationManager.Instance.playerAmmo > 0)
        {
            velocity = new Vector2(1, 0);
            speed = 2000f;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && InformationManager.Instance.playerAmmo > 0)
        {
            velocity = new Vector2(0, -1);
            speed = 2000f;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && InformationManager.Instance.playerAmmo > 0)
        {
            velocity = new Vector2(0, 1);
            speed = 2000f;
        }

        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        body.velocity = velocity * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        enemyTestScript enemy = collision.GetComponent<enemyTestScript>();  //this is for getting the collided with enemys health, so that the health can be lowered according to what enemy was hit
                                                                            //*make enemies maybe take more damage as levels get higher
        if (collision.gameObject.tag == "level1enemy")
        {
            if (enemy != null)
            {
                enemy.enemyHurt(100);
                Destroy(this.gameObject);
            }
            Debug.Log("Bullet hit level1enemy");
        }
        if (collision.gameObject.tag == "level2enemy")
        {
            if (enemy != null)
            {
                enemy.enemyHurt(100);
                Destroy(this.gameObject);
            }
            Destroy(this.gameObject);
            Debug.Log("Bullet hit level2enemy");
        }
        if (collision.gameObject.tag == "level3enemy")
        {
            if (enemy != null)
            {
                enemy.enemyHurt(100);
                Destroy(this.gameObject);
            }
            Destroy(this.gameObject);
            Debug.Log("Bullet hit level3enemy");

        }
        if (collision.gameObject.tag == "level4enemy")
        {
            if (enemy != null)
            {
                enemy.enemyHurt(100);
                Destroy(this.gameObject);
            }
            Destroy(this.gameObject);
            Debug.Log("Bullet hit level4enemy");
        }
        if (collision.gameObject.tag == "level5enemy")
        {
            if (enemy != null)
            {
                enemy.enemyHurt(100);
                Destroy(this.gameObject);
            }
            Destroy(this.gameObject);
            Debug.Log("Bullet hit level5enemy");
        }
        if (collision.gameObject.tag == "morriswormenemy")
        {
            if (enemy != null)
            {
                enemy.enemyHurt(100);
                Destroy(this.gameObject);
            }
            Destroy(this.gameObject);
            Debug.Log("Bullet hit morrisworm");
        }
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
            Debug.Log("Bullet hit wall");
        }

    }
}

