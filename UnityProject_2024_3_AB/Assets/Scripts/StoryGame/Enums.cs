using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums
{
    public enum StroyType       //���丮 Ÿ��
    {
        MAIN,
        SUB,
        SERIAL
    }

    public enum EventType       //�̺�Ʈ �߻��� üũ
    {
        NONE,
        GOTOBATTLE = 100,
        CheckSTR = 1000,
    }

    public enum ResultType      //�̺�Ʈ ��� ����
    {
        AddExperience,
        GoToNextSotry,
        GoToRandoemStory
    }


}

[System.Serializable]
public class Stats
{
    public int hpPoint;
    public int spPoint;

    public int currentHpPoint;
    public int currentSpPoint;
    public int currentXpPoint;

    public int strength;
    public int dexterity;
    public int consitution;
    public int Intelligence;
    public int wisdom;
    public int charisma;
}

