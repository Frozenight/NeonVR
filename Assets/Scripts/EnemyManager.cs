using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    [SerializeField] private TMP_Text textMesh = null;
    private int enemyCount;
    private float searchCountdown = 1f;
    private bool levelFinished=false;
    private void Update()
    {
        if (!EnemyIsAlive() && !levelFinished)
        {
            levelFinished = true;
            OpenDoors();
        }
    }
    private void OpenDoors()
    {
        animator.Play("EndDoorAnimation", 0, 0.0f);
    }
    private bool EnemyIsAlive()
    {
        searchCountdown-=Time.deltaTime;
        if(searchCountdown<=0)
        {
            searchCountdown = 1f;
            enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
            UpdateText();
            if (enemyCount == 0)
            {
                return false;
            }
        }
        return true;
    }
    private void UpdateText()
    {
        textMesh.text = $"Remaining enemies:\n{enemyCount}";
    }
}
