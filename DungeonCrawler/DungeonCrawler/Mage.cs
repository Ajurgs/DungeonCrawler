using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public class Mage : Hero
    {
        string type = "Hero";
        string name = "Mage";
        int speed = 5;
        int strength = 8;
        int inteligence = 4;
        int defence = 8;
        int hitPointsMax = 10;
        int hitPointsCurrent;
        int skillPoints = 10;

        public Mage()
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
                return hitPointsCurrent;
            }
        }
        public override void Attack(Character target)
        {
            base.Attack(target);
        }
    }
}
