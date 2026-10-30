using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FireInput : MonoBehaviour
{
    [SerializeField] GameObject[] laserGO;
    [SerializeField] RectTransform crosshair;
    [SerializeField] Transform targetPoint;
    [SerializeField] float targetdistance = 100;
    ParticleSystem.EmissionModule laser;
    bool isFireOn = false;
    float fire;
    [SerializeField] bool isFireEnable = true;
    void Start()
    {
        Cursor.visible = false;
        foreach(GameObject gameObject in laserGO)
        {
            laser = gameObject.GetComponent<ParticleSystem>().emission;
            laser.enabled = false;
        }
    }

    void OnFire(InputValue value)
    {
        isFireOn = value.isPressed;
    }

    void Update()
    {   if(isFireEnable)
        {
            ProcessFiring();
            MoveCrosshair();
            MoveTargetPoint();   
        } else
        {
            if(laser.enabled)
            {
                foreach (GameObject gameObject in laserGO)
                {
                    laser = gameObject.GetComponent<ParticleSystem>().emission;
                    laser.enabled = false;
                }
                crosshair.gameObject.GetComponent<Image>().enabled = false;
            }
        }
    }

    private void ProcessFiring()
    {
        foreach (GameObject gameObject in laserGO)
        {
            laser = gameObject.GetComponent<ParticleSystem>().emission;
            laser.enabled = isFireOn;
            RotateLaser();
        }
    }

    private void MoveCrosshair()
    {
        crosshair.position = Mouse.current.position.ReadValue();
    }

    private void MoveTargetPoint()
    {
        Vector3 targetPointPosition = new Vector3(Mouse.current.position.x.ReadValue(), Mouse.current.position.y.ReadValue(), targetdistance);
        targetPoint.position = Camera.main.ScreenToWorldPoint(targetPointPosition);
    }

    Vector3 pointingDirection;
    private void RotateLaser()
    {
        foreach (GameObject LasergameObject in laserGO)
        {
            pointingDirection = targetPoint.position - this.transform.position;
            LasergameObject.transform.rotation = Quaternion.LookRotation(pointingDirection);
        }
    }
}
