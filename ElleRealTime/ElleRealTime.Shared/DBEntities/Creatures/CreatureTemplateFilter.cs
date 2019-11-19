using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ElleRealTime.Shared.DBEntities.Creatures
{
    public class CreatureTemplateFilter : Filter
    {
        private static readonly Hashtable htDecodeColNames = new Hashtable();
        private const string DEFAULT_ORDERBY = "PrefabName";

        public int CreatureID { get; set; }
        public string PrefabNameAll { get; set; }

        static CreatureTemplateFilter()
        {
            htDecodeColNames.Add("colPrefabName", "PrefabName");
            htDecodeColNames.Add("colName", "Name");
        }

        public override string WhereCondition(Hashtable prms)
        {
            List<string> conds = new List<string>();

            if (CreatureID > 0)
            {
                conds.Add("ID = @CreatureID");
                prms["@CreatureID"] = CreatureID;
            }

            if (!string.IsNullOrWhiteSpace(PrefabNameAll))
            {
                conds.Add("LOWER(PrefabName) = LOWER(@PrefabNameAll)");
                prms["@PrefabNameAll"] = PrefabNameAll;
            }

            return MakeWhereCondition(conds);
        }

        public override string OrderByCondition()
        {
            return OrderByCondition(htDecodeColNames, DEFAULT_ORDERBY);
        }
    }
}
