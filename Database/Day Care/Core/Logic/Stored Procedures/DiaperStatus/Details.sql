IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DiaperStatusDetails')
BEGIN
	PRINT 'Dropping Procedure DiaperStatusDetails'
	DROP  Procedure DiaperStatusDetails
END
GO

PRINT 'Creating Procedure DiaperStatusDetails'
GO


/******************************************************************************
**		File: 
**		Name: DiaperStatusDetails
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

CREATE Procedure dbo.DiaperStatusDetails
(
		@DiaperStatusId		INT
	,   @AuditId			INT			
    ,   @AuditDate		    DATETIME	= NULL	
	,	@SystemEntityType	VARCHAR(50)	= 'DiaperStatus'	
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@DiaperStatusId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	

	SELECT	DiaperStatusId	
		,	ApplicationId
		,	Name					
		,	Description		
		,	SortOrder		
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'							
	FROM	dbo.DiaperStatus 
	WHERE	DiaperStatusId = @DiaperStatusId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @DiaperStatusId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END		
GO
   