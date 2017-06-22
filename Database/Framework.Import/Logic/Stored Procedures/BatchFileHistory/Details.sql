IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BatchFileHistoryDetails')
BEGIN
	PRINT 'Dropping Procedure BatchFileHistoryDetails'
	DROP  Procedure BatchFileHistoryDetails
END
GO

PRINT 'Creating Procedure BatchFileHistoryDetails'
GO


/******************************************************************************
**		File: 
**		Name: BatchFileHistoryDetails
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

CREATE Procedure dbo.BatchFileHistoryDetails
(
		@BatchFileHistoryId		INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50)	= 'BatchFileHistory'
)
AS
BEGIN
	
	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@BatchFileHistoryId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT		

	SELECT	BatchFileHistoryId	
		,	ApplicationId	
		,	BatchFileId				
		,	BatchFileSetId			
		,	BatchFileStatusId		
		,	UpdatedDate				
		,	UpdatedByPersonId		
		,	@LastUpdatedDate				AS	'Updated Date' 
		,	@LastUpdatedBy					AS	'Updated By'   
		,	@LastAuditAction				AS	'Last Action'  
	FROM	dbo.BatchFileHistory	 
	WHERE	BatchFileHistoryId = @BatchFileHistoryId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @BatchFileHistoryId
		,	@AuditAction			= 'Details'
		,	@UpdatedDate			= @AuditDate
		,	@UpdatedByPersonId		= @AuditId	
END
GO
   