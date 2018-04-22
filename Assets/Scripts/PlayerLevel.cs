using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{

    private int level = 1;

    public int Level
    {
        get { return level; }
        private set
        {
            level = value;
        }
    }

    public event EventHandler LeveledUp;

    private int experience = 0;

    public int Experience
    {
        get { return experience; }
        private set
        {
            while (true)
            {
                experience = value;
                if (experience >= GetExpForNextLevel())
                {
                    Level++;
                    Debug.Log("Level up : " + Level + "! Next needed xp : " + (GetExpForNextLevel() - GetExpForLevel(Level)).ToString());
                    GameObject.Find("LevelText").GetComponent<Text>().text = "LVL " + Level;
                    if (LeveledUp != null)
                    {
                        LeveledUp(this, null);
                    }
                }
                else
                {
                    break;
                }
            }
            GameObject.Find("LevelProgressBar").GetComponent<ProgressBar>().SetProgress(CompletionForNextLevel());
        }
    }

    int GetExpForNextLevel()
    {
        return GetExpForLevel(level + 1);
    }
    int GetExpForLevel(int lvl)
    {
        if (lvl == 1)
        {
            return 0;
        }
        return (lvl * (int)Math.Floor(Math.Pow(1.34, lvl))) + (15 * lvl);
    }
    float CompletionForNextLevel()
    {
        if (experience == 0)
        {
            return 0f;
        }
        if (level <= 1)
        {
            return (float)Experience / GetExpForNextLevel();
        }

        int currentLvl = GetExpForLevel(level);
        int nextLvl = GetExpForNextLevel();
        /*
        float expMinus = Experience - nextLvl;
        var result = expMinus / currentLvl;
        */
        var result = (float)(Experience - currentLvl) / (nextLvl - currentLvl);
        return result;
    }
    public void AddExperience(int value)
    {
        Experience += value;
    }
    // Use this for initialization
    void Start()
    {
        // StartCoroutine(TestCorountine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator TestCorountine()
    {
        while (true)
        {
            Experience += 1;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
