
ALTER TABLE dbo.Registration
	ADD CONSTRAINT FK_Registration_Course FOREIGN KEY
	(
		CourseId
	)
	REFERENCES dbo.Course
	(
		CourseId
	)
GO


ALTER TABLE dbo.Registration
	ADD CONSTRAINT FK_Registration_Student FOREIGN KEY
	(
		StudentId
	)
	REFERENCES dbo.Student
	(
		StudentId
	)
GO







