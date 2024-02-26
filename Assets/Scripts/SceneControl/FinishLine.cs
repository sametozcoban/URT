using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SnowBoarder.SceneControl
{
    public class FinishLine : MonoBehaviour
    {
        [SerializeField] float finishGameReloadTime = 1f;
        [SerializeField] ParticleSystem _finishParticleSystem;
        [SerializeField] int sceneIndex;
        [SerializeField] public  Canvas _finishImage;

        public static Action onGameCompleted;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == "Player")
            {
                _finishParticleSystem.Play();
               Invoke("FinishGame", finishGameReloadTime);
            }
        }

        private void FinishGame()
        {
            _finishImage.gameObject.SetActive(true);
            onGameCompleted?.Invoke();
        }
    }
}

