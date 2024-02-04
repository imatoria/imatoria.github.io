namespace PraveenMatoria.Models
{
    public class TodoItem(int id, string name, bool isPersonal = true, bool isComplete = false)
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public bool IsPersonal { get; set; } = isPersonal;
        public bool IsComplete { get; set; } = isComplete;
    }
}
