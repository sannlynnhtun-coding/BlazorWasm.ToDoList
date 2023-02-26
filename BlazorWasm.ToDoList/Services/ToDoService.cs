using BlazorWasm.ToDoList.Models;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorWasm.ToDoList.Services
{
    public class ToDoService
    {
        private const string STORAGE_KEY = "todo-items";
        private readonly IJSRuntime jsRuntime;

        public ToDoService(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public async Task<List<ToDoItem>> GetAllAsync()
        {
            var json = await jsRuntime.InvokeAsync<string>("localStorage.getItem", STORAGE_KEY);
            if (json == null)
                return new List<ToDoItem>();

            var items = JsonSerializer.Deserialize<List<ToDoItem>>(json);
            return items;
        }

        public async Task<ToDoItem> GetByIdAsync(int id)
        {
            var items = await GetAllAsync();
            return items.FirstOrDefault(i => i.Id == id);
        }

        public async Task AddAsync(ToDoItem item)
        {
            var items = await GetAllAsync();
            item.Id = items.Any() ? items.Max(i => i.Id) + 1 : 1;
            item.CreatedDateTime = DateTime.Now;
            item.ModifiedDateTime = DateTime.Now;
            items.Add(item);
            await SaveAllAsync(items);
        }

        public async Task UpdateAsync(ToDoItem item)
        {
            var items = await GetAllAsync();
            var existingItem = items.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem != null)
            {
                existingItem.TaskName = item.TaskName;
                existingItem.ModifiedDateTime = DateTime.Now;
                await SaveAllAsync(items);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var items = await GetAllAsync();
            var existingItem = items.FirstOrDefault(i => i.Id == id);
            if (existingItem != null)
            {
                items.Remove(existingItem);
                await SaveAllAsync(items);
            }
        }

        private async Task SaveAllAsync(List<ToDoItem> items)
        {
            var json = JsonSerializer.Serialize(items);
            await jsRuntime.InvokeVoidAsync("localStorage.setItem", STORAGE_KEY, json);
        }
    }
}
