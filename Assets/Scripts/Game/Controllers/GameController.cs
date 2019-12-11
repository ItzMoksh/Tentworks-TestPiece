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

    private List<bool> gameOver = new List<bool>() { false, false };

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

    private void OnGameOver()
    {
        var players = gameModel.playersInfo;
        int maxScore = 0;
        int maxId = 0;
        for (int i = 0, l = players.Count; i < l; i++)
        {
            if (players[i].score > maxScore)
            {
                maxScore = players[i].score;
                maxId = i;
            }
        }

        gameView.ShowGameOver(gameModel.playersInfo[maxId],maxId);
    }

    public void OnCorrectDelivery(int playerId)
    {
        int score = gameModel.playersInfo[playerId].score;
        int time = gameModel.playersInfo[playerId].timeLeft;
        score += scoreModel.correctDelivery;
        time += scoreModel.timeBonus;
        gameView.SetPlayerScore(playerId, score);
    }

    public void OnInCorrectDelivery(int playerId)
    {
        int score = gameModel.playersInfo[playerId].score;
        score -= scoreModel.incorrectDelivery;
        gameView.SetPlayerScore(playerId, score);
    }

    public void OnTimeOver(PlayerId playerId)
    {
        gameOver[(int)playerId] = true;
        playerController.OnGameOver(playerId);

        bool allOver = true;
        for (int i = 0, l = gameOver.Count; i < l; i++)
        {
            if (!gameOver[i])
            {
                allOver = false;
            }

        }

        if (allOver)
        {
            OnGameOver();
        }
    }
}
