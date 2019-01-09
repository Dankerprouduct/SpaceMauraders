using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  Microsoft.Xna.Framework;
using SpaceMauraders.Entity.Body;

namespace SpaceMauraders.Utilities
{
    public class EntitySaveTemplate<T> : SaveTemplate<T>
    {
        public Type type; 
        public string name;     
        public Vector2 position;
        public float rotation; 
        public Body body;
        public List<Components.Component> components;
        

    }

    public class SaveTemplate<T>
    {
        public T data;
        public string name;
        public Vector2 position;
        public float rotation;
        public Body body;
        public List<Components.Component> components;

    }

}


