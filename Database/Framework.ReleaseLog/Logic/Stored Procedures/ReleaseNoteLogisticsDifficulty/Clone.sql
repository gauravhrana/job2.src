IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseNoteLogisticsDifficultyClone')
BEGIN
	PRINT 'Dropping Procedure ReleaseNoteLogisticsDifficultyClone'
	DROP  Procedure ReleaseNoteLogisticsDifficultyClone
END
GO

PRINT 'Creating Procedure ReleaseNoteLogisticsDifficultyClone'
GO

/*********************************************************************************************
**		File: 
**		Name: ReleaseNoteLogisticsDifficultyClone
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
CREATE Procedure dbo.ReleaseNoteLogisticsDifficultyClone
(
		@ReleaseNoteLogisticsDifficultyId		INT				= NULL 	OUTPUT	
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
	,	@SystemEntityType				VARCHAR(50)		= 'ReleaseNoteLogisticsDifficulty'
)
AS
BEGIN

	IF @ReleaseNoteLogisticsDifficultyId IS NULL OR @ReleaseNoteLogisticsDifficultyId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ReleaseNoteLogisticsDifficultyId OUTPUT
	END			
	
	SELECT	@ApplicationId					= ApplicationId
		,	@Description					= Description
		,	@SortOrder						= SortOrder	
		,	@DateCreated					= DateCreated
		,	@DateModified					= DateModified
		,	@CreatedByAuditId				= CreatedByAuditId	
		,	@ModifiedByAuditId				= ModifiedByAuditId
	FROM	dbo.ReleaseNoteLogisticsDifficulty
	WHERE	ReleaseNoteLogisticsDifficultyId		= @ReleaseNoteLogisticsDifficultyId

	EXEC dbo.ReleaseNoteLogisticsDifficultyInsert 
			@ReleaseNoteLogisticsDifficultyId	=	NULL
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
			@SystemEntityType		= 'ReleaseNoteLogisticsDifficulty'
		,	@EntityKey				= @ReleaseNoteLogisticsDifficultyId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
