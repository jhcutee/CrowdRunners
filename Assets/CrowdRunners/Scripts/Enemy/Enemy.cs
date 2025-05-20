using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    enum EnemyState
    {
        Idle,
        Run,
    }
    [Header("Settings")]
    [SerializeField] private float searchRadius;
    [SerializeField] private float moveSpeed;
    private EnemyState enemyState;
    private Transform runnerTarget;

    [Header("Event")]
    public static Action onRunnerDie;
    private void Update()
    {
        ManageState();
    }
    private void ManageState()
    {
        switch(enemyState)
        {
            case EnemyState.Idle:
                SearchTarget();
                break;
            case EnemyState.Run:
                RunTowardTarget();
                Animator enemyAnimator = GetComponent<Animator>();
                enemyAnimator.Play("Run");
                break;
        }
    }
    private void SearchTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius);
       for(int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].TryGetComponent(out Runner runner))
            {
                if(runner.IsTarget()) continue;
                runner.SetTarget();
                runnerTarget = runner.transform;
                StartRunningTowardTarget();
                break;
            }
        }
    }
    private void StartRunningTowardTarget()
    {
        enemyState = EnemyState.Run;
    }
    private void RunTowardTarget()
    {
        if (runnerTarget == null) return;
        this.transform.position = Vector3.MoveTowards(this.transform.position, runnerTarget.position, Time.deltaTime * moveSpeed);

        if(Vector3.Distance(this.transform.position, runnerTarget.position) < 0.1f)
        {
            onRunnerDie?.Invoke();
            Destroy(this.gameObject);
            Destroy(runnerTarget.gameObject);
        }
    }
}
