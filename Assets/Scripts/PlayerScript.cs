using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
	public float speed;
	public Text scoreText;
	public Text livesText;
	public Text winText;
	public AudioSource musicSource;
	public AudioClip musicClipOne;

    private Rigidbody2D rd2d;
	private int score;
	private int lives;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
		score = 0;
		lives = 3;
		winText.text = "";
		SetScoreText ();
		SetLivesText ();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
		{
			
			if (Input.GetKey("escape"))
			{
		
				Application.Quit();
			}
		}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
		if (collision.collider.tag == "Coin")
	    {
            score = score + 1;
            scoreText.text = score.ToString();
            Destroy(collision.collider.gameObject);
			SetScoreText ();
		}

		if (collision.collider.tag == "Enemy")
		{
			lives = lives - 1;
			livesText.text = lives.ToString();
			Destroy(collision.collider.gameObject);
			SetLivesText();
		}
	}

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }

	private void SetLivesText()
	{
		livesText.text = "Lives: " + lives.ToString ();
		if (lives == 0)
		{
			Object.Destroy(gameObject);
			winText.text = "You Lose!";
		}
	}

	private void SetScoreText()
	{
		scoreText.text = "Score: " + score.ToString ();
		if (score >= 4)
		{
			winText.text = "You Win! Game created by Joshua Bonilla!";
			musicSource.clip = musicClipOne;
			musicSource.Play();
		}
	}
}