IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'HelpPageContextClone')
BEGIN
	PRINT 'Dropping Procedure HelpPageContextClone'
	DROP  Procedure HelpPageContextClone
END
GO

PRINT 'Creating Procedure HelpPageContextClone'
GO

/*********************************************************************************************
**		File: 
**		Name: HelpPageContextClone
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
CREATE Procedure dbo.HelpPageContextClone
(
		@HelpPageContextId		INT				= NULL 	OUTPUT
	,	@ApplicationId			INT				= NULL
	,	@Name					VARCHAR(50)						
	,	@Description            VARCHAR (500)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME		= NULL			
	,	@SystemEntityType		VARCHAR(50)		= 'HelpPageContext'
)
AS
BEGIN

	IF @HelpPageContextId IS NULL OR @HelpPageContextId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @HelpPageContextId OUTPUT
	END			
	
	SELECT	@ApplicationId	= ApplicationId
		,	@Description	= Description
		,	@SortOrder		= SortOrder		
	FROM	dbo.HelpPageContext
	WHERE	HelpPageContextId		= @HelpPageContextId

	EXEC dbo.HelpPageContextInsert 
			@HelpPageContextId		=	NULL
		,	@ApplicationId			=	@ApplicationId
		,	@Name					=	@Name
		,	@Description			=	@Description
		,	@SortOrder				=	@SortOrder
		,	@AuditId				=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @HelpPageContextId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO

