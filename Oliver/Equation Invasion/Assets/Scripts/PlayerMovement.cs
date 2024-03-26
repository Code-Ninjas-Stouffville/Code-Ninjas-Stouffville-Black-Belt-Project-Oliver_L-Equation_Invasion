using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpSpeed = 4f;
    private float direction = 0f;
    private bool isFacingRight = true;
    private Rigidbody2D player;
    public GameObject square;

    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;
    private bool isWallJumping;
    private bool isWalled;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(6f, 12f);

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;
    public Transform wallCheck;
    public LayerMask wallLayer;

    private Vector3 respawnPoint;
    public GameObject fallDetector;

    public GameObject[] hearts;
    public float lives = 3;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject halfheart1;
    public GameObject halfheart2;
    public GameObject halfheart3;
    public GameObject background;
    public GameObject gameOverScreen;

    public CollectPowerUp powerMeat;

    public bool doubleJump = false;
    public bool doubleJumpSkill = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
        hearts = GameObject.FindGameObjectsWithTag("heart");
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        direction = Input.GetAxis("Horizontal");

        float scaleX = transform.localScale.x;
        float scaleY = transform.localScale.y;

        if (direction > 0f)
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
            transform.localScale = new Vector2(Mathf.Abs(scaleX), scaleY);
        }
        else if (direction < 0f)
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
            transform.localScale = new Vector2(-Mathf.Abs(scaleX), scaleY);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTouchingGround)
            {
                player.velocity = new Vector2(player.velocity.x, jumpSpeed);
                doubleJump = true;
            }
            else if (doubleJump && doubleJumpSkill)
            {
                player.velocity = new Vector2(player.velocity.x, jumpSpeed);
                doubleJump = false;
            }
        }

        WallSlide();
        WallJump();

        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);

        if (lives == 2.5)
        {
            heart3.SetActive(false);
        }

        if (lives == 2)
        {
            halfheart3.SetActive(false);
            heart3.SetActive(false);
        }

        if (lives == 1.5)
        {
            heart2.SetActive(false);
        }

        if (lives == 1)
        {
            halfheart2.SetActive(false);
            heart2.SetActive(false);
        }

        if (lives == 0.5)
        {
            heart1.SetActive(false);
        }

        if (lives == 0)
        {
            halfheart1.SetActive(false);
            heart1.SetActive(false);
            gameOverScreen.SetActive(true);
            square.SetActive(false);
            background.SetActive(false);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "FallDetector")
        {
            transform.position = respawnPoint;
            lives -= 1;
        }

        if (collision.gameObject.tag == "doubleJumpToken")
        {
            doubleJumpSkill = true;
            Invoke("regularJump", 20f);
            collision.gameObject.SetActive(false);
        }
    }

    void regularJump()
    {
        doubleJumpSkill = false;
    }

    

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !isTouchingGround && direction != 0f)
        {
            isWallSliding = true;
            player.velocity = new Vector2(player.velocity.x, Mathf.Clamp(player.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            player.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

}