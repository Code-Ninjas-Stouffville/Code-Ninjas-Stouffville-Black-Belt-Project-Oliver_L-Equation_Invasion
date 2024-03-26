using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float direction = 0f;
    public float speed = 3;
    public EnemyDeath alive;
    public float leftPosition;
    public float rightPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (alive.dead == false)
        {
            if (transform.position.x >= rightPosition)
            {
                speed = speed * -1;
                transform.localScale = new Vector2(5.98175f, 5.980824f);
            }

            if (transform.position.x <= leftPosition)
            {
                speed = speed * -1;
                transform.localScale = new Vector2(-5.98175f, 5.980824f);
            }
            float newXPosition = transform.position.x + speed * Time.deltaTime;
            float newYPosition = transform.position.y;
            Vector2 newPosition = new Vector2(newXPosition, newYPosition);
            transform.position = newPosition;
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
