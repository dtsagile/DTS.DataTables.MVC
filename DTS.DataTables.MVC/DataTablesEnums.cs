using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DTS.DataTables.MVC
{
    public enum DataTablesOrder
    {
        [Description("asc")]
        Ascendant,
        [Description("desc")]
        Descendant
    }


    public enum ColumnTypes
    {
        [Description("string")]
        String,
        // sorting chronologically, filtering no effect
        [Description("date")]
        Date,
        // sorting numerically, filtering no effect
        [Description("num")]
        Number,
        [Description("num-frt")]
        // numbers formatted with separators, currency symbols, etc, sorting numerically, filtering no effect
        NumberFormatted,
        // number type with html tags, sorting numerically, filtering no effect
        [Description("html-num")]
        HtmlNumber,
        [Description("html-num-fmt")]
        // numbers formatted with separators, currency symbols, etc, and html tags, sorting numerically, filtering no effect
        HtmlNumberFormatted,
        // html string, sorting with tags removed, filtering html tags remove
        [Description("html")]
        Html
    }

    public static class EnumHelpers
    {
        public static string GetDescription(Enum en)
        {
            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return en.ToString();
        }
    }

}
