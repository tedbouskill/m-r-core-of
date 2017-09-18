using System;
using System.Threading.Tasks;
using Application;
using Application.Commands;
using Application.EventData;
using DomainCore;
using Infrastructure.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Application
{
    [TestClass]
    public class EventSourcingCQRS
    {
        private SqliteConnection _eventsConnection;
        private SqliteConnection _itemsConnection;

        private InventoryEventRepository InvokeInventoryEventRepository()
        {
            var eventsOptions = new DbContextOptionsBuilder<InventoryEventsDbContext>()
                .UseSqlite(_eventsConnection)
                .Options;

            // Create the schema in the database
            var dbEventsContext = new InventoryEventsDbContext(eventsOptions);
            dbEventsContext.Database.EnsureCreated();

            return new InventoryEventRepository(dbEventsContext);
        }

		private InventoryReadRepository InvokeInventoryReadRepository()
		{
			var itemsOptions = new DbContextOptionsBuilder<InventoryItemsReadDbContext>()
				.UseSqlite(_itemsConnection)
				.Options;

			// Create the schema in the database
			var dbItemsContext = new InventoryItemsReadDbContext(itemsOptions);
			dbItemsContext.Database.EnsureCreated();

			return new InventoryReadRepository(dbItemsContext);
		}

        private InventoryWriteRepository InvokeInventoryWriteRepository()
		{
			var itemsOptions = new DbContextOptionsBuilder<InventoryItemsWriteDbContext>()
				.UseSqlite(_itemsConnection)
				.Options;

			// Create the schema in the database
			var dbItemsContext = new InventoryItemsWriteDbContext(itemsOptions);
			dbItemsContext.Database.EnsureCreated();

            return new InventoryWriteRepository(dbItemsContext);
		}

		private InventoryCommandHandler InvokeInventoryCommandHandler()
        {
            return new InventoryCommandHandler(
                    InvokeInventoryEventRepository(),
                    InvokeInventoryWriteRepository()
                );
        }

        [TestInitialize]
        public void Initialize()
        {
            // In-memory database(s) only exists while the connection is open

            _eventsConnection = new SqliteConnection("DataSource=:memory:");
            _eventsConnection.Open();

            _itemsConnection = new SqliteConnection("DataSource=:memory:");
            _itemsConnection.Open();
        }

        [TestCleanup]
        public void Cleanup()
        {
            while (_eventsConnection.State == System.Data.ConnectionState.Executing) { };
            _eventsConnection.Close();

            while (_itemsConnection.State == System.Data.ConnectionState.Executing) { };
            _itemsConnection.Close();
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateAggregateException))]
        public async Task Duplicate_CreateInventoryItem_Exception()
        {
            InventoryItemDto item = new InventoryItemDto()
            {
                Id = Guid.NewGuid(),
                LastEventTimestamp = DateTime.UtcNow,
                Name = "Duplicat CreateInventoryItem for AggregateId",
                IsActive = false,
                Count = 0,
                Note = ""
            };

            CreateInventoryItem itemEvent = new CreateInventoryItem(item);

            // First CreateInventoryItem for the test
            await InvokeInventoryCommandHandler().Handle(itemEvent);

            // Keep the same AggregateId, but set a new time to meet Db unique constraint
            item.LastEventTimestamp = DateTime.UtcNow;
            itemEvent = new CreateInventoryItem(item);

            // Attempt to add another CreateInventoryItem for the test
            await InvokeInventoryCommandHandler().Handle(itemEvent);
        }

        [TestMethod]
        public async Task Confirm_Final_Aggregate_Model()
        {
            Guid id = Guid.NewGuid();

            InventoryItemDto item = new InventoryItemDto()
            {
                Id = id,
                LastEventTimestamp = DateTime.UtcNow,
                Name = "",
                IsActive = false,
                Count = 0,
                Note = ""
            };

            // Create Inventory Item
            await InvokeInventoryCommandHandler().Handle(new CreateInventoryItem(item));

            // Activate
            await InvokeInventoryCommandHandler().Handle(new ActivateInventoryItem(id, new SetInventoryItemActivation()));

            // Deactivate
            await InvokeInventoryCommandHandler().Handle(new DeactivateInventoryItem(id, new SetInventoryItemActivation()));

            // Update Full Item
            await InvokeInventoryCommandHandler().Handle(new UpdateInventoryItem(new InventoryItemDto()
            {
                Id = id,
                LastEventTimestamp = DateTime.UtcNow,
                Name = "UpdatedItem",
                IsActive = true,
                Count = 2,
                Note = "N/A"

            }));

            // Set Count
            await InvokeInventoryCommandHandler().Handle(new SetInventoryItemCount(
                id, new SetInventoryItemCountData() { Count = 10 }));

            // Increase Count by 1
            await InvokeInventoryCommandHandler().Handle(new IncreaseInventoryItemCount(
                id, new AdjustInventoryItemCount() { Delta = 1 }));

            // Decrease Count by 3
            await InvokeInventoryCommandHandler().Handle(new DecreaseInventoryItemCount(
                id, new AdjustInventoryItemCount() { Delta = 3 }));

            // Set Name
            await InvokeInventoryCommandHandler().Handle(new SetInventoryItemName(
                id, new SetInventoryItemNameData() { Name = "Name" }));

            // Set Note
            await InvokeInventoryCommandHandler().Handle(new SetInventoryItemNote(
                id, new SetInventoryItemNoteData() { Note = "Note" }));

            // Confirm Final Model
            InventoryItemEvents iie = new InventoryItemEvents(InvokeInventoryEventRepository(), id);
            InventoryItemAggregate iia = (InventoryItemAggregate)await iie.ModelAsync();

            Assert.IsTrue(iia.Name.Equals("Name", StringComparison.Ordinal));
            Assert.IsTrue(iia.IsActive);
            Assert.IsTrue(iia.Count == 8);
            Assert.IsTrue(iia.Note.Equals("Note", StringComparison.Ordinal));

            int itemsCount = await InvokeInventoryReadRepository().ModelsCountAsync();
			Assert.IsTrue(itemsCount == 1);

			// Delete Item
			await InvokeInventoryCommandHandler().Handle(new DeleteInventoryItem(id));
			
            itemsCount = await InvokeInventoryReadRepository().ModelsCountAsync();
			Assert.IsTrue(itemsCount == 0);

			// Confirm Nb of Events
			int eventCount = await InvokeInventoryEventRepository().ModelEventsCountAsync(id);

            Assert.IsTrue(eventCount == 10);
        }
    }
}
