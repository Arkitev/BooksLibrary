﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksLibrary.Data
{
    public interface IEntity
    {
        [Key]
        Guid Id { get; set; }
    }
}
