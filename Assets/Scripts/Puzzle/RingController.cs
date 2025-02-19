using NUnit.Framework.Constraints;
using System.Net.NetworkInformation;
using TowerOfLondon.Audio;
using TowerOfLondon.Enums;
using UnityEngine;

namespace TowerOfLondon.Puzzle
{
    public class RingController : MonoBehaviour
    {
        private bool isDragging = false;

        [SerializeField] private float _offsetY = 1.2f;
        private Vector3 _startPosition;

        [SerializeField] private RingType _ringType;
        private PinController _pin;
        private Rigidbody _rb;

        public RingType RingType
        {
            get { return _ringType; }
            private set { _ringType = value; }
        }

        public bool IsLocked { get; set; }
        public bool HasTriggeredEnter { get; set; } = false;
        public SoundManager Sound { get; private set; }

        private void Awake()
        {
            Sound = GetComponent<SoundManager>();
            _rb = GetComponent <Rigidbody>();
        }

        private void Start()
        {
            HasTriggeredEnter = false; // Сбрасываем флаг при старте
        }

        private void OnTriggerEnter(Collider other)
        {
            if (HasTriggeredEnter == true) // Проверяем, было ли уже срабатывание OnTriggerEnter
            {
                _pin = other.GetComponent<PinController>();

                if (_pin != null)
                {
                    _pin.AddRingToPin(this);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            _pin = other.GetComponent<PinController>();

            if (_pin != null)
            {
                _pin.RemoveRingFromPin(this);
                _pin = null;
            }
        }

        #region ForMouseController

        private void OnMouseDown()
        {
            if (IsLocked) return;
            HasTriggeredEnter = true; // Устанавливаем флаг после первого срабатывания
            if (!isDragging)
            {
                isDragging = true;
                _startPosition = transform.position;
                transform.up = Vector3.up;

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
            if (_pin != null)
            {
                transform.position = new Vector3(_pin.transform.position.x, transform.position.y, _pin.transform.position.z);
            }
        }

        #endregion ForMouseController
    }
}