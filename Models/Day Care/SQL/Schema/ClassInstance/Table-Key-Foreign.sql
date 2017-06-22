

ALTER TABLE dbo.ClassInstance
	ADD CONSTRAINT FK_ClassInstance_Course FOREIGN KEY
	(
		CourseId
	)
	REFERENCES dbo.Course
	(
		CourseId
	)
GO


ALTER TABLE dbo.ClassInstance
	ADD CONSTRAINT FK_ClassInstance_Department FOREIGN KEY
	(
		DepartmentId
	)
	REFERENCES dbo.Department
	(
		DepartmentId
	)
GO


ALTER TABLE dbo.ClassInstance
	ADD CONSTRAINT FK_ClassInstance_Teacher FOREIGN KEY
	(
		TeacherId
	)
	REFERENCES dbo.Teacher
	(
		TeacherId
	)
GO






