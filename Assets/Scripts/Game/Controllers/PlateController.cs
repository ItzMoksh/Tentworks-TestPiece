using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateController : MonoBehaviour
{
    [SerializeField] private GameObject controllers = null;

    private PlayerController playerController = null;

    private List<PlateModel> plateModels = new List<PlateModel>(2){
        new PlateModel(PlayerId.PLAYER_ONE),
        new PlateModel(PlayerId.PLAYER_TWO)
        };

    private List<bool> plateNearPlayer = new List<bool>(2) { false, false };

    public void OnPlayerCollisionEnter(PlayerId playerId, PlateModel plateModel)
    {
        if (plateModel.playerId == playerId)
        {
            plateNearPlayer[(int)playerId] = true;
        }
    }

    public void OnPlayerCollisionExit(PlayerId playerId, PlateModel plateModel)
    {
        if (plateModel.playerId == playerId)
        {
            plateNearPlayer[(int)playerId] = false;
        }
    }

    public bool ValidatePlayerPlate(PlayerId playerId)
    {
        return plateNearPlayer[(int)playerId];
    }

    public bool PutVegetableOnPlate(PlayerId playerId, VegetableModel vegetable)
    {
        if (plateModels[(int)playerId].vegetable == null)
        {
            plateModels[(int)playerId].vegetable = vegetable;
            return true;
        }
        else
        {
            return false;
        }
    }

    public VegetableModel PickVegetableFromPlate(PlayerId playerId)
    {
        VegetableModel vegetableOnPlate = plateModels[(int)playerId].vegetable;
        plateModels[(int)playerId].vegetable = null;
        return vegetableOnPlate;
    }
}
