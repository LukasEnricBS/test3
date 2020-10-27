using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    /*
     * 
     * SPACE GAME 
     * 
     */

    public GameObject gameControl;
    public GameObject generator1;
    public GameObject generator2;
    public GameObject generator3;
    public AudioClip hitClip;
    public AudioClip deathClip;
    public AudioClip pointClip;
    public Text hpText;

    public ParticleSystem engines;

    private Animator animator;
    private AudioSource audioPlayer;
    private float startX;
    private int hp = 3;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("hp remaining: " + hp.ToString());
        string hpString = "";

        for (int i = 0; i < hp; i++)
        {
            hpString += "X";
        }

        Debug.Log("hpString: " + hpString);

        hpText.text = ("Vides: " + hpString.ToString());
        Debug.Log("hpText: " + hpText.text);

        bool isCentered = (transform.position.x >= -1.5 && transform.position.x <= 1.5); // Marge d'error de * 10 per comoditat del jugador
        bool playing = gameControl.GetComponent<GameController>().gameState == GameState.Playing;
        //Debug.Log("Current pos: " + transform.position.x + " | Is centered? " + isCentered);

        if (playing && isCentered)
        {
            if (Input.GetKeyDown("left") || Input.GetMouseButtonDown(0))
            {
                UpdateState("player_left");
            } else if (Input.GetKeyDown("right") || Input.GetMouseButtonDown(1))
            {
                UpdateState("player_right");
            }
        }
    }

    public void UpdateState(string state = null)
    {
        if (state != null)
        {
            animator.Play(state);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Impacto");
            audioPlayer.clip = hitClip;
            audioPlayer.Play();
            hp--;
            if(hp == 0)
            {
                audioPlayer.clip = deathClip;
                audioPlayer.Play();
                UpdateState("player_explode");
                gameControl.GetComponent<GameController>().gameState = GameState.End;
                generator1.SendMessage("StopGenerator", true);
                generator2.SendMessage("StopGenerator", true);
                generator3.SendMessage("StopGenerator", true);
            }
            //gameControl.SendMessage("ResetTimeScale", 0.5f);

            //gameControl.GetComponent<AudioSource>().Stop();
        }
        else if (other.gameObject.tag == "Point")
        {
            gameControl.SendMessage("IncreasePoints");
            audioPlayer.clip = pointClip;
            audioPlayer.Play();
        }
    }

    void GameReady()
    {
        gameControl.GetComponent<GameController>().gameState = GameState.Ready;
    }

    void EnginesPlay()
    {
        engines.Play();
    }

    void EnginesStop()
    {
        engines.Stop();
    }
}
