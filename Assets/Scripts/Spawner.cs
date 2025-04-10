﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube[] _cubes;

    public event Action<List<Rigidbody>, Vector3> Spawned;
    public event Action<Cube> NotSpawned;

    private void OnEnable()
    {
        foreach (Cube cube in _cubes)
            cube.Destroed += Split;
    }

    private void OnDisable()
    {
        foreach (Cube cube in _cubes)
            cube.Destroed -= Split;
    }

    private void Split(Cube cube)
    {
        if (ShouldSplit(cube) == false)
            CreateNewCubs(cube);
        else
            NotSpawned?.Invoke(cube);
    }

    private void CreateNewCubs(Cube cubePrefab)
    {
        List<Rigidbody> rigidbodys = new();

        int cubeCount;

        cubeCount = GetRandomCubeCount();

        for (int i = 0; i < cubeCount; i++)
        {
            Cube cube;

            cube = Instantiate(cubePrefab);

            SetRandomSpawnPosition(cube);

            HalveScale(cube);

            cube.SetSplitChance(GetHalfSplitChance(cubePrefab));

            rigidbodys.Add(cube.Rigidbody);

            cube.Destroed += OnCubeDestroed;
        }

        Spawned?.Invoke(rigidbodys, cubePrefab.transform.localPosition);
    }

    private void OnCubeDestroed(Cube cube)
    {
        if (ShouldSplit(cube) == false)
            CreateNewCubs(cube);
        else
            NotSpawned?.Invoke(cube);

        cube.Destroed -= OnCubeDestroed;
    }

    private float GetHalfSplitChance(Cube cube)
    {
        return cube.SplitChance * 0.5f;
    }

    private bool ShouldSplit(Cube cube)
    {
        float maxChance = 100;

        return cube.SplitChance <= UnityEngine.Random.Range(0, maxChance) + 1;
    }

    private void HalveScale(Cube cube)
    {
        cube.transform.localScale = cube.transform.localScale / 2;
    }

    private void SetRandomSpawnPosition(Cube cube)
    {
        float spawnRadious = 2f;

        cube.transform.position += (UnityEngine.Random.insideUnitSphere + Vector3.up) * spawnRadious;
    }

    private int GetRandomCubeCount()
    {
        int minCubeCount = 2;
        int maxCubeCount = 6;

        return UnityEngine.Random.Range(minCubeCount, maxCubeCount);
    }
}