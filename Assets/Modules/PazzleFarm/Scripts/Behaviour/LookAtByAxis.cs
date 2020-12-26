using UnityEngine;

namespace Puzzle.Farm
{
    public class LookAtByAxis : MonoBehaviour
    {
        [SerializeField] private Vector3 _axisVectorMask;

        private Transform _cameraTransform;
        private void Awake()
        {
            _cameraTransform = Camera.main.transform;
        }

        public void LookAt()
        {
            transform.LookAt(_cameraTransform);

            Vector3 eulerAngles = transform.rotation.eulerAngles;

            if(_axisVectorMask.x == 0)
            {
                eulerAngles.x = 0;
            }
            if(_axisVectorMask.y == 0)
            {
                eulerAngles.y = 0;
            }
            if (_axisVectorMask.z == 0)
            {
                eulerAngles.z = 0;
            }

            // Set the altered rotation back
            transform.rotation = Quaternion.Euler(eulerAngles);
        }
    }
}
