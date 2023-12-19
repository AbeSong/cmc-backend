namespace TodoProject.Utils
{
    public class Helpers
    {
        public static DateTime GetRandomDate(int withinDays = 21, int withinMinutes = 60)
        {
            int randomDays = Random.Shared.Next(withinDays);
            int randomMinutes = Random.Shared.Next(withinMinutes);
            return DateTime.Today.AddDays(-randomDays).AddMinutes(-randomMinutes);
        }

        public static bool GetRandomBool(int distributionOfTrue = 50)
        {
            int randomPercent = Random.Shared.Next(100);
            return randomPercent < distributionOfTrue;
        }

        public static string GetRandomUser(int numberOfUsers = 3)
        {
            int num = Random.Shared.Next(numberOfUsers);
            return $"USER_{num}";
        }
    }
}
