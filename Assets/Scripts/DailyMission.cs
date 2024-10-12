using UnityEngine;

[CreateAssetMenu(menuName = "pkmkc/DailyMission")]
public class DailyMission : ScriptableObject
{
    
    public int day = 0;
    public string missionName = "Mission";
    public string missionDescription = "Description";
    public bool isCompleted = false;
    public OrderClass orderType;
    
    public class MissionDifficulty
    {
        public float rent = 0;
        public int FakeOrder = 0;
        public int RealOrder = 0;
        public int TotalOrder = 0;
        public float OrderCooldown = 0;
    }

    

}