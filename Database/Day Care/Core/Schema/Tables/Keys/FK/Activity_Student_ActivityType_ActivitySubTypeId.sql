ALTER TABLE dbo.Activity
	ADD CONSTRAINT FK_Activity_Student FOREIGN KEY 
	(
		StudentId
	) 
	REFERENCES Student
	(
		StudentId
	)
	GO
	ALTER TABLE dbo.Activity
	ADD CONSTRAINT FK_Activity_ActivityType FOREIGN KEY 
	(
		ActivityTypeId
	) 
	REFERENCES ActivityType
	(
		ActivityTypeId
	)
   GO
    ALTER TABLE dbo.Activity
	ADD CONSTRAINT FK_Activity_ActivitySubType FOREIGN KEY 
	(
		ActivitySubTypeId
	) 
	REFERENCES ActivitySubType
	(
		ActivitySubTypeId
	)
   GO 
	
