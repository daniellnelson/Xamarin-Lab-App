using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace techy.Models
{
    public class BaseTable<T>
    {
        protected static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        protected static SQLiteAsyncConnection Database => lazyInitializer.Value;
        protected static bool initialized = false;

        public BaseTable()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(T).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(T)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }
    }
}