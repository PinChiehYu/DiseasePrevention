using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputType { Down, Hold, Up }

[Serializable]
public class LaneInputData
{
    public EnemyType laneType;
    public KeyCode keyCode;
}

public class DetectiveManager : MonoBehaviour
{
    public GameManager gameManager;
    public EnemyManager enemyManager;
    public List<LaneInputData> laneInputDataList;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        foreach (LaneInputData data in laneInputDataList)
        {
            if (Input.GetKeyDown(data.keyCode))
            {
                DetectHitting(data.laneType, InputType.Down);
                Ah();
            }
            else if (Input.GetKeyUp(data.keyCode))
            {
                DetectHitting(data.laneType, InputType.Up);
            }
            else if (Input.GetKey(data.keyCode))
            {
                DetectHitting(data.laneType, InputType.Hold);
            }
        }
    }

    private void DetectHitting(EnemyType laneType, InputType inputType)
    {
        EnemyController controller = enemyManager.GetMostFrontEnemyAtLine(laneType);
        if (controller != null)
        {
            controller.GetDealWith(inputType);
        }
    }

    private void Ah()
    {
        animator.SetTrigger("Ah");
    }
}