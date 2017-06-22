IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ActivityDetails')
BEGIN
	PRINT 'Dropping Procedure ActivityDetails'
	DROP  Procedure ActivityDetails
END
GO

PRINT 'Creating Procedure ActivityDetails'
GO


/******************************************************************************
**		File: 
**		Name: Activity_Details
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

CREATE Procedure dbo.ActivityDetails
(
		@ActivityId		    INT 
	,   @AuditId			INT			
    ,   @AuditDate		    DATETIME		= NULL
	,	@SystemEntityType	VARCHAR(50)		= 'Activity'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@ActivityId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	

		
	SELECT	ActivityId	
		,	ApplicationId		
		,	StudentId					
		,	ActivityTypeId	    
		,	ActivitySubTypeId	
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'							
	FROM	dbo.Activity 
	WHERE	ActivityId = @ActivityId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ActivityId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END		
GO
   