using System;
using System.Collections.Generic;

namespace myApp.Models;

public partial class User
{
    public string? Fio { get; set; }

    public DateTime BirthDay { get; set; }

    public string? Gender { get; set; }

    public int Id { get; set; }
}
