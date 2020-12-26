using UnityEngine;

namespace Puzzle.Farm
{
    public class Bush : ParticleByTap
    {
        [SerializeField] private int _countToShow;
        [SerializeField] private string _animationTriggerName;
        [SerializeField] private Animator _animator;

        private int _localCount = 0;

        protected override void OnMouseDown()
        {
            base.OnMouseDown();
            if (_localCount == _countToShow)
            {
                _animator.SetTrigger(_animationTriggerName);
                _localCount = 0;
            }
            else
            {
                _localCount++;
            }
        }
    }
}
