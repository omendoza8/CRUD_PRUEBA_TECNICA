CREATE TABLE Libros (
    LibroID INT PRIMARY KEY AUTO_INCREMENT,
    Titulo VARCHAR(255) NOT NULL,
    Autor VARCHAR(255) NOT NULL,
    ISBN VARCHAR(13) UNIQUE NOT NULL,
    FechaPublicacion DATE,
    Genero VARCHAR(100),
    Disponibilidad TINYINT(1) NOT NULL
);

CREATE TABLE Miembros (
    MiembroID INT PRIMARY KEY AUTO_INCREMENT,
    Nombre VARCHAR(255) NOT NULL,
    Direccion VARCHAR(255),
    Telefono VARCHAR(20),
    Email VARCHAR(100) UNIQUE NOT NULL,
    FechaRegistro DATE NOT NULL
);

CREATE TABLE Prestamos (
    PrestamoID INT PRIMARY KEY AUTO_INCREMENT,
    MiembroID INT NOT NULL,
    LibroID INT NOT NULL,
    FechaPrestamo DATE NOT NULL,
    FechaDevolucion DATE,
    Devuelto TINYINT(1) NOT NULL,
    FOREIGN KEY (MiembroID) REFERENCES Miembros(MiembroID),
    FOREIGN KEY (LibroID) REFERENCES Libros(LibroID)
);

INSERT INTO Libros (Titulo, Autor, ISBN, FechaPublicacion, Genero, Disponibilidad) VALUES
('Cien años de soledad', 'Gabriel García Márquez', '9780060883287', '1967-06-05', 'Realismo mágico', 1),
('Don Quijote de la Mancha', 'Miguel de Cervantes', '9788491051040', '1605-01-16', 'Novela', 1),
('La sombra del viento', 'Carlos Ruiz Zafón', '9788408260157', '2001-04-17', 'Suspense', 0),
('El nombre del viento', 'Patrick Rothfuss', '9788408139287', '2007-03-27', 'Fantasía', 1),
('Orgullo y prejuicio', 'Jane Austen', '9780141040349', '1813-01-28', 'Romántico', 1);


INSERT INTO Miembros (Nombre, Direccion, Telefono, Email, FechaRegistro) VALUES
('Juan Pérez', 'Calle Falsa 123', '555-1234', 'juan.perez@gmail.com', '2024-01-15'),
('Ana Gómez', 'Avenida Siempre Viva 742', '555-5678', 'ana.gomez@hotmail.com', '2024-02-20'),
('Luis Martínez', 'Calle de los Olivos 9', '555-8765', 'luis.martinez@outlook.com', '2024-03-10'),
('María Fernández', 'Plaza Mayor 1', '555-4321', 'maria.fernandez@gmail.com', '2024-04-05'),
('Carlos López', 'Calle del Sol 56', '555-3456', 'carlos.lopez@hotmail.com', '2024-05-12');


INSERT INTO Prestamos (MiembroID, LibroID, FechaPrestamo, FechaDevolucion, Devuelto) VALUES
(1, 1, '2024-06-01', NULL, 0),
(2, 3, '2024-06-10', '2024-06-24', 1),
(3, 4, '2024-07-01', NULL, 0),
(4, 2, '2024-07-15', '2024-08-15', 1),
(5, 5, '2024-08-01', NULL, 0);
