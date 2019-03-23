using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Chart_Leader.Models
{
    [MetadataType(typeof(Categories_Custom))]
    public partial class Categories
    {
    }
    public class Categories_Custom
    {
        public int Cat_id { get; set; }
        public string Cat_Name { get; set; }
        public string Cat_Description { get; set; }
        public string Cat_Image { get; set; }
    }
}