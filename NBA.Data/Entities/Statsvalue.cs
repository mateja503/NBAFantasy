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

    public double Threepointsvalue { get; set; }

    public double Turnoversvalue { get; set; }

    public double Freethrowpervalue { get; set; }

    public double Fieldgoalpercvalue { get; set; }

    public virtual League? League { get; set; }
}
