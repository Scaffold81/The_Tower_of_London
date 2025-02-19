using System;
using System.Collections.Generic;
using TowerOfLondon.Puzzle;
using TowerOfLondon.Structures;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    [SerializeField] private List<PinController> _pins;
    private PinController _lastPin;
    private List<GameObject> _rings = new();

    private float _offsetY = 3f;

    public Action TurnOn;

    public PinController LastPin { get => _lastPin; set => _lastPin = value; }

    public List<PinController> Pins
    {
        get { return _pins; }
        private set { _pins = value; }
    }

    public void InitializeBoard(Board boardConfig)
    {
        for (int i = 0; i < Pins.Count; i++)
        {
            Pins[i].Initialize(this, boardConfig.pin[i].pinCapacity);
        }

        CreateRingsOnPins(boardConfig);
    }

    private void CreateRingsOnPins(Board config)
    {
        ClearRings();
        ClearPins();
        for (int i = 0; i < config.pin.Count; i++)
        {
            if (config.pin[i].rings.Count > 0)
            {
                var yOffset = 0.0f;
                foreach (RingController ring in config.pin[i].rings)
                {
                    var ringTemp = Instantiate(ring, new Vector3(Pins[i].transform.position.x, Pins[i].transform.position.y + yOffset, Pins[i].transform.position.z), Quaternion.identity);
                    _rings.Add(ringTemp.gameObject);
                    Pins[i].Rings.Add(ringTemp);
                    yOffset += 2;
                }
            }
        }
    }

    private void ClearPins()
    {
        foreach (var pin in Pins)
        {
            pin.Rings.Clear();
        }
    }

    public void ClearRings()
    {
        foreach (var ringObject in _rings)
        {
            Destroy(ringObject);
        }
        _rings.Clear();
    }

    public void SetLastPin(PinController pinController)
    {
        LastPin = pinController;
    }

    public void MoveRingToLastPin(RingController ring)
    {
        if (LastPin != null)
        {
            ring.transform.position = new Vector3(LastPin.transform.position.x, LastPin.transform.localScale.y + _offsetY, LastPin.transform.position.z);
        }
    }
}