using UnityEngine;

namespace ServiceLocatorExamples
{
    public class EditorAchievementService : IAchievementService
    {
        public void CompleteAchievement(string achievementName)
        {
            Debug.Log("(Editor) Achievement completed: " + achievementName);
        }
    }
}