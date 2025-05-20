using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdSystem : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject runnerPrefab;

    [Header("Setting")]
    [SerializeField] private float radius;
    [SerializeField] private float angle;
    
    private void Update()
    {
        if (!GameManager.Instance.IsGameState()) return;
        PlaceRunners();
        CheckLoseCondition();
    }
    private void CheckLoseCondition()
    {
        if (PlayerController.instance.RunnersParent.transform.childCount <= 0)
            GameManager.Instance.SetGameState(GameManager.GameState.GameOver);
    }
    private void PlaceRunners()
    {
        for(int i = 0; i < PlayerController.instance.RunnersParent.transform.childCount; i++)
        {
            Vector3 runnerLocalPostion = GetRunnerLocalPosition(i);
            PlayerController.instance.RunnersParent.transform.GetChild(i).localPosition = runnerLocalPostion;
        }
    }
    private Vector3 GetRunnerLocalPosition(int index)
    {
        float x = radius * Mathf.Sqrt(index) * Mathf.Cos(Mathf.Deg2Rad * index * angle);
        float z = radius * Mathf.Sqrt(index) * Mathf.Sin(Mathf.Deg2Rad * index * angle);
        return new Vector3(x, 0, z);
    }
    public float GetCrowdRadius()
    {
        return radius * Mathf.Sqrt(PlayerController.instance.RunnersParent.transform.childCount);
    }
    public void Apply(BonusType bonusType, int bonusAmount)
    {
        switch(bonusType)
        {
            case BonusType.Addition:
                PlayerController.instance.PlayerDetect.SetBonusType(BonusType.Addition);
                AddRunners(bonusAmount);
                break;
            case BonusType.Product:
                PlayerController.instance.PlayerDetect.SetBonusType(BonusType.Product);
                int amountToAdd = PlayerController.instance.RunnersParent.transform.childCount * bonusAmount - PlayerController.instance.RunnersParent.transform.childCount;
                AddRunners(amountToAdd);
                break;
            case BonusType.Difference:
                PlayerController.instance.PlayerDetect.SetBonusType(BonusType.Difference);
                RemoveRunners(bonusAmount);
                break;
            case BonusType.Division:
                PlayerController.instance.PlayerDetect.SetBonusType(BonusType.Division);
                int amountToRemove = PlayerController.instance.RunnersParent.transform.childCount - (PlayerController.instance.RunnersParent.transform.childCount / bonusAmount);
                RemoveRunners(amountToRemove);
                break;
        }
    }
    private void AddRunners(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(runnerPrefab, PlayerController.instance.RunnersParent.transform);
            PlayerController.instance.PlayerAnimator.RunAnimation();
        }
    }
    private void RemoveRunners(int amount)
    {
        if (amount > PlayerController.instance.RunnersParent.transform.childCount) amount = PlayerController.instance.RunnersParent.transform.childCount;
        int runnersAmount = PlayerController.instance.RunnersParent.transform.childCount;
        for (int i = runnersAmount - 1; i >= runnersAmount - amount; i--)
        {
            Transform runnerToRemove = PlayerController.instance.RunnersParent.transform.GetChild(i);
            runnerToRemove.SetParent(null);
            Destroy(runnerToRemove.gameObject);
        }
    }
}
