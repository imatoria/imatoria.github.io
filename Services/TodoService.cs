using PraveenMatoria.Models;

namespace PraveenMatoria.Services
{
    public class TodoService : ITodoService
    {
        private readonly IList<TodoItem> _todoItems;
        public TodoService()
        {
            _todoItems = new List<TodoItem>()
            {
                new(1, "Wash Clothes"),
                new(2, "Clean Desk", false)
            };
        }

        public List<TodoItem> GetAll()
        {
            return (List<TodoItem>)_todoItems;
        }

        public void Add(TodoItem item)
        {
            _todoItems.Add(item);
        }
        public void Update(TodoItem item)
        {
            var todoItem = _todoItems.FirstOrDefault(x => x.Id == item.Id);
            if (todoItem == null) return;

            todoItem.Name = item.Name;
            todoItem.IsPersonal = item.IsPersonal;
        }
        public void Delete(TodoItem item)
        {
            _todoItems.Remove(item);
        }
        public void ToggleComplete(TodoItem item)
        {
            item.IsComplete = !item.IsComplete;
        }
    }
}
