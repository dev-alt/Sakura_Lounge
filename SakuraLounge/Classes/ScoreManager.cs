using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace SakuraLounge
{
    public class ScoreManager
    {
        private const string ScoreKey = "userMoney";
        private const string ScoreFileName = "userMoney.txt";

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
        public static void SetScore(int score)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values[ScoreKey] = score;
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
                        Debug.WriteLine("Score loaded: " + score);
                        SetScore(score);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Debug.WriteLine("Score file not found.");
            }
            catch (Exception ex)
            {
                // Handle other exceptions or add debug logging.
                Debug.WriteLine("Error loading score: " + ex.Message);
            }
        }
    }
}
