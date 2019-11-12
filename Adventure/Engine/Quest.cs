using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Quest
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RewardEXPPoints { get; set; }
        public int RewardCoins { get; set; }
        public Item RewardItem { get; set; }
        public List<QuestCompletionItem> QuestCompletionItems { get; set; }

        public Quest(int id, string name, string description, int rewardEXPPoints, int rewardCoins)
        {
            ID = id;
            Name = name;
            Description = description;
            RewardEXPPoints = rewardEXPPoints;
            RewardCoins = rewardCoins;

            QuestCompletionItems = new List<QuestCompletionItem>();
        }

    }
}
