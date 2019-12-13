using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpView : MonoBehaviour
{
    private PowerUpController powerUpController = null;
    public PowerUpModel powerUpModel = new PowerUpModel();
    public void Init(PowerUpController powerUpController, PlayerId playerId)
    {
        this.powerUpController = powerUpController;
        powerUpModel.playerId = playerId;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        powerUpController.OnPlayerCollision(collider.gameObject, this);
    }
}
