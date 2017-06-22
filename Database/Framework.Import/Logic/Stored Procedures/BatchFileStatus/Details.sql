IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BatchFileStatusDetails')
BEGIN
	PRINT 'Dropping Procedure BatchFileStatusDetails'
	DROP  Procedure BatchFileStatusDetails
END
GO

PRINT 'Creating Procedure BatchFileStatusDetails'
GO


/******************************************************************************
**		File: 
**		Name: BatchFileStatusDetails
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

CREATE Procedure dbo.BatchFileStatusDetails
(
		@BatchFileStatusId		INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50)	= 'BatchFileStatus'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@BatchFileStatusId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	

	SELECT	BatchFileStatusId	
		,	Name						
		,	Description			
		,	SortOrder			
		,	@LastUpdatedDate				AS	'Updated Date' 
		,	@LastUpdatedBy					AS	'Updated By'   
		,	@LastAuditAction				AS	'Last Action'  
	FROM	dbo.BatchFileStatus 
	WHERE	BatchFileStatusId = @BatchFileStatusId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @BatchFileStatusId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   