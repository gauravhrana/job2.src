IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseNoteBusinessValueClone')
BEGIN
	PRINT 'Dropping Procedure ReleaseNoteBusinessValueClone'
	DROP  Procedure ReleaseNoteBusinessValueClone
END
GO

PRINT 'Creating Procedure ReleaseNoteBusinessValueClone'
GO

/*********************************************************************************************
**		File: 
**		Name: ReleaseNoteBusinessValueClone
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
CREATE Procedure dbo.ReleaseNoteBusinessValueClone
(
		@ReleaseNoteBusinessValueId		INT				= NULL 	OUTPUT	
	,	@ApplicationId					INT				= NULL
	,	@Name							VARCHAR(50)						
	,	@Description					VARCHAR (500)						
	,	@SortOrder						INT	
	,	@DateCreated					DATETIME	= NULL
	,	@DateModified					DATETIME	= NULL
	,	@CreatedByAuditId				INT			= NULL
	,	@ModifiedByAuditId				INT			= NULL								
	,	@AuditId						INT									
	,	@AuditDate						DATETIME	
		= NULL			
	,	@SystemEntityType				VARCHAR(50)		= 'ReleaseNoteBusinessValue'
)
AS
BEGIN

	IF @ReleaseNoteBusinessValueId IS NULL OR @ReleaseNoteBusinessValueId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ReleaseNoteBusinessValueId OUTPUT
	END			
	
	SELECT	@ApplicationId					= ApplicationId
		,	@Description					= Description
		,	@SortOrder						= SortOrder	
		,	@DateCreated					= DateCreated
		,	@DateModified					= DateModified
		,	@CreatedByAuditId				= CreatedByAuditId	
		,	@ModifiedByAuditId				= ModifiedByAuditId
	FROM	dbo.ReleaseNoteBusinessValue
	WHERE	ReleaseNoteBusinessValueId		= @ReleaseNoteBusinessValueId

	EXEC dbo.ReleaseNoteBusinessValueInsert 
			@ReleaseNoteBusinessValueId	=	NULL
		,	@ApplicationId				=	@ApplicationId
		,	@Name						=	@Name
		,	@Description				=	@Description
		,	@SortOrder					=	@SortOrder
		,	@DateCreated				=	@DateCreated
		,	@DateModified				=	@DateModified
		,	@CreatedByAuditId			=	@CreatedByAuditId
		,	@ModifiedByAuditId			=	@ModifiedByAuditId
		,	@AuditId					=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ReleaseNoteBusinessValue'
		,	@EntityKey				= @ReleaseNoteBusinessValueId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
