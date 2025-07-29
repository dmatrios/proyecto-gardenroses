USE dbgardenroses;
GO

-- Mostrar Clientes
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'SP_MostrarClientes')
DROP PROCEDURE SP_MostrarClientes;
GO
CREATE PROC SP_MostrarClientes
AS
BEGIN
    SELECT * FROM Cliente;
END
GO
EXEC SP_MostrarClientes;
GO

--Buscar Cliente por ID

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'SP_BuscarClientePorID')
DROP PROCEDURE SP_BuscarClientePorID;
GO
CREATE PROC SP_BuscarClientePorID
@id INT
AS
BEGIN
    SELECT * FROM Cliente WHERE idCliente = @id;
END
GO


--Registrar Cliente

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'SP_RegistrarCliente')
DROP PROCEDURE SP_RegistrarCliente;
GO
CREATE PROC SP_RegistrarCliente
@nombres VARCHAR(60),
@apellidos VARCHAR(60),
@direccion VARCHAR(100),
@telefono VARCHAR(15),
@correo VARCHAR(100)
AS
BEGIN
    BEGIN TRAN
    BEGIN TRY
        INSERT INTO Cliente(nombres, apellidos, direccion, telefono, correo)
        VALUES (@nombres, @apellidos, @direccion, @telefono, @correo);
        COMMIT TRAN
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN
    END CATCH
END
GO

--Actualizar Cliente
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'SP_ActualizarCliente')
DROP PROCEDURE SP_ActualizarCliente;
GO
CREATE PROC SP_ActualizarCliente
@id INT,
@nombres VARCHAR(60),
@apellidos VARCHAR(60),
@direccion VARCHAR(100),
@telefono VARCHAR(15),
@correo VARCHAR(100)
AS
BEGIN
    BEGIN TRAN
    BEGIN TRY
        UPDATE Cliente
        SET nombres = @nombres,
            apellidos = @apellidos,
            direccion = @direccion,
            telefono = @telefono,
            correo = @correo
        WHERE idCliente = @id;
        COMMIT TRAN
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN
    END CATCH
END
GO

--Eliminar Cliente
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'SP_EliminarCliente')
DROP PROCEDURE SP_EliminarCliente;
GO
CREATE PROC SP_EliminarCliente
@id INT
AS
BEGIN
    BEGIN TRAN
    BEGIN TRY
        DELETE FROM Cliente WHERE idCliente = @id;
        COMMIT TRAN
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN
    END CATCH
END
GO


--Mostrar Repartidores
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'SP_MostrarRepartidores')
DROP PROCEDURE SP_MostrarRepartidores;
GO
CREATE PROC SP_MostrarRepartidores
AS
BEGIN
    SELECT * FROM Repartidor;
END
GO

EXEC SP_MostrarRepartidores;
GO


--Buscar Repartidor por ID
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'SP_BuscarRepartidorPorID')
DROP PROCEDURE SP_BuscarRepartidorPorID;
GO
CREATE PROC SP_BuscarRepartidorPorID
@id INT
AS
BEGIN
    SELECT * FROM Repartidor WHERE idRepartidor = @id;
END
GO



-- Registrar Repartidor
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'SP_RegistrarRepartidor')
DROP PROCEDURE SP_RegistrarRepartidor;
GO
CREATE PROC SP_RegistrarRepartidor
@nombres VARCHAR(60),
@apellidos VARCHAR(60),
@dni CHAR(8),
@celular VARCHAR(15),
@licencia VARCHAR(20)
AS
BEGIN
    BEGIN TRAN
    BEGIN TRY
        INSERT INTO Repartidor(nombres, apellidos, dni, celular, licenciaConducir)
        VALUES (@nombres, @apellidos, @dni, @celular, @licencia);
        COMMIT TRAN
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN
    END CATCH
END
GO

--Actualizar Repartidor

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'SP_ActualizarRepartidor')
DROP PROCEDURE SP_ActualizarRepartidor;
GO
CREATE PROC SP_ActualizarRepartidor
@id INT,
@nombres VARCHAR(60),
@apellidos VARCHAR(60),
@dni CHAR(8),
@celular VARCHAR(15),
@licencia VARCHAR(20)
AS
BEGIN
    BEGIN TRAN
    BEGIN TRY
        UPDATE Repartidor
        SET nombres = @nombres,
            apellidos = @apellidos,
            dni = @dni,
            celular = @celular,
            licenciaConducir = @licencia
        WHERE idRepartidor = @id;
        COMMIT TRAN
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN
    END CATCH
END
GO


--Eliminar Repartidor
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'SP_EliminarRepartidor')
DROP PROCEDURE SP_EliminarRepartidor;
GO
CREATE PROC SP_EliminarRepartidor
@id INT
AS
BEGIN
    BEGIN TRAN
    BEGIN TRY
        DELETE FROM Repartidor WHERE idRepartidor = @id;
        COMMIT TRAN
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN
    END CATCH
END
GO

USE dbgardenroses;
GO

-- 1. Mostrar todos los productos
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'SP_MostrarProductos')
DROP PROCEDURE SP_MostrarProductos;
GO
CREATE PROC SP_MostrarProductos
AS
BEGIN
    SELECT * FROM Producto;
END
GO

-- 2. Buscar producto por ID
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'SP_BuscarProductoPorID')
DROP PROCEDURE SP_BuscarProductoPorID;
GO
CREATE PROC SP_BuscarProductoPorID
@id INT
AS
BEGIN
    SELECT * FROM Producto WHERE idProducto = @id;
END
GO

-- 3. Registrar nuevo producto
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'SP_RegistrarProducto')
DROP PROCEDURE SP_RegistrarProducto;
GO
CREATE PROC SP_RegistrarProducto
@nombre VARCHAR(100),
@descripcion TEXT,
@precio DECIMAL(10,2),
@stock INT,
@imagen VARCHAR(150)
AS
BEGIN
    BEGIN TRAN
    BEGIN TRY
        INSERT INTO Producto (nombre, descripcion, precio, stock, imagen)
        VALUES (@nombre, @descripcion, @precio, @stock, @imagen);
        COMMIT TRAN
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN
    END CATCH
END
GO

-- 4. Actualizar producto
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'SP_ActualizarProducto')
DROP PROCEDURE SP_ActualizarProducto;
GO
CREATE PROC SP_ActualizarProducto
@id INT,
@nombre VARCHAR(100),
@descripcion TEXT,
@precio DECIMAL(10,2),
@stock INT,
@imagen VARCHAR(150)
AS
BEGIN
    BEGIN TRAN
    BEGIN TRY
        UPDATE Producto
        SET nombre = @nombre,
            descripcion = @descripcion,
            precio = @precio,
            stock = @stock,
            imagen = @imagen
        WHERE idProducto = @id;
        COMMIT TRAN
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN
    END CATCH
END
GO

-- 5. Eliminar producto
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'SP_EliminarProducto')
DROP PROCEDURE SP_EliminarProducto;
GO
CREATE PROC SP_EliminarProducto
@id INT
AS
BEGIN
    BEGIN TRAN
    BEGIN TRY
        DELETE FROM Producto WHERE idProducto = @id;
        COMMIT TRAN
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN
    END CATCH
END
GO

-- 1. Mostrar todos los detalles de pedidos
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'SP_MostrarDetallePedido')
DROP PROCEDURE SP_MostrarDetallePedido;
GO
CREATE PROC SP_MostrarDetallePedido
AS
BEGIN
    SELECT 
        dp.idDetallePedido,
        dp.idPedido,
        dp.idProducto,
        p.nombre AS nombreProducto,
        dp.cantidad,
        dp.precioUnitario,
        dp.subtotal
    FROM DetallePedido dp
    INNER JOIN Producto p ON dp.idProducto = p.idProducto;
END
GO

-- 2. Buscar detalle por ID
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'SP_BuscarDetallePedidoPorID')
DROP PROCEDURE SP_BuscarDetallePedidoPorID;
GO
CREATE PROC SP_BuscarDetallePedidoPorID
@idDetallePedido INT
AS
BEGIN
    SELECT 
        dp.idDetallePedido,
        dp.idPedido,
        dp.idProducto,
        p.nombre AS nombreProducto,
        dp.cantidad,
        dp.precioUnitario,
        dp.subtotal
    FROM DetallePedido dp
    INNER JOIN Producto p ON dp.idProducto = p.idProducto
    WHERE dp.idDetallePedido = @idDetallePedido;
END
GO

-- 3. Registrar nuevo detalle de pedido
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'SP_RegistrarDetallePedido')
DROP PROCEDURE SP_RegistrarDetallePedido;
GO
CREATE PROC SP_RegistrarDetallePedido
@idPedido INT,
@idProducto INT,
@cantidad INT,
@precioUnitario DECIMAL(10,2)
AS
BEGIN
    BEGIN TRAN
    BEGIN TRY
        INSERT INTO DetallePedido (idPedido, idProducto, cantidad, precioUnitario)
        VALUES (@idPedido, @idProducto, @cantidad, @precioUnitario);
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO

-- 4. Actualizar detalle de pedido
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'SP_ActualizarDetallePedido')
DROP PROCEDURE SP_ActualizarDetallePedido;
GO
CREATE PROC SP_ActualizarDetallePedido
@idDetallePedido INT,
@idPedido INT,
@idProducto INT,
@cantidad INT,
@precioUnitario DECIMAL(10,2)
AS
BEGIN
    BEGIN TRAN
    BEGIN TRY
        UPDATE DetallePedido
        SET idPedido = @idPedido,
            idProducto = @idProducto,
            cantidad = @cantidad,
            precioUnitario = @precioUnitario
        WHERE idDetallePedido = @idDetallePedido;
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO

-- 5. Eliminar detalle de pedido
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'SP_EliminarDetallePedido')
DROP PROCEDURE SP_EliminarDetallePedido;
GO
CREATE PROC SP_EliminarDetallePedido
@idDetallePedido INT
AS
BEGIN
    DELETE FROM DetallePedido WHERE idDetallePedido = @idDetallePedido;
END
GO


--Mostrar todos los pedidos
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'SP_MostrarPedidos')
DROP PROCEDURE SP_MostrarPedidos;
GO
CREATE PROC SP_MostrarPedidos
AS
BEGIN
    SELECT 
        p.idPedido,
        p.idCliente,
        c.nombres + ' ' + c.apellidos AS nombreCliente,
        p.fechaPedido,
        p.fechaEntrega,
        p.direccionEntrega,
        p.estado,
        p.idRepartidor,
        r.nombres + ' ' + r.apellidos AS nombreRepartidor,
        ISNULL(SUM(dp.subtotal), 0) AS montoTotal
    FROM Pedido p
    INNER JOIN Cliente c ON p.idCliente = c.idCliente
    LEFT JOIN Repartidor r ON p.idRepartidor = r.idRepartidor
    LEFT JOIN DetallePedido dp ON p.idPedido = dp.idPedido
    GROUP BY 
        p.idPedido, p.idCliente, c.nombres, c.apellidos, 
        p.fechaPedido, p.fechaEntrega, p.direccionEntrega,
        p.estado, p.idRepartidor, r.nombres, r.apellidos;
END
GO

--Buscar pedido por ID
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'SP_BuscarPedidoPorID')
DROP PROCEDURE SP_BuscarPedidoPorID;
GO
CREATE PROC SP_BuscarPedidoPorID
@idPedido INT
AS
BEGIN
    SELECT 
        p.idPedido,
        p.idCliente,
        c.nombres + ' ' + c.apellidos AS nombreCliente,
        p.fechaPedido,
        p.fechaEntrega,
        p.direccionEntrega,
        p.estado,
        p.idRepartidor,
        r.nombres + ' ' + r.apellidos AS nombreRepartidor,
        ISNULL(SUM(dp.subtotal), 0) AS montoTotal
    FROM Pedido p
    INNER JOIN Cliente c ON p.idCliente = c.idCliente
    LEFT JOIN Repartidor r ON p.idRepartidor = r.idRepartidor
    LEFT JOIN DetallePedido dp ON p.idPedido = dp.idPedido
    WHERE p.idPedido = @idPedido
    GROUP BY 
        p.idPedido, p.idCliente, c.nombres, c.apellidos, 
        p.fechaPedido, p.fechaEntrega, p.direccionEntrega,
        p.estado, p.idRepartidor, r.nombres, r.apellidos;
END
GO

-- Registrar nuevo pedido
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'SP_RegistrarPedido')
DROP PROCEDURE SP_RegistrarPedido;
GO
CREATE PROC SP_RegistrarPedido
@idCliente INT,
@fechaPedido DATETIME,
@fechaEntrega DATETIME,
@direccionEntrega VARCHAR(200),
@estado VARCHAR(20),
@idRepartidor INT
AS
BEGIN
    BEGIN TRAN
    BEGIN TRY
        INSERT INTO Pedido (idCliente, fechaPedido, fechaEntrega, direccionEntrega, estado, idRepartidor)
        VALUES (@idCliente, @fechaPedido, @fechaEntrega, @direccionEntrega, @estado, @idRepartidor);
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
    END CATCH
END
GO
--Actualizar pedido
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'SP_ActualizarPedido')
DROP PROCEDURE SP_ActualizarPedido;
GO
CREATE PROC SP_ActualizarPedido
@idPedido INT,
@idCliente INT,
@fechaPedido DATETIME,
@fechaEntrega DATETIME,
@direccionEntrega VARCHAR(200),
@estado VARCHAR(20),
@idRepartidor INT
AS
BEGIN
    BEGIN TRAN
    BEGIN TRY
        UPDATE Pedido
        SET idCliente = @idCliente,
            fechaPedido = @fechaPedido,
            fechaEntrega = @fechaEntrega,
            direccionEntrega = @direccionEntrega,
            estado = @estado,
            idRepartidor = @idRepartidor
        WHERE idPedido = @idPedido;
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
    END CATCH
END
GO

--Eliminar pedido
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'SP_EliminarPedido')
DROP PROCEDURE SP_EliminarPedido;
GO
CREATE PROC SP_EliminarPedido
@idPedido INT
AS
BEGIN
    BEGIN TRAN
    BEGIN TRY
        DELETE FROM Pedido WHERE idPedido = @idPedido;
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
    END CATCH
END
GO



-- Mostrar datos de Repartidor
PRINT 'Tabla: Repartidor'
SELECT * FROM Repartidor;
PRINT '--------------------------------------'

-- Mostrar datos de Producto
PRINT 'Tabla: Producto'
SELECT * FROM Producto;
PRINT '--------------------------------------'

-- Mostrar datos de Cliente
PRINT 'Tabla: Cliente'
SELECT * FROM Cliente;
PRINT '--------------------------------------'

-- Mostrar datos de Pedido
PRINT 'Tabla: Pedido'
SELECT * FROM Pedido;
PRINT '--------------------------------------'

-- Mostrar datos de DetallePedido
PRINT 'Tabla: DetallePedido'
SELECT * FROM DetallePedido;
PRINT '--------------------------------------'

GO
ALTER PROCEDURE SP_RegistrarProducto
    @nombre NVARCHAR(100),
    @descripcion NVARCHAR(500),
    @precio DECIMAL(10,2),
    @stock INT
AS
BEGIN
    INSERT INTO Producto(nombre, descripcion, precio, stock)
    VALUES (@nombre, @descripcion, @precio, @stock)
END
GO
GO
ALTER PROCEDURE SP_ActualizarProducto
    @id INT,
    @nombre NVARCHAR(100),
    @descripcion NVARCHAR(500),
    @precio DECIMAL(10,2),
    @stock INT
AS
BEGIN
    UPDATE Producto
    SET nombre = @nombre,
        descripcion = @descripcion,
        precio = @precio,
        stock = @stock
    WHERE idProducto = @id
END
GO

