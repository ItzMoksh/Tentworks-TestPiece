using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject controllers = null;
    [SerializeField] private GameObject views = null;
    [SerializeField] private GameModel gameModel = new GameModel();
    [SerializeField] private ScoreModel scoreModel = new ScoreModel();

    private PlayerController playerController = null;
    private CustomerController customerController = null;

    private GameView gameView = null;

    private void Awake()
    {
        playerController = controllers.GetComponentInChildren<PlayerController>();
        customerController = controllers.GetComponentInChildren<CustomerController>();

        gameView = views.GetComponentInChildren<GameView>();
    }

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        customerController.StartGame();

        gameView.Init(gameModel);
        gameView.StartTimerAllPlayers();
    }

    public void OnCorrectDelivery(int playerId)
    {
        int score = gameModel.playersInfo[playerId].score;
        score += scoreModel.correctDelivery;
        gameView.SetPlayerScore(playerId,score);
    }

    public void OnInCorrectDelivery(int playerId)
    {
        gameModel.playersInfo[playerId].score -= scoreModel.incorrectDelivery;
    }

    public void OnTimeOver(PlayerId playerId)
    {

    }
}
