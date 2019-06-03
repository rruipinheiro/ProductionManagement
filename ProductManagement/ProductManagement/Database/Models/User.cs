using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Database.Models {
    public class User : IdentityUser {
        public int OperadorId { get; set; }
        public virtual Operador Operador { get; set; }
    }
}
