using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBox : MonoBehaviour
{
    [SerializeField]
    private float skySpeed;

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * skySpeed);
    }
}
