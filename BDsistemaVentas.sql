create proc mostrar_cliente
as
select * from cliente order by idcliente desc
go

/*procedimientos*/

---seccion cliente
create proc insertar_cliente
@nombre varchar(50),
@apellidos varchar(50),
@direccion varchar (100),
@telefono varchar(12),
@dni varchar(11)
as
insert into cliente(nombre,apellidos,direccion,telefono,dni) 
values (@nombre,@apellidos,@direccion,@telefono,@dni)
go

drop proc insertar_cliente

create proc editar_cliente
@idcliente integer,
@nombre varchar(50),
@apellidos varchar(50),
@direccion varchar(100),
@telefono varchar(12),
@dni varchar(11)
as
update cliente set nombre=@nombre,apellidos=@apellidos,direccion=@direccion,telefono=@telefono,dni=@dni
where idcliente=@idcliente
go

create proc eliminar_cliente
@idcliente integer
as
delete from cliente where idcliente=@idcliente
go
----fin deccion cliente
-------------seccion categoria
create proc mostrar_categoria
as
select*from categoria order by idcategoria desc
go

create proc insertar_categoria
@nombre_categoria varchar(50)
as
insert into categoria(nombre_categoria) values (@nombre_categoria)
go

create proc editar_categoria
@idcategoria integer,
@nombre_categoria varchar(50)
as
update categoria set nombre_categoria=@nombre_categoria
where idcategoria=@idcategoria
go

create proc eliminar_categoria
@idcategoria integer
as 
delete from categoria where idcategoria=@idcategoria
go

-----------fin seccion categoria
---CATALOGO DE PRODUCTOS  add imagen
alter table producto
add imagen image

--proc
drop proc modificar_producto
create proc mostrar_producto
as
select producto.idproducto,producto.idcategoria,categoria.nombre_categoria,producto.nombre,producto.descripcion,producto.stock,producto.precio_compra,producto.precio_venta,producto.fecha_vencimiento,producto.imagen
from producto inner join categoria on producto.idcategoria=categoria.idcategoria
order by producto.idproducto desc

create proc insertar_producto
@idcategoria integer,
@nombre varchar(50),
@descripcion varchar(150),
@stock int ,
@precio_compra int,
@precio_venta int,
@fecha_vencimiento date,
@imagen image
as
insert into producto (idcategoria,nombre,descripcion,stock,precio_compra,precio_venta,fecha_vencimiento,imagen)
values (@idcategoria,@nombre,@descripcion,@stock,@precio_compra,@precio_venta,@fecha_vencimiento,@imagen)
go

create proc editar_producto
@idproducto integer,
@idcategoria integer,
@nombre varchar(50),
@descripcion varchar(150),
@stock int ,
@precio_compra int,
@precio_venta int,
@fecha_vencimiento date,
@imagen image
as
update producto set idcategoria=@idcategoria,nombre=@nombre,descripcion=@descripcion,
stock=@stock,precio_compra=@precio_compra,precio_venta=@precio_venta,fecha_vencimiento=@fecha_vencimiento,imagen=@imagen
where idproducto=@idproducto
go

create proc eliminar_producto
@idproducto integer
as
delete from producto where idproducto=@idproducto
go

----fin producto

--venta---
create proc insertar_venta
@idcliente as integer,
@fecha_venta as date,
@tipo_documento as varchar(50),
@num_documento as varchar(50)
as
insert into ventas (idcliente,fecha_venta,tipo_documento,num_documento)
values (@idcliente,@fecha_venta,@tipo_documento,@num_documento)
go

create proc editar_venta
@idventa as integer,
@idcliente as integer,
@fecha_venta as date,
@tipo_documento as varchar(50),
@num_documento as varchar(50)
as 
update ventas set idcliente=@idcliente,fecha_venta=@fecha_venta,tipo_documento=@tipo_documento,num_documento=@num_documento
where idventa=@idventa
go


create proc  eliminar_venta
@idventa as integer
as
delete from ventas where idventa=@idventa
go

create proc mostrar_venta
as
SELECT        dbo.ventas.idventa, dbo.ventas.idcliente, dbo.cliente.apellidos, dbo.cliente.dni, dbo.ventas.fecha_venta, dbo.ventas.tipo_documento, dbo.ventas.num_documento
FROM            dbo.cliente INNER JOIN
                         dbo.ventas ON dbo.cliente.idcliente = dbo.ventas.idcliente
go
--fin venta--

--inicio detalle de venta--

create proc insertar_detalle_venta
@idventa as integer,
@idproducto as integer,
@cantidad as int,/*aqui lo coloque decimal porque se puede trbajr con kilogramos*/
@precio_unitario int
as
insert into detalle_venta (idventa,idproducto,cantidad,precio_unitario)
values(@idventa,@idproducto,@cantidad,@precio_unitario)
go

create proc editar_detalle_venta
@iddetalle_venta as integer,
@idventa as integer,
@idproducto as integer,
@cantidad as int,/*aqui lo coloque decimal porque se puede trbajr con kilogramos*/
@precio_unitario int
as
update detalle_venta set idventa=@idventa,idproducto=@idproducto,cantidad=@cantidad,precio_unitario=@precio_unitario
where iddetalle_venta=@iddetalle_venta
go

create proc eliminar_detalle_venta
@iddetalle_venta as integer
as

delete from detalle_venta where iddetalle_venta=@iddetalle_venta
go

create proc mostrar_detalle_venta
as
select*from detalle_venta order by iddetalle_venta desc
go

/*aumentar y disminuitr stock*/
create proc aumentar_stock
@idproducto as integer,
@cantidad as int
as update producto set stock=stock+@cantidad where idproducto=@idproducto
go
/*disminur stock*/
create proc disminuir_stock
@idproducto as integer,
@cantidad as int
as update producto set stock=stock-@cantidad where idproducto=@idproducto
go


/*RESPALDO DE MODIFICADION DE PRCEIMIENTOS*/
ALTER proc [dbo].[mostrar_detalle_venta]
as
SELECT        dbo.detalle_venta.iddetalle_venta, dbo.detalle_venta.idventa, dbo.detalle_venta.idproducto, dbo.producto.nombre, dbo.detalle_venta.cantidad, dbo.detalle_venta.precio_unitario
FROM            dbo.detalle_venta INNER JOIN
                         dbo.producto ON dbo.detalle_venta.idproducto = dbo.producto.idproducto
						 order by dbo.detalle_venta.iddetalle_venta desc
						 /*----RESPALDO DE EDICCION ----*/
						 ALTER proc [dbo].[mostrar_venta]
as
SELECT        dbo.ventas.idventa, dbo.ventas.idcliente, dbo.cliente.apellidos, dbo.cliente.dni, dbo.ventas.fecha_venta, dbo.ventas.tipo_documento, dbo.ventas.num_documento
FROM            dbo.cliente INNER JOIN
                         dbo.ventas ON dbo.cliente.idcliente = dbo.ventas.idcliente
						 order by dbo.ventas.idventa desc
/*edite order by */


/*respaldo mostrsr producto editado*/
USE [sisventas]
GO
/****** Object:  StoredProcedure [dbo].[mostrar_producto]    Script Date: 26-06-2023 17:51:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[mostrar_producto]
as
select producto.idproducto,producto.idcategoria,categoria.nombre_categoria,producto.nombre,producto.descripcion,producto.stock,producto.precio_compra,producto.precio_venta,producto.fecha_vencimiento,producto.imagen
from producto inner join categoria on producto.idcategoria=categoria.idcategoria
order by producto.idproducto desc

create proc generar_comprobante
@idventa int
as
SELECT        dbo.ventas.idventa, dbo.cliente.nombre, dbo.cliente.apellidos, dbo.cliente.dni, dbo.ventas.fecha_venta, dbo.ventas.tipo_documento, dbo.ventas.num_documento, dbo.producto.nombre AS Descripcion, 
                         dbo.detalle_venta.cantidad, dbo.detalle_venta.precio_unitario, dbo.detalle_venta.cantidad * dbo.detalle_venta.precio_unitario AS Total_Parcial
FROM            dbo.ventas INNER JOIN
                         dbo.detalle_venta ON dbo.ventas.idventa = dbo.detalle_venta.idventa INNER JOIN
                         dbo.producto ON dbo.detalle_venta.idproducto = dbo.producto.idproducto INNER JOIN
                         dbo.cliente ON dbo.ventas.idcliente = dbo.cliente.idcliente

						 where dbo.ventas.idventa=@idventa


create proc validar_usuario
@login varchar(50),
@password varchar(50)
as
select * from usuario
where login=@login and password=@password and acceso='1'
go

select*from usuario

---------------------
create proc backup_base 
as
backup database [sisventas]
TO DISK =N'C:\respaldoventas\BaseDatos\dbventas.bak'
WITH DESCRIPTION=N'Respaldo bace datos sistema de ventas 2023',
NOFORMAT,
INIT,
NAME=N'sisventas',
SKIP,
NOREWIND,
NOUNLOAD,
STATS=10,
CHECKSUM


--backup completo para realizar la aplicasion instalaldor
--motrar venta
ALTER proc [dbo].[mostrar_venta]
as
SELECT        dbo.ventas.idventa, dbo.ventas.idcliente, dbo.cliente.apellidos, dbo.cliente.dni, dbo.ventas.fecha_venta, dbo.ventas.tipo_documento, dbo.ventas.num_documento
FROM            dbo.cliente INNER JOIN
                         dbo.ventas ON dbo.cliente.idcliente = dbo.ventas.idcliente
						 order by dbo.ventas.idventa desc