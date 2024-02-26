using System;
using System.Collections;
using System.Collections.Generic;
using SnowBoarder.SceneControl;
using TMPro;
using UnityEngine;

namespace SnowBoarder.Characters
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] public float torqueAmount ;
        [SerializeField] public float boostSpeed ;
        [SerializeField] public float basedSpeed ;
        
        Rigidbody2D _rigidbody2D;
        SurfaceEffector2D _surfaceEffector2D;
        
        bool canMove = true;
        public static bool isPressed;
        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _surfaceEffector2D = FindObjectOfType<SurfaceEffector2D>();
        }

        // Update is called once per frame
        void Update()
        {
            if (canMove)
            {
                RotatePlayer();
                RespondToBoost(); 
            }
        }
        private void OnEnable()
        {
            FinishLine.onGameCompleted += StopFunction;
        }
        private void OnDisable()
        {
            FinishLine.onGameCompleted -= StopFunction;
        }
        private void StopFunction()
        {
            canMove = false;
            _rigidbody2D.AddTorque(0);
            _surfaceEffector2D.speed = 0;
        }
        
        void RotatePlayer()
        {
            if (Input.GetKey(KeyCode.A))
            {
                _rigidbody2D.AddTorque(torqueAmount);

            }
            else if (Input.GetKey(KeyCode.D))
            {
                _rigidbody2D.AddTorque(-torqueAmount);
            }
        }

        void RespondToBoost()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                _surfaceEffector2D.speed = boostSpeed;
                isPressed = true;
            }
            else
            {
                _surfaceEffector2D.speed = basedSpeed;
                isPressed = false;
            }
        }
        public bool DisableControl { get { return canMove = false; } set { return; } }
    }
}