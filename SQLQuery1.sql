CREATE TABLE [dbo].[Korisnici]
(

     [id] int NOT NULL IDENTITY(1,1) PRIMARY KEY,
     [ime] varchar (20) not null ,
     [prezime] varchar (20) not null,
     [TipKorisnika] varchar (20) not null,
     [email] varchar (20) not null,
     [aktivan] BIT not null 


)

create table [dbo].[Instruktori]
(
     [id] int not null primary key,
     constraint FK_Korisnik_Instruktor_Id
     FOreign key (ID) References dbo.Korisnici(ID)
)