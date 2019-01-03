using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics; 

namespace SpaceMauraders.Entity.Items
{
    public abstract class Item: Entity
    {
        public Item(): base()
        {

        }
        

        public abstract void Use();
        public int worldItemTextureID;
        public int guiItemID;
        public int itemID; 


    }

    public class EmptyItem: Item
    {
        public EmptyItem()
        {

        }

        public override void Use()
        {
            Console.WriteLine("using Empty Item"); 
        }
    }
}
