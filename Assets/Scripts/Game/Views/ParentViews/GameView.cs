using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameView : MonoBehaviour
{
    [SerializeField] private GameObject controllers = null;
    [SerializeField] private GameObject gameOverPanel = null;
    [SerializeField] private List<TextMeshProUGUI> playerScores = null;
    [SerializeField] private List<TextMeshProUGUI> playersTimeleft = null;

    private GameController gameController = null;
    private List<Coroutine> timerCoroutines = new List<Coroutine>();
    private GameModel gameModel = new GameModel();

    private void Awake()
    {
        gameController = controllers.GetComponentInChildren<GameController>();
    }

    private void Start()
    {

    }

    private IEnumerator Timeleft(PlayerId playerId)
    {
        while (gameModel.playersInfo[(int)playerId].timeLeft > 0)
        {
            playersTimeleft[(int)playerId].text = gameModel.playersInfo[(int)playerId].timeLeft.ToString();
            yield return new WaitForSeconds(1);
            gameModel.playersInfo[(int)playerId].timeLeft--;
        }
        gameController.OnTimeOver(playerId);
    }

    public void Init(GameModel gameModel)
    {
        this.gameModel = gameModel;
    }

    public void StartTimerAllPlayers()
    {
        for (int i = 0, l = gameModel.playersInfo.Count; i < l; i++)
        {
            timerCoroutines.Add(StartCoroutine(Timeleft((PlayerId)i)));
        }
    }

    public void SetPlayerScore(int playerId, int score)
    {
        playerScores[playerId].text = score.ToString();
    }

    public void SetPlayerTimeLeft(PlayerId playerId, int timeLeft)
    {
        gameModel.playersInfo[(int)playerId].timeLeft = timeLeft;
    }
}
