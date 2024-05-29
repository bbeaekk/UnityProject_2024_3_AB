using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;


#if UNITY_EDITOR
[CustomEditor(typeof(GameSystem))]
public class GameSystemEdiot : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GameSystem gameSystem = (GameSystem)target;

        //Reset Story Models ��ư ���� 
        if (GUILayout.Button("Rest Stroy Modes"))
        {
            gameSystem.ResetStoryModels();
        }
    }
}
#endif

public class GameSystem : MonoBehaviour
{
    public static GameSystem instance;                      //������ �̱��� ȭ

    private void Awake()
    {
        instance = this;
    }

    public enum GAMESTATE
    {
        STROYSHOW,
        WAITSELECT,
        STORYEND
    }

    public Stats stats;
    public GAMESTATE currentState;
    public int currentStoryIndex = 1;
    public StoryModel[] stroyModels;

    public void Start()
    {
        ChangeState(GAMESTATE.STROYSHOW);
    }



#if UNITY_EDITOR
    [ContextMenu("Reset Story Models")]
    public void ResetStoryModels()
    {
        stroyModels = Resources.LoadAll<StoryModel>(""); //Resource ���� �Ʒ� ��� StoryModel �� �ҷ� �´�. 
    }
#endif

    public void ChangeState(GAMESTATE temp)
    {
        currentState = temp;

        if(currentState == GAMESTATE.STROYSHOW)
        {
            StoryShow(currentStoryIndex);
        }
    }

    public void ApplyChoice(StoryModel.Reuslt result)
    {
        switch (result.resultType)
        {
            case StoryModel.Reuslt.ResultType.ChangeHp:
                stats.currentHpPoint += result.value;
                ChangeStats(result);
                break;
            case StoryModel.Reuslt.ResultType.AddExperience:
                stats.currentXpPoint += result.value;
                ChangeStats(result);
                break;
            case StoryModel.Reuslt.ResultType.GoToNextStory:
                currentStoryIndex = result.value;
                ChangeState(GAMESTATE.STROYSHOW);
                ChangeStats(result); 
                break;
            case StoryModel.Reuslt.ResultType.GoToRandomStory:
                RandomStory();
                ChangeState(GAMESTATE.STROYSHOW);
                ChangeStats(result);
                break;
            default:
                Debug.LogError("Unknown type");
                break;
        }
    }

    public void ChangeStats(StoryModel.Reuslt result)
    {
        if(result.stats.hpPoint > 0) stats.hpPoint += result.stats.hpPoint;
        if (result.stats.hpPoint > 0) stats.hpPoint += result.stats.spPoint;

        if (result.stats.hpPoint > 0) stats.hpPoint += result.stats.currentHpPoint;
        if (result.stats.hpPoint > 0) stats.hpPoint += result.stats.currentSpPoint;
        if (result.stats.hpPoint > 0) stats.hpPoint += result.stats.currentXpPoint;

        if(result.stats.strength > 0) stats.strength += result.stats.strength;
        if (result.stats.dexterity > 0) stats.dexterity += result.stats.dexterity;
        if (result.stats.consitution > 0) stats.consitution += result.stats.consitution;
        if (result.stats.wisdom > 0) stats.wisdom += result.stats.wisdom;
        if (result.stats.Intelligence > 0) stats.Intelligence += result.stats.Intelligence;
        if (result.stats.charisma > 0) stats.charisma += result.stats.charisma;
    }

    public void StoryShow(int number)
    {
        StoryModel tempStoryModels = FindStoryModel(number);

        StorySystem.instance.currentStoryModel = tempStoryModels;
        StorySystem.instance.CoShowText();
    }

    StoryModel FindStoryModel(int number)
    {
        StoryModel tempStoryModels = null;
        for (int i = 0; i < stroyModels.Length; i++)             //for ������ StroyModel �� �˻��Ͽ� Number �� ���� ���丮 ��ȣ�� ���丮 ���� ã�� ��ȯ�Ѵ�. 
        {
            if (stroyModels[i].storyNumber == number)
            {
                tempStoryModels = stroyModels[i];
                break;
            }
        }
        return tempStoryModels;
    }

    StoryModel RandomStory()
    {
        StoryModel tempStoryModels = null;

        List<StoryModel> storyModelList = new List<StoryModel>();

        for (int i = 0; i < stroyModels.Length; i++)             //for ������ StroyModel �� �˻��Ͽ� Main�� ��츸 ����. 
        {
            if (stroyModels[i].storyType == StoryModel.STORYTYPE.MAIN)
            {
                storyModelList.Add(stroyModels[i]);
            }
        }

        tempStoryModels = storyModelList[Random.Range(0, storyModelList.Count)]; //����Ʈ���� �������� �ϳ� ����
        currentStoryIndex = tempStoryModels.storyNumber;
        return tempStoryModels;
    }

}
