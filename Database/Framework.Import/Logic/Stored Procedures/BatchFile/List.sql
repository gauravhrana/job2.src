IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BatchFileList')
BEGIN
	PRINT 'Dropping Procedure BatchFileList'
	DROP  Procedure  dbo.BatchFileList
END
GO

PRINT 'Creating Procedure BatchFileList'
GO

/******************************************************************************
**		File: 
**		Name: BatchFileList
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
**     ----------					   ---------
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

CREATE Procedure dbo.BatchFileList
(
		@AuditId				INT			
	,	@ApplicationId			INT			
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'BatchFile'
)
AS
BEGIN
	
	SELECT	BatchFileId		
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
	FROM	 dbo.BatchFile 
	WHERE ApplicationId = @ApplicationId
	ORDER BY BatchFileId		ASC

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	 
END	
GO