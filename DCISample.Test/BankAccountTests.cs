using DCISample.Contexts.Account;
using DCISample.Models;
using Xunit;

namespace DCISample.Text
{
    public class BankAccountTests
    {
        private readonly Bank _bank = new Bank();
        private readonly Context _context = new Context();
        // TODO prepare variables

        [Fact]
        public void BankAccountTest()
        {
            _bank.AddAccountNo(111, 1000);
            _bank.AddAccountNo(222, 0);

            _context.Transfer(300, 111, 222, _bank);

            Assert.True(_bank.FindAccountNo(111).Balance() == 700);
            Assert.True(_bank.FindAccountNo(222).Balance() == 300);
        }
    }
}
