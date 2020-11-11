using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public class Monster : Character
    {
        Random rng = new Random((int)DateTime.Now.Millisecond);
        string type = "Monster";
        string name = "Monster";
        int speed = 5;
        int strength = 5;
        int inteligence = 5;
        int defence = 5;
        int hitPointsMax = 10;
        int hitPointsCurrent;
        int skillPoints = 10;

        public Monster()
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
        public override bool IsAlive
        {
            get
            {
                return base.IsAlive;
            }

            set
            {
                base.IsAlive = value;
            }
        }
        /// <summary>
        /// Get a living target from an array
        /// </summary>
        /// <param name="targets"></param>
        /// <returns></returns>
        public virtual CharacterDisplay GetTarget(CharacterDisplay[] targets)
        {
            CharacterDisplay target = null;
            while (target == null)
            {
                int temp = rng.Next(0, targets.GetLength(0));
                if (targets[temp].Unit.IsAlive == true)
                {
                    target = targets[temp];
                }
            }
            return target;
        }
        public override void TakeAction(CharacterDisplay[] targets)
        {
            Console.WriteLine($"{name} took turn");
        }
        public override void Attack(Character target)
        {
            Console.WriteLine($"{name} attacked {target}");
        }
        public override void Defend()
        {
            Console.WriteLine($"{name} defended for turn");
        }
        public override void Skill(Character target)
        {
            Console.WriteLine($"{name} used skill on {target}");
        }
    }
}
