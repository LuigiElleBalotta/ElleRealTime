using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ElleRealTime.Shared.DBEntities.Accounts
{
    public class AccountsFilter : Filter
    {
        private static readonly Hashtable htDecodeColNames = new Hashtable();
        private const string DEFAULT_ORDERBY = "Username";

        public int ID { get; set; }

        static AccountsFilter()
        {
            htDecodeColNames.Add("colAccountID", "ID");
            htDecodeColNames.Add("colUsername", "Username");
        }

        public override string WhereCondition(Hashtable prms)
        {
            List<string> conds = new List<string>();

            if (ID > 0)
            {
                conds.Add("ID = @ID");
                prms["@ID"] = ID;
            }

            return MakeWhereCondition(conds);
        }

        public override string OrderByCondition()
        {
            return OrderByCondition(htDecodeColNames, DEFAULT_ORDERBY);
        }
    }
}
