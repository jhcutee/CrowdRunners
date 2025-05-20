using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [Header("Element")]
    [SerializeField] private GameObject runnersParent;
    [SerializeField] private CrowdSystem crowdSystem;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private PlayerDetect playerDetect;
    public GameObject RunnersParent { get => runnersParent;}
    public CrowdSystem CrowdSystem { get => crowdSystem;}
    public PlayerAnimator PlayerAnimator { get => playerAnimator; }
    public PlayerDetect PlayerDetect { get => playerDetect; }

    void Awake()
    {
        if(instance!= null) Destroy(gameObject);
        instance = this;
    }
    private void Start()
    {
        GameManager.onGameStateChanged += GameStateChangedCallBack;
    }
    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallBack;
    }

    public void GameStateChangedCallBack(GameManager.GameState gameState)
    {
        if(gameState == GameManager.GameState.Game)
            playerMovement.StartMoving();
        else if(gameState == GameManager.GameState.GameOver)
            playerMovement.StopMoving();
        else if (gameState == GameManager.GameState.LevelCompleted)
            playerMovement.StopMoving();
    }
    
}
