IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseNoteBusinessDifficultyClone')
BEGIN
	PRINT 'Dropping Procedure ReleaseNoteBusinessDifficultyClone'
	DROP  Procedure ReleaseNoteBusinessDifficultyClone
END
GO

PRINT 'Creating Procedure ReleaseNoteBusinessDifficultyClone'
GO

/*********************************************************************************************
**		File: 
**		Name: ReleaseNoteBusinessDifficultyClone
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
CREATE Procedure dbo.ReleaseNoteBusinessDifficultyClone
(
		@ReleaseNoteBusinessDifficultyId		INT				= NULL 	OUTPUT	
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
	,	@SystemEntityType				VARCHAR(50)		= 'ReleaseNoteBusinessDifficulty'
)
AS
BEGIN

	IF @ReleaseNoteBusinessDifficultyId IS NULL OR @ReleaseNoteBusinessDifficultyId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ReleaseNoteBusinessDifficultyId OUTPUT
	END			
	
	SELECT	@ApplicationId					= ApplicationId
		,	@Description					= Description
		,	@SortOrder						= SortOrder	
		,	@DateCreated					= DateCreated
		,	@DateModified					= DateModified
		,	@CreatedByAuditId				= CreatedByAuditId	
		,	@ModifiedByAuditId				= ModifiedByAuditId
	FROM	dbo.ReleaseNoteBusinessDifficulty
	WHERE	ReleaseNoteBusinessDifficultyId		= @ReleaseNoteBusinessDifficultyId

	EXEC dbo.ReleaseNoteBusinessDifficultyInsert 
			@ReleaseNoteBusinessDifficultyId	=	NULL
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
			@SystemEntityType		= 'ReleaseNoteBusinessDifficulty'
		,	@EntityKey				= @ReleaseNoteBusinessDifficultyId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
