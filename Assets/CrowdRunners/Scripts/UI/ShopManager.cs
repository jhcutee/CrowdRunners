using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;
public class ShopManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private SkinButton[] skinButtons;
    [SerializeField] private Button purchaseButton;

    [Header("Skin")]
    [SerializeField] private Sprite[] skinImages;

    [Header("Price")]
    [SerializeField] private int skinPrice;
    [SerializeField] private TextMeshProUGUI priceText;

    [Header("Event")]
    public static Action<int> onSkinChanged;
    private void Awake()
    {
        UnlockSkin(0);
        priceText.text = skinPrice.ToString(); 
    }
     IEnumerator Start()
    {
        RewardedAdButton.onRewardAdRewarded += RewardPlayer;
        ConfigureButtons();
        UpdatePurchaseButton();
        yield return null;
        SelectSkin(GetLastSkinSelectedIndex());
    }

    // Update is called once per frame
    void OnDestroy()
    {
        RewardedAdButton.onRewardAdRewarded -= RewardPlayer;
    }
    private void RewardPlayer()
    {
        DataManager.instance.AddCoins(200);
        UpdatePurchaseButton();
    }
    private void ConfigureButtons()
    {
        for(int i = 0; i < skinButtons.Length; i++)
        {
            bool unlocked = PlayerPrefs.GetInt("skinButton" + i) == 1;
            skinButtons[i].Configure(skinImages[i], unlocked);
            int skinIndex = i;
            skinButtons[i].GetButton().onClick.AddListener(() => SelectSkin(skinIndex));
        }
    }
    public void UnlockSkin(int skinIndex)
    {
        PlayerPrefs.SetInt("skinButton" + skinIndex, 1);
        skinButtons[skinIndex].Unlock();
    }
    private void SelectSkin(int skinIndex)
    {
        for(int i = 0;i < skinButtons.Length; i++)
        {
            if (skinIndex == i)
                skinButtons[i].Select();
            else skinButtons[i].Deselect();
        }
        onSkinChanged?.Invoke(skinIndex);
        SaveLastSkinSelectedIndex(skinIndex);
    }
    public void PurchaseSkin()
    {
        List<int> skinButtonIndex = new List<int>();
        for(int i = 0; i < skinButtons.Length; i++)
        {
            if (!skinButtons[i].IsUnlocked())
            skinButtonIndex.Add(i);
        }

        if (skinButtonIndex.Count <= 0) return;

        int randomIndexSkin = skinButtonIndex[Random.Range(0, skinButtonIndex.Count)];
        UnlockSkin(randomIndexSkin);
        SelectSkin(randomIndexSkin);

        DataManager.instance.UseCoins(skinPrice);

        UpdatePurchaseButton();

    }
    public void UpdatePurchaseButton()
    {
        if(DataManager.instance.GetCoins() < skinPrice) 
            purchaseButton.interactable = false;
        else
            purchaseButton.interactable = true;
    }
    private int GetLastSkinSelectedIndex()
    {
        return PlayerPrefs.GetInt("LastSkinIndex", 0);
    }
    private void SaveLastSkinSelectedIndex(int skinIndex)
    {
        PlayerPrefs.SetInt("LastSkinIndex", skinIndex);
    }
}
