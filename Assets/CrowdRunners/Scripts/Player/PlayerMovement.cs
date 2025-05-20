using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool canMove = false;

    [Header("Control")]
    [SerializeField] private float slideSpeed;
    private Vector3 clickedScreenPos;
    private float roadWidth = 10;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {   
            HandleMoveForward();
            HandleControlSilde();
        }
    }
    public void StartMoving()
    {
        canMove = true;
        PlayerController.instance.PlayerAnimator.RunAnimation();
    }
    public void StopMoving()
    {
        canMove = false;
        PlayerController.instance.PlayerAnimator.IdleAnimation();
    }
    private void HandleMoveForward()
    {
        this.transform.position += Vector3.forward * Time.deltaTime * moveSpeed;
    }
    private void HandleControlSilde()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickedScreenPos = Input.mousePosition;
        }
        else if(Input.GetMouseButton(0))
        {
            float xScreenDiff = Input.mousePosition.x - clickedScreenPos.x;
            xScreenDiff/=Screen.width;
            xScreenDiff*=slideSpeed;

            Vector3 postion = this.transform.position;
            postion.x = xScreenDiff * slideSpeed;

            postion.x = Mathf.Clamp(postion.x, -roadWidth/2 + PlayerController.instance.CrowdSystem.GetCrowdRadius(), roadWidth/2 - PlayerController.instance.CrowdSystem.GetCrowdRadius());


            this.transform.position = postion;
        }
    }
}
