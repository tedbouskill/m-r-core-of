using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Application.Interfaces;

using DomainCore;

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

            return View(new Tuple<InventoryItemDto,IEnumerable<DomainCore.InventoryItemEvent>>(inventoryItem, inventoryItemEvents));
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
            return View();
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

            return View(inventoryItem);
        }

        // POST: Inventory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,IsActive,Count,Note")] InventoryItemDto inventoryItem)
        {
            if (id != inventoryItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _inventoryService.PutItemAsync(inventoryItem);

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
