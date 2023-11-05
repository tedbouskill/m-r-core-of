using System;
using System.Threading.Tasks;
using DomainCore;
using Infrastructure.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStores
{
    [TestClass]
    public class InventoryItems
    {
        private SqliteConnection _connection;
        private InventoryItemsWriteDbContext _dbContext;

        [TestInitialize]
        public void Initialize()
        {
            // In-memory database only exists while the connection is open
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<InventoryItemsWriteDbContext>()
                .UseSqlite(_connection)
                .Options;

            // Create the schema in the database
            _dbContext = new InventoryItemsWriteDbContext(options);
            _dbContext.Database.EnsureCreated();
        }

        [TestCleanup]
        public void Cleanup()
        {
            _connection.Close();
        }

        [TestMethod]
        [ExpectedException(typeof(DbUpdateException))]
        public async Task Duplicate_Row_Exception()
        {
            var inventoryRepository = new InventoryWriteRepository(_dbContext);

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

            await inventoryRepository.AppendAsync(id, item);
			await inventoryRepository.AppendAsync(id, item);
		}
    }
}
