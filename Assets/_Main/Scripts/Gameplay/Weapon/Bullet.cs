using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private SphereCollider sphereCollider; 

    private Action<Bullet> onShot;
    private Action<Bullet> onHit;
    private Action<Bullet> onMiss;
    private Action<GameObject, Bullet> onTriggerEnter; 

    public Action<Bullet> OnMiss { get => onMiss; set => onMiss = value; }
    public Action<Bullet> OnHit { get => onHit; set => onHit = value; }
    public Action<Bullet> OnShot { get => onShot; set => onShot = value; }
    public SphereCollider SphereCollider { get => sphereCollider; set => sphereCollider = value; }
    public Action<GameObject, Bullet> TriggerEnter { get => onTriggerEnter; set => onTriggerEnter = value; }

    #region Unity Messages

    private void Awake()
    {
    }

    private void OnEnable()
    {
        onShot?.Invoke(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        onHit?.Invoke(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        onTriggerEnter?.Invoke(other.gameObject, this);
    }

    #endregion

    #region Public Methods



    #endregion

}
