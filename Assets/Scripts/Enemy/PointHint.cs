using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Animator))]
public class PointHint : MonoBehaviour
{
    private TMP_Text text;
    private Animator animator;

    public void Start()
    {
        text = GetComponent<TMP_Text>();
        animator = GetComponent<Animator>();
    }

    public void TriggerPointHint(int point)
    {
        if (point >= 0)
        {
            text.text = string.Format("+" + point.ToString());
        }
        else
        {
            text.text = point.ToString();
        }

        animator.SetTrigger("Pop");
    }
}
