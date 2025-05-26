using InventariosModel.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventariosModel.Seeds
{
    internal class RoleSeed : IEntityTypeConfiguration<RolesModel>
    {
        public void Configure(EntityTypeBuilder<RolesModel> builder)
        {
            builder.HasData(
                new RolesModel { RoleId = 1, RoleName = "Administrador", RoleStatus = 1 },
                new RolesModel { RoleId = 2, RoleName = "Personal Administrativo", RoleStatus = 1 },
                new RolesModel { RoleId = 3, RoleName = "Ventas", RoleStatus = 1 });
        }
    }
}
