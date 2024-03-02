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

    private GameObject[] hearts;
    private int lives = 3;
    public GameObject background;
    public GameObject gameOverScreen;

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

        if (direction > 0f)
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
            transform.localScale = new Vector2(6.081014f, 5.968153f);
        }
        else if (direction < 0f)
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
            transform.localScale = new Vector2(-6.081014f, 5.968153f);
        }
        else
        {
            player.velocity = new Vector2(0, player.velocity.y);
        }

        if (Input.GetButtonDown("Jump") && isTouchingGround)
        {
            player.velocity = new Vector2(player.velocity.x, jumpSpeed);
        }

        WallSlide();
        WallJump();

        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "FallDetector")
        {
            transform.position = respawnPoint;
            lives -= 1;
            hearts[lives].SetActive(false);
            if (lives == 0)
            {
                gameOverScreen.SetActive(true);
                square.SetActive(false);
                background.SetActive(false);
            }
        }
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