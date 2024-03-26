using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public Sprite DeadEnemy;
    public bool dead = false;
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

        if (collision.gameObject.tag == "Player")
        {
            enemy.GetComponent<SpriteRenderer>().sprite = DeadEnemy;
            dead = true;
            Invoke("KillEnemy", 1);

        }
    }

    private void KillEnemy()
    {
        enemy.SetActive(false);
    }
}
