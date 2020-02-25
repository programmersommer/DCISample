using DCISample.Contexts.Account;

namespace DCISample.Models
{
    class Source
    {
        private readonly Context _context;
        public Source(Context context)
        {
            _context = context;
        }

        public object TransferTo()
        {
            if ((int)_context.Call("Source", "Balance") < _context.Amount)
            {
                // TODO log error "ERROR. Insufficient funds, Operation aborted. "
            }
            else
            {
                object[] args = { _context.Amount };
                _context.Call("Source", "Decrease", args);
                _context.Call("Destination", "TransferFrom");
            }

            return (null);
        }
    }
}