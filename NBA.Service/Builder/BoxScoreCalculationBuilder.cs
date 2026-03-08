using PlayerData = NBA.Data.Entities.Player;

namespace NBA.Service.Builder
{
    public class BoxScoreCalculationBuilder
    {
        private PlayerData playerData = new PlayerData();
        public BoxScoreCalculationBuilder CalculatePoints(int pointsScored) 
        {
            //playerData.Points = pointsScored * BoxScoreEvaluation.Points;
            return this;
        }

        public BoxScoreCalculationBuilder CalculateAssists(int assists) 
        {
            //playerData.Assists = assists * BoxScoreEvaluation.Assists;
            return this;
        }



    }
}
