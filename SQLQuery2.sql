create table [dbo].[Trening](
id int not null,
datumTreninga datetime,
vremePocetka varchar(50),
trajanjeTreninga int,
statusTreninga varchar(20),
jmbgInstruktora varchar(20),
jmbgPolaznika varchar(20),
aktivan bit,

constraint pk_Trening primary key(id),



) 