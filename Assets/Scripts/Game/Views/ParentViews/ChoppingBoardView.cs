using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoppingBoardView : MonoBehaviour
{
    [SerializeField] private List<ChoppingBoardModel> choppingBoardModels = new List<ChoppingBoardModel>(2);

    private void Start()
    {
        // ModelsInit();
        // UpdateChoppingBoardModels();
    }

    private void ModelsInit()
    {
        // choppingBoardModels.Add(new ChoppingBoardModel(PlayerId.PLAYER_ONE));
        // choppingBoardModels.Add(new ChoppingBoardModel(PlayerId.PLAYER_TWO));
    }

    private void UpdateChoppingBoardModels()
    {

    }

    public void CollisionEnterCallback(Collision2D collision, ChoppingBoardModel choppingBoardModel)
    {
        if (collision.gameObject.tag == "Player")
        {
            int playerId = collision.gameObject.GetComponent<PlayerCollisionView>().playerId;
            
        }
    }

    public void CollisionExitCallback(Collision2D collision, ChoppingBoardModel choppingBoardModel)
    {

    }
}
