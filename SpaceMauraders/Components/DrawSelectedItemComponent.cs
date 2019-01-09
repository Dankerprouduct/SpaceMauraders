using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NLua;
using SpaceMauraders.Entity.Items;

namespace SpaceMauraders.Components
{
    public class DrawSelectedItemComponent: Component
    {
        //Entity.Body.Body body;
        Vector2 itemPosition;
        Item currentEquippedItem;
        float rotation;
        Vector2 center; 

        public DrawSelectedItemComponent() : base()
        {
            componentName = "DrawSelectedItemComponent";
        }

        public override void Update(GameTime gameTime, Entity.Entity entity)
        {
            currentEquippedItem = ((InventoryComponent)entity.GetComponent("InventoryComponent")).slot1;

            if (currentEquippedItem != null)
            {
                if (entity is Entity.Player)
                {
                    Entity.Player tempPlayer = ((Entity.Player) (entity));
                    rotation = entity.rotation;
                    int[] handIndex = tempPlayer.body.GetHandIndexes();


                    itemPosition = Utilities.MathHelper.Vec2ToEntitySpace(
                        new Vector2(10, 0),
                        tempPlayer.body.bodyParts[handIndex[0]].positon, entity.rotation);

                    if (currentEquippedItem.itemID != -1)
                    {
                        center = Utilities.MathHelper.CenterOfImage(
                            Utilities.TextureManager.guiItemTextures[currentEquippedItem.guiItemID]);
                        currentEquippedItem.position = itemPosition;
                        ////.rotation = rotation; 
                        tempPlayer.body.bodyParts[3].offset = new Vector2(60, 44);
                        //tempPlayer.body.bodyParts[2].offset = new Vector2(50, 12);
                    }
                    else
                    {
                        tempPlayer.body.bodyParts[3].offset = new Vector2(30, 44);
                        //tempPlayer.body.bodyParts[2].offset = new Vector2(15, -22);
                    }

                }
                else
                {
                    Entity.NPC tempPlayer = ((Entity.NPC) (entity));
                    rotation = entity.rotation;
                    int[] handIndex = tempPlayer.body.GetHandIndexes();
                    currentEquippedItem = ((InventoryComponent) entity.GetComponent("InventoryComponent")).slot1;

                    itemPosition = Utilities.MathHelper.Vec2ToEntitySpace(
                        new Vector2(10, 0),
                        tempPlayer.body.bodyParts[handIndex[0]].positon, entity.rotation);

                    if (currentEquippedItem.itemID != -1)
                    {
                        center = Utilities.MathHelper.CenterOfImage(
                            Utilities.TextureManager.guiItemTextures[currentEquippedItem.guiItemID]);
                        currentEquippedItem.position = itemPosition;
                        currentEquippedItem.rotation = rotation;
                        tempPlayer.body.bodyParts[3].offset = new Vector2(60, 44);
                        //tempPlayer.body.bodyParts[2].offset = new Vector2(50, 12);
                    }
                    else
                    {
                        tempPlayer.body.bodyParts[3].offset = new Vector2(30, 44);
                        tempPlayer.body.bodyParts[2].offset = new Vector2(15, -22);
                    }
                }
            }

            base.Update(gameTime, entity);
        }

        public override bool FireEvent(Event _event)
        {
            return base.FireEvent(_event);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (currentEquippedItem != null)
            {
                if (currentEquippedItem.itemID != -1)
                {

                    //spriteBatch.Draw(Utilities.TextureManager.guiItemTextures[currentEquippedItem], itemPosition, Color.White);

                    spriteBatch.Draw(Utilities.TextureManager.guiItemTextures[currentEquippedItem.guiItemID],
                        itemPosition, null,
                        Color.White,
                        currentEquippedItem.rotation,
                        center,
                        1f,
                        SpriteEffects.None,
                        0f);
                }
            }
        }
    }
}
