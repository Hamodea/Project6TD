using System.Diagnostics;


namespace Project6TD.Systems
{
    public class EconomySystem
    {
        public int Money { get; private set; }

        public EconomySystem(int startMoney)
        {
            Money = startMoney;
        }

        public bool CanAfford(int cost)
        {
            return Money >= cost;
        }

        public bool Spend(int amount)
        {
            if (!CanAfford(amount))
                return false;

            Money -= amount;
            return true;
        }

        public void Earn(int amount)
        {
            Money += amount;
            Debug.WriteLine($"Economy.Earn: +{amount} -> Money={Money}");
        }

        public void Reset(int startMoney)
        {
            Money = startMoney;
        }
    }
}

