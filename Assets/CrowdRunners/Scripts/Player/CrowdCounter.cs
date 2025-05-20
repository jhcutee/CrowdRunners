using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CrowdCounter : MonoBehaviour
{
    [Header("Element")]
    [SerializeField] private TextMeshPro runnerCounterTMP;
    [SerializeField] private Transform runnersParents;
    
    void Update()
    {
        RunnersCount();
    }
    private void RunnersCount()
    {
        runnerCounterTMP.text = runnersParents.childCount.ToString();
        if(runnersParents.childCount <= 0) Destroy(gameObject);
    }
}
