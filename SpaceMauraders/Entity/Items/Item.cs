using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 
using  Microsoft.Xna.Framework.Input; 

namespace SpaceMauraders.Entity.Items
{
    public abstract class Item: Entity
    {
        public Item(): base()
        {

        }
        

        public abstract void Use(Entity entity);
        public bool inUse; 
        public int worldItemTextureID;
        public int guiItemID;
        public int itemID;
        public KeyboardState currentKeyboardState;
        public KeyboardState previousKeyboardState;
        public MouseState currentMouseState;
        public MouseState previousMouseState; 

    }

    public class EmptyItem: Item
    {
        public EmptyItem()
        {
            itemID = -1; 
        }

        public override void Use(Entity entity)
        {
            Console.WriteLine("using Empty Item"); 
        }
    }
}
