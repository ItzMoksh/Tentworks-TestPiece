using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoppingBoardView : MonoBehaviour
{
    private List<ChoppingBoardModel> choppingBoardModels;

    private void Start()
    {
        ModelsInit();
        UpdateChoppingBoardModels();
    }

    private void ModelsInit()
    {
        choppingBoardModels.Add(new ChoppingBoardModel(PlayerId.PLAYER_ONE));
        choppingBoardModels.Add(new ChoppingBoardModel(PlayerId.PLAYER_TWO));
    }

    private void UpdateChoppingBoardModels()
    {

    }

    public void CollisionEnterCallback(Collision2D collision, ChoppingBoardModel choppingBoardModel)
    {

    }

    public void CollisionExitCallback(Collision2D collision, ChoppingBoardModel choppingBoardModel)
    {

    }
}
