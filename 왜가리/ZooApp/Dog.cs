using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooApp
{
    class Dog : Animal
    {
        //아래 두개는 부모 것을 접근 못해서
        //그래도 사용해도 문제없음.
        //private string _name;
        //private int _level;
        //아래 한 개는 부모 것에 접근 할 수
        //있지만 나만으;ㅣ 것을 만들 때 쓰는 경우
        //private new COLOR _color;
        //부모에도 없는 것
        private int _year;
        //나중에 주석을 풀어서 이해를 해보세요
        //public string Name { get { return _name; } }
        //public int Level { get { return _level; } }
        //public COLOR Color { get { return _color; } }
        public int Year { get { return _year; } }

        public Dog(string name,COLOR color, int year)
            //:base() //기본 base()생성자
            : base(name, color)
        {
            _year = year;
        }
        public override string ToString()
        {
            return $"DOG:{Name}";
        }
        public string Bark(int count)
        {
            string retValue = "";
            for (int i =0; i<count; i++)
            {
                retValue += "왈!";
            }
            return retValue;
        }
        //오버라이딩
        public override void Eat()
        {
            AddLevel(5);
        }

        //하이딩
        public new void Play()
        {
            AddLevel(3);
        }
        protected override bool AddLevel (int level)
        {
            if (_level + level <= 100) {
                _level += level;
                return true;
            } else {
                _level = 100;
                return false;
            }
        }
    }
}
