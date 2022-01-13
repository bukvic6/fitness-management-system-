create table [dbo].[Adrese](
[Id] int not null IDENTITY (1, 1) Primary key,
[Ulica] varchar (20) not null,
[Broj] varchar (5) not null,
[Drzava] varchar (20) not null,
[Grad] varchar (20) not null)