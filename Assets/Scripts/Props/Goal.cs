using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    //max time to reach the goal to get 1/2/3 stars.
    [SerializeField] float star1Time;
    [SerializeField] float star2Time;
    [SerializeField] float star3Time;
    // the level data of current level.
    [SerializeField] LevelData levelData;
    //the script of the canvas to show when player has reached the goal.
    [SerializeField] VictoryCanvas _canvasVictory;
    //time playered has used to reach the goal.
    float timeUsed = 0;
    private void Update()
    {
        timeUsed += Time.deltaTime;
    }
    private void OnTriggerEnter2D( Collider2D collision )
    {
        if (collision.GetComponent<Player>())
        {
            int starsCount = 0;
            
            if (timeUsed <= star3Time) starsCount = 3;
            else if (timeUsed <= star2Time) starsCount = 2;
            else if (timeUsed <= star1Time) starsCount = 1;
            _canvasVictory.Init(starsCount);
            //Change the highest stars get by the player.
            if (starsCount > PlayerPrefs.GetInt(levelData.name + "Stars", 0))
                PlayerPrefs.SetInt(levelData.name + "Stars", starsCount);
        }
    }
}
