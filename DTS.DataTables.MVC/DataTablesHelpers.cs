﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DTS.DataTables.MVC
{
    public static class DataTablesHelpers
    {
        public static T GetDataTablesAttributeType<T>(this Enum enumItem) where T : Attribute
        {
            var type = enumItem.GetType();
            var memberInfo = type.GetMember(enumItem.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
            return attributes.Length > 0 ? (T)attributes[0] : null;
        }

        public static List<ColumnHeader> GetColumnsHeaders<T>()
        {
            var list = new List<ColumnHeader>();
            var theType = typeof(T);

            var properties = theType.GetProperties().Where(x => x.IsDefined(typeof(DataTablesColumnAttribute), false));

            foreach (var prop in properties)
            {
                var attr = prop.GetCustomAttributes(true).FirstOrDefault(x => x.GetType().ToString() == "DTS.DataTables.MVC.DataTablesColumnAttribute");
                if (attr != null)
                {
                    list.Add(new ColumnHeader((DataTablesColumnAttribute) attr, prop.Name)); 
                }
            }

            return list;
        }


        public static TEnum ParseEnum<TEnum>(this string value, bool ignoreCase = false) where TEnum : struct
        {
            TEnum result;
            if (!Enum.TryParse<TEnum>(value, ignoreCase, out result)) {
               throw new Exception("value is not valid member of enum");
            }
            return result;
        }        

    }
}
