using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    public GameOverScreen GameOverScreen;

    public void GameOver(float topScore)
    {
        GameOverScreen.Setup(Player.topScore);
    }
}
