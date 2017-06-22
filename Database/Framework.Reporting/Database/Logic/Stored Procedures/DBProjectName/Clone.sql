IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DBProjectNameClone')
BEGIN
	PRINT 'Dropping Procedure DBProjectNameClone'
	DROP  Procedure DBProjectNameClone
END
GO

PRINT 'Creating Procedure DBProjectNameClone'
GO

/*********************************************************************************************
**		File: 
**		Name: DBProjectNameClone
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

CREATE Procedure dbo.DBProjectNameClone
(
		@DBProjectNameId				INT			= NULL 	OUTPUT	
	,	@ApplicationId	        INT         = NULL
	,	@Name					VARCHAR(50)						
	,	@Description			VARCHAR(500)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'DBProjectName'
)
AS
BEGIN

	IF @DBProjectNameId IS NULL OR @DBProjectNameId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @DBProjectNameId OUTPUT
	END						
	
	SELECT	@ApplicationId			=	ApplicationId
		,	@Description			=	[Description]
		,	@SortOrder				=	SortOrder				
	FROM	dbo.DBProjectName
	WHERE   DBProjectNameId		=	@DBProjectNameId
	ORDER BY DBProjectNameId

	EXEC dbo.DBProjectNameInsert 
			@DBProjectNameId		=	NULL
		,   @ApplicationId			=   ApplicationId
		,	@Name					=	@Name
		,	@Description			=	@Description
		,	@SortOrder				=	@SortOrder
		,	@AuditId				=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'DBProjectName'
		,	@EntityKey				= @DBProjectNameId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
