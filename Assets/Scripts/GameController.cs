using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    //serialized field seralizing variable so it can be visible in unitys interface
    [SerializeField] private PlayerInput playerInput;
    private InputAction move;
    private InputAction restart;
    private InputAction quit;

    private bool isPaddleMoving;
    [SerializeField] private GameObject paddle;
    [SerializeField] private float paddleSpeed;
    private float moveDirection;

    [SerializeField] private GameObject brick;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private int score;

    [SerializeField] private TMP_Text endGameText;

    private BallController ball;

    // Start is called before the first frame update

    void Start()
    {
        DefinePlayerInput();
        CreateAllBricks();

        endGameText.gameObject.SetActive(false);

        ball = GameObject.FindObjectOfType<BallController>();
    }

    private void CreateAllBricks()
    {
        Vector2 brickPos = new Vector2(-9, 4.5f);

        for (int j = 0; j < 4; j++)
        {
            brickPos.y -= 1;
            brickPos.x = -9;

            for (int i = 0; i < 10; i++)
            {
                brickPos.x += 1.5f;
                Instantiate(brick, brickPos, Quaternion.identity);
            }
        }
    }

    public void UpdateScore()
    {
        score += 100;
        scoreText.text = "Score: " + score.ToString();

        if (score >= 4000)
        {
            endGameText.text = "YOU WIN!!!";
            endGameText.gameObject.SetActive(true);
            ball.ResetBall();
        }
    }


    void DefinePlayerInput()
    {
        //activiating the action map
        playerInput.currentActionMap.Enable();

        move = playerInput.currentActionMap.FindAction("MovePaddle");
        restart = playerInput.currentActionMap.FindAction("RestartGame");
        quit = playerInput.currentActionMap.FindAction("QuitGame");

        move.started += Move_started;
        move.canceled += Move_canceled;
        restart.started += Restart_started;
        quit.started += Quit_started;

        isPaddleMoving = false;
            
    }


    private void Quit_started(InputAction.CallbackContext obj)
    {
        throw new System.NotImplementedException();
    }

    private void Restart_started(InputAction.CallbackContext obj)
    {

    }

    private void FixedUpdate()
    {
        if (isPaddleMoving)
        {
            //movepaddle
            paddle.GetComponent<Rigidbody2D>().velocity = new Vector2(paddleSpeed * moveDirection, 0);
        }
        else
        {
            //stop the paddle
            paddle.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        }
    }


        private void Move_canceled(InputAction.CallbackContext obj)
    {
        isPaddleMoving = false;
    }

    private void Move_started(InputAction.CallbackContext obj)
    {
        isPaddleMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPaddleMoving)
        {
            moveDirection = move.ReadValue<float>();
        }
    }
}
