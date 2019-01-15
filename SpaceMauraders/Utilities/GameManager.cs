using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMauraders.Utilities
{
    public static class GameManager
    {
        private  static GameData<Entity.Entity> entityData = new GameData<Entity.Entity>();

        public static void Update()
        {

        }

        public static void SaveGame()
        {
            entityData.SaveData(Game1.world.dynamicCellSpacePartition.GetDynamicEntities(), "save2");
        }

        public static void LoadGame()
        {
            Entity.Entity[] ents = entityData.LoadData("save2");

            for (int i = 0; i < ents.Length; i++)
            {
                Game1.world.AddEntity(ents[i]);
            }
        }
    }
}
