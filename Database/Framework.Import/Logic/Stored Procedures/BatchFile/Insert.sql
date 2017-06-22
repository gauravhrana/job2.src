IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BatchFileInsert')
BEGIN
	PRINT 'Dropping Procedure BatchFileInsert'
	DROP  Procedure BatchFileInsert
END
GO

PRINT 'Creating Procedure BatchFileInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:BatchFileInsert
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
CREATE Procedure dbo.BatchFileInsert
(
		@BatchFileId			INT				= NULL	OUTPUT	
	,	@ApplicationId			INT		
	,	@Name					VARCHAR(50)						 
	,	@Folder					VARCHAR(1000)
	,	@BatchFile				VARCHAR(150)		
	,	@BatchFileSetId			INT					 			 
	,	@Description			VARCHAR(150)	= NULL			
	,	@FileTypeId				INT					 
	,	@SystemEntityTypeId		INT								
	,	@BatchFileStatusId		INT						
	,	@CreatedDate			DATETIME						
	,	@CreatedByPersonId		INT								
	,	@UpdatedDate			DATETIME		= NULL			
	,	@UpdatedByPersonId		INT				= NULL			
	,	@Errors					VARCHAR(1000)	= NULL			
	,	@AuditId				INT								
	,	@AuditDate				DATETIME		= NULL			
	,	@SystemEntityType		VARCHAR(50)		= 'BatchFile'
)
AS
BEGIN
	
	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @BatchFileId OUTPUT, @AuditId
		
	INSERT INTO dbo.BatchFile 
	( 
			BatchFileId	
		,	ApplicationId	
		,	Name				
		,	Folder				
		,	BatchFile
		,	BatchFileSetId			
		,	Description		
		,	FileTypeId			
		,	SystemEntityTypeId	
		,	BatchFileStatusId		
		,	CreatedDate		
		,	CreatedByPersonId	
		,	UpdatedDate		
		,	UpdatedByPersonId
		,	Errors															
	)
	VALUES 
	(  		
			@BatchFileId
		,	@ApplicationId			
		,	@Name					
		,	@Folder					
		,	@BatchFile	
		,	@BatchFileSetId			
		,	@Description			
		,	@FileTypeId				
		,	@SystemEntityTypeId		
		,	@BatchFileStatusId			
		,	@CreatedDate			
		,	@CreatedByPersonId		
		,	@UpdatedDate			
		,	@UpdatedByPersonId	
		,	@Errors				
	)

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @BatchFileId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END	
GO

 