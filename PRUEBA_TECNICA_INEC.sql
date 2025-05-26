USE PruebaPractica
GO

CREATE TABLE Empleados (
	Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100),
    Cargo NVARCHAR(100),
    Email NVARCHAR(100)
)

CREATE PROCEDURE sp_ObtenerEmpleados
AS
BEGIN
	SELECT 
		* 
	FROM 
		Empleados;
END;

CREATE PROCEDURE sp_ObtenerEmpleadoPorId
@Id INT
AS
BEGIN
	SELECT 
		* 
	FROM 
		Empleados 
	WHERE 
		Id= @Id
END;

CREATE PROCEDURE sp_AgregarEmpleados
@Nombre NVARCHAR(100),
@Cargo NVARCHAR(100),
@Email NVARCHAR(100)
AS
BEGIN
	INSERT INTO Empleados
	(
		 Nombre,
		 Cargo,
		 Email
	 )
	VALUES
	(
		@Nombre,
		@Cargo,
		@Email
	)
END;



CREATE PROCEDURE sp_ActualizarEmpleados
@Id INT,
@Nombre NVARCHAR(100),
@Cargo NVARCHAR(100),
@Email NVARCHAR(100)
AS
BEGIN
	UPDATE 
		Empleados
	SET 
		Nombre = @Nombre, 
		Cargo= @Cargo, 
		Email= @Email
	WHERE
		Id = @Id
END;


CREATE PROCEDURE sp_EliminarEmpleado
@Id INT
AS
BEGIN
	DELETE FROM 
		Empleados
	WHERE 
		Id = @Id
END;

---contrasena api: PruebaLoginApi

---Login:

CREATE TABLE Usuarios (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Usuario NVARCHAR(100) NOT NULL,
    Password NVARCHAR(100) NOT NULL 
);

CREATE PROCEDURE ValidarUsuario
    @Usuario NVARCHAR(100),
    @Password NVARCHAR(100)
AS
BEGIN
    SELECT Id, Usuario
    FROM Usuarios
    WHERE Usuario = @Usuario AND Password = @Password
END


INSERT INTO Usuarios
(
  Usuario,
  Password
)
VALUES
(
'admin',
'PruebaLoginApi'
)