using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElleRealTime.Shared.Entities.Generic;

namespace ElleRealTime.Shared.BO
{
    public static class EnumUtils
    {

        public static T[] OrderByValue<T>()
        {
            T[] items = Enum.GetValues(typeof(T)).Cast<T>().OrderBy(x => x).ToArray();

            return items;
        }

        public static EnumEntity[] ToEnumEntity<T>(Array values = null)
        {
            T[] items;
            if (values == null)
                items = Enum.GetValues(typeof(T)).Cast<T>().OrderBy(x => x).ToArray();
            else
            {
                List<T> tmp = new List<T>();
                foreach (var value in values)
                {
                    tmp.Add((T)Enum.ToObject(typeof(T), value));
                }
                items = tmp.ToArray().OrderBy(x => x).ToArray();
            }


            List<EnumEntity> ret = new List<EnumEntity>();

            foreach (var item in items)
            {
                string txtVal = Enum.GetName(typeof(T), item);
                if (!string.IsNullOrWhiteSpace(txtVal))
                {
                    int id = (int)Enum.Parse(typeof(T), txtVal);
                    string desc = txtVal;

                    ret.Add(new EnumEntity
                    {
                        Description = desc,
                        ID = id
                    });
                }
            }

            return ret.ToArray();
        }
    }
}
