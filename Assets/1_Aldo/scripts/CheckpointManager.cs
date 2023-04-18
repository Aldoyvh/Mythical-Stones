using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheckpointManager : MonoBehaviour
{
    public Checkpoint[] checkpoints;
    static int currentCheckpoint;
    public int CurrentCheckpoint
    {
        get
        {
            return currentCheckpoint;
        }
        set
        {
            currentCheckpoint = value;
            for(int i =0; i< checkpoints.Length; i++)
            {
                if(checkpoints[i].id<= currentCheckpoint)
                {
                    checkpoints[i].gameObject.SetActive(false);
                }
            }
        }
    }
    public Checkpoint GetActiveCheckpoint()
    {
        for (int i = 0; i < checkpoints.Length; i++)
        {
            if (checkpoints[i].id == currentCheckpoint)
            {
                sCORE.points =0;
                return checkpoints[i];
            }
        }
        return null;
    }
}
