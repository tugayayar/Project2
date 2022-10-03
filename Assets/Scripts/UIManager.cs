using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI successText;
    [SerializeField] TextMeshProUGUI failText;

    public void SetLevelText(int lvlNumber)
    {
        levelText.text = "LEVEL " + lvlNumber.ToString();
    }

    public void SuccessText(bool isActive)
    {
        successText.gameObject.SetActive(isActive);
    }

    public void FailText(bool isActive)
    {
        failText.gameObject.SetActive(isActive);
    }
}
