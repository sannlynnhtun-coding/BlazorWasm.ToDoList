using BlazorWasm.ToDoList.Models;

namespace BlazorWasm.ToDoList.Pages
{
    public partial class Index
    {
        private string title = "To Do List";
        private List<ToDoItem> items = new List<ToDoItem>();
        private ToDoItem newItem = new ToDoItem();

        protected override async Task OnInitializedAsync()
        {
            items = await ToDoService.GetAllAsync();
        }

        private async Task AddItem()
        {
            await ToDoService.AddAsync(newItem);
            newItem = new ToDoItem();
            items = await ToDoService.GetAllAsync();
        }

        private async Task EditItem(int id)
        {
            newItem = await ToDoService.GetByIdAsync(id);
        }

        private async Task UpdateItem()
        {
            await ToDoService.UpdateAsync(newItem);
            newItem = new ToDoItem();
            items = await ToDoService.GetAllAsync();
        }

        private async Task DeleteItem(int id)
        {
            await ToDoService.DeleteAsync(id);
            items = await ToDoService.GetAllAsync();
        }
    }
}
