using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoppingBoardController : MonoBehaviour
{
    private List<ChoppingBoardModel> choppingBoardInPlayerViscinity = new List<ChoppingBoardModel>(2) { null, null };

    public void UpdateChoppingBoardOnPlayer(int playerId, ChoppingBoardModel choppingBoardModel)
    {
        choppingBoardInPlayerViscinity[playerId] = choppingBoardModel;
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

    public void PlaceVegetableToChop(VegetableModel vegetable,PlayerId playerId)
    {
        
    }
}
