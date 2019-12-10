using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetableView : MonoBehaviour
{
    // [SerializeField] private List<VegetableModel> vegetables = null;
    [SerializeField] private GameObject controllers = null;
    [SerializeField] private Color vegetableColorInteractable = new Color();
    [SerializeField] private Color vegetableColorNormal = new Color();
    private VegetableController vegetableController = null;

    private void Awake()
    {
        vegetableController = controllers.GetComponentInChildren<VegetableController>();
    }

    /// <summary>
    /// If the collider is a player, it stores it's data with vegetable model.
    /// </summary>
    /// <param name="collider">Object collided with the vegetable</param>
    /// <param name="vegetableModel">Vegetable on which collision happened.</param>
    public void TriggerEnterCallback(Collider2D collider, VegetableModel vegetableModel)
    {
        if (collider.tag == "Player")
        {   
            vegetableModel.vegetableObject.GetComponent<SpriteRenderer> ().color = vegetableColorInteractable;
            int playerId = collider.GetComponent<PlayerCollisionView>().playerId;
            vegetableController.UpdateVegetableOnPlayer(playerId, vegetableModel);
        }
    }

    public void TriggerExitCallback(Collider2D collider, VegetableModel vegetableModel)
    {
        if (collider.tag == "Player")
        {
            vegetableModel.vegetableObject.GetComponent<SpriteRenderer> ().color = vegetableColorNormal;
            int playerId = collider.GetComponent<PlayerCollisionView>().playerId;
            vegetableController.UpdateVegetableOnPlayer(playerId, null);
        }
    }

}
