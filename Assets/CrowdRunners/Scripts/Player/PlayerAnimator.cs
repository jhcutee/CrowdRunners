using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public void RunAnimation()
    {
        for (int i = 0; i < PlayerController.instance.RunnersParent.transform.childCount;i++)
        {
            Transform runner = PlayerController.instance.RunnersParent.transform.GetChild(i);
            Animator runnerAnimator = runner.GetComponent<Runner>().GetAnimator();

            runnerAnimator.Play("Run");
        }
    }
    public void IdleAnimation()
    {
        for (int i = 0; i < PlayerController.instance.RunnersParent.transform.childCount; i++)
        {
            Transform runner = PlayerController.instance.RunnersParent.transform.GetChild(i);
            Animator runnerAnimator = runner.GetComponent<Runner>().GetAnimator();

            runnerAnimator.Play("Idle");
        }
    }
}
