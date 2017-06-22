

ALTER TABLE dbo.CourseInstance
	ADD CONSTRAINT FK_CourseInstance_Course FOREIGN KEY
	(
		CourseId
	)
	REFERENCES dbo.Course
	(
		CourseId
	)
GO


ALTER TABLE dbo.CourseInstance
	ADD CONSTRAINT FK_CourseInstance_Department FOREIGN KEY
	(
		DepartmentId
	)
	REFERENCES dbo.Department
	(
		DepartmentId
	)
GO


ALTER TABLE dbo.CourseInstance
	ADD CONSTRAINT FK_CourseInstance_Teacher FOREIGN KEY
	(
		TeacherId
	)
	REFERENCES dbo.Teacher
	(
		TeacherId
	)
GO






