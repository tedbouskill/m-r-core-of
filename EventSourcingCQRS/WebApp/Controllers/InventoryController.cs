using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Application.Interfaces;
using DomainCore;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class InventoryController : Controller
    {
        private IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        // GET: Inventory
        public async Task<IActionResult> Index()
        {
            return View(await _inventoryService.InventoryAsync());
        }

		// GET: Inventory/Events/5
		public async Task<IActionResult> Events(Guid? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var inventoryItem = await _inventoryService.GetItemAsync(id.Value);

			if (inventoryItem == null)
			{
				return NotFound();
			}

            var inventoryItemEvents = await _inventoryService.InventoryEventsAsync(id.Value);

            return View(new Tuple<InventoryItemDto,IEnumerable<AInventoryItemEvent>>(inventoryItem, inventoryItemEvents));
		}
		
        // GET: Inventory/Details/5
		public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryItem = await _inventoryService.GetItemAsync(id.Value);
                
            if (inventoryItem == null)
            {
                return NotFound();
            }

            return View(inventoryItem);
        }

        // GET: Inventory/Create
        public IActionResult Create()
        {
            return View(new InventoryItemDto() { IsActive = false, Count = 0, Note = ""});
        }

        // POST: Inventory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IsActive,Count,Note")] InventoryItemDto inventoryItem)
        {
            if (ModelState.IsValid)
            {
                inventoryItem.Id = Guid.NewGuid();
                inventoryItem.LastEventTimestamp = DateTime.UtcNow;
                inventoryItem.Note = (inventoryItem.Note ?? "");

                await _inventoryService.PostItemAsync(inventoryItem);

                return RedirectToAction(nameof(Index));
            }

            return View(inventoryItem);
        }

        // GET: Inventory/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

			var inventoryItem = await _inventoryService.GetItemAsync(id.Value);

			if (inventoryItem == null)
            {
                return NotFound();
            }

            var editInventoryItem = new EditInventoryItem(inventoryItem);

            return View(editInventoryItem);
        }

        // POST: Inventory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            Guid id,
            [Bind("Editable,Editable.Id,Editable.Name,Editable.IsActive,Editable.Count,Editable.Note,Original,Original.Name,Original.IsActive,Original.Count,Original.Note")] EditInventoryItem inventoryItem)
        {
            if (id != inventoryItem.Editable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                inventoryItem.Editable.Note = (inventoryItem.Editable.Note ?? "");
                inventoryItem.Original.Note = (inventoryItem.Original.Note ?? "");

				int nameChange = Convert.ToInt32(!inventoryItem.Editable.Name.Equals(inventoryItem.Original.Name, StringComparison.Ordinal));
                int isActiveChange = Convert.ToInt32(inventoryItem.Editable.IsActive != inventoryItem.Original.IsActive);
                int countDelta = inventoryItem.Editable.Count - inventoryItem.Original.Count;
                int noteChange = Convert.ToInt32(!inventoryItem.Editable.Note.Equals(inventoryItem.Original.Note, StringComparison.Ordinal));

                // If more than one record changed, update the entire object
                // NOTE: Because there is one DbContext for this controller, multiple async updates will fail
                if ((nameChange + isActiveChange + Math.Abs(countDelta) + noteChange) > 1)
                {
					await _inventoryService.PutItemAsync(inventoryItem.Editable);
				}
                else
                {
                    if (nameChange > 0)
                        await _inventoryService.PatchItemNameAsync(id, inventoryItem.Editable.Name, null);
                    
                    if (isActiveChange > 0)
                    {
                        if (inventoryItem.Editable.IsActive)
                            await _inventoryService.ActivateItem(id, null);
                        else
                            await _inventoryService.DisableItem(id, null);
					}

                    if (countDelta < 0)
                        await _inventoryService.DecreaseInventory(id, (uint)Math.Abs(countDelta), null);
                    else if (countDelta > 0)
                        await _inventoryService.IncreaseInventory(id, (uint)countDelta, null);

                    if (noteChange > 0)
                        await _inventoryService.PatchItemNoteAsync(id, inventoryItem.Editable.Note, null);
				}

				return RedirectToAction(nameof(Index));
            }

            return View(inventoryItem);
        }

        // GET: Inventory/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

			var inventoryItem = await _inventoryService.GetItemAsync(id.Value);
			
            if (inventoryItem == null)
            {
                return NotFound();
            }

            return View(inventoryItem);
        }

        // POST: Inventory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _inventoryService.DeleteItemAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
