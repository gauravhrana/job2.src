IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseNoteLogisticsDifficultyInsert')
BEGIN
	PRINT 'Dropping Procedure ReleaseNoteLogisticsDifficultyInsert'
	DROP  Procedure ReleaseNoteLogisticsDifficultyInsert
END
GO

PRINT 'Creating Procedure ReleaseNoteLogisticsDifficultyInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:ReleaseNoteLogisticsDifficultyInsert
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
**********************************************************************************************/
CREATE Procedure dbo.ReleaseNoteLogisticsDifficultyInsert
(
		@ReleaseNoteLogisticsDifficultyId		INT				= NULL 	OUTPUT
	,	@ApplicationId					INT				= NULL		
	,	@Name							VARCHAR(50)						
	,	@Description					VARCHAR (500)					
	,	@SortOrder						INT									
	,	@AuditId						INT									
	,	@AuditDate						DATETIME		= NULL				
	,	@SystemEntityType				VARCHAR(50)		= 'ReleaseNoteLogisticsDifficulty'
)
AS
BEGIN
	
	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ReleaseNoteLogisticsDifficultyId OUTPUT, @AuditId

	DECLARE		@DateCreated		AS		DATETIME
	DECLARE		@DateModified		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT	
	

	SET @DateCreated		= GETDATE() 
	SET @DateModified		= @DateCreated
	SET @CreatedByAuditId	= @AuditId
	SET @ModifiedByAuditId	= @AuditId

	
	INSERT INTO dbo.ReleaseNoteLogisticsDifficulty 
	( 
			ReleaseNoteLogisticsDifficultyId
		,	ApplicationId							
		,	Name				
		,	Description			
		,	SortOrder	
		,	DateCreated
		,	DateModified
		,	CreatedByAuditId
		,	ModifiedByAuditId
			
	)
	VALUES 
	(  
			@ReleaseNoteLogisticsDifficultyId
		,	@ApplicationId			
		,	@Name				
		,	@Description
		,	@SortOrder	
		,	@DateCreated
		,	@DateModified
		,	@CreatedByAuditId
		,	@ModifiedByAuditId
	)

	SELECT @ReleaseNoteLogisticsDifficultyId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ReleaseNoteLogisticsDifficulty'
		,	@EntityKey				= @ReleaseNoteLogisticsDifficultyId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 