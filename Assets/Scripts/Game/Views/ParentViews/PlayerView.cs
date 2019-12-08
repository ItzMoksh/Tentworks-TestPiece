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
    private List<bool> choppingBoardNear = new List<bool>(2) { false, false };
    private List<bool> restrictMovement = new List<bool>(2) { false, false };

    #region ------------------------------------------- Monobehaviour ----------------------------------------------
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

    #endregion ------------------------------------------------------------------------------------------------------

    #region ------------------------------------------- Player Input ----------------------------------------------

    private void CheckForPlayerOneInteractInput()
    {
        if (restrictMovement[0])
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (vegetableNear[0])
            {
                playerController.OnVegetableInteract(PlayerId.PLAYER_ONE);
            }
            else if (choppingBoardNear[0])
            {
                playerController.PlaceVegetableOnBoard(PlayerId.PLAYER_ONE);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (choppingBoardNear[0])
            {
                playerController.PickSaladFromBoard(PlayerId.PLAYER_ONE);
            }
        }
    }

    private void CheckForPlayerTwoInteractInput()
    {
        if (restrictMovement[1])
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            if (vegetableNear[1])
            {
                playerController.OnVegetableInteract(PlayerId.PLAYER_TWO);
            }
            else if (choppingBoardNear[1])
            {
                playerController.PlaceVegetableOnBoard(PlayerId.PLAYER_TWO);
            }
        }
        if (Input.GetKeyDown(KeyCode.RightAlt))
        {
            if (choppingBoardNear[1])
            {
                playerController.PickSaladFromBoard(PlayerId.PLAYER_TWO);
            }
        }
    }

    private void CheckForPlayerOneMovementInput()
    {
        if (restrictMovement[0])
        {
            return;
        }
        moveHorizontal = Input.GetAxis("HorizontalOne") * playerController.playerModels[0].speed;
        moveVertical = Input.GetAxis("VerticalOne") * playerController.playerModels[0].speed;

        movementP1 = new Vector2(moveHorizontal, moveVertical);
        movementP1.Normalize();
        playerController.playerModels[0].playerRb.velocity = movementP1 * playerController.playerModels[0].speed;
    }

    private void CheckForPlayerTwoMovementInput()
    {
        if (restrictMovement[1])
        {
            return;
        }
        moveHorizontal = Input.GetAxis("HorizontalTwo") * playerController.playerModels[1].speed;
        moveVertical = Input.GetAxis("VerticalTwo") * playerController.playerModels[1].speed;

        movementP2 = new Vector2(moveHorizontal, moveVertical);
        movementP2.Normalize();
        playerController.playerModels[1].playerRb.velocity = movementP2 * playerController.playerModels[1].speed;

    }

    #endregion --------------------------------------------------------------------------------------------------------

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

    public void RestrictMovement(PlayerId playerId, bool doRestrict)
    {
        playerModels[(int)playerId].playerRb.velocity = Vector2.zero; //To prevent player from shifting away if they interact while in motion.
        restrictMovement[(int)playerId] = doRestrict;
    }

    #region ------------------------------------------- Player Collisions ----------------------------------------------

    public void CollisionCallback(Collision2D collision, int playerId)
    {

    }

    public void CollisionExitCallback(Collision2D collision, int playerId)
    {

    }

    public void TriggerEnterCallback(Collider2D collider, int playerId)
    {
        switch (collider.tag)
        {
            case "Vegetable":
                {
                    // Debug.LogFormat("Setting vegetableNear {0} to {1}",playerId,true);
                    vegetableNear[playerId] = true;
                }
                break;
            case "ChoppingBoard":
                {
                    choppingBoardNear[playerId] = true;
                }
                break;
            case "Dustbin":
                {

                }
                break;
        }
    }

    public void TriggerExitCallback(Collider2D collider, int playerId)
    {
        switch (collider.tag)
        {
            case "Vegetable":
                {
                    // Debug.LogFormat("Setting vegetableNear {0} to {1}",playerId,false);
                    vegetableNear[playerId] = false;
                }
                break;
            case "ChoppingBoard":
                {
                    choppingBoardNear[playerId] = false;
                }
                break;
            case "Dustbin":
                {

                }
                break;
        }
    }

    #endregion ------------------------------------------------------------------------------------------------------------

}
