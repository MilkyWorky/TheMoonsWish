using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;



public class CameraScipt : MonoBehaviour
{
    public enum InputChoice
    {
        KeyboardAndMouse, Controller,
    }

    [Serializable]
    public struct InvertSettings
    {
        public bool invertX;
        public bool invertY;
    }


    public Transform follow;
    public Transform lookAt;
    public CinemachineFreeLook keyboardAndMouseCamera;
    public CinemachineFreeLook controllerCamera;
    public InputChoice inputChoice;
    public InvertSettings keyboardAndMouseInvertSettings;
    public InvertSettings controllerInvertSettings;
    public bool allowRuntimeCameraSettingsChanges;

    public CinemachineFreeLook Current
    {
        get { return inputChoice == InputChoice.KeyboardAndMouse ? keyboardAndMouseCamera : controllerCamera; }
    }

    void Reset()
    {
        Transform keyboardAndMouseCameraTransform = transform.Find("KeyboardAndMouseFreeLookRig");
        if (keyboardAndMouseCameraTransform != null)
            keyboardAndMouseCamera = keyboardAndMouseCameraTransform.GetComponent<CinemachineFreeLook>();

        Transform controllerCameraTransform = transform.Find("ControllerFreeLookRig");
        if (controllerCameraTransform != null)
            controllerCamera = controllerCameraTransform.GetComponent<CinemachineFreeLook>();

        playerHP playerController = GameObject.Find("PlaYer").GetComponent<playerHP>();
        //if (playerController != null && playerController.name == "Ellen")
        //{


        //    if (playerController.cameraSettings == null)
        //        playerController.cameraSettings = this;
        //}

        follow = playerController.transform;

        lookAt = follow.Find("HeadTarget");
    }

    void Awake()
    {
        UpdateCameraSettings();

    }

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (allowRuntimeCameraSettingsChanges)
        {
            UpdateCameraSettings();
        }
    }

    void UpdateCameraSettings()
    {
        keyboardAndMouseCamera.Follow = follow;
        keyboardAndMouseCamera.LookAt = lookAt;
        keyboardAndMouseCamera.m_XAxis.m_InvertInput = keyboardAndMouseInvertSettings.invertX;
        keyboardAndMouseCamera.m_YAxis.m_InvertInput = keyboardAndMouseInvertSettings.invertY;

            //controllerCamera.m_XAxis.m_InvertInput = controllerInvertSettings.invertX;
            //controllerCamera.m_YAxis.m_InvertInput = controllerInvertSettings.invertY;
        //controllerCamera.Follow = follow;
        //controllerCamera.LookAt = lookAt;

        keyboardAndMouseCamera.Priority = inputChoice == InputChoice.KeyboardAndMouse ? 1 : 0;
        //controllerCamera.Priority = inputChoice == InputChoice.Controller ? 1 : 0;
    }
}
