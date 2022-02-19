using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for weapons
/// </summary>
public interface IWeapon
{
    void Shot(Bullet bullet);
    void Hit(Bullet bullet);
    void Miss(Bullet bullet);
    void OnTriggerEnterFunction(GameObject element, Bullet buller); 
    //void OnHit(Action<GameObject> onHit); 
}
