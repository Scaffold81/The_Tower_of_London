using System.Linq;
using System.Net.NetworkInformation;
using TowerOfLondon.Structures;
using UnityEngine;

namespace TowerOfLondon.Puzzle
{
    public class PinController : MonoBehaviour
    {
        private BoardController _boardController;

        [SerializeField]
        private Pin _pinData;
        
        private void Start()
        {

        }

        public void Initialize(BoardController boardController)
        {
            _boardController = boardController;
        }

        public void AddRing(RingController ring)
        {
            if (_pinData.rings.Count < _pinData.pinCapacity)
                _pinData.rings.Add(ring);
            else 
                _boardController.MoveRingToLastPin(ring);
        }

        public void RemoveRing(RingController ring)
        {
            if (_pinData.rings.Count > 0)
                _pinData.rings.Remove(ring);
        }

        private void LockRing()
        {
            RingController lastRing = _pinData.rings.LastOrDefault();

            foreach (RingController ring in _pinData.rings)
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
               _boardController.SetLastPin(this);
            }
           

        }
    }
}
