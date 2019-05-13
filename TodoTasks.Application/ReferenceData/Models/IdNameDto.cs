using System.Text.RegularExpressions;

namespace CleanTodoTasks.Application.ReferenceData.Models
{
    public class IdNameDto
    {
        public int Id { get; set; }
        private string _name;

        public string Name {
            get { return _name; }
            set {
                if(!string.IsNullOrEmpty(value))
                {
                    var splitString = Regex.Split(value, @"(?<!^)(?=[A-Z])");
                    _name = string.Join(' ', splitString);
                }
                else
                {
                    _name = value;
                }               
            }
        }
    }
}
