using System.Collections;
using System.Collections.Generic;

namespace ServiceLocatorExamples
{
    /// <summary>
    /// Achievement service interface for multiple platform support.
    /// </summary>
    public interface IAchievementService
    {
        void CompleteAchievement(string achievementName);
    }
}