using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BTController : EnemyController
{
    [Header("BT Specific")]
    public SpriteRenderer spriteRenderer;
    public float dissolveTimeLength;

    private float startHoldingTime = -1f;
    private int bonusTime = 3;

    protected override void Update()
    {
        timer += Time.deltaTime;

        if (startHoldingTime < 0f) // Not holding yet
        {
            transform.Translate(-transform.right * Time.deltaTime * speed);
        }

        if (timer > 3f && isInteractable && startHoldingTime < 0f)
        {
            isInteractable = false;
            OnEnemyStopInteract?.Invoke(int.Parse(gameObject.name));
        }

        if (timer >= 4f)
        {
            Destroy(gameObject);
        }
    }

    public override void GetDealWith(InputType inputType)
    {
        if (timer < 2f || !isInteractable)
        {
            return;
        }

        if (inputType == InputType.Down && startHoldingTime < 0f)
        {
            AddBasePointByTime();
            startHoldingTime = timer;
            animator.SetTrigger("GetHit");
        }
        else if (inputType == InputType.Hold && startHoldingTime > 0f)
        {
            if (timer - startHoldingTime >= 0.25f)
            {
                gameManager.ChangeScore(1);
                pointHint.TriggerPointHint(1);
                startHoldingTime += 0.25f;
                bonusTime--;
                if (bonusTime == 0)
                {
                    Dissolve();
                }
            }
        }
        else if (inputType == InputType.Up && startHoldingTime > 0f)
        {
            Dissolve();
        }

        return;
    }

    private void Dissolve()
    {
        DyingTrigger();
        StartCoroutine(Dissolving());
    }

    private IEnumerator Dissolving()
    {
        isInteractable = false;
        OnEnemyStopInteract?.Invoke(int.Parse(gameObject.name));

        float alpha = 1f;
        float rate = 0.01f / dissolveTimeLength;

        while (alpha > 0f)
        {
            alpha -= rate;
            if (alpha <= 0f) alpha = 0f;
            spriteRenderer.color = new Color(1f, 1f, 1f, alpha);
            yield return new WaitForSeconds(0.01f);
        }

        Destroy(gameObject);
    }
}
