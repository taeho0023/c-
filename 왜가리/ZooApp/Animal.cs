using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooApp
{
    abstract class Animal
    {
        private string _name;
        protected int _level;
        protected COLOR _color;

        public string Name { get { return _name; } }
        public int Level { get { return _level; } }
        public COLOR Color { get { return _color; } }


        public Animal(string name, COLOR color)
        {
            _name = name;
            _color = color;
        }
        public override string ToString()
        {
            return $"ANIMAL:{_name}";
        }

        abstract protected bool AddLevel(int level);
        
        public virtual void Eat()
        {
            AddLevel(3);
        }
        public void Play()
        {
            AddLevel(2);
        }
    }
}
