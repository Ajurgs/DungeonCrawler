using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DungeonCrawler
{
    public partial class Dungeon : Form
    {
        Random rng = new Random((int)DateTime.Now.Millisecond);
        CharacterDisplay[] heroes;
        CharacterDisplay[] monsters;
        List<CharacterDisplay> turnOrder;
        int numberOfUnits;
        int roomsCleared = 0;
        int turn = 0;
        bool heroesAlive;
        CharacterDisplay selected = null;
        AttackDialogue attackMessage;
        SkillDialogue skillMessage;
                
        public Dungeon()
        {
            
            InitializeComponent();
            attackBtn.Visible = false;
            defendBtn.Visible = false;
            skillBtn.Visible = false;
            heroes = GetHeroes(1);
            monsters = GetMonsters();
            heroesAlive = true;
            numberOfUnits = heroes.GetLength(0) + monsters.GetLength(0);
            Console.WriteLine(numberOfUnits);
        }

        /// <summary>
        /// Advance to the next room
        /// </summary>
        /// <param name="heroes"></param>
        /// <param name="monsters"></param>
        private void NextRoom(CharacterDisplay[] heroes, CharacterDisplay[] monsters)
        {
            room.BackgroundImage = Properties.Resources.stoneWall;
            for (int i = 0; i < heroes.GetLength(0); i++)
            {
                room.Controls.Add(heroes[i]);
            }
            for (int i = 0; i < monsters.GetLength(0); i++)
            {
                room.Controls.Add(monsters[i]);
            }
        }
        /// <summary>
        /// make the array for heroes
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private CharacterDisplay[] GetHeroes(int length)
        {
            CharacterDisplay[] temp = new CharacterDisplay[length];
            for (int i = 0; i < temp.GetLength(0); i++)
            {
                temp[i] = new CharacterDisplay(new Warrior());
                temp[i].Location = new Point(4, 4 + (106 * i));
                setSprite(temp[i]);
            }
            return temp;
        }
        /// <summary>
        /// Get an array of monsters
        /// </summary>
        /// <returns></returns>
        private CharacterDisplay[] GetMonsters()
        {
            CharacterDisplay[] temp = new CharacterDisplay[1];
            for (int i = 0; i < temp.GetLength(0); i++)
            {
                temp[i] = new CharacterDisplay(new Bandit());
                temp[i].Location = new Point(564, 4+(106*i));
                setSprite(temp[i]);
            }
            return temp;
        }
        /// <summary>
        /// Set the CharacterDisplay's SpriteBox image
        /// depending on the type of unit.
        /// </summary>
        /// <param name="display"></param>
        private void setSprite(CharacterDisplay display)
        {
            Console.WriteLine(display.GetName);
            if (display.GetName == "Warrior")
            {
                display.GetSpriteBox.Image = Properties.Resources.knightsprite;
            }
            else if (display.GetName == "Mage")
            {
            }
            else if (display.GetName == "Cleric")
            {
            }
            else if (display.GetName == "Bandit")
            {
                display.GetSpriteBox.Image = Properties.Resources.Bandit;
            }
            else if (display.GetName == "Ogre")
            {
            }
            else if (display.GetName == "Dragon")
            {
            }
        }
        /// <summary>
        /// Make a list in decending order bised on speed
        /// </summary>
        /// <param name="heroes"></param>
        /// <param name="monsters"></param>
        /// <returns></returns>
        private List<CharacterDisplay> SetTurnOrder(CharacterDisplay[] heroes, CharacterDisplay[] monsters)
        {
            List<CharacterDisplay> temp = new List<CharacterDisplay>();
            for (int i = 0; i < heroes.GetLength(0); i++)
            {
                temp.Add(heroes[i]);
            }
            for (int i = 0; i < monsters.GetLength(0); i++)
            {
                temp.Add(monsters[i]);
            }
            Sort(temp);
            return temp;
        }
        /// <summary>
        /// sort list bases on speed in decending order
        /// </summary>
        /// <param name="list"></param>
        private void Sort(List<CharacterDisplay> list)
        {
            bool sorted = false;
            while (sorted != true)
            {
                sorted = true;
                for (int i = 0; i < list.Count()-1; i++)
                {
                    if (list[i].Unit.Speed < list[i + 1].Unit.Speed)
                    {
                        sorted = false;
                        Swap(list, i, i + 1);
                    }
                }
            } 
        }
        /// <summary>
        /// swap two indexes of a list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="indexA"></param>
        /// <param name="indexB"></param>
        private static void Swap<T>(IList<T> list, int indexA, int indexB)
        {
            T temp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = temp;
        }
        /// <summary>
        /// highlight charaicter whoes turn it is
        /// </summary>
        /// <param name="current"></param>
        /// <param name="next"></param>
        private void ChangeSelected(CharacterDisplay current, CharacterDisplay next)
        {
            if (current == null)
            {
                selected = next;
                selected.Highlight.BackColor = Color.Aquamarine;
            }
            else
            {
                current.Highlight.BackColor = Color.White;
                selected = next;
                selected.Highlight.BackColor = Color.Aquamarine;
            }
            if (selected.Unit.IsAlive == false)
            {
                GoToNextTurn();
            }
            if (selected.Type == "Monster")
            {
                this.Update();
                System.Threading.Thread.Sleep(1000);
                // add selected monsters action
                selected.Unit.TakeAction(heroes);
                GoToNextTurn();
            }

        }
        private void startBtn_Click(object sender, EventArgs e)
        {
            startBtn.Visible = false;
            NextRoom(heroes, monsters);
            attackBtn.Visible = true;
            defendBtn.Visible = true;
            skillBtn.Visible = true;
            turnOrder = SetTurnOrder(heroes, monsters);
            UpdateRoom();
            ChangeSelected(selected, turnOrder[turn]);
            
        }
        /// <summary>
        /// when the attack button is pressed open dialog to select
        /// who to attack
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void attackBtn_Click(object sender, EventArgs e)
        {
            attackMessage = new AttackDialogue(monsters,selected,this);
            attackMessage.ShowDialog(this);
        }
        /// <summary>
        /// when the defend button is pressed defend for the turn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void defendBtn_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Defend for Round?", "Defend", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                selected.Unit.Defend();
                GoToNextTurn();
                

            }
            else if (dialogResult == DialogResult.No)
            {
                // do nothing;
            }

        }
        /// <summary>
        /// When Skill button is pressed open a diolog
        /// to select the target
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void skillBtn_Click(object sender, EventArgs e)
        {
            skillMessage = new SkillDialogue(monsters, selected, this);
            skillMessage.Show(this);
        }
        /// <summary>
        /// increment turn and either go to next round or next to take an action
        /// </summary>
        public void GoToNextTurn()
        {
            UpdateRoom();
            turn += 1;
            if (turn >= turnOrder.Count)
            {
                if (RoomClear() == true)
                {
                    // go to new room
                    Console.WriteLine("room Cleared");
                }
                else
                {
                    System.Threading.Thread.Sleep(1000);
                    GoToNextRound();
                }
            }
            else
            {
                ChangeSelected(selected, turnOrder[turn]);
            }
            this.Update();

        }
        private void GoToNextRound()
        {
            turn = 0;
            turnOrder = SetTurnOrder(heroes, monsters);
            this.Update();
            ChangeSelected(selected, turnOrder[turn]);
        }
        private bool RoomClear()
        {
            bool isClear = true;
            for (int i = 0; i < monsters.GetLength(0); i++)
            {
                if (monsters[i].Unit.IsAlive == true)
                {
                    isClear = false;
                }
            }
            return isClear;
        }
        private void UpdateRoom()
        {
            for (int i = 0; i < turnOrder.Count(); i++)
            {
                turnOrder[i].UpdateInfo();
            }
        }
    }
}
