using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    #region Singleton
    public static LevelManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    int fakeLevelNumber;
    public static string levelKey = "level"; 

    private void Start()
    {
        GetLevelNumber();
        UIManager.Instance.SetLevelText(fakeLevelNumber);
    }

    private void GetLevelNumber()
    {
        fakeLevelNumber = PlayerPrefs.GetInt(levelKey, 1);
    }

    public void SaveLevelNumber()
    {
        fakeLevelNumber++;
        PlayerPrefs.SetInt(levelKey, fakeLevelNumber);
        StartCoroutine(NextLevel());
    }

    public IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }
}
