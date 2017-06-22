IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'HelpPageClone')
BEGIN
	PRINT 'Dropping Procedure HelpPageClone'
	DROP  Procedure HelpPageClone
END
GO

PRINT 'Creating Procedure HelpPageClone'
GO

/*********************************************************************************************
**		File: 
**		Name: HelpPageClone
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
**		Date:		Author:				Content:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.HelpPageClone
(
		@HelpPageId					INT				= NULL 	OUTPUT	
	,   @ApplicationId				INT					
	,	@SystemEntityTypeId			INT				
	,	@HelpPageContextId			INT	
	,	@Name						VARCHAR(50)						
	,	@Content					VARCHAR(MAX)						
	,	@SortOrder					INT	
	,	@AuditId					INT									
	,	@AuditDate					DATETIME		= NULL				
	,	@SystemEntityType			VARCHAR(50)		= 'HelpPage'
)
AS
BEGIN					
	
	SELECT	@ApplicationId			= ApplicationId
		,	@Content				= Content
		,	@SortOrder				= SortOrder	
		,	@SystemEntityTypeId		= SystemEntityTypeId
		,	@HelpPageContextId		= HelpPageContextId				
	FROM	dbo.HelpPage
	WHERE   HelpPageId				= @HelpPageId
	ORDER BY HelpPageId

	EXEC dbo.HelpPageInsert 
			@HelpPageId				=	NULL
		,   @ApplicationId			=   ApplicationId
		,	@Name					=	@Name
		,	@Content				=	@Content
		,	@SortOrder				=	@SortOrder
		,	@SystemEntityTypeId		=	@SystemEntityTypeId
		,	@HelpPageContextId		=	@HelpPageContextId
		,	@AuditId				=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @HelpPageId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
