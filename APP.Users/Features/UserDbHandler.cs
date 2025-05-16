using CORE.APP.Features;
using System;
using System.Collections.Generic;
using APP.Users.Domain;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;


namespace APP.Users.Features
{
    public abstract class UserDbHandler:Handler
    {
        protected readonly UsersDb _db;
        protected UserDbHandler(UsersDb db) : base(new CultureInfo("en-US"))
        {
            _db = db;
        }
    }
}
