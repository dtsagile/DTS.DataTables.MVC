using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTS.DataTables.MVC
{
    /// <summary>
    /// Attribute for decorating enums
    /// </summary>
    public class DataTablesColumnAttribute : Attribute
    {
        private string _name;
        private int _target;
        private bool _visible = true;

        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public virtual int Target
        {
            get { return _target; }
            set { _target = value; }
        }

        public virtual bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }
    }
}
