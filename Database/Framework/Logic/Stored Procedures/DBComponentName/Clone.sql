IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DBComponentNameClone')
BEGIN
	PRINT 'Dropping Procedure DBComponentNameClone'
	DROP  Procedure DBComponentNameClone
END
GO

PRINT 'Creating Procedure DBComponentNameClone'
GO

/*********************************************************************************************
**		File: 
**		Name: DBComponentNameClone
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

CREATE Procedure dbo.DBComponentNameClone
(
		@DBComponentNameId				INT			= NULL 	OUTPUT	
	,	@ApplicationId	        INT         = NULL
	,	@Name					VARCHAR(50)						
	,	@Description			VARCHAR(500)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'DBComponentName'
)
AS
BEGIN

	IF @DBComponentNameId IS NULL OR @DBComponentNameId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @DBComponentNameId OUTPUT
	END						
	
	SELECT	@ApplicationId			=	ApplicationId
		,	@Description			=	[Description]
		,	@SortOrder				=	SortOrder				
	FROM	dbo.DBComponentName
	WHERE   DBComponentNameId		=	@DBComponentNameId
	ORDER BY DBComponentNameId

	EXEC dbo.DBComponentNameInsert 
			@DBComponentNameId		=	NULL
		,   @ApplicationId			=   ApplicationId
		,	@Name					=	@Name
		,	@Description			=	@Description
		,	@SortOrder				=	@SortOrder
		,	@AuditId				=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'DBComponentName'
		,	@EntityKey				= @DBComponentNameId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
