using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NewGameManagerExtension
{
    public static void Enqueue4MonstersFromData(this NewGameManager gm, int dungeon)
    {
        gm.CurrentDungeonMonsterQueue = new Queue<Monster>();
        List<int> randomList = new List<int>() { 1, 2, 3, 4, 5 };

        for (int i = 0; i < 4; i++)
        {
            int randomIndex = Random.Range(0, randomList.Count);
            int randomNum = randomList[randomIndex] + ((dungeon - 1) * 5);
            randomList.RemoveAt(randomIndex);
            Monster monster = DataManager.Instance.GetMonster(randomNum);
            gm.CurrentDungeonMonsterQueue.Enqueue(monster);
        }
    }

    public static void DequeueMonsterCurrentStage(this NewGameManager gm)
    {
        if(gm.CurrentDungeonMonsterQueue.Count > 0)
        {
            gm.CurrentMonster = gm.CurrentDungeonMonsterQueue.Dequeue();
        }
        else
        {
            Debug.LogError("gm.CurrentDungeonMonsterQueue is Empty");
        }
    }
}