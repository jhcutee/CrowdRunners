using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private VibrationManager vibrationManager;
    [SerializeField] private Sprite onStateImage;
    [SerializeField] private Sprite offStateImage;
    [SerializeField] private Image soundButtonImage;
    [SerializeField] private Image vibrationButtonImage;

    private bool soundState = true;
    private bool vibrationState = true;
    private void Awake()
    {
        soundState = PlayerPrefs.GetInt("SoundState", 1) == 1;
        vibrationState = PlayerPrefs.GetInt("VibrationState", 1) == 1;
    }
    private void Update()
    {
        UpdateSoundSprite();
        UpdateVibrationSprite();
    }
    private void UpdateSoundSprite()
    {
        if (soundState)
        {
            EnableSounds();
        }
        else DisableSounds();
    }
    public void ChangeSoundState()
    {
        if (soundState)
        {
            DisableSounds();
        }
        else EnableSounds();
        soundState = !soundState;
        PlayerPrefs.SetInt("SoundState", soundState ? 1 : 0);
    }
    private void UpdateVibrationSprite()
    {
        if (vibrationState)
        {
            EnableVibrate();
        }
        else DisableVibrate();
    }
    public void ChangeVibrationState()
    {
        if (vibrationState)
        {
            DisableVibrate();
        }
        else EnableVibrate();
        vibrationState = !vibrationState;
        PlayerPrefs.SetInt("VibrationState", vibrationState ? 1 : 0);
    }
    private void DisableSounds()
    {
        soundManager.DisableSounds();
        soundButtonImage.sprite = offStateImage;
    }
    private void EnableSounds()
    {
        soundManager.EnableSounds();
        soundButtonImage.sprite = onStateImage;
    }
    private void DisableVibrate()
    {
        vibrationManager.DisableVibration();
        vibrationButtonImage.sprite = offStateImage;
    }
    private void EnableVibrate()
    {
        vibrationManager.EnableVibration();
        vibrationButtonImage.sprite= onStateImage;
    }
}
