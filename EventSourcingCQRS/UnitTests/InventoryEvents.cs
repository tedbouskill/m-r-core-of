using System;
using System.Threading.Tasks;
using Application.Commands;
using DomainCore;
using Infrastructure.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStores
{
    [TestClass]
    public class InventoryEvents
    {
        private SqliteConnection _connection;
        private InventoryEventsDbContext _dbContext;

        [TestInitialize]
        public void Initialize()
        {
            // In-memory database only exists while the connection is open
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<InventoryEventsDbContext>()
                .UseSqlite(_connection)
                .Options;

            // Create the schema in the database
            _dbContext = new InventoryEventsDbContext(options);
            _dbContext.Database.EnsureCreated();
        }

        [TestCleanup]
        public void Cleanup()
        {
            while (_connection.State == System.Data.ConnectionState.Executing){};
            _connection.Close();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task Duplicate_Row_Exception()
        {
            // Note: DbContext with SQLite throws a different exception when a compound unique key is used
            // In this case the unique index key is Guid and Timestamp

            var inventoryRepository = new InventoryEventRepository(_dbContext);

            Guid id = Guid.NewGuid();

            InventoryItemDto item = new InventoryItemDto()
            {
                Id = Guid.NewGuid(),
                LastEventTimestamp = DateTime.UtcNow,
                Name = "Duplicate Test",
                IsActive = false,
                Count = 0,
                Note = ""
            };

            CreateInventoryItem itemEvent = new CreateInventoryItem(item);

            await inventoryRepository.AppendEventAsync(itemEvent);
            await inventoryRepository.AppendEventAsync(itemEvent);
        }
	}
}
