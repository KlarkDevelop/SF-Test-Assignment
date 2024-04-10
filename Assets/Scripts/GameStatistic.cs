using System;
using UnityEngine;

public class GameStatistic : MonoBehaviour
{
    public int killsToIncrDificult = 10;
    private int killsValue;
    public static Action<int> onKillsValueUpdate;
    public static Action onGoalReach;


    private void Awake()
    {
        Enemie.onDeath += UpdateCounter;
    }

    private void UpdateCounter()
    {
        killsValue++;
        onKillsValueUpdate?.Invoke(killsValue);
        if (killsValue % killsToIncrDificult == 0)
        {

        }
    }


}
