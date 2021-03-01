using System;
using System.Collections.Generic;

#nullable disable

namespace CapStone4_ToDoProjects.Models
{
    public partial class Project
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string ProjectDesc { get; set; }
        public DateTime? DueDate { get; set; }
        public bool? IsComplete { get; set; }
    }
}
