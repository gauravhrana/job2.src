IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BatchFileSetDetails')
BEGIN
	PRINT 'Dropping Procedure BatchFileSetDetails'
	DROP  Procedure BatchFileSetDetails
END
GO

PRINT 'Creating Procedure BatchFileSetDetails'
GO


/******************************************************************************
**		File: 
**		Name: BatchFileSetDetails
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

CREATE Procedure dbo.BatchFileSetDetails
(
		@BatchFileSetId			INT		
	,	@ApplicationId			INT			= NULL			
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50)	= 'BatchFileSet'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@BatchFileSetId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	

	SELECT	BatchFileSetId	
		,	ApplicationId	
		,	Name						
		,	Description			
		,	CreatedDate			
		,	CreatedByPersonId	
		,	@LastUpdatedDate				AS	'Updated Date' 
		,	@LastUpdatedBy					AS	'Updated By'   
		,	@LastAuditAction				AS	'Last Action' 
	FROM	dbo.BatchFileSet 
	WHERE	BatchFileSetId = @BatchFileSetId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @BatchFileSetId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END
GO
   