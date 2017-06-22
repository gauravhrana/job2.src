ALTER TABLE dbo.TabChildStructure
	ADD CONSTRAINT FK_TabChildStructure_TabParentStructure FOREIGN KEY
	(
		TabParentStructureId
	)
	REFERENCES dbo.TabParentStructure
	(
		TabParentStructureId 
	)
GO