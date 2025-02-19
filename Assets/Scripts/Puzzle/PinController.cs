using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TowerOfLondon.Puzzle
{
    public class PinController : MonoBehaviour
    {
        private BoardController _boardController;

        private int _pinCapacity = 1;
        private List<RingController> _rings = new();

        public List<RingController> Rings 
        {
            get{ return _rings; }
            private set{ _rings = value; }
        }

        public void Initialize(BoardController boardController, int pinCapacity)
        {
            _pinCapacity = pinCapacity;
            _boardController = boardController;
            transform.localScale = new Vector3(transform.localScale.x, pinCapacity, transform.localScale.z);
        }

        public void AddRing(RingController ring)
        {
            if (Rings.Count < _pinCapacity)
                Rings.Add(ring);
            else
                _boardController.MoveRingToLastPinController(ring);
            // ����� ���������� ������ ����������� ���
            _boardController.Turn();
        }

        public void RemoveRing(RingController ring)
        {
            if (Rings.Count > 0)
                Rings.Remove(ring);
        }

        private void LockRing()
        {
            RingController lastRing = Rings.LastOrDefault();

            foreach (RingController ring in Rings)
            {
                if (ring != lastRing)
                    ring.IsLock = true;
                else
                    ring.IsLock = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var ring = other.GetComponent<RingController>();

            if (ring != null)
            {
                if (IsNeighboringPin())
                {
                    //��� �������. ��� �� ������ ������ ��������� �� ��������
                    ring.transform.position = new Vector3(transform.position.x, ring.transform.position.y, transform.position.z);
                    //��������� ������ �������
                    AddRing(ring);
                    LockRing();
                    ring.Sound.PlaySound();

                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var ring = other.GetComponent<RingController>();

            if (ring != null)
            {
                //��������� ������ �������
                RemoveRing(ring);
                LockRing(); 
                _boardController.SetLastPinController(this);
                ring.Sound.PlaySound();
            }
        }

        /// <summary>
        /// �� ������� ��������� ����� ������ ����� ����������� ������ �� �������� �������
        /// </summary>
        /// <returns></returns>
        private bool IsNeighboringPin()
        {
            int currentIndex = _boardController.Pins.IndexOf(this);
            int lastPinIndex = _boardController.Pins.IndexOf(_boardController.LastPin);

            return Mathf.Abs(lastPinIndex - currentIndex) == 1;
        }
    }
}
