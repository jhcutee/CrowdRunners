using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public enum GameState
    {
        Menu,
        Game,
        LevelCompleted,
        GameOver,
    }
    private GameState gameState;
    public static Action<GameState> onGameStateChanged;
    private void Awake()
    {
        if(Instance != null) Destroy(this.gameObject);
        else Instance = this;
    }
    public void SetGameState(GameState GameState)
    {
        this.gameState = GameState;
        onGameStateChanged?.Invoke(GameState);
    }
    public bool IsGameState()
    {
        return gameState == GameState.Game;
    }
}
