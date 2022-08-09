using UnityEngine;

namespace ServiceLocatorExamples
{
    public class StandaloneAchievementService : IAchievementService
    {
        public void CompleteAchievement(string achievementName)
        {
            Debug.Log("(Standalone) Achievement completed: " + achievementName);
        }
    }
}