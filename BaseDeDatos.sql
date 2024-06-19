use COCTELES
go

CREATE TABLE USUARIOS
(
 idUser int identity (1,1) PRIMARY KEY
, Usuario varchar(50) NOT NULL
, Contrasenia varchar (20) NOT NULL
 ,Estatus bit not null default 1
, Fecha_Alta datetime default getdate()
, Fecha_Modificacion  datetime
);
go

CREATE TABLE COCTEL_FAVORITO
(
  IdCoctelFav int identity (1,1) PRIMARY KEY
  , idUser int NOT NULL
  ,idDrink int not null 
  , Fecha_Alta datetime default getdate()
   , Fecha_Modificacion  datetime,
   CONSTRAINT FK_USUARIOS_COCTEL_FAVORITO FOREIGN KEY (idUser) REFERENCES USUARIOS(idUser)
)
;
go
CREATE TABLE BITACORA
(
   idBitacora bigint identity (1,1) PRIMARY KEY,
   idUser int NOT NULL 
  ,Busqueda varchar(20) not null 
  , Fecha_Alta datetime default getdate()
)
;
go


insert into USUARIOS (Usuario, Contrasenia)
select 'Karla', '123'



INSERT INTO COCTEL_FAVORITO( idUser   ,idDrink )
SELECT 1, 11007

INSERT INTO COCTEL_FAVORITO( idUser   ,idDrink )
SELECT 1, 11118

INSERT INTO COCTEL_FAVORITO( idUser   ,idDrink )
SELECT 1, 17216

INSERT INTO COCTEL_FAVORITO( idUser   ,idDrink )
SELECT 1, 16158
INSERT INTO COCTEL_FAVORITO( idUser   ,idDrink )
SELECT 1,12322