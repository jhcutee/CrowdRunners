using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI[] coinsTexts;
    private int coins;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else instance = this;
    }
    private void Start()
    {
        coins = PlayerPrefs.GetInt("Coins", 0);
        UpdateCoinText();
    }
    private void UpdateCoinText()
    {
        foreach (TextMeshProUGUI coinsText in coinsTexts)
        {
            coinsText.text = coins.ToString();
        }
    }
    public void AddCoins(int amount)
    {
        coins += amount;

        UpdateCoinText();
        PlayerPrefs.SetInt("Coins",coins);
    }
    public int GetCoins()
    {
        return coins;
    }
    public void UseCoins(int amount) {
        coins -= amount;

        UpdateCoinText();
        PlayerPrefs.SetInt("Coins", coins);
    }
}
