using SQLManager.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLManager.Model
{
    class Database
    {
        private readonly Lazy<IEnumerable<DBEntity>> tables;
        private readonly Lazy<IEnumerable<DBEntity>> views;

        public Database()
        {
            tables = new Lazy<IEnumerable<DBEntity>>(() => Repository.GetDBEntities(this, DBEntityType.Table));
            views = new Lazy<IEnumerable<DBEntity>>(() => Repository.GetDBEntities(this, DBEntityType.View));
        }

        public IList<DBEntity> Tables
        {
            get => new List<DBEntity>(tables.Value);
        }

        public IList<DBEntity> Views
        {
            get => new List<DBEntity>(views.Value);
        }

        public string Name { get; set; }
        public override string ToString() => Name;
    }
}
