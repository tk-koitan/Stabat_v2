using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using KoitanLib;

public class TestPlayer : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction StickInput;
    private InputAction AInput;
    private InputAction BInput;
    private InputAction XInput;
    private InputAction YInput;
    private InputAction StartInput;

    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent(out playerInput);
        StickInput = playerInput.currentActionMap.FindAction("Stick");
        AInput = playerInput.currentActionMap.FindAction("A");
        BInput = playerInput.currentActionMap.FindAction("B");
        XInput = playerInput.currentActionMap.FindAction("X");
        YInput = playerInput.currentActionMap.FindAction("Y");
        StartInput = playerInput.currentActionMap.FindAction("Start");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = StickInput.ReadValue<Vector2>();
        transform.Translate(10f * input * Time.deltaTime);
        KoitanDebug.Display($"Index{playerInput.playerIndex} : {playerInput.devices[0].device} : {AInput.ReadValue<float>()}{BInput.ReadValue<float>()}{XInput.ReadValue<float>()}{YInput.ReadValue<float>()}{StartInput.ReadValue<float>()}\n");
    }
}
