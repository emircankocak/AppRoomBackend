using System.Xml.XPath;

namespace AppRoomGameBackend.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int Health { get; set; }
        public int Mana { get; set; }
        public int Gold { get; set; }

        public int XP { get; set; }

        public void AddExperience(int amount)//Level Kontrolü
        {
            XP += amount; 

            while(XP >= 100)
            {
                XP -= 100;
                Level += 1;
            }
        }
        
        
        public List<Item> Inventory { get; set; } = new List<Item>(); //Envanter Listesi
    }
}
