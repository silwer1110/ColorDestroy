using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Renderer _renderere;

    public event Action<Cube> OnDestroed;

    public float SplitChance { get; private set; } = 100f;

    private void Start()
    {
        ChangeRandomColor();
    }

    private void ChangeRandomColor()
    {
        _renderere.material.color = UnityEngine.Random.ColorHSV();
    }

    public void SetSplitChance(float splitChance)
    {
        SplitChance = splitChance;
    }
    
    public Rigidbody GetRigidbody()
    {
        return GetComponent<Rigidbody>();
    }

    public void HandleDestruction()
    {
        OnDestroed?.Invoke(this);
        Destroy(gameObject);
    }
}