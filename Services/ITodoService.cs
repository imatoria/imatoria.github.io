using PraveenMatoria.Models;

namespace PraveenMatoria.Services
{
    public interface ITodoService
    {
        public List<TodoItem> GetAll();
        public void Add(TodoItem item);
        public void Update(TodoItem item);
        public void Delete(TodoItem item);
        public void ToggleComplete(TodoItem item);
    }
}
