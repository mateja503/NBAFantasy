using System;
using System.Collections.Generic;

namespace NBA.Data.Entities;

public partial class Statsvalue
{
    public long Statsvalueid { get; set; }

    public double Pointsvalue { get; set; }

    public double Assistsvalue { get; set; }

    public double Reboundsvalue { get; set; }

    public double Blocksvalue { get; set; }

    public double Threepointsvaluemade { get; set; }

    public double Threepointsvaluemissed { get; set; }

    public double Turnoversvalue { get; set; }

    public double Freethrowvaluemade { get; set; }

    public double Freethrowvaluemissed { get; set; }

    public double Fieldgoalvaluemade { get; set; }

    public double Fieldgoalvaluemissed { get; set; }

    public virtual League? League { get; set; }
}
