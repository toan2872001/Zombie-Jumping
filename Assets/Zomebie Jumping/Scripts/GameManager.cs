using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameState state;
    public int startingPlatform;
    public float xSpawnOffset;
    public float minYspawnPos;
    public float maxYspawnPos;
    public Platform[] platformPrefabs;
    public CollectableItem[] collectableItems;

    private Platform m_lastPlatformSpawned;
    private List<int> m_platformLandedIds;
    private float m_halfCamSizeX;
    private int m_score;
    public Player player;

    public Platform LastPlatformSpawned { get => m_lastPlatformSpawned; set => m_lastPlatformSpawned = value; }
    public List<int> PlatformLandedIds { get => m_platformLandedIds; set => m_platformLandedIds = value; }
    
    public int Score { get => m_score; set => m_score = value; }

    public override void Awake()
    {
        MakeSingleton(false);
        m_platformLandedIds = new List<int>();
        m_halfCamSizeX = Helper.Get2DCamSize().x / 2;
    }
    public override void Start()
    {
        base.Start();
        state = GameState.Starting;
        if (AudioController.Ins)
        {
            AudioController.Ins.PlayBackgroundMusic();
        }
    }
    public void PlayGame()
    {
        if (GuiManager.Ins)
        {
            GuiManager.Ins.ShowGamePlay(true);
        }
        Invoke("PlatformInit", 0.5f);
        Invoke("PlayGameIvk", 1f);

    }
    private void PlayGameIvk()
    {
        state = GameState.Playing;
        if (player)
        {
            player.Jump();
        }
    }
    private void PlatformInit()
    {
        m_lastPlatformSpawned = player.PlatformLanded;
        for (int i = 0; i < startingPlatform; i++)
        {
            SpawnPlatform();
        }
    }
    public bool isPlatformLanded(int id)
    {
        if (m_platformLandedIds == null || m_platformLandedIds.Count <= 0) return false;
        return m_platformLandedIds.Contains(0);
    }
    public void SpawnPlatform()
    {
        if (!player || platformPrefabs == null || platformPrefabs.Length <= 0) return;

        float spawnPosX = Random.Range(
            -(m_halfCamSizeX - xSpawnOffset), (m_halfCamSizeX - xSpawnOffset)
            );
        float DistanceBeetweenPlat = Random.Range(minYspawnPos, maxYspawnPos);
        float spawnPosY = m_lastPlatformSpawned.transform.position.y + DistanceBeetweenPlat;
        Vector3 spawnPos = new Vector3(spawnPosX, spawnPosY, 0f);

        int ranIdx = Random.Range(0, platformPrefabs.Length);
        var platformPrefab = platformPrefabs[ranIdx];

        if (!platformPrefab) return;
        var platformClone = Instantiate(platformPrefab, spawnPos, Quaternion.identity);
        platformClone.Id = m_lastPlatformSpawned.Id + 1;
        m_lastPlatformSpawned = platformClone;

    }
    public void SpawnCollectable(Transform spawnPoint)
    {
        if (collectableItems == null || collectableItems.Length <= 0|| state !=GameState.Playing) return;
        int randIdx = Random.Range(0, collectableItems.Length);
        var collectItem = collectableItems[randIdx];

        if (collectItem == null) return;
        float randCheck = Random.Range(0f, 1f);
        if(randCheck <= collectItem.spawnRate && collectItem.collectablePrefab)
        {
            var cClone = Instantiate(collectItem.collectablePrefab, spawnPoint.position, Quaternion.identity);
            cClone.transform.SetParent(spawnPoint);
        }
    }

    public void AddScore(int scoreToAdd)
    {
        if (state != GameState.Playing) return;
        m_score += scoreToAdd;
        Pref.bestScore = m_score;
        if (GuiManager.Ins)
        {
            GuiManager.Ins.UpdateScore(m_score);
        }
    }
}
