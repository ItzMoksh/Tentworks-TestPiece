using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject controllers = null;
    [SerializeField] private GameObject views = null;
    [SerializeField] private int vegetablePickLimit = 2;

    private VegetableController vegetableController = null;
    private ChoppingBoardController choppingBoardController = null;


    private ChoppingBoardView chopBoardView = null;
    [HideInInspector] public List<PlayerModel> playerModels;

    private void Awake()
    {
        vegetableController = controllers.GetComponentInChildren<VegetableController>();
        chopBoardView = views.GetComponentInChildren<ChoppingBoardView>();
    }

    private void PickupVegetable(VegetableModel vegetableModel, PlayerId playerId)
    {
        playerModels[(int)playerId].vegetablesInHand.Add(vegetableModel);
        Debug.LogFormat("{0} picked by {1}", vegetableModel.type, playerId);
    }

    private void ChopVegetable(VegetableModel vegetableToChop, PlayerId playerId)
    {
        choppingBoardController.PlaceVegetableToChop(vegetableToChop,playerId);
    }

    private void RestrictMovement(PlayerId playerId, bool doRestrict)
    {

    }

    public void OnVegetableInteract(PlayerId playerId)
    {
        if (playerModels[(int)playerId].vegetablesInHand.Count < vegetablePickLimit)
        {
            VegetableModel vegetableModel = vegetableController.GetVegetableModelOnPlayer(playerId);
            PickupVegetable(vegetableModel, playerId);
        }
    }

    public void OnChoppingBoardInteract(PlayerId playerId)
    {
        if (choppingBoardController.ValidateChoppingBoardId(playerId))
        {
            if (playerModels[(int)playerId].vegetablesInHand.Count > 0)
            {
                var vegetableModel = playerModels[(int)playerId].vegetablesInHand[0];
                playerModels[(int)playerId].vegetablesInHand.RemoveAt(0);
                ChopVegetable(vegetableModel, playerId);
            }
            else
            {
                Debug.LogError("Get Some Vegetables!");
            }
        }
        else
        {
            Debug.LogError("Not your board!");
        }
    }

    public void UpdatePlayersModel(List<PlayerModel> playerModels)
    {
        this.playerModels = playerModels;
    }
}
