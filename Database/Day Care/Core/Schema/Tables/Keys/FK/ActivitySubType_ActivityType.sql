ALTER TABLE dbo.ActivitySubType
	ADD CONSTRAINT FK_ActivitySubType_ActivityType FOREIGN KEY 
	(
		ActivityTypeId
	) 
	REFERENCES ActivityType
	(
		ActivityTypeId
	)
	GO
	
