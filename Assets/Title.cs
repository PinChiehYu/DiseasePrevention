using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    private bool start_finish;

    public Transform Plane;
    public float P_x;
    public float P_y;
    private float P_init_x;
    private float P_init_y;

    public Transform Title_Word;
    private float T_init_y;

    public Transform Version;
    private float V_init_x;

    public Transform Ah;
    public float A_jump_strength;
    private float A_init_x;
    private float A_init_y;
    private int count;

    IEnumerator ShowBtn()
    {
        while(true)
        {
            if(start_finish)
            {
                GameObject.Find("TitleUI").transform.GetChild(0).localScale = Vector3.one;
                GameObject.Find("TitleUI").transform.GetChild(1).localScale = Vector3.one;
                break;
            }
            yield return null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        start_finish = false;

        P_init_x = Plane.position.x;
        P_init_y = Plane.position.y;

        T_init_y = Title_Word.position.y;

        V_init_x = Version.position.x;

        A_init_x = Ah.position.x;
        A_init_y = Ah.position.y;
        count = 0;

        Title_Word.position = new Vector3(Title_Word.position.x, 9, Title_Word.position.z);
        Version.position = new Vector3(15, Version.position.y, Version.position.z);
        Ah.position = new Vector3(-15, Ah.position.y, Ah.position.z);
        StartCoroutine(ShowBtn());
        
        StartCoroutine(Ah_In());
        StartCoroutine(Title_In());
        StartCoroutine(Version_In());
    }

    // Update is called once per frame
    void Update()
    {
        if (start_finish)
        {
            if (Plane.position.x > -24)
            {
                Plane.position = new Vector3(Plane.position.x - P_x, Plane.position.y - P_y, Plane.position.z);
            }
            else
            {
                Plane.position = new Vector3(P_init_x, P_init_y, Plane.position.z);
            }
            count++;
            if (count == 300)
            {
                StartCoroutine("Ah_Jump");
                count = 0;
            }
        }
    }

    private float A_speed;
    private IEnumerator Ah_In()
    {
        A_speed = 0;
        while(Ah.position.x < A_init_x)
        {
            A_speed += 0.02f;
            Ah.position = new Vector3(Ah.position.x + A_speed, Ah.position.y, Ah.position.z);
            yield return null;
        }
        while (Ah.position.x > A_init_x)
        {
            A_speed -= 0.05f;
            Ah.position = new Vector3(Ah.position.x + A_speed, Ah.position.y, Ah.position.z);
            if (Ah.position.x < A_init_x) Ah.position = new Vector3(A_init_x, Ah.position.y, Ah.position.z);
            yield return null;
        }
    }
    private IEnumerator Title_In()
    {
        yield return new WaitForSeconds(1.5f);
        while (Title_Word.position.y > T_init_y)
        {
            Title_Word.position = new Vector3(Title_Word.position.x, Title_Word.position.y - 0.07f, Title_Word.position.z);
            if (Title_Word.position.y < T_init_y) Title_Word.position = new Vector3(Title_Word.position.x, T_init_y, Title_Word.position.z);
            yield return null;
        }
    }
    private IEnumerator Version_In()
    {
        yield return new WaitForSeconds(2f);
        Version.position = new Vector3(V_init_x, Version.position.y, Version.position.z);
        count = -20;
        start_finish = true;
    }

    private IEnumerator Ah_Jump()
    {
        while(true)
        {
            yield return new WaitForSeconds(4.0f);

            while (Ah.position.y < 0)
            {
                Ah.position = new Vector3(Ah.position.x, Ah.position.y + A_jump_strength, Ah.position.z);
                yield return null;
            }
            while (Ah.position.y > A_init_y)
            {
                Ah.position = new Vector3(Ah.position.x, Ah.position.y - A_jump_strength, Ah.position.z);
                if (Ah.position.y < A_init_y) Ah.position = new Vector3(Ah.position.x, A_init_y, Ah.position.z);
                yield return null;
            }
            while (Ah.position.y < 0)
            {
                Ah.position = new Vector3(Ah.position.x, Ah.position.y + A_jump_strength, Ah.position.z);
                yield return null;
            }
            while (Ah.position.y > A_init_y)
            {
                Ah.position = new Vector3(Ah.position.x, Ah.position.y - A_jump_strength, Ah.position.z);
                if (Ah.position.y < A_init_y) Ah.position = new Vector3(Ah.position.x, A_init_y, Ah.position.z);
                yield return null;
            }
        }
    }

}
