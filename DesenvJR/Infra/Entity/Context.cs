using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Infra.Entity
{
    public class Context: DbContext
    {
        public Context()
            :base(ConfigurationManager.ConnectionStrings["DBNibo"].ConnectionString)
        {

        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Transacao> DBSetTransactions { get; set; }
    }
}
