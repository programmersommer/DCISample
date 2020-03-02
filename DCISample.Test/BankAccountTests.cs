using DCISample.Contexts;
using DCISample.Models;
using Xunit;

namespace DCISample.Text
{
    public class BankAccountTests
    {
        private readonly Bank _bank;
        private readonly AccountContext _context;

        public BankAccountTests()
        {
            _bank = new Bank();
            _context = new AccountContext();
        }

        [Theory]
        [InlineData(1000, 0, 300, 700, 300)]
        public void BankAccountTest(int initial1, int initial2, int amount, int final1, int final2)
        {
            _bank.AddAccountNo(1, initial1);
            _bank.AddAccountNo(2, initial2);

            _context.Transfer(amount, 1, 2, _bank);

            Assert.True(_bank.FindAccountNo(1).Balance() == final1);
            Assert.True(_bank.FindAccountNo(2).Balance() == final2);
        }
    }
}
