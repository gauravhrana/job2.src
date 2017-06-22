IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TabParentStructureClone')
BEGIN
	PRINT 'Dropping Procedure TabParentStructureClone'
	DROP  Procedure TabParentStructureClone
END
GO

PRINT 'Creating Procedure TabParentStructureClone'
GO

/*********************************************************************************************
**		File: 
**		Name: TabParentStructureClone
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
**     ----------						-----------
**
**		Auth: 
**		Date: 
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.TabParentStructureClone
(
		@TabParentStructureId	INT			= NULL 	OUTPUT	
	,	@ApplicationId	        INT         = NULL
	,	@Name					VARCHAR(50)						
	,	@Description			VARCHAR(50)						
	,	@SortOrder				INT	
	,	@IsAllTab				INT
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'TabParentStructure'
)
AS
BEGIN

	IF @TabParentStructureId IS NULL OR @TabParentStructureId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @TabParentStructureId OUTPUT
	END						
	
	SELECT	@ApplicationId		= ApplicationId
		,	@Description		= Description
		,	@SortOrder			= SortOrder	
		,	@IsAllTab			= IsAllTab			
	FROM	dbo.TabParentStructure
	WHERE   TabParentStructureId				= @TabParentStructureId
	ORDER BY TabParentStructureId

	EXEC dbo.TabParentStructureInsert 
			@TabParentStructureId	=	NULL
		,   @ApplicationId			=   ApplicationId
		,	@Name					=	@Name
		,	@Description			=	@Description
		,	@SortOrder				=	@SortOrder	
		,	@IsAllTab				=	@IsAllTab
		,	@AuditId				=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'TabParentStructure'
		,	@EntityKey				= @TabParentStructureId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
