using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int vegetablePickLimit = 2;
    [SerializeField] private GameObject controllers = null;
    private VegetableController vegetableController = null;
    [HideInInspector] public List<PlayerModel> playerModels;

    private void Awake()
    {
        vegetableController = controllers.GetComponentInChildren<VegetableController>();
    }

    private void PickupVegetable(VegetableModel vegetableModel, PlayerId playerId)
    {
        playerModels[(int)playerId].vegetablesInHand.Add(vegetableModel);
        Debug.LogFormat("{0} picked by {1}", playerModels[(int)playerId].vegetablesInHand[0].type, playerId);
    }

    public void OnVegetableInteract(PlayerId playerId)
    {
        if (playerModels[(int)playerId].vegetablesInHand.Count < vegetablePickLimit)
        {
            VegetableModel vegetableModel = vegetableController.GetVegetableModelOnPlayer(playerId);
            PickupVegetable(vegetableModel, playerId);
        }
    }

    public void UpdatePlayersModel(List<PlayerModel> playerModels)
    {
        this.playerModels = playerModels;
    }
}
