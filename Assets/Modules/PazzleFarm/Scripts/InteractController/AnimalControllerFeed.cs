using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Puzzle.Farm
{
    public class AnimalControllerFeed : InteractObjectController
    {
        [SerializeField] private string _animationBlendTreeName;
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _objectToEat;
        [SerializeField] private ParticleSystem _eatParticle;

        private bool _isFeed = false;
        private float _animationBlendTreeValue = 0;
        private float _stepChangeValue = 0.03f;
        private Coroutine _switchAnimationCoroutine;

        public override void DoAction()
        {
            base.DoAction();
            _isFeed = !_isFeed;

            if(_switchAnimationCoroutine!=null)
            {
                StopCoroutine(_switchAnimationCoroutine);
            }
            _switchAnimationCoroutine = StartCoroutine(SwitchAnimationCoroutine(_isFeed));
        }

        private IEnumerator SwitchAnimationCoroutine(bool isFeed)
        {
            if( isFeed)
            {
                if(_eatParticle!=null && _objectToEat!=null)
                {
                    _eatParticle.Play();
                    yield return new WaitForSeconds(0.1f);
                    _objectToEat.SetActive(true);
                }

                while (_animationBlendTreeValue <= 1f)
                {
                    _animationBlendTreeValue += _stepChangeValue;
                    _animator.SetFloat(_animationBlendTreeName, _animationBlendTreeValue);
                    yield return new WaitForSeconds(_stepChangeValue);
                }
            }
            else
            {
                if (_eatParticle != null && _objectToEat != null)
                {
                    _eatParticle.Play();
                    yield return new WaitForSeconds(0.1f);
                    _objectToEat.SetActive(false);
                }

                while (_animationBlendTreeValue >= _stepChangeValue)
                {
                    _animationBlendTreeValue -= _stepChangeValue;
                    _animator.SetFloat(_animationBlendTreeName, _animationBlendTreeValue);
                    yield return new WaitForSeconds(_stepChangeValue);
                }
                
            }
            
        }
    }
}
