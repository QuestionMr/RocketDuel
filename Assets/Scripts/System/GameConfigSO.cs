using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfigSO
{
    public static bool m_hazard;
    public static float m_posThreshold  = 30f;
    public static float m_doubleTapTime = 0.2f;
    public static float m_volume = 1f;
    public static int m_lives = 3;
    public static int m_invert = 1;
    public static bool[] m_isCPU = {false, false};

    public static void SetHazard(bool h){
        m_hazard = h;
    }
}
