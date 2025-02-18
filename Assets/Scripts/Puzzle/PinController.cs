using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using TowerOfLondon.Structures;
using UnityEngine;

namespace TowerOfLondon.Puzzle
{
    public class PinController : MonoBehaviour
    {
        private BoardController _boardController;

        private int _pinCapacity  = 1;
        private List<RingController> _rings=new();

        public void Initialize(BoardController boardController,int pinCapacity)
        {
            _pinCapacity = pinCapacity;
            _boardController = boardController;
            transform.localScale=new Vector3(transform.localScale.x, pinCapacity, transform.localScale.z);
        }

        public void AddRing(RingController ring)
        {
            if (_rings.Count < _pinCapacity)
                _rings.Add(ring);
            else 
                _boardController.MoveRingToLastPinController(ring);
        }

        public void RemoveRing(RingController ring)
        {
            if (_rings.Count > 0)
                _rings.Remove(ring);
        }

        private void LockRing()
        {
            RingController lastRing = _rings.LastOrDefault();

            foreach (RingController ring in _rings)
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
                //для красоты. Что бы кольцо всегда одевалось на стержень
                ring.transform.position = new Vector3(transform.position.x, ring.transform.position.y, transform.position.z);
                //Добавляем кольцо стержню
                AddRing(ring);
                LockRing();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var ring = other.GetComponent<RingController>();

            if (ring != null)
            {
                //Добавляем кольцо стержню
                RemoveRing(ring);
                LockRing(); 
                _boardController.TurnON();
                _boardController.SetLastPinController(this);
            }
        }
    }
}
