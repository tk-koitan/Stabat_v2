using System.Collections;
using System.Collections.Generic;


namespace Koitan
{
    public class BattleSetting
    {
        public static int playerCount = BattleGlobal.MaxPlayerNum;
        public static int[] ControllPlayers = new int[BattleGlobal.MaxPlayerNum];
        public static int[] teamColorIndexes = new int[BattleGlobal.MaxPlayerNum];
        public static int[] playerIndexes = new int[BattleGlobal.MaxPlayerNum];
        public static int[] charaColorIndexes = new int[BattleGlobal.MaxPlayerNum];
        public static int battleStageIndex = 0;
    }

    public enum ControllPlayer
    {
        None,
        Human,
        CPU
    }
}

