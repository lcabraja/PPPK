USE PPPK_lcabraja
CREATE TABLE Student
(
    IDStudent int primary key identity,
    PersonID  int          not null,
    GroupID   nvarchar(20) not null,
    constraint fk_personid foreign key (PersonID) references Person (IDPerson),
    constraint fk_classid foreign key (GroupID) references Class (IDClass)
)
GO

CREATE PROC GetStudents
AS
SELECT *
FROM Student
GO

CREATE PROC GetStudent @idStudent int
AS
SELECT *
FROM Student
WHERE IDStudent = @idStudent
GO

CREATE PROC DeleteStudent @idStudent int
AS
DELETE
FROM Student
WHERE IDStudent = @idStudent
GO

CREATE PROC AddStudent @personId int,
                       @groupId nvarchar(20),
                       @idStudent INT OUTPUT
AS
BEGIN
    INSERT INTO Student VALUES (@personId, @groupId)
    SET @idStudent = SCOPE_IDENTITY()
END
GO

CREATE PROC UpdateStudent @personId int,
                          @groupId int
AS
UPDATE Student
SET PersonID = @personId,
    GroupID  = @groupId
WHERE IDStudent = @idStudent