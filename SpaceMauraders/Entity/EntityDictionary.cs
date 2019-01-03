using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO; 

namespace SpaceMauraders.Entity
{
    public static class EntityDictionary
    {
        public static Dictionary<string, EntityPrototype> entityDictionary = new Dictionary<string, EntityPrototype>(); 

        public static void Init()
        {
            StreamReader sR = new StreamReader(@"JSON\Entities.json");
            string json = sR.ReadToEnd();

            EntityPrototype[] entityPrototypes = JsonConvert.DeserializeObject<EntityPrototype[]>(json); //new EntityPrototype();
            
            foreach(EntityPrototype prototype in entityPrototypes)
            {
                entityDictionary.Add(prototype.entityName, prototype);
                Console.WriteLine(prototype.entityName); 
            }
        }

        public static Entity GetEntityPrototypeByName(string name)
        {

            Entity entity = new Entity();
            entity.entityName = entityDictionary[name].entityName;

            foreach (Components.Component component in entityDictionary[name].components)
            {
                switch (component.componentName)
                {
                    case "TransformComponent":
                        {
                            entity.AddComponent(new Components.TransfromComponent());
                            break;
                        }
                    case "SolidColliderComponent":
                        {
                            entity.AddComponent(new Components.SolidColliderComponent(entity, entity.id));
                            break;
                        }
                    case "InputComponent":
                        {
                            entity.AddComponent(new Components.InputComponent(entity.id));
                            break; 
                        }
                    case "HealthComponent":
                        {
                            entity.AddComponent(new Components.HealthComponent(((Components.HealthComponent)component).health));
                            break; 
                        }
                    case "PhysicsComponent":
                        {
                            entity.AddComponent(new Components.PhysicsComponent(entity.id));
                            break; 
                        }
                    case "InventoryComponent":
                        {
                            entity.AddComponent(new Components.InventoryComponent(5,5)); 
                            break; 
                        }
                    case "TriggerColliderComponent":
                        {
                            entity.AddComponent(new Components.TriggerColliderComponent()); 
                            break;
                        }
                    case "SpeedModifierComponent":
                        {
                            entity.AddComponent(new Components.SpeedModifierComponent(1));
                            break;
                        }

                }
            }

            return entity; 
                
        }

    }
}
