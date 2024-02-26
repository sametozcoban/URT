using System;
using System.Collections;
using System.Collections.Generic;
using SnowBoarder.Characters;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace SnowBoarder.CrashControl
{
    public class CrashDetector : MonoBehaviour
    {
        [SerializeField] ParticleSystem _crashParticle;
        [SerializeField] ParticleSystem _dustTrailEffect;
        

        public static bool hasCrashed = false;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == "Ground" && !hasCrashed)
            {
                hasCrashed = true;
                _crashParticle.Play();
                var disableControl = FindObjectOfType<PlayerController>().DisableControl;
            }
        }
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.tag == "Ground")
            {
                _dustTrailEffect.Play();
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.tag == "Ground")
            {
                _dustTrailEffect.Stop();
            }
        }
    }
}

