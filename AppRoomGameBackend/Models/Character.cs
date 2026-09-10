using System.Text.Json.Serialization;
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

        [JsonIgnore]
        public List<Item>? Items { get; set; }// One-to-Many : Bir character'in birden fazla Item'i olabilir. 

        public void AddExperience(int amount)//Level Kontrolü
        {
            XP += amount; 

            while(XP >= 100)
            {
                XP -= 100;
                Level += 1;
            }
        }
        
        public bool SpendGold(int amount)
        {
            if(amount <=0)
            {
                return false;
            }
            if(amount > Gold)
            {
                return false;
            }
            Gold -= amount;
            return true;
        }
        
        
    }
}
