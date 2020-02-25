using DCISample.Contexts.Account;

namespace DCISample.Models
{
    class Destination
    {
        private readonly Context _context;
        public Destination(Context context)
        {
            _context = context;
        }

        public object TransferFrom()
        {
            object[] args = { _context.Amount };
            _context.Call("Destination", "Increase", args);
            return (null);
        }
    }
}