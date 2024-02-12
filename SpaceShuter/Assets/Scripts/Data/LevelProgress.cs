using System;

namespace LiveToday
{
    [Serializable]
    public class LevelProgress
    {
        public Progress[] Levels = new Progress[999];
        private LevelProgress()
        {
            for (var i = 0; i < Levels.Length; i++)
                Levels[i] = new Progress();
            
            Levels[0].IsOpened = true;
        }
    }

    [Serializable]
    public class Progress
    {
        public bool IsOpened;
    }
}