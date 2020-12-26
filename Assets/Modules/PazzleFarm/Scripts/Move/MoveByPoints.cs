using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Puzzle.Farm
{
    public class MoveByPoints : MonoBehaviour
    {
        [SerializeField] protected Rigidbody _rigitbody;
        [SerializeField] protected Transform[] _pointArray;
        [SerializeField] protected float _moveTime = 1f;   // Time it takes to get to the position - acts a speed
        [SerializeField] protected float _fastedMoveTime = 0.5f;   // Time it takes to get to the position - acts a speed
        [SerializeField] protected float _speedRotation = 1f;

        protected float _currentMoveTime;
        protected bool _isMoveing = false;
        protected Vector3 _startPos;
        protected Vector3 _endPosition;
        protected Coroutine _moveByPointCoroutine;
        protected int _nextIndexToMovePoint = 0;
        protected int _lastSetIndex = 0;
        protected float _localMoveTime;
        public bool IsEndlessMovement
        {
            get;
            set;
        }

        public void MoveToNextPoint(bool moveFaster = false)
        {
           _localMoveTime = (moveFaster) ? _fastedMoveTime : _moveTime;

            StopMove();
            _moveByPointCoroutine = StartCoroutine(MoveByPoint());
        }

        private IEnumerator MoveByPoint()
        {
            RestartMove();

            while (_isMoveing)
            {
                MoveToTarget(_endPosition);
                Rotate(_endPosition);

                if (!_isMoveing && IsEndlessMovement)
                {
                    RestartMove(true);
                }
                yield return new WaitForEndOfFrame();
            }
        }

        private void RestartMove(bool slowDown = false)
        {
            if(slowDown)
            {
                _localMoveTime = _moveTime;
            }
           
            _nextIndexToMovePoint = RandomIndexExeptLastSelected();
            _lastSetIndex = _nextIndexToMovePoint;
            _endPosition = _pointArray[_nextIndexToMovePoint].position;
            _startPos = transform.position;
            _isMoveing = true;
            _currentMoveTime = 0;
        }

        protected virtual int RandomIndexExeptLastSelected()
        {
            return RandomExeptList(_pointArray.Length - 1, new int[] { _lastSetIndex});
        }

        protected int RandomExeptList(int n, int[] x)
        {
            System.Random r = new System.Random();
            int result = r.Next(n - x.Length);

            for (int i = 0; i < x.Length; i++)
            {
                if (result < x[i])
                    return result;
                result++;
            }
            return result;
        }

        private void MoveToTarget(Vector3 targetPosition)
        {
            _currentMoveTime += Time.deltaTime;
            if (_currentMoveTime > _localMoveTime)
            {
                _currentMoveTime = _localMoveTime;
                _isMoveing = false;
            }

            float percentComplete = _currentMoveTime / _localMoveTime;
            _rigitbody.MovePosition(Vector3.Lerp(_startPos, targetPosition, percentComplete));
        }

        private void StopMove()
        {
            _isMoveing = false;

            if (_moveByPointCoroutine != null)
            {
                StopCoroutine(_moveByPointCoroutine);
            }
        }

        private void Rotate(Vector3 target)
        {
            var targetRotation = Quaternion.LookRotation(target - transform.position, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _speedRotation);
        }
    }
}
