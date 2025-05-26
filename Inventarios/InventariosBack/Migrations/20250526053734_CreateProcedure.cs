using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventariosModel.Migrations
{
    /// <inheritdoc />
    public partial class CreateProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE InsertUser
                @Name NVARCHAR(100),
                @LastName NVARCHAR(100),
                @Email NVARCHAR(255),
                @Password NVARCHAR(255),
                @RoleId INT
            AS
            BEGIN
                SET NOCOUNT ON;

                BEGIN TRY
                    -- Verificar si el email ya está registrado
                    IF EXISTS (SELECT 1 FROM users WHERE Email = @Email)
                    BEGIN
                        SELECT 'Email ya registrado' AS Resultado;
                        RETURN;
                    END

                    INSERT INTO users (Name, LastName, Email, Password, RoleId)
                    VALUES (
                        @Name,
                        @LastName,
                        @Email,
                        @Password,
                        @RoleId
                    );

                    -- Obtener el ID recién insertado
                    DECLARE @NewUserId INT = SCOPE_IDENTITY();

                    -- Devolver los datos del nuevo usuario junto al nombre del rol
                    SELECT 
                        u.UserId,
                        u.Name,
                        u.LastName,
                        u.Email,
                        r.RoleName,
                        'Usuario insertado correctamente' AS Resultado
                    FROM users u
                    INNER JOIN roles r ON u.RoleId = r.RoleId
                    WHERE u.UserId = @NewUserId;

                END TRY
                BEGIN CATCH
                    SELECT 
                        ERROR_MESSAGE() AS Error,
                        'Ocurrió un error al insertar el usuario' AS Resultado;
                END CATCH
            END;
            ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE InsertUser");
        }
    }
}
