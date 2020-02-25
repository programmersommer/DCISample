namespace DCISample.Models
{
    // Data
    public class Account
    {
        private int _balance;

        public Account(int initialBalance)
        {
            _balance = initialBalance;
        }

        public int Balance()
        {
            return _balance;
        }

        public void Increase(int amount)
        {
            _balance += amount;
        }

        public void Decrease(int amount)
        {
            _balance -= amount;
        }

    }
}