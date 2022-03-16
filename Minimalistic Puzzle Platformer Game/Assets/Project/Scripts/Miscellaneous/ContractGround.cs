using System;
using UnityEngine;

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
            
            if (useParticles) {
                particleToEnable.SetActive(true);
            }
        }

    }
}