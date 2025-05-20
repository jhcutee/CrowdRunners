using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public enum BonusType
{
    Addition,
    Difference,
    Product,
    Division,
}
public class Doors : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private SpriteRenderer rightDoorRederer;
    [SerializeField] private SpriteRenderer leftDoorRederer;
    [SerializeField] private TextMeshPro rightDoorText;
    [SerializeField] private TextMeshPro leftDoorText;
    [SerializeField] private new Collider collider;

    [Header("Settings")]
    [SerializeField] private BonusType rightDoorBonusType;
    [SerializeField] private int rightDoorBonusAmout;
    [SerializeField] private BonusType leftDoorBonusType;
    [SerializeField] private int leftDoorBonusAmout;
    [SerializeField] private Color bonusColor;
    [SerializeField] private Color penaltyColor;
    private void Start()
    {
        ConfigureDoors();
    }
    private void ConfigureDoors()
    {
        switch (rightDoorBonusType)
        {
            case BonusType.Addition:
                rightDoorRederer.color = bonusColor;
                rightDoorText.text = "+" + rightDoorBonusAmout;
                break;
            case BonusType.Difference:
                rightDoorRederer.color = penaltyColor;
                rightDoorText.text = "-" + rightDoorBonusAmout;
                break;
            case BonusType.Product:
                rightDoorRederer.color = bonusColor;
                rightDoorText.text = "x" + rightDoorBonusAmout;
                break;
            case BonusType.Division:
                rightDoorRederer.color = penaltyColor;
                rightDoorText.text = "/" + rightDoorBonusAmout;
                break;
        }

        switch (leftDoorBonusType)
        {
            case BonusType.Addition:
                leftDoorRederer.color = bonusColor;
                leftDoorText.text = "+" + leftDoorBonusAmout;
                break;
            case BonusType.Difference:
                leftDoorRederer.color = penaltyColor;
                leftDoorText.text = "-" + leftDoorBonusAmout;
                break;
            case BonusType.Product:
                leftDoorRederer.color = bonusColor;
                leftDoorText.text = "x" + leftDoorBonusAmout;
                break;
            case BonusType.Division:
                leftDoorRederer.color = penaltyColor;
                leftDoorText.text = "/" + leftDoorBonusAmout;
                break;
        }
    }
    public int GetBonusAmount(float xPos)
    {
        if (xPos > 0) return rightDoorBonusAmout;
        else return leftDoorBonusAmout;
    }
    public BonusType GetBonusType(float xPos)
    {
        if (xPos > 0) return rightDoorBonusType;
        else return leftDoorBonusType;
    }
    public void DisableCollider()
    {
        collider.enabled = false;
    }
}
