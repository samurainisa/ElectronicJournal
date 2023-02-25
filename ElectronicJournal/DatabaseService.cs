using ElectronicJournal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectronicJournal
{
    public class DatabaseService : IDatabaseService
    {
        private readonly InstDBEntities1 db;

        public DatabaseService()
        {
            this.db = new InstDBEntities1();
        }

        public async Task<BindingSource> GetTableAsync<T>() where T : class
        {
            var table = await Task.Run(() => db.Set<T>().ToList());
            return new BindingSource(table, null);
        }

        public void DeleteRecord<T>(int id) where T : class
        {
            using (InstDBEntities1 db = new InstDBEntities1())
            {
                var entity = db.Set<T>().Find(id);
                if (entity != null)
                {
                    db.Set<T>().Remove(entity);
                    db.SaveChanges();
                }
            }
        }
    }

    public interface IDatabaseService
    {
        Task<BindingSource> GetTableAsync<T>() where T : class;
        void DeleteRecord<T>(int id) where T : class;
    }
}
