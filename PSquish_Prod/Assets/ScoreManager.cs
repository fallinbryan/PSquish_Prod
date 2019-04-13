using ProfessorSquish.Components.Audio;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    private static GameObject scoreCounter;
    private static GameObject highestScoreCounter;



    void Start()
    {
        Debug.Log("Score Counter Started");
        scoreCounter = GameObject.Find("ScoreCounter");
        highestScoreCounter = GameObject.Find("highestScoreCounter");
        highestScoreCounter.GetComponentInChildren<Text>().text = PlayerPrefs.GetInt("highscore", 0).ToString();
       
    }
       
    void Update()
    {
        
    }

    public static void Increase(int amount)
    {
        SoundManagerScript.PlayOneShot("coin");
        Debug.LogFormat("Increasing score by {0}" , amount);

        int score = int.Parse(scoreCounter.GetComponentInChildren<Text>().text);

        score += amount;

        scoreCounter.GetComponentInChildren<Text>().text = score.ToString();

        int highestScore = int.Parse(highestScoreCounter.GetComponentInChildren<Text>().text);

        if (score > highestScore)
        {
            Debug.LogFormat("New Highest Score ! {0}", score);

            PlayerPrefs.SetInt("highscore", score);

            highestScoreCounter.GetComponentInChildren<Text>().text = score.ToString();
        }
               
    }

    public static void Set(int amount)
    {
        Debug.LogFormat("Setting score to {0}", amount);
        int score = int.Parse(scoreCounter.GetComponentInChildren<Text>().text);
        score = amount;
        scoreCounter.GetComponentInChildren<Text>().text = score.ToString();
    }

    public static string Get()
    {
        Debug.LogFormat("Current score is {0}", scoreCounter.GetComponentInChildren<Text>().text);
        return scoreCounter.GetComponentInChildren<Text>().text;
    }
}
