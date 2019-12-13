using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    [SerializeField] private GameObject controllers = null;
    [SerializeField] private GameObject spawnRegion = null;
    [SerializeField] private List<GameObject> powerUpPrefabs = null;
    [SerializeField] private Transform xMin = null;
    [SerializeField] private Transform xMax = null;
    [SerializeField] private Transform yMin = null;
    [SerializeField] private Transform yMax = null;

    private PlayerController playerController = null;
    private GameController gameController = null;

    private void Awake()
    {
        playerController = controllers.GetComponentInChildren<PlayerController>();
        gameController = controllers.GetComponentInChildren<GameController>();
    }

    public void SpawnRandomPowerUp(PlayerId playerId)
    {
        Vector2 randomPos = new Vector3();
        randomPos.x = Random.Range(xMin.position.x, xMax.position.x);
        randomPos.y = Random.Range(yMin.position.y, yMax.position.y);

        int randomIndex = Random.Range(0, powerUpPrefabs.Count);

        var powerUp = Instantiate(powerUpPrefabs[randomIndex]);
        powerUp.transform.parent = spawnRegion.transform;
        powerUp.transform.position = randomPos;
        powerUp.GetComponent<PowerUpView>().Init(this, playerId);
    }

    public void OnPlayerCollision(GameObject player, PowerUpView powerUpView)
    {
        PlayerId playerCollidedId = (PlayerId)player.GetComponent<PlayerCollisionView>().playerId;
        if (playerCollidedId == powerUpView.powerUpModel.playerId)
        {
            switch (powerUpView.powerUpModel.type)
            {
                case PowerUpType.SPEED:
                    {
                        playerController.OnSpeedPowerUp(playerCollidedId);
                    }
                    break;

                case PowerUpType.SCORE:
                case PowerUpType.TIME:
                    {
                        gameController.OnPowerUpPickup(playerCollidedId, powerUpView.powerUpModel.type);
                    }
                    break;
            }
        }
        else
        {
            playerController.SetActivityLog((int)playerCollidedId, "Not your power up!");
        }
    }
}
