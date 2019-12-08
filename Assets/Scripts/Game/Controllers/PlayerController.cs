using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject controllers = null;
    [SerializeField] private GameObject views = null;
    [SerializeField] private int vegetablePickLimit = 2;
    [SerializeField] private float choppingTime = 2f;

    private VegetableController vegetableController = null;
    private ChoppingBoardController choppingBoardController = null;

    private PlayerView playerView = null;
    private ChoppingBoardView chopBoardView = null;


    [HideInInspector] public List<PlayerModel> playerModels;

    private void Awake()
    {
        vegetableController = controllers.GetComponentInChildren<VegetableController>();
        choppingBoardController = controllers.GetComponentInChildren<ChoppingBoardController>();

        chopBoardView = views.GetComponentInChildren<ChoppingBoardView>();
        playerView = views.GetComponentInChildren<PlayerView>();
    }

    private void PickupVegetable(VegetableModel vegetableModel, PlayerId playerId)
    {
        playerModels[(int)playerId].vegetablesInHand.Add(vegetableModel);
        Debug.LogFormat("{0} picked by {1}", vegetableModel.type, playerId);
    }

    private void ChopVegetable(VegetableModel vegetableToChop, PlayerId playerId)
    {
        choppingBoardController.PlaceVegetableToChop(vegetableToChop, playerId);
        StartCoroutine(ChoppingEnumerator(playerId));
    }

    private IEnumerator ChoppingEnumerator(PlayerId playerId)
    {
        RestrictMovement(playerId, true);
        yield return new WaitForSeconds(choppingTime);
        RestrictMovement(playerId, false);
    }

    private void RestrictMovement(PlayerId playerId, bool doRestrict)
    {
        playerView.RestrictMovement(playerId, doRestrict);
    }

    public void OnVegetableInteract(PlayerId playerId)
    {
        if (playerModels[(int)playerId].vegetablesInHand.Count < vegetablePickLimit)
        {
            VegetableModel vegetableModel = vegetableController.GetVegetableModelOnPlayer(playerId);
            PickupVegetable(vegetableModel, playerId);
        }
    }

    public void PlaceVegetableOnBoard(PlayerId playerId)
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

    public void PickSaladFromBoard(PlayerId playerId)
    {

    }

    public void UpdatePlayersModel(List<PlayerModel> playerModels)
    {
        this.playerModels = playerModels;
    }
}
