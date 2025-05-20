using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public static ChunkManager instance;
    [Header("Elements")]
    [SerializeField] private LevelSO[] levels;
    private GameObject finishLine;
    private void Awake()
    {
        if (instance != null) Destroy(this.gameObject);
        else instance = this;

        GenarateLevel();
        finishLine = GameObject.FindWithTag("Finish");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void GenarateLevel()
    {
        int currentLevel = GetLevel();
        currentLevel = currentLevel % levels.Length;
        LevelSO level = levels[currentLevel];
        CreateOrderedChunks(level.chunks);
    }
    private void CreateOrderedChunks(Chunk[] chunksLevel)
    {
        Vector3 chunkPos = Vector3.zero;
        for (int i = 0; i < chunksLevel.Length; i++)
        {
            Chunk chunkToCreate = chunksLevel[i];
            if (i > 0) chunkPos.z += chunkToCreate.GetLength() / 2;
            Chunk chunkInstance = Instantiate(chunkToCreate, chunkPos, Quaternion.identity, transform);
            chunkPos.z += chunkToCreate.GetLength() / 2;
        }
    }
    public float GetzPosFinishLine()
    {
        return finishLine.transform.position.z;
    }
    public int GetLevel()
    {
        return PlayerPrefs.GetInt("Level", 0);
    }
}
