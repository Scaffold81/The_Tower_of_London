using TowerOfLondon.Audio;
using TowerOfLondon.Enums;
using UnityEngine;

namespace TowerOfLondon.Puzzle
{
    public class RingController : MonoBehaviour
    {
        private Rigidbody _rb;
        private bool isDragging = false;
        [SerializeField]
        private float _offsetY = 1.2f;

        private Vector3 _startPosition;

        [SerializeField]
        private RingType _ringType;

        public RingType RingType
        {
            get { return _ringType; }
            private set { _ringType = value; }
        }

        public bool IsLock { get; set; }
        
        public SoundManager Sound { get; private set; }

        private void Awake()
        {
            Sound=GetComponent<SoundManager>();
            _rb = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// При отсутвии ВР контроллера 
        /// </summary>
        #region ForMousrController

        private void OnMouseDown()
        {
            if (IsLock) return;

            if (!isDragging)
            {
                isDragging = true;
                _startPosition = transform.position;
                transform.up = Vector3.up;
                //Сбрасываем у Rigidbody скорость. Что бы не крутилось 
                _rb.angularVelocity = Vector3.zero;
                _rb.linearVelocity = Vector3.zero;

            }
        }

        private void OnMouseDrag()
        {
            if (isDragging)
            {
                var mousePos = Input.mousePosition;

                mousePos.z = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);

                var worldPos = Camera.main.ScreenToWorldPoint(mousePos);

                transform.position = new Vector3(worldPos.x, worldPos.y * _offsetY, _startPosition.z);
            }
        }

        private void OnMouseUp()
        {
            isDragging = false;
        }

        #endregion ForMousrController
    }
}
