using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetableView : MonoBehaviour
{
    // [SerializeField] private List<VegetableModel> vegetables = null;
    [SerializeField] private GameObject controllers = null;
    private VegetableController vegetableController = null;


    private void Awake()
    {
        vegetableController = controllers.GetComponentInChildren<VegetableController>();
    }

    public void TriggerEnterCallback(Collider2D collider, VegetableModel vegetableModel)
    {
        if (collider.tag == "Player")
        {
            int playerId = collider.GetComponent<PlayerCollisionView>().playerId;
            vegetableController.UpdateVegetableOnPlayer(playerId, vegetableModel);
        }
    }

    public void TriggerExitCallback(Collider2D collider, VegetableModel vegetableModel)
    {
        
    }

}
