using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public GameObject player;
    public PlayerMovement lives;
    public EnemyDeath dead;
    public CollectPowerUp meat;
    public PlayerMovement characteristics;
    public EnemyMovement movement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
            if (collision.gameObject.tag == "Player" && dead.dead == false && movement.speed == 1)
            {
                movement.speed = movement.speed * -1;
                transform.localScale = new Vector2(5.98175f, 5.980824f);
                lives.lives -= 0.5f;
            }

            if (collision.gameObject.tag == "Player" && dead.dead == false && movement.speed == -1)
            {
                movement.speed = movement.speed * -1;
                transform.localScale = new Vector2(-5.98175f, 5.980824f);
                lives.lives -= 0.5f;
            }

            
                if (collision.gameObject.tag == "Player" && meat.meat == true)
                {
                    characteristics.speed = 6f;
                    characteristics.jumpSpeed = 8f;
                    player.transform.localScale = new Vector2(6f, 6f);
                    meat.meat = false;
                }
            
            }
        }

