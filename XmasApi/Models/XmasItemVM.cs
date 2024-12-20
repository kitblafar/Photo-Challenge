﻿using System.ComponentModel.DataAnnotations.Schema;

namespace XmasAPI.Models
{
    public class XmasItemVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Challenge { get; set; }
        public IFormFile File { get; set; }
    }
}
