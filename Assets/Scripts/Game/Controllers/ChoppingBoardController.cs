using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoppingBoardController : MonoBehaviour
{
    private List<ChoppingBoardModel> choppingBoardModels = new List<ChoppingBoardModel>(2);
    private List<ChoppingBoardModel> choppingBoardInPlayerViscinity = new List<ChoppingBoardModel>(2) { null, null };

    public void UpdateChoppingBoardOnPlayer(int playerId, ChoppingBoardModel choppingBoardModel)
    {
        choppingBoardInPlayerViscinity[playerId] = choppingBoardModel;
    }

    public void UpdateChoppingBoardModels(List<ChoppingBoardModel> choppingBoardModels)
    {
        this.choppingBoardModels = choppingBoardModels;
    }

    public bool ValidateChoppingBoardId(PlayerId playerId)
    {
        if (choppingBoardInPlayerViscinity[(int)playerId].playerId == playerId)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void PlaceVegetableToChop(VegetableModel vegetable, PlayerId playerId)
    {
        choppingBoardModels[(int)playerId].vegetablesOnBoard.Add(vegetable);
        Debug.LogFormat("{0} placed on board by {1}", vegetable.type, playerId);
    }

    public SaladModel GetSaladFromBoard(PlayerId playerId)
    {
        var vegetablesOnBoard = choppingBoardModels[(int)playerId].vegetablesOnBoard;
        if (vegetablesOnBoard.Count > 0)
        {
            SaladModel saladModel = new SaladModel(vegetablesOnBoard);
            choppingBoardModels[(int)playerId].vegetablesOnBoard = new List<VegetableModel>();
            return saladModel;
        }
        else
        {
            return null;
        }
    }
}
