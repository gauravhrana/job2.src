IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ActivityTypeDetails')
BEGIN
	PRINT 'Dropping Procedure ActivityTypeDetails'
	DROP  Procedure ActivityTypeDetails
END
GO

PRINT 'Creating Procedure ActivityTypeDetails'
GO


/******************************************************************************
**		File: 
**		Name: ActivityTypeDetails
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

CREATE Procedure dbo.ActivityTypeDetails
(
		@ActivityTypeId   	INT
	,   @AuditId			INT			
    ,   @AuditDate		    DATETIME	= NULL
	,	@SystemEntityType	VARCHAR(50)	= 'ActivityType'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@ActivityTypeId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	

	SELECT	ActivityTypeId	
		,	ApplicationId		
		,	Name					
		,	Description		
		,	SortOrder	
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'								
	FROM	dbo.ActivityType 
	WHERE	ActivityTypeId = @ActivityTypeId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ActivityTypeId
		,	@AuditAction			= 'Details' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END		
GO
   