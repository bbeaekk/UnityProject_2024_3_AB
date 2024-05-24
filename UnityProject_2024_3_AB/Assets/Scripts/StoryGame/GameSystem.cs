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



#if UNITY_EDITOR
    [ContextMenu("Reset Story Models")]
    public void ResetStoryModels()
    {
        stroyModels = Resources.LoadAll<StoryModel>(""); //Resource ���� �Ʒ� ��� StoryModel �� �ҷ� �´�. 
    }
#endif

    public void StoryShow(int number)
    {
        StoryModel tempStoryModels = FindStoryModel(number);

        //StorySystem.Instace.currentStoryModel = tempStoryMoels;
        //StorySystem.Instance.CoShowText();
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
