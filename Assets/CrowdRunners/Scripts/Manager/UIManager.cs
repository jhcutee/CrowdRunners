using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    [Header("Elements")]
    [SerializeField] private ShopManager shopManager;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI levelText;
    private void Awake()
    {
        if(instance != null) Destroy(gameObject);
        instance = this;
    }
    void Start()
    {
        progressBar.value = 0;
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        levelCompletePanel.SetActive(false);
        settingPanel.SetActive(false);
        int level = ChunkManager.instance.GetLevel() + 1;
        levelText.text = "Level " + level;
        GameManager.onGameStateChanged += GameStateChangedCallBack;
    }
    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallBack;
    }

    void Update()
    {
        UpdateProgressBar();
    }
    public void Play()
    {
        GameManager.Instance.SetGameState(GameManager.GameState.Game);
        menuPanel.gameObject.SetActive(false);
        gamePanel.SetActive(true);
    }
    private void GameStateChangedCallBack(GameManager.GameState gameState)
    {
        if (gameState == GameManager.GameState.GameOver) ShowGameOverPanel();
        if(gameState == GameManager.GameState.LevelCompleted) ShowLevelCompletePanel();
    }
    private void ShowGameOverPanel()
    {
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }
    private void ShowLevelCompletePanel()
    {
        gamePanel.SetActive(false);
        levelCompletePanel.SetActive(true);
    }
    public void RetryButtonPressed()
    {
        InterstitialAd.Instance.ShowAd();
        SceneManager.LoadScene(0);
    }
    public void UpdateProgressBar()
    {
        if (!GameManager.Instance.IsGameState()) return;
        float progress = PlayerController.instance.transform.position.z / ChunkManager.instance.GetzPosFinishLine();
        progressBar.value = progress;
    }
    public void ShowSettingPanel()
    {
        settingPanel.SetActive(true);
    }
    public void HideSettingPanel()
    {
        settingPanel.SetActive(false);
    }
    public void ShowShopPanel()
    {
        shopPanel.SetActive(true);
        shopManager.UpdatePurchaseButton();
    }
    public void HideShopPanel()
    {
        shopPanel.SetActive(false);
    }
}
