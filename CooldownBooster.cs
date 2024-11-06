using System;
using System.Collections;
using BLINK.RPGBuilder.World;
using UnityEngine;

namespace _Development.Scripts.Booster
{
    public class CooldownBooster : MonoBehaviour
    {
        public TimerView Timer;
        public InteractableObject Interactable;

        private Coroutine _coroutine;

        private bool _isShowTimer;

        public void SetShowTimer() => 
            _isShowTimer = true;

        public void StartTimer()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                Timer.gameObject.SetActive(false);
            }

            _coroutine = StartCoroutine(StartCoroutineAction(DateTime.Now.AddSeconds(Interactable.Cooldown)));
        }

        private IEnumerator StartCoroutineAction(DateTime time)
        {
            while (time >= DateTime.Now)
            {
                if (_isShowTimer)
                {
                    if (Timer.gameObject.activeInHierarchy == false)
                        Timer.gameObject.SetActive(true);
                    
                    Timer.Show(time - DateTime.Now);
                }

                yield return new WaitForSeconds(0.2f);
            }

            _isShowTimer = false;
            Timer.gameObject.SetActive(false);
        }
    }
}