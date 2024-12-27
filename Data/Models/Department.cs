namespace session3.Data.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
       public ICollection <Employee> Employees { get; set;}
    }
}
