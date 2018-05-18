using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class director : MonoBehaviour {

    public GameObject player;
    public GameObject startBuilding;

    public Text scoreText; //the sum of scores you win
    public Text winText; //record winning info
    public Slider Strength; //the state of holding the jumping button


    // Use this for initialization
    void Awake () {

        Debug.Log("Starting Game!!!!!!!!!!!");

        //inin do not start the player until we scanned the picture we want;
        player.SetActive(false);

        //do not run the script on Building until it is active
        startBuilding.transform.GetComponent<activatePlayer>().enabled = false;

        //set Strength
        Strength.maxValue = playerController.HOLDINGLIMIT;
        Strength.minValue = 0;
        Strength.value = 0;

        //init scores
        playerController.scores = 0;
        scoreText.text = "Score: " + playerController.scores;
        scoreText.color = Color.blue;

        //set winning text
        winText.text = " ";
        winText.fontSize = 20;
        winText.color = Color.red;
        winText.resizeTextMaxSize = 1;
    }
	
	// Update is called once per frame
	void Update () {
		if(startBuilding.activeSelf == true)
        {
            startBuilding.transform.GetComponent<activatePlayer>().enabled = true;
        }

        if (player.activeSelf == true)
        {
            Strength.value = playerController.holdingFrame;
            scoreText.text = "Score: " + playerController.scores;
            judgeGameState();
        }

    }

    void judgeGameState()
    {
        if (playerController.gameState == 1) gameWinned();
        else if (playerController.gameState == 2) gameFailed();
    }

    void gameWinned()
    {
        //send gameover info and restart the game
        Debug.Log("Game Over");
        winText.text = "Congratulations You Win! Your Score is " + playerController.scores;
        StartCoroutine(waitAndRestart(2.0f));
    }

    void gameFailed()
    {
        winText.text = "YOU FAILED!!!";
        StartCoroutine(waitAndRestart(2.0f));
    }

    IEnumerator waitAndRestart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(0);
    }
}
