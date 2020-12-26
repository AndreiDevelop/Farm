using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Puzzle.Farm
{
    public class AnimalControllerRest : InteractObjectController
    {
        [SerializeField] private string _animationBlendTreeName;
        [SerializeField] private Animator _animator;
        [SerializeField] private ParticleSystem[] _restParticleSystem;

        private bool _isRest = false;
        private float _animationBlendTreeValue = 0;
        private float _stepChangeValue = 0.03f;
        private Coroutine _switchAnimationCoroutine;

        public override void DoAction()
        {
            base.DoAction();
            _isRest = !_isRest;

            if(_isRest)
            {
                foreach(var particle in _restParticleSystem)
                    particle.Play();
            }
            else
            {
                foreach (var particle in _restParticleSystem)
                    particle.Stop();
            }

            if (_switchAnimationCoroutine != null)
            {
                StopCoroutine(_switchAnimationCoroutine);
            }
            _switchAnimationCoroutine = StartCoroutine(SwitchAnimationCoroutine(_isRest));
        }

        private IEnumerator SwitchAnimationCoroutine(bool isRest)
        {
            if (isRest)
            {
                while (_animationBlendTreeValue <= 1f)
                {
                    _animationBlendTreeValue += _stepChangeValue;
                    _animator.SetFloat(_animationBlendTreeName, _animationBlendTreeValue);
                    yield return new WaitForSeconds(_stepChangeValue);
                }
            }
            else
            {
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
