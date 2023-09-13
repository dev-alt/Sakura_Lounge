using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace SakuraLounge
{
    public class ScoreManager
    {
        private const string ScoreKey = "UserScore";
        private const string ScoreFileName = "userScore.txt";

        public static int GetScore()
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.TryGetValue(ScoreKey, out var value) && value is int score)
            {
                return score;
            }
            return 0;
        }

        public static void AddScore(int points)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            var currentScore = GetScore();
            currentScore += points;
            localSettings.Values[ScoreKey] = currentScore;
        }

        public static async Task SaveScoreToFileAsync()
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var scoreFile = await localFolder.CreateFileAsync(ScoreFileName, CreationCollisionOption.ReplaceExisting);

            using (var stream = await scoreFile.OpenStreamForWriteAsync())
            using (var writer = new StreamWriter(stream))
            {
                await writer.WriteLineAsync(GetScore().ToString());
            }
        }

        public static async Task LoadScoreFromFileAsync()
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            try
            {
                var scoreFile = await localFolder.GetFileAsync(ScoreFileName);

                using (var stream = await scoreFile.OpenStreamForReadAsync())
                using (var reader = new StreamReader(stream))
                {
                    if (int.TryParse(await reader.ReadLineAsync(), out int score))
                    {
                        AddScore(score);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                
            }
        }
    }
}
