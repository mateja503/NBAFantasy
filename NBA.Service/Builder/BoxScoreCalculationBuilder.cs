using NBA.Data.Context;
using PlayerData = NBA.Data.Entities.Player;

namespace NBA.Service.Builder
{
    public readonly struct BoxScoreCalculationBuilder(PlayerData _player)
    {
        private readonly PlayerData player = _player;
        public BoxScoreCalculationBuilder CalculatePoints(int pointsScored) 
        {
            player.Points = pointsScored * BoxScoreEvaluation.Points;
            return this;
        }
        public BoxScoreCalculationBuilder CalculateAssists(int assists) 
        {
            player.Assists = assists * BoxScoreEvaluation.Assists;
            return this;
        }
        public BoxScoreCalculationBuilder CalculateRebounds(int rebounds) 
        {
            player.Rebounds = rebounds * BoxScoreEvaluation.Rebounds;
            return this;
        }

        public BoxScoreCalculationBuilder CalculateBlocks(int blocks)
        {
            player.Blocks = blocks * BoxScoreEvaluation.Blocks;
            return this;
        }

        public BoxScoreCalculationBuilder CalculateSteals(int steals)
        {
            player.Steals = steals * BoxScoreEvaluation.Steals;
            return this;
        }

        public BoxScoreCalculationBuilder CalculateThreePointers(int threePointsMade, int threePointsAttempted)
        {
            player.Threepointers = (threePointsMade * BoxScoreEvaluation.ThreePointsMade) + ((threePointsAttempted - threePointsMade )  * BoxScoreEvaluation.ThreePointsMissed);
            return this;
        }
        public BoxScoreCalculationBuilder CalculateTurnovers(int turnovers)
        {
            player.Turnovers = turnovers * BoxScoreEvaluation.Turnovers;
            return this;
        }

        public BoxScoreCalculationBuilder CalculateFreeThrows(int freeThrowsMade, int freeThrowsAttempted)
        {
            player.Freethrow = (freeThrowsMade * BoxScoreEvaluation.FreeThrowMade) + ((freeThrowsAttempted - freeThrowsMade) * BoxScoreEvaluation.FreeThrowMissed);
            return this;
        }
        public BoxScoreCalculationBuilder CalculateFieldGoals(int fieldGoalsMade, int fieldGoalAttempted)
        {
            player.Fieldgoal = (fieldGoalsMade * BoxScoreEvaluation.FieldGoalMade) + ((fieldGoalAttempted - fieldGoalsMade) * BoxScoreEvaluation.FieldGoalMissed);
            return this;
        }

        public PlayerData Calculate() => player;
    }
}
