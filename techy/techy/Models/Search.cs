
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace techy.Models
{
    public class Search : BaseTable<Search>
    {

        [PrimaryKey]
        public int id { get; set; }
        public string name { get; set; }
        //
        // Interface methods
        //

        public Search() : base()
        {
        }

        // This is required to trigger the base constructor to create the table if necessary
        static Search()
        {
            var init = (new Search());
        }

        public static async Task<int> CountAsync()
        {
            return await Database.Table<Search>().CountAsync();
        }

        public static async Task<List<Search>> GetItemsAsync()
        {
            return await Database.Table<Search>().ToListAsync();
        }

        public static async Task<Search> GetItemAsync(int id)
        {
            return await Database.Table<Search>().Where(i => i.id == id).FirstOrDefaultAsync();
        }



        public static async Task<int> SaveItemAsync(Search item)
        {
            if (item.id != null && item.id > 0)
            {
                return await Database.UpdateAsync(item);
            }
            else
            {
                item.id = 0;
                return await Database.InsertAsync(item);
            }
        }

        public static async Task<int> SaveOrUpdateItemAsync(Search item)
        {
            var currentItem = await GetItemAsync(item.id);
            if (currentItem != null)
            {
                item.id = currentItem.id;
            }

            return await SaveItemAsync(item);
        }

        public static Task<int> DeleteItemAsync(Search item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
