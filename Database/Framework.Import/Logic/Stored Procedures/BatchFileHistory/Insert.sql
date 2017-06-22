IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BatchFileHistoryInsert')
BEGIN
	PRINT 'Dropping Procedure BatchFileHistoryInsert'
	DROP  Procedure BatchFileHistoryInsert
END
GO

PRINT 'Creating Procedure BatchFileHistoryInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:BatchFileHistoryInsert
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
**     ----------							-----------
**
**		Auth: 
**		Date: 
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**********************************************************************************************/

CREATE Procedure dbo.BatchFileHistoryInsert
(
		@BatchFileHistoryId		INT			= NULL 	OUTPUT	
	,	@ApplicationId			INT	
	,	@BatchFileId			INT								
	,	@BatchFileSetId			INT								
	,	@BatchFileStatusId		INT								
	,	@UpdatedDate			DATETIME						
	,	@UpdatedByPersonId		INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'BatchFileHistory'
)
AS
BEGIN
	
	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @BatchFileHistoryId OUTPUT, @AuditId
		
	INSERT INTO dbo.BatchFileHistory 
	( 
			BatchFileHistoryId
		,	ApplicationId
		,	BatchFileId			
		,	BatchFileSetId		
		,	BatchFileStatusId	
		,	UpdatedDate			
		,	UpdatedByPersonId
	)
	VALUES 
	(  
			@BatchFileHistoryId
		,	@ApplicationId
		,	@BatchFileId		
		,	@BatchFileSetId		
		,	@BatchFileStatusId	
		,	@UpdatedDate		
		,	@UpdatedByPersonId		
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @BatchFileHistoryId
		,	@AuditAction			= 'Insert'
		,	@UpdatedDate			= @AuditDate
		,	@UpdatedByPersonId		= @AuditId

END	
GO

