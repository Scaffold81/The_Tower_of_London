using System;
using System.Collections.Generic;
using TowerOfLondon.Puzzle;
using TowerOfLondon.Structures;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    [SerializeField]
    private List<PinController> _pins;

    private PinController _lastPin;
    private float _offsetY=1.5f;

    void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        foreach (var pin in _pins)
        {
            pin.Initialize(this);
        }
    }

    public void SetLastPin(PinController pinController)
    {
        _lastPin=pinController;
    }

    public void MoveRingToLastPin(RingController ring)
    {
        ring.transform.position = new Vector3(_lastPin.transform.position.x, _lastPin.transform.position.y * _offsetY, _lastPin.transform.position.z);
    }
    
}
