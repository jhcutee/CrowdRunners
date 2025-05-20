using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private AudioSource buttonSound;
    [SerializeField] private AudioSource hitPositiveDoorSound;
    [SerializeField] private AudioSource hitNegativeDoorSound;
    [SerializeField] private AudioSource runnerDieSound;
    [SerializeField] private AudioSource levelCompleteSound;
    [SerializeField] private AudioSource gameOverSound;
    void Start()
    {
        PlayerDetect.onHitDoors += PlayHitDoorSound;
        GameManager.onGameStateChanged += GameStateChangedCallBack;
        Enemy.onRunnerDie += PlayRunnerDieSound;
    }

    // Update is called once per frame
    void OnDestroy()
    {
        PlayerDetect.onHitDoors -= PlayHitDoorSound;
        GameManager.onGameStateChanged -= GameStateChangedCallBack;
        Enemy.onRunnerDie -= PlayRunnerDieSound;
    }
    private void PlayHitDoorSound(BonusType bonusType)
    {
        if(bonusType == BonusType.Product || bonusType == BonusType.Addition)
        {
            hitPositiveDoorSound.Play();
        }
        else if (bonusType == BonusType.Division || bonusType == BonusType.Difference)
        {
            hitNegativeDoorSound.Play();
        }
    }
    private void GameStateChangedCallBack(GameManager.GameState gameState)
    {
        if(gameState == GameManager.GameState.GameOver)
        {
            gameOverSound.Play();
        }
        else if (gameState == GameManager.GameState.LevelCompleted)
        {
            levelCompleteSound.Play();
        }
    }
    private void PlayRunnerDieSound()
    {
        runnerDieSound.Play();
    }
    public void DisableSounds()
    {
        buttonSound.volume = 0;
        hitPositiveDoorSound.volume = 0;
        hitNegativeDoorSound.volume = 0;
        runnerDieSound.volume = 0;
        levelCompleteSound.volume = 0;
        gameOverSound.volume = 0;
    }
    public void EnableSounds()
    {
        buttonSound.volume = 1;
        hitPositiveDoorSound.volume = 1;
        hitNegativeDoorSound.volume = 1;
        runnerDieSound.volume = 1;
        levelCompleteSound.volume = 1;
        gameOverSound.volume = 1;
    }
}
