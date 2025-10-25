using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using PraveenMatoria.Database;
using PraveenMatoria.Database.Services;

namespace PraveenMatoria.Pages.Portfolio
{

    public partial class TodoTask() : BasePage
    {
        [Inject]
        public TodoService TodoService { get; set; } = default!;

        private Todo Todo { get; set; } = new Todo(0, "");
        private IList<Todo> TodoList { get; set; } = [];
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            //Todo = new Todo(0, "");
            TodoList = await TodoService.GetAllAsync();
        }
        protected string GetTodoItemClass(Todo todoItem)
        {
            return "border-5 border-top-0 border-end-0 border-bottom-0 " +
                    (todoItem.IsPersonal ? "border-success" : "border-danger");
        }
        protected string GetTodoItemNameClass(Todo todoItem)
        {
            return (todoItem.IsComplete ? "text-decoration-line-through" : "");
        }
        protected async void ItemChangedAsync()
        {
            TodoList = await TodoService.GetAllAsync();
            Todo = new Todo(0, "");
            StateHasChanged();
        }
        protected void ItemEdit(Todo item)
        {
            Todo = item;
            StateHasChanged();
        }
        protected async void ItemDeletedAsync(Todo item)
        {
            await TodoService.DeleteAsync(item);
            ItemChangedAsync();
        }
        protected async void ToggleCompleteAsync(Todo item)
        {
            await TodoService.ToggleCompleteAsync(item);
            TodoList = await TodoService.GetAllAsync();
            StateHasChanged();
        }
    }
}