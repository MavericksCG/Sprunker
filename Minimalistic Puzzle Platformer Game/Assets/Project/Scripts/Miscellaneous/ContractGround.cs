using System;
using UnityEngine;
using EZCameraShake;
using UnityEditor;

namespace Sprunker.Miscellaneous {
    public class ContractGround : MonoBehaviour {

        #region Singleton

        public static ContractGround instance;

        private void Awake () {
            instance = this;
        }

        #endregion

        [Header("References and Variables")] [SerializeField]
        private Animator contractAnimator;

        [SerializeField] private bool useParticles;
        [SerializeField] private GameObject particleToEnable;


        public void Contract () {
            contractAnimator.SetTrigger("contract");
            CameraShaker.Instance.ShakeOnce(.5f, 0.3f, 0.5f, 1f);

            if (useParticles) {
                particleToEnable.SetActive(true);
            }
        }

    }
}