using UnityEngine;
using UnityEngine.Events;

namespace Puzzle.Farm
{
    public class ButtonClick3D : MonoBehaviour
    {
        public UnityEvent OnClick;
        protected virtual void OnMouseDown()
        {
            Debug.Log("OnMouseDown");
            OnClick?.Invoke();
        }

        protected virtual void OnMouseUp()
        {
            Debug.Log("OnMouseUp");
        }
    }
}

