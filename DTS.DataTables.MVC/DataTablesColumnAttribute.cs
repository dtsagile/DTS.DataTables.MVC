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
        private string _data;
        private string _name;
        private int _target;
        private bool _visible = true;
        private bool _sortable = true;
        private bool _isNull = false;
        private string _width;
        private string _className;

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

        public virtual bool Sortable 
        {
            get { return _sortable; }
            set { _sortable = value; }
        }

        public virtual string Data
        {
            get {  
                if (!_isNull) {
                    return _data;
                }
                return "null";
            }
            set { _data = value; }
        }

        public virtual string Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public virtual string ClassName
        {
            get { return _className; }
            set { _className = value; }
        }

        public virtual bool IsNull
        {
            get { return _isNull; }
            set { _isNull = value; }
        }
    }
}
