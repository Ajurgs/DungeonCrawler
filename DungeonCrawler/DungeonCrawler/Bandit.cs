using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    class Bandit: Monster
    {
        string type = "Monster";
        string name = "Bandit";
        int speed = 6;
        int strength = 5;
        int inteligence = 5;
        int defence = 3;
        int hitPointsMax = 10;
        int hitPointsCurrent;
        int skillPoints = 2;

        public Bandit()
        {
            hitPointsCurrent = hitPointsMax;
        }
        public override string Type
        {
            get
            {
                return type;
            }
        }
        public override string Name
        {
            get
            {
                return name;
            }
        }
        public override int Speed
        {
            get
            {
                return speed;
            }
        }
        public override int Strength
        {
            get
            {
                return strength;
            }
        }
        public override int Inteligence
        {
            get
            {
                return inteligence;
            }
        }
        public override int Defence
        {
            get
            {
                return defence;
            }
        }
        public override int HitPointsCurrent
        {
            get
            {
                return base.HitPointsCurrent;
            }

            set
            {
                base.HitPointsCurrent = value;
            }
        }
        public override void TakeAction(CharacterDisplay[] targets)
        {
            base.TakeAction(targets);
            Random rng = new Random((int)DateTime.Now.Millisecond);
            int act = rng.Next(0, 3);
            switch (act)
            {
                case 0:
                    // attack
                    Attack(GetTarget(targets).Unit);
                    break;
                case 1:
                    // defend
                    Defend();
                    break;
                case 2:
                    // skill
                    if (skillPoints == 0)
                    {
                        // attack
                        goto case 0;
                    }
                    else
                    {
                        // use skill
                        Skill(GetTarget(targets).Unit);
                    }
                    break;
            }
        }
        /// <summary>
        /// attack the target
        /// </summary>
        /// <param name="target"></param>
        public override void Attack(Character target)
        {
            base.Attack(target);
            int damage = strength + (inteligence / 4);
            target.HitPointsCurrent -= (damage - (target.Defence / 2));
            Console.WriteLine($"{name} did {damage} target is at {target.HitPointsCurrent} health");
        }
        public override void Defend()
        {
            base.Defend();
        }
        public override void Skill(Character target)
        {
            base.Skill(target);
        }
    }
}
