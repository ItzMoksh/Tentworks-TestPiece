using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private GameObject controllers = null;
    [SerializeField] private List<PlayerModel> playerModels = null;

    private PlayerController playerController = null;

    private float moveHorizontal;
    private float moveVertical;
    private Vector2 movementP1;
    private Vector2 movementP2;

    private List<bool> vegetableNear = new List<bool>(2) { false, false };

    private void Awake()
    {
        playerController = controllers.GetComponentInChildren<PlayerController>();
    }

    private void Start()
    {
        playerController.UpdatePlayersModel(playerModels);
    }

    private void FixedUpdate()
    {
        CheckForPlayerOneMovementInput();
        CheckForPlayerTwoMovementInput();
    }

    private void Update()
    {
        CheckForPlayerOneInteractInput();
        CheckForPlayerTwoInteractInput();
    }

    private void CheckForPlayerOneInteractInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && vegetableNear[0])
        {
            playerController.OnVegetableInteract(PlayerId.PLAYER_ONE);
        }
    }

    private void CheckForPlayerTwoInteractInput()
    {
        if (Input.GetKeyDown(KeyCode.RightShift) && vegetableNear[1])
        {
            playerController.OnVegetableInteract(PlayerId.PLAYER_TWO);
        }
    }

    private void CheckForPlayerOneMovementInput()
    {
        moveHorizontal = Input.GetAxis("HorizontalOne") * playerController.playerModels[0].speed;
        moveVertical = Input.GetAxis("VerticalOne") * playerController.playerModels[0].speed;

        movementP1 = new Vector2(moveHorizontal, moveVertical);
        movementP1.Normalize();
        playerController.playerModels[0].playerRb.velocity = movementP1 * playerController.playerModels[0].speed;
    }

    private void CheckForPlayerTwoMovementInput()
    {
        moveHorizontal = Input.GetAxis("HorizontalTwo") * playerController.playerModels[1].speed;
        moveVertical = Input.GetAxis("VerticalTwo") * playerController.playerModels[1].speed;

        movementP2 = new Vector2(moveHorizontal, moveVertical);
        movementP2.Normalize();
        playerController.playerModels[1].playerRb.velocity = movementP2 * playerController.playerModels[1].speed;

    }

    private void StopPlayerMovement()
    {
        Debug.Log("Stop");
        playerModels.ForEach(player =>
        {
            player.playerRb.velocity = Vector2.zero;
        });
    }

    private void StopPlayerMovement(int playerId)
    {

    }

    public void CollisionCallBack(Collision2D collision, int playerId)
    {

    }

    public void CollisionExitCallBack(Collision2D collision, int playerId)
    {

    }

    public void TriggerEnterCallBack(Collider2D collider, int playerId)
    {
        if (collider.tag == "Vegetable")
        {
            Debug.LogFormat("Setting vegetableNear {0} to {1}",playerId,true);
            vegetableNear[playerId] = true;
        }
    }

    public void TriggerExitCallBack(Collider2D collider, int playerId)
    {
        if (collider.gameObject.tag == "Vegetable")
        {
             Debug.LogFormat("Setting vegetableNear {0} to {1}",playerId,false);
            vegetableNear[playerId] = false;
        }
    }
}
