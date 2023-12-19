create database dbBackendAPI

go
use dbBackendAPI

go
create table Users(
IdUser int primary key identity,
UserName varchar(50) unique not null,
Password varchar(100) not null
)

go
create table FamilyGroup(
Id int primary key identity,
Usuario varchar(50) unique not null,
Cedula varchar(10) unique not null,
Nombres varchar(50) not null,
Apellidos varchar(50) not null,
Genero varchar(10),
Parentesco varchar(20),
Edad smallint not null,
MenorEdad bit,
FechaNacimiento date,
IdUser int references Users(IdUser)
)

go
insert into Users (UserName, Password) values 
('Juan', 'Juan123*')

go
insert into FamilyGroup(Usuario, Cedula, Nombres ,Apellidos, Genero, Parentesco, Edad, MenorEdad, IdUser) values 
('Juan', '10201020', 'Juan', 'Lopera', 'Masculino', 'Padre', 33, 0,	1)
go
insert into FamilyGroup(Usuario, Cedula, Nombres ,Apellidos, Genero, Parentesco, Edad, MenorEdad, FechaNacimiento, IdUser) values 
('Naomi', '1234567', 'Naomi', 'Lopera', 'Femenino', 'Hija', 5, 1, '2018-10-20',	1)

go
create table LogPetition(
IdLog int primary key identity,
dataHour date,
method varchar(30) not null,
route varchar(10) not null,
exito bit,
messageError varchar(500)
)

go
create table Posts(
userId int not null,
id int primary key,
title varchar(200) not null,
body varchar(500) not null
)

go
create table Comments(
id int primary key,
postId int references Posts(id),
name varchar(200) not null,
email varchar(100) not null,
body varchar(500) not null
)

go
create procedure sp_SaveUser
(
@UserName varchar(50),
@Password varchar(100)
)
as
begin
	insert into Users(UserName,Password) values
	(@UserName,@Password)
end

go
create procedure sp_EditUser
(
@IdUser int,
@UserName varchar(50),
@Password varchar(100)
)
as
begin
	update Users set
	UserName = @UserName,
	Password = @Password
	where IdUser = @IdUser
end

go
create procedure sp_DeleteUser
(
@IdUser int
)
as
begin
	delete from Users where IdUser = @IdUser
end 

go 
create procedure sp_GetFamilyGroup
as
begin
 	set dateformat dmy

	select Id, Usuario, Cedula,	Nombres, Apellidos, Genero, Parentesco,	Edad, MenorEdad,
	convert(char(10),FechaNacimiento,103) as 'FechaNacimiento', IdUser
	from FamilyGroup
end

go
create procedure sp_SaveFamilyGroup
(
@Usuario varchar(50),
@Cedula varchar(10),
@Nombres varchar(10),
@Apellidos varchar(10),
@Genero varchar(10),
@Parentesco varchar(10),
@Edad smallint,
@MenorEdad bit,
@FechaNacimiento dateTime,
@IdUser int
)
as
begin
	set dateformat dmy
	
	insert into FamilyGroup(Usuario,Cedula,Nombres,Apellidos,Genero,Parentesco,Edad,MenorEdad,FechaNacimiento,IdUser) 
	values
	(@Usuario,@Cedula,@Nombres,@Apellidos,@Genero,@Parentesco,@Edad,@MenorEdad,convert(char(10),@FechaNacimiento,103),@IdUser)
end

go
create procedure sp_EditFamilyGroup
(
@Usuario varchar(50),
@Cedula varchar(10),
@Nombres varchar(10),
@Apellidos varchar(10),
@Genero varchar(10),
@Parentesco varchar(10),
@Edad smallint,
@MenorEdad bit,
@FechaNacimiento dateTime,
@IdUser int
)
as
begin
	set dateformat dmy
	
	update FamilyGroup set
	Usuario = @Usuario,
	Cedula = @Cedula,
	Nombres = @Nombres,
	Apellidos = @Apellidos,
	Genero = @Genero,
	Parentesco = @Parentesco,
	Edad = @Edad,
	MenorEdad = @MenorEdad,
	FechaNacimiento = convert(char(10),@FechaNacimiento,103),
	IdUser = @IdUser
	where Usuario = @Usuario
end

go
create procedure sp_DeleteFamilyGroup
(
@cedula varchar(10)
)
as
begin
	delete from FamilyGroup where Cedula = @cedula
end

go
create procedure sp_EditPost
(
@userId int,
@id int,
@title varchar(200),
@body varchar(500)
)
as
begin
	update Posts set
	Title = @title,
	Body = @body
	where Id = @id
end

go
create procedure sp_DeletePost
(
@id int
)
as
begin
	delete from Posts where Id = @id
end 

go
create procedure sp_SavePost
(
@userId int,
@id int,
@title varchar(200),
@body varchar(500)
)
as
begin
	insert into Posts(UserId, Id, Title, Body) values
	(@UserId, @Id, @Title, @Body)
end

go
create procedure sp_GuardarComment
(
@Id int,
@PostId int,
@Name varchar(200),
@Email varchar(100),
@body varchar(500)
)
as
begin
	insert into Comments(Id, PostId, Name, Email, Body) values
	(@Id, @PostId, @Name, @Email, @Body)
end

go
create procedure sp_EditComment
(
@Id int,
@PostId int,
@Name varchar(200),
@Email varchar(100),
@body varchar(500)
)
as
begin
	update Comments set
	Name = @Name,
	Email = @Email,
	Body = @body
	where Id = @id
end

go
create procedure sp_DeleteComment
(
@id int
)
as
begin
	delete from Comments where Id = @id
end 