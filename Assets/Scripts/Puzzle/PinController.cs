using System.Collections.Generic;
using System.Linq;
using System.Timers;
using UnityEngine;

namespace TowerOfLondon.Puzzle
{
    public class PinController : MonoBehaviour
    {
        private BoardController _boardController;
        private List<RingController> _rings = new List<RingController>();
        private int _pinCapacity = 1;

        public List<RingController> Rings
        {
            get { return _rings; }
            private set { _rings = value; }
        }

        public void Initialize(BoardController boardController, int pinCapacity)
        {
            _pinCapacity = pinCapacity;
            _boardController = boardController;
            transform.localScale = new Vector3(transform.localScale.x, pinCapacity, transform.localScale.z);
        }

        public void AddRingToPin(RingController ring, bool isBeingMoved = false)
        {
            if (IsNeighboringPin())
            {
                ring.transform.position = new Vector3(transform.position.x, ring.transform.position.y, transform.position.z);

                if (Rings.Count < _pinCapacity)
                {
                    Rings.Add(ring);
                    LockRings();
                    ring.Sound.PlaySound();
                    _boardController.TurnOn();
                }
                else
                {
                    _boardController.MoveRingToLastPin(ring);
                }
            }
            else
            {
                ring.HasTriggeredEnter = false;
                _boardController.MoveRingToLastPin(ring);
            }
        }

        public void RemoveRingFromPin(RingController ring)
        {
            Rings.Remove(ring);
            LockRings();
            ring.Sound.PlaySound();
            _boardController.SetLastPin(this);
        }

        private void LockRings()
        {
            var lastRing = Rings.LastOrDefault();

            foreach (RingController ring in Rings)
            {
                ring.IsLocked = (ring != lastRing);
            }
        }

        private bool IsNeighboringPin()
        {
            var currentIndex = _boardController.Pins.IndexOf(this);
            var lastPinIndex = _boardController.Pins.IndexOf(_boardController.LastPin);

            return Mathf.Abs(lastPinIndex - currentIndex) == 1;
        }
    }
}