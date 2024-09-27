using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAS_Addresses_Changes
{
    internal class MyDataCollection : IComparable, IEquatable<MyDataCollection>
    {
		private string? name;

		public string? Name
        {
			get { return name; }
			set { name = value; }
		}

		private string? typename;

		public string? Typename
        {
			get { return typename; }
			set { typename = value; }
		}

		private int level;

		public int Level
		{
			get { return level; }
			set { level = value; }
		}


		public MyDataCollection(string name, string typename, int level)
		{
			this.name = name;
			this.typename = typename;
			this.level = level;
		}

        public int CompareTo(object? obj)
        {
			if (obj is MyDataCollection data)
			{
				return Name.CompareTo(data.Name);
			}
			else
			{
                throw new ArgumentException("Некорректное значение параметра");
            }
        }

        public bool Equals(MyDataCollection? other)
        {
            return this.Name == other.Name &&
				   this.Typename == other.Typename &&
				   this.Level == other.Level;
        }
    }
}
