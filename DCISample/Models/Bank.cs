using System.Collections;

namespace DCISample.Models
{
    // Data
    public class Bank
    {
        private readonly Hashtable _accountTable;

        public Bank()
        {
            _accountTable = new Hashtable();
        }

        public Account FindAccountNo(int accountNo)
        {
            return (Account)_accountTable[accountNo];
        }

        public void AddAccountNo(int accountNo, int initialBalance)
        {
            _accountTable.Add(accountNo, new Account(initialBalance));
        }

    }
}