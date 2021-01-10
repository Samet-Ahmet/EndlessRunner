using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    private List<GameObject> activeTiles;
    private Transform playerTransform;
    private int amountOfTileOnScreen = 20;
    private int lastPrefabIndex = 0;
    private float spawnZ = -5.0f; // Yeni oluşturulacak yolun konumu
    private float tileLength = 10.0f; // Her bir prefab'ın uzunluğu
    private float safeZone = 15.0f;

    void Start()
    {
        activeTiles = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        // Oyunun başlangıcında oluşan yollar
        for (int i = 0; i < amountOfTileOnScreen; i++)
        {
            // İlk beş yol engelsiz olmalı
            if (i < 5)
                SpawnTile(0);
            else
                SpawnTile();
        }
    }

    void Update()
    {
        // İleriye yol ekle, arkada kalan yolları sil
        if (playerTransform.position.z - safeZone > (spawnZ - amountOfTileOnScreen * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }
    }
    // Yol oluşturan fonksiyon
    private void SpawnTile(int prefabIndex = -1)
    {
        GameObject go;

        if (prefabIndex == -1)
            go = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
        else
            go = Instantiate(tilePrefabs[prefabIndex]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        activeTiles.Add(go);
    }
    // Arkada kalan yolları kaldıran fonksiyon
    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    // Yolların rastgele oluşmasını sağlayan fonksiyon
    private int RandomPrefabIndex()
    {
        if (tilePrefabs.Length <= 1)
            return 0;

        int randomIndex = lastPrefabIndex;
        // Aynı yolu arka arkaya oluşturma
        while (randomIndex == lastPrefabIndex)
            randomIndex = Random.Range(0, tilePrefabs.Length);
        lastPrefabIndex = randomIndex;
        return randomIndex;
    }
}