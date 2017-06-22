ALTER TABLE dbo.HelpPage
	ADD CONSTRAINT FK_HelpPage_HelpPageContext FOREIGN KEY
	(
		HelpPageContextId
	)
	REFERENCES dbo.HelpPageContext
	(
		HelpPageContextId 
	)
GO