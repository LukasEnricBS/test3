using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * 
 *  SPACE GAME
 * 
 * 
 */

public enum GameState { Idle, Playing, End, Ready };

public class GameController : MonoBehaviour
{
    [Range(0f, 1f)]
    public float parallaxSpeed = 0.2f;
    public RawImage bg;
    public GameObject uiIdle;
    public GameObject uiScore;
    public Text pointsText;
    public Text recordText;
    public AudioClip mainTheme;
    public GameState gameState = GameState.Idle;

    public GameObject player;
    public GameObject generator1;
    public GameObject generator2;
    public GameObject generator3;

    //public float scaleTime = 6f;
    //public float scaleInc = 0.25f;

    private AudioSource musicPlayer;
    private int points = 0;

    // Start is called before the first frame update
    void Start()
    {
        musicPlayer = GetComponent<AudioSource>();
        recordText.text = "Highscore:\n" + GetMaxScore().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gameState.ToString());
        Parallax();

        if (gameState == GameState.Idle)
        {
            if (Input.GetKeyDown("up") || Input.GetMouseButtonDown(0))
            {
                gameState = GameState.Playing;
                uiIdle.SetActive(false);
                uiScore.SetActive(true);
                player.SendMessage("UpdateState", "player_idle");
                generator1.SendMessage("StartGenerator");
                generator2.SendMessage("StartGenerator");
                generator3.SendMessage("StartGenerator");
                musicPlayer.clip = mainTheme;
                musicPlayer.Play();
            }
        }
        else if (gameState == GameState.Playing)
        {
            FasterParallax();
        }
        else if (gameState == GameState.Ready)
        {
            if (Input.GetKeyDown("up") || Input.GetMouseButtonDown(0))
            {
                RestartGame();
            }
        }
    }

    void Parallax()
    {
        float finalSpeed = parallaxSpeed * Time.deltaTime;
        bg.uvRect = new Rect(0f, bg.uvRect.y + finalSpeed, 1f, 1f);
    }

    void FasterParallax()
    {
        parallaxSpeed = 1f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void IncreasePoints()
    {
        points++;
        pointsText.text = points.ToString();
        if (points >= GetMaxScore())
        {
            recordText.text = "Highscore:\n" + points.ToString();
            SaveScore(points);
        }
    }

    public int GetMaxScore()
    {
        return PlayerPrefs.GetInt("Max Points", 0);
    }

    public void SaveScore(int currentPoints)
    {
        PlayerPrefs.SetInt("Max Points", currentPoints);
    }
}