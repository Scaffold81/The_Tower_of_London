using System.Collections.Generic;
using TowerOfLondon.Puzzle;
using TowerOfLondon.Structures;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    [SerializeField]
    private List<PinController> _pins;
    private List<GameObject> _rings;
    private PinController _lastPin;

    private float _offsetY = 2.5f;

    public void InitializeBoard(Board boardConfig)
    {
        for (int i = 0; i < _pins.Count; i++)
        {
            _pins[i].Initialize(this, boardConfig.pin[i].pinCapacity);
        }

        CreateRingsOnPins(boardConfig);
    }

    private void CreateRingsOnPins(Board config)
    {
        if(_rings != null)
            foreach (GameObject r in _rings) Destroy(r);

        for (int i = 0; i < config.pin.Count; i++)
        {
            if (config.pin[i].rings.Count > 0)
            {
                var yOffset = 0.0f;
                foreach (RingController ring in config.pin[i].rings)
                {
                    yOffset += 2;
                    Instantiate(ring, new Vector3(_pins[i].transform.position.x, _pins[i].transform.position.y + yOffset, _pins[i].transform.position.z), Quaternion.identity);
                }
            }
        }
    }

    public void SetLastPinController(PinController pinController)
    {
        _lastPin = pinController;
    }

    public void MoveRingToLastPinController(RingController ring)
    {
        ring.transform.position = new Vector3(_lastPin.transform.position.x, _lastPin.transform.localScale.y + _offsetY, _lastPin.transform.position.z);
    }

    public void TurnON()
    {
    }
}
