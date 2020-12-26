using System.Collections;
using UnityEngine;

namespace Puzzle.Farm
{
    public class AnimalControllerChangeDestination : InteractObjectController
    {
        [SerializeField] private string _animationByDefaultName;
        [SerializeField] private int _animationByDefaultValue;
        [SerializeField] private int _animationWhenTap;
        [SerializeField] private float _animationWhenTapDurationIsSeconds;
        [SerializeField] private Animator _animator;
        [SerializeField] private MoveByPoints _move;
        [SerializeField] private bool _isEndlessMovement;

        private Coroutine _switchAnimationCoroutine;

        private void Start()
        {
            _animator.SetInteger(_animationByDefaultName, _animationByDefaultValue);
            _move.IsEndlessMovement = _isEndlessMovement;
            _move.MoveToNextPoint();
        }

        public override void DoAction()
        {
            base.DoAction();

            _move.MoveToNextPoint(true);

            if(_switchAnimationCoroutine!=null)
            {
                StopCoroutine(_switchAnimationCoroutine);
            }

            _switchAnimationCoroutine = StartCoroutine(SwitchAnimation());
        }

        private IEnumerator SwitchAnimation()
        {
            _animator.SetInteger(_animationByDefaultName, _animationWhenTap);
            yield return new WaitForSeconds(_animationWhenTapDurationIsSeconds);
            _animator.SetInteger(_animationByDefaultName, _animationByDefaultValue);           
        }
    }
}
