using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventoryInput : MonoBehaviour
{
    Player_Wolf actions;
    PlayerWolf player;


    private void Awake()
    {
        actions = new Player_Wolf();
        player = GetComponent<PlayerWolf>();

    }

    private void OnEnable()
    {
        actions.Inventory.Enable();
        actions.Inventory.InventoryOnOff.performed += OnInventoryShortcut;
    }

    private void OnDisable()
    {
        actions.Inventory.InventoryOnOff.performed -= OnInventoryShortcut;
        actions.Inventory.Disable();
    }

    private void OnInventoryShortcut(InputAction.CallbackContext obj)
    {
        GameManager.INSTANCE.InvenUI.InventoryOnOffSwitch();
    }
}
