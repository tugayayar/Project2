using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCheck : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    [SerializeField] private Knife leftKnifeSC;
    [SerializeField] private Knife rightKnifeSC;
    private float leftXPos;
    private float rightXPos;
    private float nextStackXPos;
    [HideInInspector] public bool isPlayerSkillSuccessfull;
    [HideInInspector] public int knifeCutCounter = 0;

    public IEnumerator CheckIsKnivesCutted()
    {
        yield return new WaitForSeconds(.1f);

        if (knifeCutCounter == 0) DoThis();
    }

    //private void SkillRangeAdjuster() //yetenek aralýðý (X pozisyon cinsinden )
    //{
    //    leftXPos = leftKnifeSC.transform.position.x;
    //    rightXPos = rightKnifeSC.transform.position.x;
    //    //nextStackXPos = PlayerController.Instance.movePointObjSC.transform.position.x; //GameManager.Instance.nextStack.transform.position.x;
    //    Debug.Log(leftXPos + " >= " + nextStackXPos + "  " + rightXPos);
    //}

    private void CheckIsPlayerSkillSuccessfull()
    {
        //SkillRangeAdjuster();

        //bu karþýlaþtýrmayý hareýket eden stack üzerinden yapmalýsýn player'ýn posizyonu ile deðil!!!
        //if (nextStackXPos >= leftXPos && nextStackXPos <= rightXPos) isPlayerSkillSuccessfull = true;
        //else isPlayerSkillSuccessfull = false;

        if (knifeCutCounter == 2) isPlayerSkillSuccessfull = true;
    }

    void DoThis() //bu isim deðiþecek
    {
        CheckIsPlayerSkillSuccessfull();

        if (isPlayerSkillSuccessfull)
        {
            GameManager.Instance.CurrentStackAdjuster(GameManager.Instance.nextStack);

            leftKnifeSC.AfterCutting();

            Debug.Log("ADAM BAYAÐI ÝYÝ BAÞARILI! SES DENEME SES! ÇOCUKLARIMIZI PÝSTTEN ALALIM"); //play succes sound
        }
        else
        {
            Debug.Log("BABA NE OLDU SANA BÖYLE BABBBAAA");//boþluktan aþaðý düþürüp oyunu bitir
            PlayerController.Instance.Fail();
        }
    }
}
