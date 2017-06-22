IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseNoteLogisticsDifficultyUpdate')
BEGIN
	PRINT 'Dropping Procedure ReleaseNoteLogisticsDifficultyUpdate'
	DROP  Procedure  ReleaseNoteLogisticsDifficultyUpdate
END
GO

PRINT 'Creating Procedure ReleaseNoteLogisticsDifficultyUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ReleaseNoteLogisticsDifficultyUpdate
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
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.ReleaseNoteLogisticsDifficultyUpdate
(
		@ReleaseNoteLogisticsDifficultyId		INT			 			
	,	@Name							VARCHAR(50)				
	,	@Description					VARCHAR(500)			
	,	@SortOrder						INT						 
	,	@AuditId						INT					
	,	@AuditDate						DATETIME	= NULL	
	,	@SystemEntityType				VARCHAR(50)	= 'ReleaseNoteLogisticsDifficulty'
)
AS
BEGIN 

	DECLARE		@DateModified		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT

	SET			@DateModified		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId


	UPDATE	dbo.ReleaseNoteLogisticsDifficulty 
	SET		Name						=	@Name		
		,	Description					=	@Description				
		,	SortOrder					=	@SortOrder	
		,   DateModified				=	@DateModified
		,	ModifiedByAuditId			=   @ModifiedByAuditId	
	WHERE	ReleaseNoteLogisticsDifficultyId	=	@ReleaseNoteLogisticsDifficultyId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ReleaseNoteLogisticsDifficulty'
		,	@EntityKey				= @ReleaseNoteLogisticsDifficultyId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

 END		
 GO