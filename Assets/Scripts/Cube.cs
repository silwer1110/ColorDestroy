using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Renderer _renderere;

    public event Action<Cube> Destroed;

    public Rigidbody Rigidbody { get; private set; }
    public float SplitChance { get; private set; } = 100f;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        ChangeRandomColor();
    }

    public void SetSplitChance(float splitChance)
    {
        SplitChance = splitChance;
    }
   
    public void Destroy()
    {
        Destroed?.Invoke(this);
        Destroy(gameObject);
    }

    private void ChangeRandomColor()
    {
        _renderere.material.color = UnityEngine.Random.ColorHSV();
    }
}