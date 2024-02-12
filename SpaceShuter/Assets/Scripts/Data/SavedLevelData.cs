namespace LiveToday
{
    public class SavedLevelData
    {
        private const string Key = "SaveLevelData";

        private readonly IStorageService _storageService;
        private LevelProgress _levelProgress;

        private SavedLevelData(IStorageService storageService, LevelProgress levelProgress)
        {
            _storageService = storageService;
            _levelProgress = levelProgress;
        }

        public void SaveLevelData()
        {
            _storageService.Save(Key, _levelProgress);
        }

        public LevelProgress GetLevelProgress()
        { 
            _storageService.Load<LevelProgress>(Key, data =>
            {
                if (data != default(LevelProgress))
                    _levelProgress = data;
                else
                    SaveLevelData();
            });
            return _levelProgress;
        }
    }
}