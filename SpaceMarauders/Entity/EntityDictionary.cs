using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using SpaceMarauders.Components;

namespace SpaceMarauders.Entity
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

            foreach (Component component in entityDictionary[name].components)
            {
                switch (component.ComponentName)
                {
                    case "TransformComponent":
                        {
                            entity.AddComponent(new TransfromComponent());
                            break;
                        }
                    case "SolidColliderComponent":
                        {
                            entity.AddComponent(new SolidColliderComponent(entity, entity.id));
                            break;
                        }
                    case "InputComponent":
                        {
                            entity.AddComponent(new InputComponent(entity.id));
                            break; 
                        }
                    case "HealthComponent":
                        {
                            entity.AddComponent(new HealthComponent(((HealthComponent)component).health));
                            break; 
                        }
                    case "PhysicsComponent":
                        {
                            entity.AddComponent(new PhysicsComponent(entity.id));
                            break; 
                        }
                    case "InventoryComponent":
                        {
                            entity.AddComponent(new InventoryComponent(5,5)); 
                            break; 
                        }
                    case "TriggerColliderComponent":
                        {
                            entity.AddComponent(new TriggerColliderComponent()); 
                            break;
                        }
                    case "SpeedModifierComponent":
                        {
                            entity.AddComponent(new SpeedModifierComponent(1));
                            break;
                        }

                }
            }

            return entity; 
                
        }

    }
}
