using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    [SerializeField] private TMP_Text textMesh = null;
    [SerializeField] private GameObject enemyHolder;
    private int enemyCount;
    private float searchCountdown = 1f;
    private bool levelFinished=false;

    private void Start()
    {
        EnemyIsAlive();
    }

    private void Update()
    {
        CheckForEndLevel();
    }

    public void CheckForEndLevel()
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
        if (enemyHolder == null)
            enemyHolder = GameObject.Find("Enemies");
        enemyCount = enemyHolder.transform.childCount;
        UpdateText();
        if (enemyCount == 0)
        {
            return false;
        }
        return true;
    }
    private void UpdateText()
    {
        if (textMesh != null)
        textMesh.text = $"Remaining enemies:\n{enemyCount}";
    }
}
