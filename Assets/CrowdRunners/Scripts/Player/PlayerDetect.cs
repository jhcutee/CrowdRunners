using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class PlayerDetect : MonoBehaviour
{
    //[Header("Elements")]
    //[SerializeField] private CrowdSystem crowdSystem;
    [Header("Event")]
    public static Action<BonusType> onHitDoors;
    private BonusType bonusType;

    private void Update()
    {
        if (!GameManager.Instance.IsGameState()) return;
        HandlePlayerDetectColliders();
    }
    private void HandlePlayerDetectColliders()
    {
        Collider[] detectedColliders = Physics.OverlapSphere(this.transform.position, PlayerController.instance.CrowdSystem.GetCrowdRadius());
        for(int i = 0; i< detectedColliders.Length; i++)
        {
            if(detectedColliders[i].TryGetComponent(out Doors doors))
            {
                int bonusAmout = doors.GetBonusAmount(this.transform.position.x);
                BonusType bonusType = doors.GetBonusType(this.transform.position.x);
                doors.DisableCollider();
                PlayerController.instance.CrowdSystem.Apply(bonusType, bonusAmout);
            }
            else if (detectedColliders[i].tag == "Finish")
            {
                int level = ChunkManager.instance.GetLevel();
                level++;
                PlayerPrefs.SetInt("Level", level);

                GameManager.Instance.SetGameState(GameManager.GameState.LevelCompleted);

            }
            else if (detectedColliders[i].tag == "Coin")
            {
                Destroy(detectedColliders[i].gameObject);
                DataManager.instance.AddCoins(1);
            }
        }
    }
    public void SetBonusType(BonusType bonusType)
    {
        this.bonusType = bonusType;
        onHitDoors?.Invoke(bonusType);
    }
}
