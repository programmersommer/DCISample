using System;
using System.Collections;
using System.Reflection;
using DCISample.Models;

namespace DCISample.Contexts.Account
{
    // Context = use cases
    public class Context
    {
        // The Roles = interaction                       
        private Models.Account _source;
        private Models.Account _destination;
        public int Amount;
        Hashtable _roleMap;


        #region System operations

        public void Transfer(int amt, int from, int to, Bank bank)
        {
            // Role mappings
            Amount = amt;
            _source = bank.FindAccountNo(from);
            _destination = bank.FindAccountNo(to);
            _roleMap = new Hashtable
            {
                {"Source", _source},
                {"Destination", _destination},
                {"Amount", Amount}
            };
            // Perform operation
            Call("Source", "TransferTo");
        }

        #endregion


        #region The Role classes  with role methods 

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

        #endregion

        #region The execution engine  

        public object Call(string toRole, string methodName)
        {
            object[] args = { };
            return (Call(toRole, methodName, args));
        }

        public object Call(string toRole, string methodName, object[] args)
        {
            string roleClassName = "DCISample.Contexts.Account.Context+" + toRole;
            MethodInfo roleMethod = null;
            MethodInfo dataMethod = null;
            try
            {
                // Try to execute role method.
                var roleClass = Type.GetType(roleClassName);
                var roleMethods = roleClass.GetMethods();
                foreach (var m in roleMethods)
                {
                    if (m.Name == methodName) { roleMethod = m; }
                }

                if (roleMethod != null)
                {
                    var roleConstructor = roleClass.GetConstructor(new[] { typeof(Context) });
                    var instance = roleConstructor.Invoke(new object[] { this });
                    var roleMethodResult = roleMethod.Invoke(instance, new object[] { });
                    return roleMethodResult;
                }

                // No role method. Try to execute instance method.
                var dataObject = (Models.Account)_roleMap[toRole];
                var dataClass = dataObject.GetType();
                var dataMethods = dataClass.GetMethods();

                foreach (var m in dataMethods)
                {
                    if (m.Name == methodName) { dataMethod = m; }
                }

                if (dataMethod == null)
                {
                    // TODO log methodName + " method not found "
                    return (null);
                }

                var dataMethodResult = dataMethod.Invoke(dataObject, args);
                return dataMethodResult;
            }
            catch (Exception e)
            {
                // TODO log
            }
            return (null);
        }

        #endregion

    }
}