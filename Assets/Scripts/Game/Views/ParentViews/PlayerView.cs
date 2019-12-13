using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles all visual changes and Inputs on players
/// </summary>
public class PlayerView : MonoBehaviour
{
    [SerializeField] private GameObject controllers = null;
    [SerializeField] private List<PlayerModel> playerModels = null;

    private PlayerController playerController = null;

    private float moveHorizontal = 0f;
    private float moveVertical = 0f;

    private List<Vector2> movement = new List<Vector2>() { Vector2.zero, Vector2.zero };

    private List<InteractablesNear> interactablesNear = new List<InteractablesNear>(2) { new InteractablesNear(), new InteractablesNear() };

    private List<bool> restrictMovement = new List<bool>(2) { false, false };


    #region ------------------------------------------- Monobehaviour ----------------------------------------------
    private void Awake()
    {
        playerController = controllers.GetComponentInChildren<PlayerController>();
    }

    private void Start()
    {

        playerModels[0].saladInHand = new SaladModel(); //Needed for bug fix.
        playerModels[1].saladInHand = new SaladModel(); //Needed for bug fix.

        playerController.UpdatePlayersModel(playerModels);
    }

    private void FixedUpdate()
    {
        CheckForPlayerMovementInput(0);
        CheckForPlayerMovementInput(1);
    }

    private void Update()
    {
        CheckForPlayerInteractInput(0);
        CheckForPlayerInteractInput(1);
    }

    #endregion ------------------------------------------------------------------------------------------------------

    #region ------------------------------------------- Player Input ----------------------------------------------

    /// <summary>
    /// Checks for player interaciton inputs
    /// </summary>
    /// <param name="playerId">Specify whcih player to check movement for.</param>
    private void CheckForPlayerInteractInput(int playerId)
    {
        if (restrictMovement[playerId])
        {
            return;
        }
        if ((Input.GetKeyDown(KeyCode.LeftShift) && playerId == 0) || (Input.GetKeyDown(KeyCode.RightShift) && playerId == 1))
        {
            if (interactablesNear[playerId].vegetable)
            {
                playerController.OnVegetableInteract((PlayerId)playerId);
            }
            else if (interactablesNear[playerId].choppingBoard)
            {
                playerController.PlaceVegetableOnBoard((PlayerId)playerId);
            }
            else if (interactablesNear[playerId].dustbin)
            {
                playerController.ThrowSalad((PlayerId)playerId);
            }
            else if (interactablesNear[playerId].plate)
            {
                playerController.PlaceVegetableOnPlate((PlayerId)playerId);
            }
        }
        if ((Input.GetKeyDown(KeyCode.LeftAlt) && playerId == 0) || (Input.GetKeyDown(KeyCode.RightAlt) && playerId == 1))
        {
            if (interactablesNear[playerId].choppingBoard)
            {
                playerController.PickSaladFromBoard((PlayerId)playerId);
            }
            else if (interactablesNear[playerId].plate)
            {
                playerController.PickVegetableFromPlate((PlayerId)playerId);
            }
        }
    }

    /// <summary>
    /// Checks for movement Input for player one.
    /// </summary>
    private void CheckForPlayerMovementInput(int playerId)
    {

        if (restrictMovement[playerId])
        {
            return;
        }
        if (playerId == 0)
        {
            moveHorizontal = Input.GetAxis("HorizontalOne") * playerController.playerModels[playerId].speed;
            moveVertical = Input.GetAxis("VerticalOne") * playerController.playerModels[playerId].speed;
        }
        else
        {
            moveHorizontal = Input.GetAxis("HorizontalTwo") * playerController.playerModels[playerId].speed;
            moveVertical = Input.GetAxis("VerticalTwo") * playerController.playerModels[playerId].speed;
        }


        movement[playerId] = new Vector2(moveHorizontal, moveVertical);
        movement[playerId].Normalize();
        playerModels[playerId].playerRb.isKinematic = false;
        playerController.playerModels[playerId].playerRb.velocity = movement[playerId] * playerController.playerModels[playerId].speed;
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
        Debug.LogWarning("stoping");
        playerModels[playerId].playerRb.velocity = Vector2.zero;
        playerModels[playerId].playerRb.isKinematic = true;
    }

    /// <summary>
    /// Restricts movement for given player Id.
    /// </summary>
    /// <param name="playerId">Player to be restricted from movement</param>
    /// <param name="doRestrict">Whether to restrict or un-restrict</param>
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
                    interactablesNear[playerId].vegetable = true;
                }
                break;
            case "ChoppingBoard":
                {
                    interactablesNear[playerId].choppingBoard = true;
                }
                break;
            case "Dustbin":
                {
                    interactablesNear[playerId].dustbin = true;
                }
                break;
            case "Plate":
                {
                    interactablesNear[playerId].plate = true;
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
                    interactablesNear[playerId].vegetable = false;
                }
                break;
            case "ChoppingBoard":
                {
                    interactablesNear[playerId].choppingBoard = false;
                }
                break;
            case "Dustbin":
                {
                    interactablesNear[playerId].dustbin = false;
                }
                break;
            case "Plate":
                {
                    interactablesNear[playerId].plate = false;
                }
                break;
        }
    }

    #endregion ------------------------------------------------------------------------------------------------------------
}
public class InteractablesNear
{
    public bool vegetable = false;
    public bool choppingBoard = false;
    public bool dustbin = false;
    public bool plate = false;

}

