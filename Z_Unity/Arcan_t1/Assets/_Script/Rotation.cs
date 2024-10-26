using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float Roationsp = 100f;

    public void Update(){
        transform.Rotate(Vector3.forward, Roationsp * Time.deltaTime);
    }
}
