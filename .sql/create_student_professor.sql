USE PPPK_lcabraja
CREATE TABLE Group
(
    IDGroup int primary key identity,
    Title nvarchar(20) not null,
    Description nvarchar(120)
)
GO
CREATE TABLE Professor_Group
(
    GroupID int,
    ProfessorID int not null,
    constraint fk_groupid foreign key (GroupID) references Person (IDPerson),
    constraint fk_professorid foreign key (ProfessorID) references Professor (IDProfessor)
)
GO
CREATE TABLE Group_Student
(
    GroupID int,
    StudentID int not null,
    constraint fk_groupid foreign key (GroupID) references Person (IDPerson),
    constraint fk_studentid foreign key (StudentID) references Student (IDStudent)
)
GO

CREATE PROC GetGroups
AS
SELECT *
FROM Professor
GO

CREATE PROC GetProfessor @idProfessor int
AS
SELECT *
FROM Professor
WHERE IDProfessor = @idProfessor
GO

CREATE PROC DeleteProfessor @idProfessor int
AS
DELETE
FROM Professor
WHERE IDProfessor = @idProfessor
GO

CREATE PROC AddProfessor @personId int,
                         @idProfessor INT OUTPUT
AS
BEGIN
    INSERT INTO Professor VALUES (@personId, @classId)
    SET @idProfessor = SCOPE_IDENTITY()
END
GO

CREATE PROC UpdateProfessor @personId int,
                            @classId int
AS
UPDATE Professor
SET PersonID = @personId
WHERE IDProfessor = @idProfessor