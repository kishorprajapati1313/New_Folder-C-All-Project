using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Travel : MonoBehaviour
{
    public Transform start;
    public Transform end;
    public float speed = 5f;

    public Transform Target;

    private void Start()
    {
        Target = start;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target.position, speed * Time.deltaTime);
        if (transform.position == Target.position)
        {
            if (Target == start)
            {
                Target = end;
            }
            else
            {
                Target = start;
            }
        }
    }
}
