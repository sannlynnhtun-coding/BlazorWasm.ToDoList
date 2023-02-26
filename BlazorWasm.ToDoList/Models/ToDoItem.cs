namespace BlazorWasm.ToDoList.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
    }
}
