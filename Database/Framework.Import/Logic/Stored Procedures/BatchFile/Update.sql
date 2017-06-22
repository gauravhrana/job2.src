IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BatchFileUpdate')
BEGIN
	PRINT 'Dropping Procedure BatchFileUpdate'
	DROP  Procedure  BatchFileUpdate
END
GO

PRINT 'Creating Procedure BatchFileUpdate'
GO

/******************************************************************************
**		File: 
**		Name: BatchFileUpdate
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
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.BatchFileUpdate
(
		@BatchFileId			INT		 						
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

	UPDATE	dbo.BatchFile 
	SET		Name					=	@Name	
		,	Folder					=	@Folder				
		,	BatchFile				=	@BatchFile
		,	BatchFileSetId			=	@BatchFileSetId		
		,	Description				=	@Description		
		,	FileTypeId				=	@FileTypeId			
		,	SystemEntityTypeId		=	@SystemEntityTypeId	
		,	BatchFileStatusId		=	@BatchFileStatusId		
		,	CreatedDate				=	@CreatedDate		
		,	CreatedByPersonId		=	@CreatedByPersonId	
		,	UpdatedDate				=	@UpdatedDate		
		,	UpdatedByPersonId		=	@UpdatedByPersonId
		,	Errors					=	@Errors		
	WHERE	BatchFileId	=	@BatchFileId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @BatchFileId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END		
GO