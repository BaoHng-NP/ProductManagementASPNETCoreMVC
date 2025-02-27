using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class AccountDAO
    {

        //private readonly MystoreContext _context;
        //public AccountDAO(MystoreContext context)
        //{
        //    _context = context;
        //}
        MystoreContext _context;
        public AccountMember GetAccountById(string memberEmail)
            {
            _context = new();
            return _context.AccountMembers.FirstOrDefault(p => p.EmailAddress == memberEmail);
            }
    }
}
