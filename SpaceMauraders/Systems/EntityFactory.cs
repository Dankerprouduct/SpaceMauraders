using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json; 

namespace SpaceMauraders.Systems
{
    public static class EntityFactory
    {

        public static Dictionary<string, Components.Component> componentDictionary = new Dictionary<string, Components.Component>();

        public static void Init()
        {
        }

        public static void LoadComponents()
        {
            componentDictionary.Add("InputComponent", new Components.InputComponent());
            componentDictionary.Add("InventoryComponent", new Components.InventoryComponent());
            componentDictionary.Add("PhysicsComponent", new Components.PhysicsComponent());
            componentDictionary.Add("SolidColliderComponent", new Components.SolidColliderComponent());


        }



    }
}
