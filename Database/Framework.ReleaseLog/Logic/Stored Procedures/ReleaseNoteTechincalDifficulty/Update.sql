IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseNoteTechnicalDifficultyUpdate')
BEGIN
	PRINT 'Dropping Procedure ReleaseNoteTechnicalDifficultyUpdate'
	DROP  Procedure  ReleaseNoteTechnicalDifficultyUpdate
END
GO

PRINT 'Creating Procedure ReleaseNoteTechnicalDifficultyUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ReleaseNoteTechnicalDifficultyUpdate
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
CREATE Procedure dbo.ReleaseNoteTechnicalDifficultyUpdate
(
		@ReleaseNoteTechnicalDifficultyId		INT			 			
	,	@Name							VARCHAR(50)				
	,	@Description					VARCHAR(500)			
	,	@SortOrder						INT						 
	,	@AuditId						INT					
	,	@AuditDate						DATETIME	= NULL	
	,	@SystemEntityType				VARCHAR(50)	= 'ReleaseNoteTechnicalDifficulty'
)
AS
BEGIN 

	DECLARE		@DateModified		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT

	SET			@DateModified		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId


	UPDATE	dbo.ReleaseNoteTechnicalDifficulty 
	SET		Name						=	@Name		
		,	Description					=	@Description				
		,	SortOrder					=	@SortOrder	
		,   DateModified				=	@DateModified
		,	ModifiedByAuditId			=   @ModifiedByAuditId	
	WHERE	ReleaseNoteTechnicalDifficultyId	=	@ReleaseNoteTechnicalDifficultyId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ReleaseNoteTechnicalDifficulty'
		,	@EntityKey				= @ReleaseNoteTechnicalDifficultyId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

 END		
 GO