using UnityEngine;
using System.Collections;

public enum BreakoutGameState { playing, won, lost };

public class BreakoutGame : MonoBehaviour
{
    public static BreakoutGame SP;

    public Transform ballPrefab;

    private int totalBlocks;
    private int blocksHit;
    private BreakoutGameState gameState;


    void Awake()
    {
        SP = this;
        blocksHit = 0;
        gameState = BreakoutGameState.playing;
        totalBlocks = GameObject.FindGameObjectsWithTag("Pickup").Length;
        Time.timeScale = 1.0f;
        SpawnBall();
    }

    void SpawnBall()
    {
        GameObject cloneObjectleft = (Instantiate(ballPrefab, new Vector3(1.81f, 0.0f, 9.75f), Quaternion.identity) as Transform).gameObject ; // spawn ball
        cloneObjectleft.GetComponent<Ball>().thisball_type = ballType.left;



        GameObject cloneObjectright = (Instantiate(ballPrefab, new Vector3(1.81f, 0.0f, 9.75f+35f), Quaternion.identity) as Transform).gameObject;
        cloneObjectright.GetComponent<Ball>().thisball_type = ballType.right;
    }

    void OnGUI(){
    
        GUILayout.Space(10);
        GUILayout.Label("  Hit: " + blocksHit + "/" + totalBlocks);

        if (gameState == BreakoutGameState.lost)
        {
            GUILayout.Label("You Lost!");
            if (GUILayout.Button("Try again"))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
        else if (gameState == BreakoutGameState.won)
        {
            GUILayout.Label("You won!");
            if (GUILayout.Button("Play again"))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    public void HitBlock()
    {
        blocksHit++;
        
        //For fun:
        if (blocksHit%10 == 0) //Every 10th block will spawn a new ball
        {
            SpawnBall();
        }

        
        if (blocksHit >= totalBlocks)
        {
            WonGame();
        }
    }

    public void WonGame()
    {
        Time.timeScale = 0.0f; //Pause game
        gameState = BreakoutGameState.won;
    }

    public void LostBall()
    {
        int ballsLeft = GameObject.FindGameObjectsWithTag("Player").Length;
        if(ballsLeft<=1){
            //Was the last ball..
            SetGameOver();
        }
    }

    public void SetGameOver()
    {
        Time.timeScale = 0.0f; //Pause game
        gameState = BreakoutGameState.lost;
    }
}
