using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PraveenMatoria.Models;
using PraveenMatoria.Services;

namespace PraveenMatoria.Pages.Portfolio
{

    public partial class ToDo() : BasePage
    {
        [Inject]
        public ITodoService _todoService { get; set; }

        private TodoItem TodoItem { get; set; }
        private IList<TodoItem> TodoList { get; set; } = [];
        protected override void OnInitialized()
        {
            base.OnInitialized();

            TodoItem = new TodoItem(0, "");
            TodoList = _todoService.GetAll();
        }
        protected string GetTodoItemClass(TodoItem todoItem)
        {
            return "border-5 border-top-0 border-end-0 border-bottom-0 " +
                    (todoItem.IsPersonal ? "border-success" : "border-danger");
        }
        protected string GetTodoItemNameClass(TodoItem todoItem)
        {
            return (todoItem.IsComplete ? "text-decoration-line-through" : "");
        }
        protected void ItemChanged()
        {
            TodoList = _todoService.GetAll();
            TodoItem = new TodoItem(0, "");
            StateHasChanged();
        }
        protected void ItemEdit(TodoItem item)
        {
            TodoItem = item;
            StateHasChanged();
        }
        protected void ItemDeleted(TodoItem item)
        {
            _todoService.Delete(item);
            ItemChanged();
        }
        protected void ToggleComplete(TodoItem item)
        {
            _todoService.ToggleComplete(item);
            TodoList = _todoService.GetAll();
            StateHasChanged();
        }
    }
}