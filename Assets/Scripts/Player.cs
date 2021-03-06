﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position =  new Vector3(0,0,0);
    }

    // Update is called once per frame, typically about 60 frames per second
    void Update()
    {
        CalculateMovement();
        FireLaserIfNeeded();
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0); 
        
        transform.Translate(direction * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), transform.position.z);

        if (transform.position.x > 11.3f){
            transform.position = new Vector3(-11.3f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -11.3f){
            transform.position = new Vector3(11.3f, transform.position.y, transform.position.z);
        }
    }

    void FireLaserIfNeeded()
    {
        if (ShouldFireLaser()){
            _canFire = Time.time + _fireRate;
            FireLaser();
        }
    }

    bool ShouldFireLaser()
    {
        bool spaceKeyClicked = Input.GetKeyDown(KeyCode.Space);
        bool isCooldownFinished = Time.time > _canFire;
        return spaceKeyClicked && isCooldownFinished;
    }

    void FireLaser()
    {
        float yOffset = 0.8f;
        Vector3 laserStartingPosition = transform.position + new Vector3(0, yOffset, 0);
        Instantiate(_laserPrefab, laserStartingPosition, Quaternion.identity); // Quaternion.identity is the default rotation
    }
}
