using System;
using System.Threading.Tasks;
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
    public class EventSourcing
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
            _ = dbEventsContext.Database.EnsureCreated();

            return new InventoryEventRepository(dbEventsContext);
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
            _ = await InvokeInventoryEventRepository().AppendEventAsync(new CreateInventoryItem(item));

            // Activate
            await InvokeInventoryEventRepository().AppendEventAsync(new ActivateInventoryItem(id, new SetInventoryItemActivation()));

            // Deactivate
            await InvokeInventoryEventRepository().AppendEventAsync(new DeactivateInventoryItem(id, new SetInventoryItemActivation()));

            // Update Full Item
            await InvokeInventoryEventRepository().AppendEventAsync(new UpdateInventoryItem(new InventoryItemDto()
            {
                Id = id,
                LastEventTimestamp = DateTime.UtcNow,
                Name = "UpdatedItem",
                IsActive = true,
                Count = 2,
                Note = "N/A"

            }));

            // Set Count
            await InvokeInventoryEventRepository().AppendEventAsync(new SetInventoryItemCount(
                id, new SetInventoryItemCountData() { Count = 10 }));

            // Increase Count by 1
            await InvokeInventoryEventRepository().AppendEventAsync(new IncreaseInventoryItemCount(
                id, new AdjustInventoryItemCount() { Delta = 1 }));

            // Decrease Count by 3
            await InvokeInventoryEventRepository().AppendEventAsync(new DecreaseInventoryItemCount(
                id, new AdjustInventoryItemCount() { Delta = 3 }));

            // Set Name
            await InvokeInventoryEventRepository().AppendEventAsync(new SetInventoryItemName(
                id, new SetInventoryItemNameData() { Name = "Name" }));

            // Set Note
            await InvokeInventoryEventRepository().AppendEventAsync(new SetInventoryItemNote(
                id, new SetInventoryItemNoteData() { Note = "Note" }));

            // Confirm Final Model
            InventoryItemEvents iie = new InventoryItemEvents(InvokeInventoryEventRepository(), id);
            InventoryItemAggregate iia = (InventoryItemAggregate)await iie.ModelAsync();

            Assert.IsTrue(iia.Name.Equals("Name", StringComparison.Ordinal));
            Assert.IsTrue(iia.IsActive);
            Assert.IsTrue(iia.Count == 8);
            Assert.IsTrue(iia.Note.Equals("Note", StringComparison.Ordinal));

            // Delete Item
            await InvokeInventoryEventRepository().AppendEventAsync(new DeleteInventoryItem(id));

            // Confirm Nb of Events
            int eventCount = await InvokeInventoryEventRepository().ModelEventsCountAsync(id);

            Assert.IsTrue(eventCount == 10);
        }
    }
}
