IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TabChildStructureClone')
BEGIN
	PRINT 'Dropping Procedure TabChildStructureClone'
	DROP  Procedure TabChildStructureClone
END
GO

PRINT 'Creating Procedure TabChildStructureClone'
GO

/*********************************************************************************************
**		File: 
**		Name: TabChildStructureClone
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
**		Date:		Author:				EntityName:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.TabChildStructureClone
(
		@TabChildStructureId	INT				= NULL 	OUTPUT	
	,	@ApplicationId	        INT				= NULL
	,	@Name					VARCHAR(50)		= NULL					
	,	@EntityName				VARCHAR(50)		= NULL					
	,	@SortOrder				INT				= NULL
	,	@TabParentStructureId	INT				= NULL
	,	@InnerControlPath		VARCHAR(200)	= NULL
	,	@AuditId				INT									
	,	@AuditDate				DATETIME		= NULL				
	,	@SystemEntityType		VARCHAR(50)		= 'TabChildStructure'
)
AS
BEGIN

	IF @TabChildStructureId IS NULL OR @TabChildStructureId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @TabChildStructureId OUTPUT
	END						
	
	SELECT	@ApplicationId				= ApplicationId
		,	@EntityName					= EntityName
		,	@SortOrder					= SortOrder	
		,	@TabParentStructureId		= TabParentStructureId		
		,	@InnerControlPath			= InnerControlPath
	FROM	dbo.TabChildStructure
	WHERE   TabChildStructureId			= @TabChildStructureId
	ORDER BY TabChildStructureId

	EXEC dbo.TabChildStructureInsert 
			@TabChildStructureId	=	NULL
		,   @ApplicationId			=   ApplicationId
		,	@Name					=	@Name
		,	@EntityName				=	@EntityName
		,	@SortOrder				=	@SortOrder	
		,	@TabParentStructureId	=	@TabParentStructureId
		,	@InnerControlPath		=	@InnerControlPath
		,	@AuditId				=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'TabChildStructure'
		,	@EntityKey				= @TabChildStructureId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
