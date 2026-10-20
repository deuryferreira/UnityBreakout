using TMPro;
using UnityEngine;

public class Puntaje : MonoBehaviour
{
    public Transform transformHighScore;
    public Transform transformActualScore;
    public TMP_Text HighScoreText;
    public TMP_Text ActualScoreText;
    public PuntajeAlto highScoreSO;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transformActualScore = GameObject.Find("ActualScoreText").transform;
        transformHighScore = GameObject.Find("HighScoreText").transform;
        ActualScoreText = transformActualScore.GetComponent<TMP_Text>();
        HighScoreText = transformHighScore.GetComponent<TMP_Text>();
        //  if (PlayerPrefs.HasKey("HighScore"))
        //  {
        //     highScoreSO.highScore = PlayerPrefs.GetInt("HighScore");
        //   HighScoreText.text = "High Score: " + highScoreSO.highScore .ToString();
        //  }
        highScoreSO.LoadScore();
        HighScoreText.text = "High Score: " + highScoreSO.highScore.ToString();
        highScoreSO.score = 0;
    }


    private void FixedUpdate()
    {
        highScoreSO.score +=50;
       
    }

    // Update is called once per frame
    void Update()
    {
        ActualScoreText.text = "Score: " + highScoreSO.score.ToString();
        if (highScoreSO.score >= highScoreSO.highScore)
        {
            highScoreSO.highScore = highScoreSO.score;
        HighScoreText.text = "High Score: " + highScoreSO.highScore .ToString();
          //  PlayerPrefs.SetInt("HighScore", highScoreSO.highScore);
          highScoreSO.SaveScore();
        }
    }
}
