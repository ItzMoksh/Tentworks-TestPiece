using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetableController : MonoBehaviour
{

    private List<VegetableModel> vegetableInPlayerViscinity = new List<VegetableModel>(2){null,null};

    public void UpdateVegetableOnPlayer(int playerId, VegetableModel vegetableModel)
    {
        vegetableInPlayerViscinity[playerId] = vegetableModel;
    }

    public VegetableModel GetVegetableModelOnPlayer(PlayerId playerId)
    {
        return vegetableInPlayerViscinity[(int)playerId];
    }
}
