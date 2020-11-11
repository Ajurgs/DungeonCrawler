using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCrawler
{
    public class Character
    {
        string type = "Character";
        string name = "Character";
        int speed = 5;
        int strength = 5;
        int inteligence = 5;
        int defence = 5;
        int hitPointsMax = 10;
        int hitPointsCurrent;
        int prevHP;
        int skillPoints = 10;
        bool isAlive = true;

        public Character()
        {
            hitPointsCurrent = hitPointsMax;
        }
        public virtual string Type
        {
            get
            {
                return type;
            }
        }
        public virtual string Name
        {
            get
            {
                return name;
            }
        }
        public virtual int Speed
        {
            get
            {
                return speed;
            }
        }
        public virtual int Strength
        {
            get
            {
                return strength;
            }
        }
        public virtual int Inteligence
        {
            get
            {
                return inteligence;
            }
        }
        public virtual int Defence
        {
            get
            {
                return defence;
            }
        }
        public virtual bool IsAlive
        {
            get
            {
                return isAlive;
            }
            set
            {
                isAlive = value;
            }
        }
        public virtual int HitPointsCurrent
        {
            get
            {
                return hitPointsCurrent;
            }
            set
            {
                hitPointsCurrent = value;
                if (hitPointsCurrent > hitPointsMax)
                {
                    hitPointsCurrent = hitPointsMax;
                }
                if (hitPointsCurrent <= 0)
                {
                    hitPointsCurrent = 0;
                    isAlive = false;
                }
            }
        }
        public virtual int HitPointsMax
        {
            get
            {
                return hitPointsMax;
            }
        }
        public virtual int PrevHp
        {
            get
            {
                return prevHP;
            }
            set
            {
                prevHP = value;
            }
        }
        public virtual void Attack(Character target)
        {

        }
        public virtual void Defend()
        {

        }
        public virtual void Skill(Character target)
        {

        }
        public virtual void TakeAction()
        {

        }
        public virtual void TakeAction(CharacterDisplay[] targets)
        {

        }
    }
}
