using System.Collections.Generic;

namespace owd
{
    public static class BossNames
    {
        private static readonly HashSet<string> names = new HashSet<string>{
            "Trobbio",
            "Coral Warrior Grey",
            "Zap Core Enemy",
            "Song Knight",
            "Song Knight BattleEncounter",
            "Tormented Trobbio",
            "Giant Flea",
            "Garmond Black Threaded Fighter",
            "Pinstress Boss",
            "First Weaver",
            "Mossbone Mother A",
            "Mossbone Mother B",
            "Mossbone Mother Ambient",
            "Mossbone Mother",
            "Bone Beast",
            "Roof Crab",
            "Splinter Queen",
            "Spinner Boss",
            "Skull King",
            "Cloverstag White Boss",
            "SG_head",
            "Bone Flyer Giant",
            "Dancer A",
            "Dancer B",
            "Vampire Gnat",
            "Roachkeeper Chef (1)",
            "Swamp Shaman",
            "Phantom",
            "Coral Conch Driller Giant Solo",
            "Driller A",
            "Driller B",
            "Bone Hunter Trapper",
            "Conductor Boss",
            "Seth",
            "Silk Boss",
            "Flower Queen Boss",
            "Crawfather",
            "Hunter Queen Boss",
            "Coral King",
            "Lace Boss1",
            "Lace Boss2 New",
            "Lost Lace Boss",
            "Last Judge",
            "Dock Guard Slasher",
            "Dock Guard Thrower",
            "Giant Centipede Butt",
            "Giant Centipede Head",
            "Mapper Spar NPC",
            "Blue Assistant",
            "Garmond Fighter",
            "Slab Fly Broodmother",
            "Abyss Mass"
        };

        public static bool IsBossName(string name) =>
            names.Contains(name);
    }
}
