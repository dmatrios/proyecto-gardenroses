-- 1. Eliminar la base de datos si ya existe
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'dbgardenroses')
BEGIN
    ALTER DATABASE dbgardenroses SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE dbgardenroses;
END
GO

-- 2. Crear la base de datos
CREATE DATABASE dbgardenroses;
GO

-- 3. Usar la base de datos
USE dbgardenroses;
GO

-- 4. Crear tablas

CREATE TABLE Cliente (
    idCliente INT IDENTITY PRIMARY KEY,
    nombres VARCHAR(60) NOT NULL,
    apellidos VARCHAR(60) NOT NULL,
    direccion VARCHAR(100) NOT NULL,
    telefono VARCHAR(15) NOT NULL,
    correo VARCHAR(100)
);
GO

CREATE TABLE Repartidor (
    idRepartidor INT IDENTITY PRIMARY KEY,
    nombres VARCHAR(60) NOT NULL,
    apellidos VARCHAR(60) NOT NULL,
    dni CHAR(8) NOT NULL,
    celular VARCHAR(15),
    licenciaConducir VARCHAR(20)
);
GO

CREATE TABLE Producto (
    idProducto INT IDENTITY PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    descripcion TEXT,
    precio DECIMAL(10,2) NOT NULL,
    stock INT NOT NULL,
    imagen VARCHAR(150)
);
GO

CREATE TABLE Pedido (
    idPedido INT IDENTITY PRIMARY KEY,
    idCliente INT NOT NULL,
    fechaPedido DATE NOT NULL DEFAULT GETDATE(),
    fechaEntrega DATE,
    direccionEntrega VARCHAR(100),
    estado VARCHAR(20) DEFAULT 'Pendiente',
    idRepartidor INT,
    FOREIGN KEY (idCliente) REFERENCES Cliente(idCliente),
    FOREIGN KEY (idRepartidor) REFERENCES Repartidor(idRepartidor)
);
GO

CREATE TABLE DetallePedido (
    idDetallePedido INT IDENTITY PRIMARY KEY,
    idPedido INT NOT NULL,
    idProducto INT NOT NULL,
    cantidad INT NOT NULL,
    precioUnitario DECIMAL(10,2) NOT NULL,
    subtotal AS (cantidad * precioUnitario) PERSISTED,
    FOREIGN KEY (idPedido) REFERENCES Pedido(idPedido),
    FOREIGN KEY (idProducto) REFERENCES Producto(idProducto)
);
GO

-- 5. Insertar datos de ejemplo

-- Clientes
INSERT INTO Cliente (nombres, apellidos, direccion, telefono, correo)
VALUES 
('Lucía', 'Vargas Ríos', 'Av. Primavera 456, Surco', '987654321', 'lucia.vargas@gmail.com'),
('Pedro', 'Ramírez Soto', 'Jr. Las Camelias 123, San Borja', '912345678', 'pedro.ramirez@yahoo.com'),
('Sandra', 'Mendoza León', 'Calle Los Tulipanes 99, Miraflores', '998877665', 'sandra.mendoza@hotmail.com');

-- Repartidores
INSERT INTO Repartidor (nombres, apellidos, dni, celular, licenciaConducir)
VALUES
('Carlos', 'Pérez Torres', '45678912', '986512347', 'B12345678'),
('Ana', 'Gonzales Ruiz', '78912345', '955123789', 'C98765432');

-- Productos (arreglos florales)
INSERT INTO Producto (nombre, descripcion, precio, stock, imagen)
VALUES
('Ramo Rosas Rojas', '12 rosas rojas premium con envoltura elegante', 75.00, 20, 'rosas_rojas.jpg'),
('Caja Floral Deluxe', 'Caja cuadrada con flores mixtas y tarjeta personalizada', 120.00, 10, 'caja_deluxe.jpg'),
('Bouquet de Girasoles', 'Arreglo de 8 girasoles y eucalipto decorativo', 65.00, 15, 'girasoles.jpg'),
('Arreglo Tropical', 'Flores tropicales variadas con base de vidrio', 90.00, 12, 'tropical.jpg');

-- Pedidos
INSERT INTO Pedido (idCliente, fechaPedido, fechaEntrega, direccionEntrega, estado, idRepartidor)
VALUES
(1, '2025-07-20', '2025-07-22', 'Av. Benavides 1500, Miraflores', 'Entregado', 1),
(2, '2025-07-21', '2025-07-23', 'Jr. Los Olivos 876, Surquillo', 'Pendiente', 2);

-- Detalle de pedidos
INSERT INTO DetallePedido (idPedido, idProducto, cantidad, precioUnitario)
VALUES
(1, 1, 1, 75.00),
(1, 2, 1, 120.00),
(2, 3, 2, 65.00);

-- 6. Consultas finales para mostrar los datos

PRINT '✅ TABLA CLIENTE';
SELECT * FROM Cliente;

PRINT '✅ TABLA REPARTIDOR';
SELECT * FROM Repartidor;

PRINT '✅ TABLA PRODUCTO';
SELECT * FROM Producto;

PRINT '✅ TABLA PEDIDO';
SELECT * FROM Pedido;

PRINT '✅ TABLA DETALLEPEDIDO';
SELECT * FROM DetallePedido;
