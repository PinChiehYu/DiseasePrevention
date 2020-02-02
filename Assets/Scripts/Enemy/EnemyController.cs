using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected GameManager gameManager;

    public Action<int> OnEnemyStopInteract;

    protected bool isInteractable;
    protected bool isStop;
    protected float speed = 8f;

    public Animator animator;
    public PointHint pointHint;

    [Header("Point Settings")]
    public List<int> pointsList;

    [Header("Bottom Settings")]
    public SpriteRenderer bottom;
    public List<Color> bottomColorList;

    [Header("Audios")]
    public List<AudioClip> clipList;
    private AudioSource source;

    protected float timer;

    private void Awake()
    {
        timer = 0f;
        isInteractable = true;
        isStop = false;
    }

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        source = GetComponent<AudioSource>();
    }

    protected virtual void Update()
    {
        if (isStop) return;

        timer += Time.deltaTime;

        transform.Translate(-transform.right * Time.deltaTime * speed);

        if (timer > 3f && isInteractable)
        {
            OnEnemyStopInteract?.Invoke(int.Parse(gameObject.name));
            isInteractable = false;
        }

        if (timer >= 4f)
        {
            Destroy(gameObject);
        }
    }

    public virtual void GetDealWith(InputType inputType)
    {
        //Debug.Log("Get Deal With At Time: " + timer.ToString());
        if (inputType != InputType.Down || timer < 2f || !isInteractable)
        {
            return;
        }

        AddBasePointByTime();

        OnEnemyStopInteract?.Invoke(int.Parse(gameObject.name));
        isStop = true;

        StartCoroutine(DyingAnimation());
        return;
    }

    private IEnumerator DyingAnimation()
    {
        DyingTrigger();
        animator.SetTrigger("GetHit");
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    protected void ChangeBottomColor(Color color)
    {
        bottom.color = color;
    }

    protected void AddBasePointByTime()
    {
        if ((timer >= 2f && timer <= 2.25f) || (timer >= 2.7f && timer <= 2.75f))
        {
            gameManager.ChangeScore(pointsList[0]);
            gameManager.ChangeHP(-1);
            pointHint.TriggerPointHint(pointsList[0]);
            ChangeBottomColor(bottomColorList[0]);
        }
        else if ((timer >= 2.25f && timer <= 2.3f) || (timer >= 2.6f && timer <= 2.7f))
        {
            pointHint.TriggerPointHint(pointsList[1]);
            ChangeBottomColor(bottomColorList[1]);
        }
        else if ((timer >= 2.3f && timer <= 2.4f) || (timer >= 2.6f && timer <= 2.65f))
        {
            gameManager.ChangeScore(pointsList[2]);
            pointHint.TriggerPointHint(pointsList[2]);
            ChangeBottomColor(bottomColorList[2]);
        }
        else if ((timer >= 2.4f && timer <= 2.45f) || (timer >= 2.55f && timer <= 2.6f))
        {
            gameManager.ChangeScore(pointsList[3]);
            pointHint.TriggerPointHint(pointsList[3]);
            ChangeBottomColor(bottomColorList[3]);
        }
        else if (timer >= 2.45f && timer <= 2.55f)
        {
            gameManager.ChangeScore(pointsList[4]);
            gameManager.ChangeHP(1);
            pointHint.TriggerPointHint(pointsList[4]);
            ChangeBottomColor(bottomColorList[4]);
        }
    }

    protected void DyingTrigger()
    {
        source.PlayOneShot(clipList[UnityEngine.Random.Range(0, clipList.Count)]);
    }
}
