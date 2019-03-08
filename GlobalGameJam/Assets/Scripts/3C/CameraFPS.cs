using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFPS : MonoBehaviour
{

    public void Rotate(float p_MouseX, float p_MouseY)
    {
        Vector3 rotation = new Vector3(p_MouseY , p_MouseX, 0);
        transform.rotation = Quaternion.Euler(rotation);
    }
}
