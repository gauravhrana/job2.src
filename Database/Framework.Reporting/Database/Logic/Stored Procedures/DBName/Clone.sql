IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DBNameClone')
BEGIN
	PRINT 'Dropping Procedure DBNameClone'
	DROP  Procedure DBNameClone
END
GO

PRINT 'Creating Procedure DBNameClone'
GO

/*********************************************************************************************
**		File: 
**		Name: DBNameClone
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

CREATE Procedure dbo.DBNameClone
(
		@DBNameId				INT			= NULL 	OUTPUT	
	,	@ApplicationId	        INT         = NULL
	,	@Name					VARCHAR(50)						
	,	@Description			VARCHAR(500)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'DBName'
)
AS
BEGIN

	IF @DBNameId IS NULL OR @DBNameId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @DBNameId OUTPUT
	END						
	
	SELECT	@ApplicationId			=	ApplicationId
		,	@Description			=	[Description]
		,	@SortOrder				=	SortOrder				
	FROM	dbo.DBName
	WHERE   DBNameId		=	@DBNameId
	ORDER BY DBNameId

	EXEC dbo.DBNameInsert 
			@DBNameId		=	NULL
		,   @ApplicationId			=   ApplicationId
		,	@Name					=	@Name
		,	@Description			=	@Description
		,	@SortOrder				=	@SortOrder
		,	@AuditId				=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'DBName'
		,	@EntityKey				= @DBNameId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
