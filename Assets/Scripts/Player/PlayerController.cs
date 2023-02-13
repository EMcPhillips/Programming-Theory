using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public GameManager gameManager;
    public float moveSpeed = 3.0f;
    public float strafeSpeed = 3.0f;
    public float jumpForce = 6.0f;
    public float jumpCooldown = 0.5f;
    public float sprintSpeed = 10.0f;
    public bool isOnGround = true;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameManager.gameOver == false)
        {
            MovePlayer();
            CameraControl();
            PlayerJump();
            ResetPosition();
        }
    }
    
    public void MovePlayer()
    {
        playerRb.transform.Translate(Vector3.forward * moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
        playerRb.transform.Translate(Vector3.right * strafeSpeed * Input.GetAxis("Horizontal") * Time.deltaTime);

        FirstObjective();

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = 5.0f;
            SecondObjective();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = 3.0f;
        }
    }

    public void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround == true)
        {
            playerRb.AddForce(Vector3.up * jumpForce * 100, ForceMode.Impulse);
            isOnGround = false;
            StartCoroutine(Waiting());
        }
    }
    IEnumerator Waiting()
    {
        yield return new WaitForSeconds(jumpCooldown);
        isOnGround = true;
    }

    public void CameraControl()
    {
        if (gameManager.gameOver == false || gameManager.gameWon == false)
        {
            float mouseSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 1);
            transform.localRotation = Quaternion.AngleAxis(transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity, Vector3.up);
            Camera.main.transform.localRotation = Quaternion.AngleAxis(Camera.main.transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * mouseSensitivity, Vector3.right);
        }
    }

    public void FirstObjective()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            gameManager.firstObjective = true;
        }
    }

    public void SecondObjective()
    {
        gameManager.secondObjective = true;
    }

    public void ResetPosition()
    {
        if (transform.position.y <= -5)
        {
            transform.position = new Vector3(44, 1, 1201);
        }
    }
}