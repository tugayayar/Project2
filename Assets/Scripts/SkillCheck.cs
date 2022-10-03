using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCheck : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    public float pitchIncreaseValue = .5f;
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
        CheckIsPlayerSkillSuccessfull();
        if (knifeCutCounter == 0) DoThis();
    }

    private void CheckIsPlayerSkillSuccessfull()
    {
        if (knifeCutCounter == 2) isPlayerSkillSuccessfull = true;

        if (GameManager.Instance.skillCheckCount == 2)
        {
            ShrillSound();
            PlaySkillSound();
        }
        else SetPitchValue(1f);

        GameManager.Instance.skillCheckCount = 0;
    }

    void DoThis() //bu isim deðiþecek
    {
        if (!isPlayerSkillSuccessfull) PlayerController.Instance.Fail();
    }

    private float GetPitchValue()
    {
        return source.pitch;
    }

    public void SetPitchValue(float value)
    {
        source.pitch = value;
    }

    private void ShrillSound()
    {
        float soundVal = GetPitchValue();
        soundVal += pitchIncreaseValue;
        SetPitchValue(soundVal);
    }

    private void PlaySkillSound()
    {
        source.PlayOneShot(clip);
    }
}
