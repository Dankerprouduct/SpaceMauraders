using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceMauraders.Entity.Items
{
    public static class ItemDictionary
    {

        public static List<Item> itemDictinary = new List<Item>(); 

        public static void LoadItemDatabase()
        {
            itemDictinary.Add(new Weapons.TestGun());


        }
    }
}
