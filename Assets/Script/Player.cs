using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    public float playerHp = 100.0f;
    public float maxHP = 100.0f;
    PlayerInput action;
    PotionDelay potion;
    Artifact artifact;

    //public float HP
    //{
    //    get { return playerHp; }
    //    set { playerHp = value; }
    //}
    

    //public float MaxHP
    //{
    //    get => maxHP;
    //    set => playerHp = value;
    //}

    //public Action onHealthChange { get; set; }

    private void Awake()
    {
        action = new();
        artifact = new Artifact();
        potion = new PotionDelay();
    }
    private void FixedUpdate()
    {
        playerHp = artifact.Healing(playerHp);
    }

    private void OnEnable()
    {
        action.Player.Enable();
        action.Player.DrinkPotion.performed += OnDrink;
    }

    private void OnDisable()
    {
        action.Player.DrinkPotion.performed -= OnDrink;
        action.Player.Disable();
    }

    private void OnDrink(InputAction.CallbackContext _)
    {
        playerHp = potion.DrinkPotion(playerHp);
        Debug.Log($"현재 남은 Hp는 {playerHp}입니다.");
    }
}
