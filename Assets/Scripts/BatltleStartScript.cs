using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatltleStartScript : MonoBehaviour
{
    [SerializeField]
    GameObject Pointer;

    void OnMouseDown()
    {
        Pointer.SetActive(true);
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
            Pointer.SetActive(false);
    }

}
