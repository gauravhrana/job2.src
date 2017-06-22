IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MenuRenumber')
BEGIN
	PRINT 'Dropping Procedure MenuRenumber'
	DROP  Procedure MenuRenumber
END

GO

PRINT 'Creating Procedure MenuRenumber'
GO


/******************************************************************************
**		File: 
**		Name: MenuRenumber
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------							-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.MenuRenumber
(
		@MenuIds				VARCHAR(MAX)
	,	@ApplicationId			INT			= NULL	
	,	@AuditId				INT				
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50) = 'Menu'
)
AS
BEGIN

	DECLARE @Split CHAR(1), @X XML

	SELECT @Split = ','

	SELECT @X = CONVERT(XML,'<root><s>' + REPLACE(@MenuIds,	@Split,'</s><s>') + '</s></root>')

	SELECT	[Value] = T.c.value('.','varchar(20)')
	INTO	#tmpRenumber
	FROM	@X.nodes('/root/s') T(c)

	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)

	-- 1. Drop/Disable Existing Foreing Key Relationships

	ALTER TABLE dbo.MenuDisplayName		NOCHECK CONSTRAINT FK_MenuDisplayName_Menu
	ALTER TABLE dbo.MenuCategoryXMenu	NOCHECK CONSTRAINT FK_MenuCategoryXMenu_Menu

	-- ALTER TABLE dbo.Menu DROP CONSTRAINT FK_MenuDisplayName_Menu
	-- ALTER TABLE dbo.Menu DROP CONSTRAINT FK_MenuCategoryXMenu_Menu


	-- 2. Get Max Id value from table
	DECLARE @MaxMenuId AS INT
	SELECT	@MaxMenuId = MAX(MenuId)
	FROM	dbo.Menu

	-- 3. Insert the selected records into migration table with new proposed PK Ids
	INSERT INTO LoggingAndTrace.dbo.RenumberMigration
	(
			ApplicationId
		,	SystemEntityTypeId
		,	OriginalKey
		,	MigratedKey
		,	RecordDate
	)
	SELECT	a.ApplicationId
		,	@SystemEntityTypeId
		,	a.MenuId
		,	@MaxMenuId + ROW_NUMBER() OVER (ORDER BY a.MenuId)
		,	GETDATE()
	FROM	dbo.Menu	a
	WHERE	a.MenuId IN
		(	
			Select Value From #tmpRenumber
		)

	-- 4. Update PKs into (Menu)	 table
	UPDATE	dbo.Menu
	SET		MenuId = (	SELECT	TOP 1 MigratedKey	
						FROM	LoggingAndTrace.dbo.RenumberMigration
						WHERE	OriginalKey	=	MenuId 
						ORDER BY RenumberMigrationId DESC )
	WHERE	MenuId IN
		(	
			Select Value From #tmpRenumber
		)

	--	5. Update Foreign Key Tables

	-- Update MenuDisplayName Table MenuIds
	UPDATE	dbo.MenuDisplayName
	SET		MenuId = (	SELECT	TOP 1 MigratedKey	
						FROM	LoggingAndTrace.dbo.RenumberMigration
						WHERE	OriginalKey	=	MenuId 
						ORDER BY RenumberMigrationId DESC )
	WHERE	MenuId IN
		(	
			Select Value From #tmpRenumber
		)

	-- Update MenuCategoryxMenu Table MenuIds
	UPDATE	dbo.MenuCategoryxMenu
	SET		MenuId = (	SELECT	TOP 1 MigratedKey	
						FROM	LoggingAndTrace.dbo.RenumberMigration
						WHERE	OriginalKey	=	MenuId 
						ORDER BY RenumberMigrationId DESC )
	WHERE	MenuId IN
		(	
			Select Value From #tmpRenumber
		)

	-- 6. Enable/Recreate the ForeignKey Relationships

	ALTER TABLE dbo.MenuDisplayName		CHECK CONSTRAINT FK_MenuDisplayName_Menu
	ALTER TABLE dbo.MenuCategoryXMenu	CHECK CONSTRAINT FK_MenuCategoryXMenu_Menu
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'Details' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END		
GO
   