using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    [SerializeField] private bool isVibrate;
    void Start()
    {
        PlayerDetect.onHitDoors += HitDoorVibrate;
        GameManager.onGameStateChanged += GameStateChangedCallBack;
        Enemy.onRunnerDie += Vibrate;
    }
    private void OnDestroy()
    {
        PlayerDetect.onHitDoors -= HitDoorVibrate;
        GameManager.onGameStateChanged -= GameStateChangedCallBack;
        Enemy.onRunnerDie -= Vibrate;
    }

    // Update is called once per frame
    void Vibrate()
    {
        if (isVibrate)
        Handheld.Vibrate();
    }
    private void HitDoorVibrate(BonusType bonusType)
    {
        if (bonusType == BonusType.Product || bonusType == BonusType.Addition)
        {
            Vibrate();
        }
        else if (bonusType == BonusType.Division || bonusType == BonusType.Difference)
        {
            Vibrate();
        }
    }
    private void GameStateChangedCallBack(GameManager.GameState gameState)
    {
        if (gameState == GameManager.GameState.GameOver)
        {
            Vibrate();
        }
        else if (gameState == GameManager.GameState.LevelCompleted)
        {
            Vibrate();
        }
    }
    public void EnableVibration()
    {
        isVibrate = true;
    }
    public void DisableVibration() 
    {  
        isVibrate = false; 
    }
}
