IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FileTypeDetails')
BEGIN
	PRINT 'Dropping Procedure FileTypeDetails'
	DROP  Procedure FileTypeDetails
END
GO

PRINT 'Creating Procedure FileTypeDetails'
GO


/******************************************************************************
**		File: 
**		Name: FileTypeDetails
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

CREATE Procedure dbo.FileTypeDetails
(
		@FileTypeId				INT				
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50)	= 'FileType'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey					=	@FileTypeId
		,	@SystemEntityType			=	@SystemEntityType
		,	@LastUpdatedBy				=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate			=	@LastUpdatedDate		OUT
		,	@LastAuditAction			=	@LastAuditAction		OUT
			
	SELECT	FileTypeId		
		,	Name					
		,	Description		
		,	SortOrder		
		,	@LastUpdatedDate				AS	'Updated Date' 
		,	@LastUpdatedBy					AS	'Updated By'   
		,	@LastAuditAction				AS	'Last Action' 
	FROM	dbo.FileType 
	WHERE	FileTypeId = @FileTypeId	
		
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @FileTypeId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   