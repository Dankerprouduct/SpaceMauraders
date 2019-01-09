using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceMauraders.Components;


namespace SpaceMauraders.World
{
    public class World
    {
        
        public Entity.SpaceStation spaceStation;

        public Systems.CellSpacePartition dynamicCellSpacePartition;

        public World(int width, int height)
        {
            spaceStation = new Entity.SpaceStation(301);
            dynamicCellSpacePartition = new Systems.CellSpacePartition(spaceStation.diameter, spaceStation.diameter, 4);
            
        }
        
        public void Update(GameTime gameTime)
        {
            spaceStation.Update();
            UpdateDynamicCellPartition(gameTime); 
        }
       
        public void AddEntity(Entity.Entity entity)
        {
            Entity.Entity tempEntity = entity;
            tempEntity.components.Remove(entity.GetComponent("PhysicsComponent"));
            Console.WriteLine(entity.GetComponent("PhysicsComponent"));
            tempEntity.AddComponent(new PhysicsComponent());
            dynamicCellSpacePartition.AddEntity(entity); 
        }

        public void LoadEntity(Entity.Entity entity)
        {
            Entity.Entity temp = entity; 
            // reseting components
            temp.components = new List<Component>();

            for (int i = 0; i < entity.components.Count; i++)
            {
                temp.AddComponent(entity.components[i]);
            }

            dynamicCellSpacePartition.LoadEntity(temp);
        }

        public void UpdateDynamicCellPartition(GameTime gameTime)
        {
            if (EntityWithinBounds(Game1.player.GetCenterPartition()))
            {
                dynamicCellSpacePartition.dynamicCells[Game1.player.GetCenterPartition()].Update(gameTime);
            }

            if (EntityWithinBounds(Game1.player.GetTopLeftPartition()))
            {
                dynamicCellSpacePartition.dynamicCells[Game1.player.GetTopLeftPartition()].Update(gameTime);
            }

            if (EntityWithinBounds(Game1.player.GetTopPartition()))
            {
                dynamicCellSpacePartition.dynamicCells[Game1.player.GetTopPartition()].Update(gameTime);
            }

            if (EntityWithinBounds(Game1.player.GetTopRightPartition()))
            {
                dynamicCellSpacePartition.dynamicCells[Game1.player.GetTopRightPartition()].Update(gameTime);
            }

            if (EntityWithinBounds(Game1.player.GetRightPartition()))
            {
                dynamicCellSpacePartition.dynamicCells[Game1.player.GetRightPartition()].Update(gameTime);
            }

            if (EntityWithinBounds(Game1.player.GetLeftPartition()))
            {
                dynamicCellSpacePartition.dynamicCells[Game1.player.GetLeftPartition()].Update(gameTime);
            }

            if (EntityWithinBounds(Game1.player.GetBottomLeftPartition()))
            {
                dynamicCellSpacePartition.dynamicCells[Game1.player.GetBottomLeftPartition()].Update(gameTime);
            }

            if (EntityWithinBounds(Game1.player.GetBottomPartition()))
            {
                dynamicCellSpacePartition.dynamicCells[Game1.player.GetBottomPartition()].Update(gameTime);
            }

            if (EntityWithinBounds(Game1.player.GetBottomRightPartition()))
            {
                dynamicCellSpacePartition.dynamicCells[Game1.player.GetBottomRightPartition()].Update(gameTime);
            }
        }

        public void DrawDynamicCellPartition(SpriteBatch spriteBatch)
        {
            if (EntityWithinBounds(Game1.player.GetCenterPartition()))
            {
                dynamicCellSpacePartition.dynamicCells[Game1.player.GetCenterPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetTopLeftPartition()))
            {
                dynamicCellSpacePartition.dynamicCells[Game1.player.GetTopLeftPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetTopPartition()))
            {
                dynamicCellSpacePartition.dynamicCells[Game1.player.GetTopPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetTopRightPartition()))
            {
                dynamicCellSpacePartition.dynamicCells[Game1.player.GetTopRightPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetRightPartition()))
            {
                dynamicCellSpacePartition.dynamicCells[Game1.player.GetRightPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetLeftPartition()))
            {
                dynamicCellSpacePartition.dynamicCells[Game1.player.GetLeftPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetBottomLeftPartition()))
            {
                dynamicCellSpacePartition.dynamicCells[Game1.player.GetBottomLeftPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetBottomPartition()))
            {
                dynamicCellSpacePartition.dynamicCells[Game1.player.GetBottomPartition()].Draw(spriteBatch);
            }

            if (EntityWithinBounds(Game1.player.GetBottomRightPartition()))
            {
                dynamicCellSpacePartition.dynamicCells[Game1.player.GetBottomRightPartition()].Draw(spriteBatch);
            }
        }

        public bool EntityWithinBounds(int checkedCell)
        {
            if (checkedCell >= 0 && checkedCell < dynamicCellSpacePartition.cellLength)
            {
                return true;
            }
            return false;
        }

        public void GenerateSpaceStation(int radius)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spaceStation.Draw(spriteBatch);
            DrawDynamicCellPartition(spriteBatch); 

        }

        public bool FireGlobalEvent(Components.Event _event, Entity.Entity entity)
        {
            
            if(spaceStation.FireEvent(_event, entity))
            {
                return true; 
            }


            return false; 
        }


        public static void Savegame()
        {

        }

        public static void LoadGame()
        {

        }

    }
}
