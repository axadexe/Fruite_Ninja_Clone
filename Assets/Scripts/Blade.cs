using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{

    public Vector3 direction { get; private set; }
    public float sliceForce = 5f;
    private Camera mainCamera;
    private bool slicing;
    private Collider bladeColider;
    private TrailRenderer bladetrail;
    public float minSliceVelocity = 0.01f;
    private void Awake()
    {
        mainCamera = Camera.main;
        bladeColider = GetComponent<Collider>();
        bladetrail = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        StopSlicing();

    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartSlicing();

        }
        else if(Input.GetMouseButtonUp(0))
        {
            StopSlicing();
        }
        else if (slicing)
        {
            ContinuewSlicing();
        }

        
            
        
    }
    private void StartSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;
        transform.position = newPosition;
        slicing = true;
        bladeColider.enabled = true;
        bladetrail.enabled = true;
        bladetrail.Clear();

    }
    private void StopSlicing()
    {
        slicing = false;
        bladeColider.enabled = false;
        bladetrail.enabled = false;

    }
    private void OnDisable()
    {
        StopSlicing();
    }
    private void ContinuewSlicing()
    {
        Vector3 newPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        direction = newPosition - transform.position;
        float velocity = direction.magnitude / Time.deltaTime;
        bladeColider.enabled = velocity > minSliceVelocity;
        transform.position = newPosition;

        

    }
}
