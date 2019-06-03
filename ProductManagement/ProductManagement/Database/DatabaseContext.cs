using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProductManagement.Database {
    public class DatabaseContext : IdentityDbContext {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {
        }

    }
}
