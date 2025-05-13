using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Data.SQLite;
using System.IO;
using ServiceStack.OrmLite;
using ServiceStack.DataAnnotations;

namespace SSS
{
    [Alias("todos")]
    public class TodoItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public bool IsCompleted { get; set; }
        public string Title { get; set; }
        public TodoItem() { }
        public override string ToString()
        {
            return $"TodoItem {{ Id: {Id}, Title: {Title}, IsCompleted: {IsCompleted} }}";
        }
    }

    public class DBState
    {
        private OrmLiteConnectionFactory _connection;
        private string _filePath;
        public DBState()
        {
            var dbPath = Directory.GetCurrentDirectory() + "todos.db";
            _connection = new OrmLiteConnectionFactory(dbPath, SqliteDialect.Provider);
            _filePath = dbPath;
            using (var db = _connection.Open())
            {
                db.CreateTableIfNotExists<TodoItem>();
            }
        }

        public List<TodoItem> GetItems()
        {
            List<TodoItem> list = new List<TodoItem>();
            using (var db = _connection.Open())
            {
                list = db.Select<TodoItem>().ToList();
            }
            return list;
        }
        public void InsertOrReplace(TodoItem item)
        {
            using (var db = _connection.Open())
            {
                if (item.Id != 0) { db.Update(item); }
                else { db.Insert(item); }
            }
        }
        public void DeleteItem(TodoItem item)
        {
            using (var db = _connection.Open()) { db.Delete(item); }
        }
    }
}
