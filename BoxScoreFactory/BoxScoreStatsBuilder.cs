

using BoxScoreBuilder.Model;

namespace BoxScoreBuilder
{
    public class BoxScoreStatsBuilder
    {
        private PlayerStats gameStats = new PlayerStats();
        private Random random = new Random();

        public BoxScoreStatsBuilder AddPoints()
        {
            gameStats.pts = random.Next(0, 50); // Random points between 0 and 50
            return this;
        }

        public BoxScoreStatsBuilder AddAssists()
        {
            gameStats.ast = random.Next(0, 15); // Random assists between 0 and 15
            return this;
        }
        public BoxScoreStatsBuilder AddRebounds()
        {
            gameStats.reb = random.Next(0, 20); // Random rebounds between 0 and 20
            return this;
        }
        public BoxScoreStatsBuilder AddBlocks()
        {
            gameStats.blk = random.Next(0, 10); // Random blocks between 0 and 10
            return this;
        }
        public BoxScoreStatsBuilder AddSteals()
        {
            gameStats.stl = random.Next(0, 10); // Random steals between 0 and 10
            return this;
        }

        public BoxScoreStatsBuilder AddThreePointersMade()
        {
            gameStats.fg3m = random.Next(0, 10); // Random three-pointers made between 0 and 10
            return this;
        }

        public BoxScoreStatsBuilder AddThreePointersAttemped()
        {
            gameStats.fg3a = random.Next(gameStats.fg3m, 10); // Random three-pointers made between 0 and 10
            return this;
        }

        public BoxScoreStatsBuilder AddTurnovers()
        {
            gameStats.turnover = random.Next(0, 10); // Random three-pointers made between 0 and 10
            return this;
        }

        public BoxScoreStatsBuilder AddFieldGoalsMade()
        {
            gameStats.fgm = random.Next(0, 20); // Random field goal percentage between 0 and 100
            return this;
        }

        public BoxScoreStatsBuilder AddFieldGoalsAttempted()
        {
            gameStats.fga = random.Next(gameStats.fgm, 20); // Random field goal percentage between 0 and 100
            return this;
        }

        public BoxScoreStatsBuilder AddFreeThrowsMade()
        {
            gameStats.ftm = random.Next(0, 20); // Random field goal percentage between 0 and 100
            return this;
        }

        public BoxScoreStatsBuilder AddFreeThrowsAttempted()
        {
            gameStats.fta = random.Next(gameStats.ftm, 20); // Random field goal percentage between 0 and 100
            return this;
        }

        public PlayerStats Build()  
        {
            PlayerStats finalStats = gameStats;

            gameStats = new PlayerStats(); // Reset for next build

            return finalStats;
        }

    }
}
