
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace techy.Models
{
    public class Game : BaseTable<Game>
    {

        [PrimaryKey]
        public int id { get; set; }
        public string name { get; set; }
        //
        // Interface methods
        //

        public Game() : base()
        {
        }

        // This is required to trigger the base constructor to create the table if necessary
        static Game()
        {
            var init = (new Game());
        }

        public static async Task<int> CountAsync()
        {
            return await Database.Table<Game>().CountAsync();
        }

        public static async Task<List<Game>> GetItemsAsync()
        {
            return await Database.Table<Game>().ToListAsync();
        }

        public static async Task<Game> GetItemAsync(int id)
        {
            return await Database.Table<Game>().Where(i => i.id == id).FirstOrDefaultAsync();
        }



        public static async Task<int> SaveItemAsync(Game item)
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

        public static async Task<int> SaveOrUpdateItemAsync(Game item)
        {
            var currentItem = await GetItemAsync(item.id);
            if (currentItem != null)
            {
                item.id = currentItem.id;
            }

            return await SaveItemAsync(item);
        }

        public static Task<int> DeleteItemAsync(Game item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
