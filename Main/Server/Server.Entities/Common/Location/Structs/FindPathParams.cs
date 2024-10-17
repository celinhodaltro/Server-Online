﻿using System.Runtime.InteropServices;

namespace Server.Entities.Common.Location.Structs;

[StructLayout(LayoutKind.Auto)]
public struct FindPathParams
{
    public static FindPathParams EscapeParams => new(false, true, default, true, 12, 1, 12, false);

    public FindPathParams(bool fullPathSearch, bool clearSight, bool allowDiagonal, bool keepDistance,
        int maxSearchDist, int minTargetDist, int maxTargetDist, bool oneStep)
    {
        FullPathSearch = fullPathSearch;
        ClearSight = clearSight;
        AllowDiagonal = allowDiagonal;
        KeepDistance = keepDistance;
        MaxSearchDist = maxSearchDist;
        MinTargetDist = minTargetDist;
        MaxTargetDist = maxTargetDist;
        OneStep = oneStep;
    }

    public FindPathParams(bool useDefault)
    {
        FullPathSearch = default;
        ClearSight = default;
        AllowDiagonal = default;
        KeepDistance = default;
        MaxSearchDist = default;
        MinTargetDist = default;
        MaxTargetDist = default;
        OneStep = default;

        if (useDefault)
        {
            FullPathSearch = true; 
            ClearSight = true;
            AllowDiagonal = true;
            KeepDistance = false;
            MaxSearchDist = 12;
            MinTargetDist = 1;
            MaxTargetDist = 1;
        }
    }

    public bool FullPathSearch { get; set; }
    public bool ClearSight { get; set; }
    public bool AllowDiagonal { get; set; }
    public bool KeepDistance { get; set; }
    public bool OneStep { get; set; }
    public int MaxSearchDist { get; set; }
    public int MinTargetDist { get; set; }
    public int MaxTargetDist { get; set; }

    public bool CannotWalk(Location startPos, Location pos)
    {
        return MaxSearchDist != 0 && (startPos.GetSqmDistanceX(pos) > MaxSearchDist ||
                                      startPos.GetSqmDistanceY(pos) > MaxSearchDist);
    }
}