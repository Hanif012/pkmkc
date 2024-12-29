using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

[CreateAssetMenu(menuName = "pkmkc/DailyMission")]
public class DailyMission : ScriptableObject
{
    public int missionDay = 0;
    public string missionName = "Mission Name";
    public quest[] quests = new quest[3]; 
}

[System.Serializable]
public class quest
{
    public string questName;
    public string DialogueNode = "Start";
    public string NPCName = "Them";
    public Sprite NPCImage;
    public enum QuestType
    {
        Talk,
        Scam,
        Order
    }
    public QuestType type;

}
