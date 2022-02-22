USE PPPK_lcabraja
CREATE TABLE Professor
(
    IDProfessor int primary key identity,
    PersonID    int not null,
    constraint fk_personid foreign key (PersonID) references Person (IDPerson),
    constraint fk_classid foreign key (GroupID) references Class (IDClass)
)
GO

CREATE PROC GetProfessors
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